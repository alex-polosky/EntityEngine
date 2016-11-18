using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityFramework.ComponentInterfaces
{
    public abstract class IStressTestSystem : AComponentSystem<IStressTest>
    {
        public override void Update(double timeDelta)
        {
            base.Update(timeDelta);

            foreach (IStressTest com in _components)
            {
                switch (com.StressLevel)
                {
                    case IStressTest.Level.None:
                        break;
                    case IStressTest.Level.Timed1MS:
                        System.Threading.Thread.Sleep(1);
                        break;
                    case IStressTest.Level.Timed5MS:
                        System.Threading.Thread.Sleep(5);
                        break;
                    case IStressTest.Level.Timed10MS:
                        System.Threading.Thread.Sleep(10);
                        break;
                    case IStressTest.Level.CompLow:
                        var a = 4;
                        var b = 3;
                        var c = a + b;
                        break;
                    case IStressTest.Level.CompMid:
                        for (int i = 0; i < 20; i++)
                            c = i * 3 + 43;
                        break;
                    case IStressTest.Level.CompHi:
                        for (int i = 0; i < 100; i++)
                            c = i * i * i * i * i + 432 * 4;
                        break;
                    case IStressTest.Level.CompExt:
                        break;
                }
            }
        }
    }
}
