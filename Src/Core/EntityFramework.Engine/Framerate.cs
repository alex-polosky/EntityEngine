using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityFramework.Engine
{
    public class FrameRate
    {
        private System.Diagnostics.Stopwatch sW;
        private long lastTime;
        private long currentTime;
        private int currentFPS;
        private float currentPastTime;

        public int CurrentFPS { get { return this.currentFPS; } }
        public float ElaspedTime { get { return this.currentPastTime; } }
        public long ElaspedMS { get { return this.sW.ElapsedMilliseconds; } }
        public bool IsRunning { get { return this.sW.IsRunning; } }

        public void StartFrame()
        {
            this.lastTime = this.currentTime;
        }

        public void EndFrame()
        {
            this.currentTime = this.sW.ElapsedMilliseconds;
            this.currentPastTime = this.currentTime - this.lastTime;
            this.currentFPS = (int)(1000.0f / this.currentPastTime);
        }

        public void Start()
        {
            this.sW.Start();
        }

        public void Stop()
        {
            this.sW.Stop();
        }

        public FrameRate()
        {
            this.sW = new System.Diagnostics.Stopwatch();
            this.lastTime = 0;
            this.currentTime = 0;
        }
    }
}
