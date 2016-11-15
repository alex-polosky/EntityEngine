using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MathExt;

namespace EntityFramework.ComponentInterfaces
{
    public abstract class IPosition : Component
    {
        public Matrix RotationX { get; private set; }
        public Matrix RotationY { get; private set; }
        public Matrix RotationZ { get; private set; }

        public Matrix Scale { get; private set; }

        public Matrix TranslationLocal { get; private set; }
        public Matrix TranslationWorld { get; private set; }
    }
}
