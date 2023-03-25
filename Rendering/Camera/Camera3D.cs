using GLFW_Test.Rendering.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GLFW_Test.Rendering.Camera
{
    internal class Camera3D : Camera
    {
        public Camera3D(Vector3 position, Vector3 target, float fOV, float nearPlane, float farPlane)
        {
            Position = position;
            Target = target;
            FOV = fOV;
            this.nearPlane = nearPlane;
            this.farPlane = farPlane;
        }

        public Vector3 Position { get; set; }
        public Vector3 Target { get; set; }
        public float FOV { get; set; }
        public float nearPlane { get; set; }
        public float farPlane { get; set; }



        public Matrix4x4 GetProjectionMatrix()
        {
            float aspectRatio = (float)DisplayManager.WindowSize.X / DisplayManager.WindowSize.Y;
            float fovRadians = MathF.PI * FOV / 180f;
            float yScale = 1f / MathF.Tan(fovRadians / 2f);
            float xScale = yScale / aspectRatio;
            float zRange = farPlane - nearPlane;

            return new Matrix4x4(
                xScale, 0f, 0f, 0f,
                0f, yScale, 0f, 0f,
                0f, 0f, -(farPlane + nearPlane) / zRange, -1f,
                0f, 0f, -2f * nearPlane * farPlane / zRange, 0f);
        }
    }

}
