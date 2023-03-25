using System;
using GLFW;
using GLFW_Test.GameLoop;
using GLFW_Test.Rendering.Display;
using static GLFW_Test.OpenGL.GL;
using GLFW_Test.Shaders;
using GLFW_Test.Rendering.Camera;
using System.Numerics;

namespace GLFW_Test
{
    internal class TestGame : Game
    {
        uint vao, vbo;
        Shader shader;
        Camera camera;
        public TestGame(int initialWindowWidth, int initialWindowHeight, string initialWindowTitle) : base(initialWindowWidth, initialWindowHeight, initialWindowTitle)
        {
            this.InitialWindowWidth = initialWindowWidth;
            this.InitialWindowHeight = initialWindowHeight;
            this.InitialWindowTitle = initialWindowTitle;
        }

        protected override void Initialize()
        {

        }

        protected unsafe override void LoadContent()
        {
            string vertexShader = @"#version 330 core
                                    layout (location = 0) in vec2 aPosition;
                                    layout (location = 1) in vec3 aColor;
                                    out vec4 vertexColor;

                                    uniform mat4 projection;
                                    uniform mat4 model;

                                    void main() 
                                    {
                                        vertexColor = vec4(aColor.rgb, 1.0);
                                        gl_Position = projection * model * vec4(aPosition.xy, 0, 1.0);
                                    }";
            string fragmentShader = @"#version 330 core
                                    out vec4 FragColor;
                                    in vec4 vertexColor;

                                    void main() 
                                    {
                                        FragColor = vertexColor;
                                    }";
            shader = new Shader(vertexShader, fragmentShader);
            shader.Load();

            vao = glGenVertexArray();
            vbo = glGenBuffer();

            glBindVertexArray(vao);
            glBindBuffer(GL_ARRAY_BUFFER, vbo);

            float[] vertices =
            {
                -0.5f,  0.5f,  0f, 0f, 1f, 0f,
                0.5f,   0.5f,  0f,0f, 0.5f, 0f,
                -0.5f,  -0.5f, 0f, 0f, 0.5f, 0f,

                0.5f,   0.5f,  0f, 0f, 0.5f, 0f,
                0.5f,   -0.5f, 0f, 0f, 0f, 0f,
                -0.5f,  -0.5f, 0f, 0f, 0.5f, 0f
            };
            fixed (float* v = &vertices[0]) {
                glBufferData(GL_ARRAY_BUFFER, sizeof(float) * vertices.Length, v,GL_STATIC_DRAW);
            }
            glVertexAttribPointer(0, 3, GL_FLOAT, false, 6 * sizeof(float),(void*)0);
            glEnableVertexAttribArray(0);
            glVertexAttribPointer(1, 4, GL_FLOAT, false, 6 * sizeof(float), (void*)(2 * sizeof(float)));
            glEnableVertexAttribArray(1);
            glBindBuffer(GL_ARRAY_BUFFER, 0);
            glBindVertexArray(0);

            camera = new Camera2D(DisplayManager.WindowSize/2f, 1f);
            //camera = new Camera3D(new Vector3(DisplayManager.WindowSize.X/2f, DisplayManager.WindowSize.Y/2f, 5),Vector3.Zero, 60, 0.1f, 1000f);
        }

        protected override void Render()
        {
            glClearColor(0,0,0,0);
            glClear(GL_COLOR_BUFFER_BIT);

            Vector2 position = new Vector2(300, 300);
            Vector2 scale = new Vector2(150, 150);
            float rotation = 0;

            //camera.FocusPosition = new Vector2(DisplayManager.WindowSize.X/2f + MathF.Cos(GameTime.TotalElapsedSeconds) * 100, DisplayManager.WindowSize.Y/2f + MathF.Sin(GameTime.TotalElapsedSeconds) * 100);
            //camera.Zoom = MathF.Sin(GameTime.TotalElapsedSeconds) + 2;

            Matrix4x4 trans = Matrix4x4.CreateTranslation(position.X, position.Y, 0);
            Matrix4x4 sca = Matrix4x4.CreateScale(scale.X, scale.Y, 1);
            Matrix4x4 rot = Matrix4x4.CreateRotationZ(rotation);

            shader.SetMatrix4x4("model", sca * rot * trans);

            shader.Use();
            shader.SetMatrix4x4("projection", camera.GetProjectionMatrix());
            glBindVertexArray(vao);
            glDrawArrays(GL_TRIANGLES, 0, 6);

            Glfw.SwapBuffers(DisplayManager.Window);
        }

        protected override void Update()
        {

        }
    }
}
