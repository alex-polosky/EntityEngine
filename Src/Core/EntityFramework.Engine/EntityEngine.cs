using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

using EntityFramework;
using EntityFramework.Manager;

using EntityFramework.AssetFileInterface;

namespace EntityFramework.Engine
{
    public class EntityEngine : IEntityEngine, IDisposable
    {
        #region Protected Variables
        protected bool running = false;
        protected bool paused = false;
        protected bool manualTickAllowed = true;
        protected IntPtr drawHandle = IntPtr.Zero;

        protected List<Events.ErrorEventArgs> errorEventArgCache;
        #endregion Protected Variables

        #region IDisposable
        public event EventHandler Disposed;

        public virtual void Dispose()
        {
            if (Disposed != null)
                Disposed(this, EventArgs.Empty);
        }
        #endregion

        #region Public delegates and events

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

        #endregion Public delegates and events

        #region Public Variables
        public bool IsRunning { get { return running; } }
        public bool IsPaused { get { return paused; } }
        public bool IsManualTickAllowed { get { return manualTickAllowed; } }
        public bool IsEngineReady { get { return FrameRate != null && FileManager != null && GuidManager != null && SystemManager != null; } }
        public IntPtr DrawHandle { get { return drawHandle; } }

        public FrameRate FrameRate { get; set; }
        public FileManager FileManager { get; set; }
        public GuidManager GuidManager { get; set; }
        public SystemManager SystemManager { get; set; }

        public bool FrameSystemUpdate { get; set; }
        public bool FrameRenderPhysics { get; set; }
        public bool FrameRenderDraw { get; set; }

        #endregion Public Variables

        #region Private Methods

        private bool engineFrameTick()
        {
            if (!IsEngineReady)
            {
                ErrorEvent(this, new Events.ErrorEventArgs { 
                    errorLevel = Events.ErrorEventArgs.ErrorLevel.Error, 
                    message = "Engine not in ready state"
                });
                return false;
            }
            if (!FrameRate.IsRunning)
            {
                ErrorEvent(this, new Events.ErrorEventArgs { 
                    errorLevel = Events.ErrorEventArgs.ErrorLevel.Error, 
                    message = "Engine's FrameRate object has not been started" 
                });
                return false;
            }

            OnEFTPreFrame(this, new Events.EFTPreFrameEventArgs { });
            FrameRate.StartFrame();

            OnEFTProcessFrame(this, new Events.EFTProcessFrameEventArgs { });
            //if (FrameSystemUpdate)
            //    OnETFSystemUpdate(sender, e);
            //if (FrameRenderPhysics)
            //    OnETFRenderPhysics(sender, e);
            //if (FrameRenderDraw)
            //    OnETFRenderDraw(sender, e);

            FrameRate.EndFrame();
            OnEFTPostFrame(this, new Events.EFTPostFrameEventArgs { });

            return true;
        }

        #endregion Private Methods

        #region Protected Methods

        #region Event Methods
        private void HandlerCall(Delegate eventDelegate, object sender, EventArgs e)
        {
            if (eventDelegate == null)
            {
                if (ErrorEvent != null)
                {
                    // ToDo: find a way to output what event was attempted to be called
                    RaiseErrorEvent(new Events.ErrorEventArgs {
                        errorLevel = Events.ErrorEventArgs.ErrorLevel.Error, 
                        message = "Event called, but no event handlers were attached" 
                    });
                }
                else
                {
                    throw new EntryPointNotFoundException("Serious error occurred and no ErrorEventHandler is attached");
                }
            }
            else
            {
                eventDelegate.DynamicInvoke(sender, e);
            }
        }

        protected virtual void OnErrorEvent(object sender, Events.ErrorEventArgs e)
        {
            HandlerCall(ErrorEvent, sender, e);
        }

        protected virtual void OnStartEvent(object sender, Events.StartEventArgs e)
        {
            HandlerCall(StartEvent, sender, e);
        }

        protected virtual void OnStopEvent(object sender, Events.StopEventArgs e)
        {
            HandlerCall(StopEvent, sender, e);
        }

        protected virtual void OnPauseEvent(object sender, Events.PauseEventArgs e)
        {
            HandlerCall(PauseEvent, sender, e);
        }

        protected virtual void OnResumeEvent(object sender, Events.ResumeEventArgs e)
        {
            HandlerCall(ResumeEvent, sender, e);
        }

        protected virtual void OnEngineFrameTick(object sender, Events.EngineFrameTickEventArgs e)
        {
            HandlerCall(EngineFrameTickEvent, sender, e);
        }

        protected virtual void OnEFTPreFrame(object sender, Events.EFTPreFrameEventArgs e)
        {
            HandlerCall(EFTPreFrameEvent, sender, e);
        }

        protected virtual void OnEFTPostFrame(object sender, Events.EFTPostFrameEventArgs e)
        {
            HandlerCall(EFTPostFrameEvent, sender, e);
        }

        protected virtual void OnEFTProcessFrame(object sender, Events.EFTProcessFrameEventArgs e)
        {
            HandlerCall(EFTProcessFrameEvent, sender, e);
        }

        protected virtual void OnETFSystemUpdate(object sender, Events.EFTSystemUpdateEventArgs e)
        {
            SystemManager.Update(FrameRate.ElaspedTime);

            HandlerCall(ETFSystemUpdateEvent, sender, e);
        }

        protected virtual void OnETFRenderDraw(object sender, Events.EFTRenderDrawEventArgs e)
        {
            SystemManager.RenderDraw(FrameRate.ElaspedTime, drawHandle);

            HandlerCall(ETFRenderDrawEvent, sender, e);
        }

        protected virtual void OnETFRenderPhysics(object sender, Events.EFTRenderPhysicsEventArgs e)
        {
            SystemManager.RenderPhysics(FrameRate.ElaspedTime);

            HandlerCall(ETFRenderPhysicsEvent, sender, e);
        }
        #endregion Event Methods

        #endregion Protected Methods

        #region Public Methods
        
        public void GenerateManagers()
        {
            FileManager = new Manager.FileManager(OnErrorEvent);
            GuidManager = new Manager.GuidManager();
            SystemManager = new Manager.SystemManager();
            //SystemManager.AddComponentSystem<Render.RenderSystem>();
            //SystemManager.AddComponentSystem<Render.CollisionSystem>();
        }

        public void GenerateFrameRate(FrameRate fr = null)
        {
            if (fr == null)
                fr = new FrameRate();
            FrameRate = fr;
        }

        public void SetDrawHandle(IntPtr handle)
        {
            if (drawHandle == IntPtr.Zero)
                drawHandle = handle;
        }

        #endregion Public Methods

        #region Public Static Methods

        public static Thread GetRunLoopThread(EntityEngine engine = null, bool generateMembers = false)
        {
            bool nullEngine = false;

            if (engine == null)
            { engine = new EntityEngine(); nullEngine = true; }

            if (generateMembers || nullEngine)
            {
                engine.GenerateManagers();
                engine.GenerateFrameRate();
            }

            if (engine.IsEngineReady)
            {
                return new Thread(() =>
                {
                    engine.manualTickAllowed = false;
                    engine.Start();
                    while (engine.IsRunning)
                    {
                        if (!engine.IsPaused)
                        {
                            if (engine.engineFrameTick())
                                engine.OnEngineFrameTick(engine, new Events.EngineFrameTickEventArgs { });
                        }
                    }
                    engine.manualTickAllowed = true;
                });
            }
            else
            {
                engine.RaiseErrorEvent(new Events.ErrorEventArgs
                {
                    errorLevel = Events.ErrorEventArgs.ErrorLevel.Error,
                    message = "Engine not ready to start"
                });
                return null;
            }
        }

        public static bool RunLoop(EntityEngine engine = null, bool generateMembers = false)
        {
            bool nullEngine = false;

            if (engine == null)
            { engine = new EntityEngine(); nullEngine = true; }

            if (generateMembers || nullEngine)
            {
                engine.GenerateManagers();
                engine.GenerateFrameRate();
            }

            if (engine.IsEngineReady)
            {
                engine.manualTickAllowed = false;
                engine.Start();
                while (engine.IsRunning)
                {
                    if (!engine.IsPaused)
                    {
                        if (engine.engineFrameTick())
                            engine.OnEngineFrameTick(engine, new Events.EngineFrameTickEventArgs { });
                    }
                }
                engine.manualTickAllowed = true;
                return true;
            }
            else
            {
                engine.RaiseErrorEvent(new Events.ErrorEventArgs {
                    errorLevel = Events.ErrorEventArgs.ErrorLevel.Error, 
                    message = "Engine not ready to start" 
                });
                return false;
            }
        }

        #endregion Public Static Methods

        #region Handler Exteneral Calls
        private bool RaiseEvent(Delegate eventDelegate, object sender, EventArgs e, Func<bool> preEventCall = null, bool requiresManualCheck = false)
        {
            if (requiresManualCheck)
            {
                if (!manualTickAllowed)
                {
                    ErrorEvent(this,
                        new Events.ErrorEventArgs 
                        {
                            errorLevel = Events.ErrorEventArgs.ErrorLevel.Error, 
                            message = string.Format("Event '{0}' flagged as non-callable while manual ticks are disabled", eventDelegate.Method.Name) 
                        }
                    );
                    return false;
                }
            }
            if (preEventCall != null)
            {
                if (!preEventCall())
                {
                    ErrorEvent(this,
                        new Events.ErrorEventArgs
                        {
                            errorLevel = Events.ErrorEventArgs.ErrorLevel.Error,
                            message = string.Format("Event '{0}' failed upon calling preEventCall '{1}'", eventDelegate.Method.Name, preEventCall.Method.Name)
                        }
                    );
                    return false;
                }
            }

            HandlerCall(eventDelegate, sender, e);
            return true;
        }

        public void RaiseErrorEvent(Events.ErrorEventArgs e)
        {
            OnErrorEvent(this, e);
        }

        public bool RaiseEngineTickEvent(Events.EngineFrameTickEventArgs e)
        {
            return RaiseEvent(EngineFrameTickEvent, (object)this, e, engineFrameTick, true);
        }

        public bool Start()
        {
            if (!running)
            {
                running = true;
                paused = false;
                FrameRate.Start();
                OnStartEvent(this, new Events.StartEventArgs { });
                return true;
            }
            else
            {
                OnErrorEvent(this, new Events.ErrorEventArgs {
                    errorLevel = Events.ErrorEventArgs.ErrorLevel.Warning,
                    message = "Tried to start engine while engine was already running"
                });
                return false;
            }
        }

        public bool Stop()
        {
            if (running)
            {
                FrameRate.Stop();
                running = false;
                paused = false;
                OnStopEvent(this, new Events.StopEventArgs { });
                return true;
            }
            else
            {
                OnErrorEvent(this, new Events.ErrorEventArgs {
                    errorLevel = Events.ErrorEventArgs.ErrorLevel.Warning, 
                    message = "Tried to stop engine while engine was not running" 
                });
                return false;
            }
        }

        public bool Pause()
        {
            if (running && !paused)
            {
                FrameRate.Stop();
                paused = true;
                OnPauseEvent(this, new Events.PauseEventArgs { });
                return true;
            }
            else if (running && paused)
            {
                OnErrorEvent(this, new Events.ErrorEventArgs {
                    errorLevel = Events.ErrorEventArgs.ErrorLevel.Warning,
                    message = "Tried to pause engine while engine was already paused"
                });
                return false;
            }
            else if (!running)
            {
                OnErrorEvent(this, new Events.ErrorEventArgs {
                    errorLevel = Events.ErrorEventArgs.ErrorLevel.Warning,
                    message = "Tried to pause engine while engine was not running"
                });
                return false;
            }
            else
            {
                OnErrorEvent(this, new Events.ErrorEventArgs {
                    errorLevel = Events.ErrorEventArgs.ErrorLevel.SystemFailure,
                    message = "Should never reach this code, only required by vs compiler"
                });
                throw new SystemException("Code branch only required by compiler, no logical way to hit this");
            }
        }

        public bool Resume()
        {
            if (running && paused)
            {
                paused = false;
                FrameRate.Start();
                OnResumeEvent(this, new Events.ResumeEventArgs { });
                return true;
            }
            else if (running && !paused)
            {
                OnErrorEvent(this, new Events.ErrorEventArgs {
                    errorLevel = Events.ErrorEventArgs.ErrorLevel.Warning,
                    message = "Tried to resume engine while engine was not paused"
                });
                return false;
            }
            else if (!running)
            {
                OnErrorEvent(this, new Events.ErrorEventArgs {
                    errorLevel = Events.ErrorEventArgs.ErrorLevel.Warning,
                    message = "Tried to resume engine while engine was not running"
                });
                return false;
            }
            else
            {
                OnErrorEvent(this, new Events.ErrorEventArgs {
                    errorLevel = Events.ErrorEventArgs.ErrorLevel.SystemFailure,
                    message = "Should never reach this code, only required by vs compiler"
                });
                throw new SystemException("Code branch only required by compiler, no logical way to hit this");
            }
        }
        #endregion Handler Exteneral Calls

        #region Constructor
        public EntityEngine()
        {
            errorEventArgCache = new List<Events.ErrorEventArgs>();
            ErrorEvent += new Events.ErrorEventHandler((sender, e) =>
            {
                this.errorEventArgCache.Add(e);
            });
            EFTProcessFrameEvent += new Events.EFTProcessFrameHandler((sender, e) =>
            {
                if (FrameSystemUpdate)
                    OnETFSystemUpdate(sender, new Events.EFTSystemUpdateEventArgs { });
                if (FrameRenderPhysics)
                    OnETFRenderPhysics(sender, new Events.EFTRenderPhysicsEventArgs { });
                if (FrameRenderDraw)
                    OnETFRenderDraw(sender, new Events.EFTRenderDrawEventArgs { });
            }
            );
            
        }
        #endregion Constructor
    }

    public static partial class Extensions
    {
        // Thanks to nawfal (http://stackoverflow.com/questions/1266674/how-can-one-get-an-absolute-or-normalized-file-path-in-net)
        // (I prefer using lower case instead of upper case
        public static string PathNormalize(this string path)
        {
            return Path.GetFullPath(new Uri(path).LocalPath)
               .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
               .ToLowerInvariant();
        }

        // Thanks to http://stackoverflow.com/questions/2483023/how-to-test-if-a-type-is-anonymous
        //public static bool IsAnonymousType(this Type type)
        //{
        //    if (type == null)
        //        throw new ArgumentNullException("type");

        //    // HACK: The only way to detect anonymous types right now.
        //    return (type.Name.StartsWith("<>") || type.Name.StartsWith("VB$"));
        //    return Attribute.IsDefined(type, typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), false)
        //        && type.IsGenericType && type.Name.Contains("AnonymousType")
        //        && (type.Name.StartsWith("<>") || type.Name.StartsWith("VB$"))
        //        && (type.Attributes & System.Reflection.TypeAttributes.NotPublic) == System.Reflection.TypeAttributes.NotPublic;
        //}
    }
}
