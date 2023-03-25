using GLFW_Test.Rendering.Display;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace GLFW_Test.Rendering.Camera
{
    internal class Camera2D : Camera
    {
        public Vector2 FocusPosition { get; set; }

        public float Zoom { get; set; }

        public Camera2D(Vector2 focusPosition, float zoom)
        {
            FocusPosition = focusPosition;
            Zoom = zoom;
        }

        public Matrix4x4 GetProjectionMatrix()
        {
            float left = FocusPosition.X - DisplayManager.WindowSize.X / 2f,
                right = FocusPosition.X + DisplayManager.WindowSize.X / 2f,
                top = FocusPosition.Y - DisplayManager.WindowSize.Y / 2f,
                bottom = FocusPosition.Y + DisplayManager.WindowSize.Y / 2f;
            Matrix4x4 orthoMat = Matrix4x4.CreateOrthographicOffCenter(left, right, bottom, top, 0.01f, 100f);
            Matrix4x4 zoomMatrix = Matrix4x4.CreateScale(Zoom);
            return orthoMat * zoomMatrix;
        }
    }
}
