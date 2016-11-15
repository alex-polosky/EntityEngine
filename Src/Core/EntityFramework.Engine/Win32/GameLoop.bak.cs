using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

using EntityFramework.Win32;

namespace EntityFramework
{
    public class GameLoopBak : IDisposable
    {
        private IntPtr controlHandle;
        private Control control;
        private bool isControlAlive;
        private bool switchControl;
        private FrameRate frameRate;

        public FrameRate FrameRate
        {
            get
            {
                return frameRate;
            }
            set
            {
                frameRate = value;
            }
        }

        public Control Control
        {
            get
            {
                return control;
            }
            set
            {
                if (control == value)
                    return;

                // Remove previous control if exists
                if (control != null && !switchControl)
                {
                    isControlAlive = false;
                    control.Disposed -= ControlDisposed;
                    controlHandle = IntPtr.Zero;
                }

                if (value != null && value.IsDisposed)
                    throw new InvalidOperationException("Control is already disposed");

                control = value;
                switchControl = true;
            }
        }

        public bool UseApplicationDoEvents { get; set; }

        public event EventHandler PreProcess;
        public event EventHandler PostProcess;
        public event EventHandler ProcessFrame;

        public bool NextFrame()
        {
            // set up new control
            // ToDo: set up thread-safe ability
            if (switchControl && control != null)
            {
                controlHandle = control.Handle;
                control.Disposed += ControlDisposed;
                isControlAlive = true;
                switchControl = false;
            }

            if (isControlAlive)
            {
                if (UseApplicationDoEvents)
                    Application.DoEvents();
                else
                {
                    var localHandle = controlHandle;
                    if (localHandle != IntPtr.Zero)
                    {
                        // Previous code not compatible with Application.AddMessageFilter but faster then DoEvents
                        NativeMessage msg;
                        while (Win32Native.PeekMessage(out msg, IntPtr.Zero, 0, 0, 0) != 0)
                        {
                            if (Win32Native.GetMessage(out msg, IntPtr.Zero, 0, 0) == -1)
                            {
                                throw new InvalidOperationException(String.Format(CultureInfo.InvariantCulture,
                                    "An error happened in game loop while processing windows messages. Error: {0}",
                                    Marshal.GetLastWin32Error()));
                            }

                            // NCDESTROY event?
                            if (msg.msg == 130)
                            {
                                isControlAlive = false;
                            }

                            var message = new Message() { HWnd = msg.handle, LParam = msg.lParam, Msg = (int)msg.msg, WParam = msg.wParam };
                            if (!Application.FilterMessage(ref message))
                            {
                                Win32Native.TranslateMessage(ref msg);
                                Win32Native.DispatchMessage(ref msg);
                            }
                        }
                    }
                }
            }

            return isControlAlive || switchControl;
        }

        private void ControlDisposed(object sender, EventArgs e)
        {
            isControlAlive = false;
        }

        public void Dispose()
        {
            Control = null;
        }

        public void RunLoop()
        {
            if (Control == null)
                throw new ArgumentException("Control cannot be null on loop start");

            if (frameRate == null)
                frameRate = new FrameRate();

            frameRate.Start();
            while (NextFrame())
            {
                frameRate.StartFrame();
                if (PreProcess != null)
                    PreProcess(this, new EventArgs());

                if (ProcessFrame != null)
                    ProcessFrame(this, new EventArgs());

                if (PostProcess != null)
                    PostProcess(this, new EventArgs());
                frameRate.EndFrame();
            }
            frameRate.Stop();
        }

        //public delegate void RenderCallback();

        //public static void Run(ApplicationContext context, RenderCallback renderCallback)
        //{
        //    Run(context.MainForm, renderCallback);
        //}

        //public static void Run(Control form, RenderCallback renderCallback, bool useApplicationDoEvents = false)
        //{
        //    if (form == null) throw new ArgumentNullException("form");
        //    if (renderCallback == null) throw new ArgumentNullException("renderCallback");

        //    form.Show();
        //    using (var renderLoop = new RenderLoop(form) { UseApplicationDoEvents = useApplicationDoEvents })
        //    {
        //        while (renderLoop.NextFrame())
        //        {
        //            renderCallback();
        //        }
        //    }
        //}

        public static bool IsIdle
        {
            get
            {
                NativeMessage msg;
                return (bool)(Win32Native.PeekMessage(out msg, IntPtr.Zero, 0, 0, 0) == 0);
            }
        }

        public GameLoopBak()
        {

        }

        public GameLoopBak(Control control)
        {
            Control = control;
        }
    }
}
