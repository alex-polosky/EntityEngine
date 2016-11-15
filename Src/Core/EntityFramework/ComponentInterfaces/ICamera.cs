using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MathExt;

namespace EntityFramework.ComponentInterfaces
{
    public abstract class ICamera : Component
    {
        public Matrix Projection { get; private set; }

        public Entity Following { get; private set; }
        public bool IsFollowing { get { return Following != null; } }

        public Entity Tracking { get; private set; }
        public bool IsTracking { get { return Following != null; } }

        public bool IsOrtho { get; set; }
        
        public bool IsMoveWithRotation { get; set; }
        public bool IsMoveWithRotLockX { get; set; }
        public bool IsMoveWithRotLockY { get; set; }
        public bool IsMoveWithRotLockZ { get; set; }

        public bool IsLockAxes { get; set; }

        public bool IsZBuffer { get; set; }
    }
}
