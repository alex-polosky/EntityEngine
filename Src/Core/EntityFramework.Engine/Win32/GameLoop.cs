using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

using EntityFramework.Win32;

namespace EntityFramework.Engine
{
    public class GameLoop : IDisposable
    {
        private bool isControlAlive;

        private FrameRate frameRate;
        private Manager.FileManager fileManager;
        private Manager.GuidManager guidManager;
        private Manager.SystemManager systemManager;

        public FrameRate FrameRate { get { return frameRate; } }

        public bool IsControlAlive { get { return isControlAlive; } set { isControlAlive = value; } }

        public bool UseApplicationDoEvents { get; set; }
        public bool UseWindowsLoop { get; set; }
        public bool ProcessSystems { get; set; }

        public event EventHandler PreProcess;
        public event EventHandler PostProcess;
        public event EventHandler ProcessFrame;

        public bool NextWindowFrame()
        {
            // ToDo: set up thread-safe ability
            if (UseWindowsLoop && isControlAlive)
            {
                if (UseApplicationDoEvents)
                    Application.DoEvents();
                else
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

            return isControlAlive;
        }

        private void ControlDisposed(object sender, EventArgs e)
        {
            isControlAlive = false;
        }

        public void Dispose()
        {
        }

        public void RunLoop()
        {
            if (frameRate == null)
                frameRate = new FrameRate();

            frameRate.Start();
            while (NextWindowFrame())
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

        public static bool IsIdle
        {
            get
            {
                NativeMessage msg;
                return (bool)(Win32Native.PeekMessage(out msg, IntPtr.Zero, 0, 0, 0) == 0);
            }
        }

        public GameLoop()
        {
            isControlAlive = true;
        }
    }
}
