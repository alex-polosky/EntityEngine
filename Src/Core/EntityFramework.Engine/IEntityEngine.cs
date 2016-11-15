using System;

using EntityFramework;
using EntityFramework.Manager;

namespace EntityFramework.Engine
{
    interface IEntityEngine
    {
        event Events.ErrorEventHandler ErrorEvent;

        event Events.StartHandler StartEvent;
        event Events.PauseHandler PauseEvent;
        event Events.ResumeHandler ResumeEvent;
        event Events.StopHandler StopEvent;

        event Events.EngineFrameTickHandler EngineFrameTickEvent;

        event Events.EFTProcessFrameHandler EFTProcessFrameEvent;
        event Events.EFTPostFrameHandler EFTPostFrameEvent;
        event Events.EFTPreFrameHandler EFTPreFrameEvent;

        event Events.EFTSystemUpdateHandler ETFSystemUpdateEvent;
        event Events.EFTRenderDrawHandler ETFRenderDrawEvent;
        event Events.EFTRenderPhysicsHandler ETFRenderPhysicsEvent;

        bool IsEngineReady { get; }
        bool IsManualTickAllowed { get; }
        bool IsPaused { get; }
        bool IsRunning { get; }
        IntPtr DrawHandle { get; }

        bool FrameRenderDraw { get; set; }
        bool FrameRenderPhysics { get; set; }
        bool FrameSystemUpdate { get; set; }

        FrameRate FrameRate { get; set; }
        FileManager FileManager { get; set; }
        GuidManager GuidManager { get; set; }
        SystemManager SystemManager { get; set; }

        bool Start();
        bool Stop();
        bool Pause();
        bool Resume();

        void RaiseErrorEvent(Events.ErrorEventArgs e);
        bool RaiseEngineTickEvent(Events.EngineFrameTickEventArgs e);

        void GenerateFrameRate(FrameRate fr = null);
        void GenerateManagers();
    }
}
