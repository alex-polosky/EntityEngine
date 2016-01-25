using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using EntityFramework;
using EntityEngine;
using EntityFramework.Components;
using EntityEngine.Components;

namespace Test
{
    public partial class Main : Game1_Form
    {
        protected override void SetUpEnts()
        {
            FileManager.LoadAllEntities(Path.Combine("..\\", "..\\", "..\\", "..\\", "..\\",
                                                     "Maps", "Test", "ObjDefs", "Entities"), this.sys);
            var com = new InputComponent();
            com.IsActive = true;
            com.Input = (timeDelta, kbState) =>
            {
                var move = new List<float>() { 0, 0, 0 };
                var rot = new List<float>() { 0, 0, 0 };

                if (kbState["W"])
                    move[2] += 0.2f;
                if (kbState["S"])
                    move[2] += -0.2f;
                if (kbState["A"])
                    move[0] += 0.2f;
                if (kbState["D"])
                    move[0] += -0.2f;
                if (kbState["Q"])
                    move[1] += -0.2f;
                if (kbState["E"])
                    move[1] += 0.2f;

                if (kbState["Left"])
                {
                    float degree = 1;
                    float angle = (float)Math.PI * degree / 180.0f;
                    rot[1] += -angle;
                }
                if (kbState["Right"])
                {
                    float degree = 1;
                    float angle = (float)Math.PI * degree / 180.0f;
                    rot[1] += angle;
                }
                if (kbState["Up"])
                {
                    float degree = 1;
                    float angle = (float)Math.PI * degree / 180.0f;
                    rot[0] += angle;
                }
                if (kbState["Down"])
                {
                    float degree = 1;
                    float angle = (float)Math.PI * degree / 180.0f;
                    rot[0] += -angle;
                }
                if (kbState["C"])
                {
                    float degree = 1;
                    float angle = (float)Math.PI * degree / 180.0f;
                    rot[2] += angle;
                }
                if (kbState["Z"])
                {
                    float degree = 1;
                    float angle = (float)Math.PI * degree / 180.0f;
                    rot[2] += -angle;
                }

                if (move != new List<float>() { 0, 0, 0 })
                    render.Camera.Move(move[0], move[1], move[2]);
                if (rot != new List<float>() { 0, 0, 0 })
                    render.Camera.Rotate(rot[0], rot[1], rot[2]);
                render.Camera.UpdateViewMatrix();
            };
            this.sys.GetComponentSystem<InputComponent, InputSystem>()
                .AddComponent(com);
        }

        public Main(int renderMode = 6, bool usePyConsole = true, bool useGrid = false, int customWidth = -1, int customHeight = -1)
            : base(renderMode, usePyConsole, useGrid, customWidth, customHeight)
        {
            InitializeComponent();
        }
    }
}