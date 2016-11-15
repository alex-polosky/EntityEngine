using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityFramework.Engine.Events
{
    // ToDo: Create custom eventargs for each event that doesn't have one

    public delegate void ErrorEventHandler(object sender, ErrorEventArgs e);

    public delegate void StartHandler(object sender, StartEventArgs e);
    public delegate void StopHandler(object sender, StopEventArgs e);
    public delegate void PauseHandler(object sender, PauseEventArgs e);
    public delegate void ResumeHandler(object sender, ResumeEventArgs e);

    public delegate void EngineFrameTickHandler(object sender, EngineFrameTickEventArgs e);

    public delegate void EFTProcessFrameHandler(object sender, EFTProcessFrameEventArgs e);
    public delegate void EFTPreFrameHandler(object sender, EFTPreFrameEventArgs e);
    public delegate void EFTPostFrameHandler(object sender, EFTPostFrameEventArgs e);

    public delegate void EFTSystemUpdateHandler(object sender, EFTSystemUpdateEventArgs e);
    public delegate void EFTRenderDrawHandler(object sender, EFTRenderDrawEventArgs e);
    public delegate void EFTRenderPhysicsHandler(object sender, EFTRenderPhysicsEventArgs e);

    public class ErrorEventArgs : EventArgs
    {
        public enum ErrorLevel
        {
            None,
            AllClear,
            Warning,
            Error,
            SystemFailure,
        }

        public string message;
        public ErrorLevel errorLevel;
    }

    public class StartEventArgs : EventArgs
    {

    }

    public class StopEventArgs : EventArgs
    {

    }

    public class PauseEventArgs : EventArgs
    {

    }

    public class ResumeEventArgs : EventArgs
    {

    }

    public class EngineFrameTickEventArgs : EventArgs
    {

    }

    public class EFTProcessFrameEventArgs : EventArgs
    {

    }

    public class EFTPreFrameEventArgs : EventArgs
    {

    }

    public class EFTPostFrameEventArgs : EventArgs
    {

    }

    public class EFTSystemUpdateEventArgs : EventArgs
    {

    }

    public class EFTRenderDrawEventArgs : EventArgs
    {

    }

    public class EFTRenderPhysicsEventArgs : EventArgs
    {

    }
}
