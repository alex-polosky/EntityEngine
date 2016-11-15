using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MenuCreater
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"P:\Code\Git\EntityEngine\menu_generated_example.xml";

            string xmlData = "";
            using (StreamReader sr = new StreamReader(path))
            {
                xmlData = sr.ReadToEnd();
            }

            Parser parser = new Parser();

            var menu = parser.ParseXmlDocumentS(xmlData);

            Component cc = new CameraComponent();
            Component child = new ChildrenComponent();
            ICameraSystem cs = new CameraSystem();

            cs.AddComponent((ICameraComponent)cc); // this will work
            cs.AddComponent((ICameraComponent)child); // This will not
            //cs.AddComponent(cc); // This throws compile error
            //cs.AddComponent(child); // This throws compile error

            var m = new Moar();
            //var mw = m.gen_wam();E
        }
    }
    public class Component
    {
    }
    public abstract class ICameraComponent : Component
    {
    }
    public class CameraComponent : ICameraComponent
    {
    }

    public class ChildrenComponent : Component
    {
    }

    public interface IComponentSystem
    {
        void Update(double timeDelta);
        void Init();
    }

    public abstract class ComponentSystem<TComponent> : IComponentSystem
        where TComponent : Component
    {
        protected List<Component> coms;

        public void AddComponent(TComponent com)
        {
            if (this.coms == null)
                this.coms = new List<Component>();
            this.coms.Add(com);
        }

        public abstract void Update(double timeDelta);
        public abstract void Init();
    }

    public abstract class ICameraSystem : ComponentSystem<ICameraComponent>
    {
    }

    public class CameraSystem : ICameraSystem
    {
        public override void Update(double timeDelta)
        {
        }
        public override void Init()
        {
        }
    }

    public class Moar
    {
        /*private*/public class wampum
        {
            public int a;
            public int b;
            public bool c;
        }

        public wampum gen_wam()
        {
            wampum toret = new wampum();
            toret.a = 3;
            toret.b = 234;
            toret.c = true;
            return toret;
        }
    }

    //public interface IWam
    //{
    //    public int a;
    //    public int b;
    //    public void Wah(int a);
    //}
}
