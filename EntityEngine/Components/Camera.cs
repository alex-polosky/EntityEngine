using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SharpDX;

using EntityFramework;
using EntityFramework.Components;

namespace EntityEngine.Components
{   
    public class CameraComponent : EntityFramework.Component
    {
        private Vector3 eye;
        private Vector3 view;
        private Vector3 up { get { return this.camRotation.Up; } }
        private Matrix viewMatrix;

        private float radianX;
        private float radianY;
        private float radianZ;
        private Matrix camRotation;
        private Vector3 rotate;

        public Matrix projectionMatrix;
        public Entity Following = null;
        public bool IsFollowing = false;
        public bool IsOrtho = false;
        public bool IsMoveWithRot = false;
        public bool IsMoveWithRotLockX = false;
        public bool IsMoveWithRotLockY = false;
        public bool IsMoveWithRotLockZ = false;
        public bool IsLockAxes = true;
        public bool IsZBuffer = true;

        public float RadianX { get { return this.radianX; } }
        public float RadianY { get { return this.radianY; } }
        public float RadianZ { get { return this.radianZ; } }

        public Vector3 Eye { get { return this.eye; } }
        public Vector3 View { get { return this.view; } }
        public Vector3 Up { get { return this.up; } }
        public Matrix ViewMatrix { get { return this.viewMatrix; } }

        public void Rotate(Vector3 v, bool updateView = false)
        {
            this.radianX += v.X;
            this.radianY += v.Y;
            this.radianZ += v.Z;
            this.radianX %= (float)Math.PI * 2;
            this.radianY %= (float)Math.PI * 2;
            this.radianZ %= (float)Math.PI * 2;
            this.rotate = v;
            if (updateView)
                UpdateViewMatrix();
        }

        public void Move(Vector3 v, bool updateView = false)
        {
            if (this.IsMoveWithRot)
            {
                if (this.IsMoveWithRotLockZ)
                    this.eye += v.Z * Vector3.ForwardLH;
                else
                    this.eye += v.Z * this.camRotation.Forward;
                if (this.IsMoveWithRotLockX)
                    this.eye += v.X * Vector3.Right;
                else
                    this.eye += v.X * this.camRotation.Right;
                if (this.IsMoveWithRotLockY)
                    this.eye += v.Y * Vector3.Up;
                else
                    this.eye += v.Y * this.camRotation.Up;
            }
            else
            {
                this.eye += v.Z * Vector3.ForwardLH;
                this.eye += v.X * Vector3.Right;
                this.eye += v.Y * Vector3.Up;
            }
            if (updateView)
                UpdateViewMatrix();
        }

        public void ResetProjectionMatrix(int targetWidth, int targetHeight)
        {
            if (this.IsOrtho)
            {
                var aspect = (float)targetWidth / (float)targetHeight;
                var scale = 0;
                var near = 0f;
                var far = 1f;
                this.projectionMatrix = Matrix.OrthoLH(
                    targetWidth + aspect * scale, targetHeight + scale, near, far
                );
            }
            else
            {
                var fov = MathUtil.Pi / 4.0f;
                var aspect = (float)targetWidth / (float)targetHeight;
                var near = 0.1f;
                var far = 500.0f;
                this.projectionMatrix = Matrix.PerspectiveFovLH(
                    fov, aspect, near, far
                );
            }
        }

        public void UpdateViewMatrix()
        {
            this.camRotation.Forward.Normalize();
            this.camRotation.Up.Normalize();
            this.camRotation.Right.Normalize();

            if (this.IsLockAxes)
            {
                this.camRotation = Matrix.Identity;
                this.camRotation *= Matrix.RotationX(this.radianX);
                this.camRotation *= Matrix.RotationY(this.radianY);
                this.camRotation *= Matrix.RotationZ(this.radianZ);
            }
            else
            {
                this.camRotation *= Matrix.RotationAxis(this.camRotation.Right, this.rotate.X);
                this.camRotation *= Matrix.RotationAxis(this.camRotation.Up, this.rotate.Y);
                this.camRotation *= Matrix.RotationAxis(this.camRotation.Forward, this.rotate.Z);
            }

            this.rotate = Vector3.Zero;

            if (!this.IsOrtho)
                this.view = this.eye + this.camRotation.Forward;

            this.viewMatrix = Matrix.LookAtLH(this.eye, this.view, this.camRotation.Up);
        }

        public CameraComponent(int targetWidth = 800, int targetHeight = 600, bool ortho = false, bool moveWithRot = true, bool lockXmove = false, bool lockYmove = false, bool lockZmove = false, bool lockAxes = true, bool zBuffer = true)
        {
            // TODO: Check if this we need to reset camera view matrix on window resize???
            this.IsOrtho = ortho;
            this.IsMoveWithRot = moveWithRot;
            this.IsMoveWithRotLockX = lockXmove;
            this.IsMoveWithRotLockY = lockYmove;
            this.IsMoveWithRotLockZ = lockZmove;
            this.IsLockAxes = lockAxes;
            this.IsZBuffer = zBuffer;

            this.camRotation = Matrix.Identity;

            ResetProjectionMatrix(targetWidth, targetHeight);

            if (ortho)
            {
                this.eye = new SharpDX.Vector3(
                    targetWidth / 2,
                    targetHeight / 2,
                    0f
                );
                this.view = new SharpDX.Vector3(
                    targetWidth / 2,
                    targetHeight / 2,
                    1f
                );
            }
            else
            {
                this.eye = Vector3.ForwardLH;
                this.view = Vector3.Zero;
            }
            this.viewMatrix = Matrix.LookAtLH(this.eye, this.view, this.camRotation.Up);
        }

        public CameraComponent()
        {
            throw new NotImplementedException();
        }
    }

    public class CameraSystem : ComponentSystem<CameraComponent>
    {
        public override void Update(double timeDelta = 0.0f)
        {
            foreach (CameraComponent com in this._components)
                com.UpdateViewMatrix();
        }
    }
}
