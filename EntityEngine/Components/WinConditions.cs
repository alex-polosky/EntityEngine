using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntityFramework;
using PyInterface;

namespace EntityEngine.Components
{

    public partial class WinComponent : EntityFramework.Component
    {
        public bool win = false;
    }

    public class WinSystem : ComponentSystem<WinComponent>
    {
        public delegate List<WinComponent> winConditionMain
            (SystemManager sys, FrameRate fps, List<WinComponent> coms);
        public struct WinCondition
        {
            public string name;
            public string description;
            public winConditionMain main;
        }

        private bool worldSet = false;
        private SystemManager world;

        private bool fpsSet = false;
        private FrameRate fps;

        private bool pySet = false;
        private clsPyInterface py;
        private Dictionary<string, object> pyVars;
        private static string pyCode = @"\
import winConditions
WinConditions = winConditions.WinConditions
";

        public List<WinCondition> WinConditions { get { return this.winConditions;  } }
        private List<WinCondition> winConditions;
        private WinCondition winCondition;

        private List<WinComponent> winners;
        public List<WinComponent> Winner { get { return this.winners; } }

        public void SetWorld(ref SystemManager world)
        {
            this.world = world;
            this.worldSet = true;
        }

        public void SetPy(ref clsPyInterface py)
        {
            this.py = py;
            this.pySet = true;
        }

        public void SetFPS(ref FrameRate fps)
        {
            this.fps = fps;
            this.fpsSet = true;
        }

        public void LoadWinConditions()
        {
            this.py.SetSource(pyCode);
            this.py.SetVariables(this.pyVars);
            this.pyVars = this.py.Run();

            this.winConditions = new List<WinCondition>();
            dynamic wins = this.py.ConvertList(this.pyVars["WinConditions"]);
            for (int i = 0; i < wins.Count; i++)
            {
                this.winConditions.Add(new WinCondition()
                    {
                        name = wins[0].name,
                        description = wins[0].description,
                        main = wins[0].main
                    }
                );
            }
        }

        public void SetWinCondition(WinCondition win)
        {
            this.winCondition = win;
        }

        public void SetWinCondition(string name)
        {
            this.winCondition = new WinCondition();
            foreach (dynamic win in this.WinConditions)
                if (win.name == name)
                    this.winCondition = win;
            if (this.winCondition.name == "")
                throw new Exception("Win Condition with name '" + name + "' not found!");
        }

        public override void Update(double timeDelta = 0.0f)
        {
            if (!this.worldSet)
                throw new Exception("Internal SystemManager has not been set!");
            if (!this.pySet)
                throw new Exception("Internal clsPyInterface has not been set!");
            if (!this.fpsSet)
                throw new Exception("Internal FrameRate has not been set!");
            
            if (this.winCondition.name != "")
                this.winners = this.winCondition.main(this.world, this.fps, this._components);

            if (this.winners != null)
                foreach (var com in this._components)
                    com.win = true;

            //int a;
            //if (this._components[0].nullBOOL == true)
            //{
            //    a = 0;
            //    this._components[0].nullBOOL = null;
            //}
            //else if (this._components[0].nullBOOL == false)
            //{
            //    a = 1;
            //    this._components[0].nullBOOL = true;
            //}
            //else
            //{
            //    a = 2;
            //    this._components[0].nullBOOL = false;
            //}
            //Console.Write(a);
        }

        public WinSystem()
            : base()
        {
            this.pyVars = new Dictionary<string, object>()
            {
                {"WinConditions", null},
            };
        }
    }

    // These are the by default null values
    public partial class WinComponent
    {
        public static int nullINT = -1;
        public static string nullSTR = null;
        public static bool? nullBOOL = null;
    }
}
