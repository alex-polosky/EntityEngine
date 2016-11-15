using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EntityFramework.Engine.Win32
{
    public class EntityEngineWin32Form : Control, IEntityEngine
    {
        #region Private Variables
        private bool manualTickAllowed;
        private bool running;
        private bool paused;
        #endregion Private Variables

        #region Protected Variables
        #endregion Protected Variables

        #region Public Variables
        public bool IsEngineReady 
        { 
            get 
            {
                return FrameRate != null && FileManager != null && GuidManager != null && SystemManager != null; 
            }
        }
        public bool IsManualTickAllowed { get { return manualTickAllowed; } }
        public bool IsPaused { get { return paused; } }
        public bool IsRunning { get { return running; } }
        public IntPtr DrawHandle { get { return IntPtr.Zero; } }

        public bool FrameRenderDraw { get; set; }
        public bool FrameRenderPhysics { get; set; }
        public bool FrameSystemUpdate { get; set; }

        public FrameRate FrameRate { get; set; }
        public EntityFramework.Manager.GuidManager GuidManager { get; set; }
        public EntityFramework.Manager.FileManager FileManager { get; set; }
        public EntityFramework.Manager.SystemManager SystemManager { get; set; }
        #endregion Public Variables

        #region Public Events
        public event Events.ErrorEventHandler ErrorEvent;

        public event Events.StartHandler StartEvent;
        public event Events.PauseHandler PauseEvent;
        public event Events.ResumeHandler ResumeEvent;
        public event Events.StopHandler StopEvent;

        public event Events.EngineFrameTickHandler EngineFrameTickEvent;

        public event Events.EFTProcessFrameHandler EFTProcessFrameEvent;
        public event Events.EFTPostFrameHandler EFTPostFrameEvent;
        public event Events.EFTPreFrameHandler EFTPreFrameEvent;

        public event Events.EFTSystemUpdateHandler ETFSystemUpdateEvent;
        public event Events.EFTRenderDrawHandler ETFRenderDrawEvent;
        public event Events.EFTRenderPhysicsHandler ETFRenderPhysicsEvent;
        #endregion Public Events

        #region Private Methods
        #endregion Private Methods

        #region Protected Methods
        #region Protected Event Callers
        #endregion Protected Event Callers
        #endregion Protected Methods

        #region Public Methods
        #region Public Event Callers
        public bool Start()
        {
            return false;
        }
        public bool Stop()
        {
            return false;
        }
        public bool Pause()
        {
            return false;
        }
        public bool Resume()
        {
            return false;
        }

        public bool RaiseErrorEvent(Events.ErrorEventArgs e)
        {
            return false;
        }
        public bool RaiseEngineTickEvent(Events.EngineFrameTickEventArgs e)
        {
            return false;
        }
        #endregion Public Event Callers

        public void GenerateFrameRate(FrameRate fr = null)
        {

        }

        public void GenerateManagers()
        {

        }
        #endregion Public Methods

        #region Constructor
        #endregion Constructor
    }
}
