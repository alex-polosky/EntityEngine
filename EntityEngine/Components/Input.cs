﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SharpDX;
using SharpDX.DirectInput;

using EntityFramework;

namespace EntityEngine.Components
{
    public static partial class Extensions
    {
        public static bool IsPressed(this KeyboardState kbState, string key)
        {
            Enum.Parse(typeof(Key), "W");
            return false;
        }
    }

    //public delegate void InputFunction(double timeDelta, KeyboardState kbState);
    public delegate void InputFunction(double timeDelta, Dictionary<string, bool> kbState);

    public class InputComponent : EntityFramework.Component
    {
        public bool IsActive = false;
        public InputFunction Input;
    }

    public class InputSystem : ComponentSystem<InputComponent>
    {
        private DirectInput dInput;
        private Keyboard keyboard;
        private Mouse mouse;
        private bool kbAcq = false;
        private bool mAcq = false;

        public override void Update(double timeDelta = 0.0f)
        {
            if (this.kbAcq)
            {
                var kbState = this.keyboard.GetCurrentState();
                var kbDir = new Dictionary<string, bool>();
                foreach (var key in kbState.AllKeys)
                    if (kbState.IsPressed(key))
                        kbDir.Add(key.ToString(), true);
                    else
                        kbDir.Add(key.ToString(), false);
                foreach (var com in this._components)
                    if (com.IsActive)
                        if (com.Input != null)
                            com.Input(timeDelta, kbDir);
                //com.Input(timeDelta, kbState);
            }
        }

        public void Enable()
        {
            if (this.keyboard != null)
            {
                this.keyboard.Acquire();
                this.kbAcq = true;
            }
            if (this.mouse != null)
            {
                this.mouse.Acquire();
                this.mAcq = true;
            }
        }

        public void Disable()
        {
            if (this.keyboard != null)
            {
                this.keyboard.Unacquire();
                this.kbAcq = false;
            }
            if (this.mouse != null)
            {
                this.mouse.Unacquire();
                this.mAcq = false;
            }
        }

        public InputSystem()
        {
            this.dInput = new DirectInput();
            this.keyboard = new Keyboard(this.dInput);
            this.keyboard.Acquire();
            this.kbAcq = true;
            this.mouse = new Mouse(this.dInput);
            this.mouse.Acquire();
            this.mAcq = true;
        }
    }
}
