using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityFramework.Render
{
    public class CameraSystem : EntityFramework.ComponentInterfaces.ICameraSystem
    {
        public override void Update(double timeDelta)
        {
            base.Update(timeDelta);
        }

        public override void Init()
        {
            base.Init();
        }
    }
}
