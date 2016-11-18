using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityFramework.ComponentInterfaces
{
    public abstract class IStressTest : Component
    {
        public enum Level
        {
            None,
            Timed1MS,
            Timed5MS,
            Timed10MS,
            CompLow,
            CompMid,
            CompHi,
            CompExt
        }

        public Level StressLevel;
    }
}
