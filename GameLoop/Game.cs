using GLFW;
using GLFW_Test.Rendering.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLFW_Test.GameLoop
{
    abstract class Game
    {

        protected int InitialWindowWidth { get; set; }
        protected int InitialWindowHeight { get; set; }
        protected string InitialWindowTitle { get; set; }

        public Game(int initialWindowWidth, int initialWindowHeight, string initialWindowTitle)
        {
            InitialWindowWidth = initialWindowWidth;
            InitialWindowHeight = initialWindowHeight;
            InitialWindowTitle = initialWindowTitle;
        }

        public void Run()
        {
            Initialize();
            DisplayManager.CreateWindow(InitialWindowWidth, InitialWindowHeight, InitialWindowTitle);
            LoadContent();
            while (!Glfw.WindowShouldClose(DisplayManager.Window))
            {
                GameTime.DeltaTime = (float)(Glfw.Time - GameTime.TotalElapsedSeconds);
                GameTime.TotalElapsedSeconds = (float)Glfw.Time;
                Update();
                Glfw.PollEvents();
                Render();
            }
            DisplayManager.CloseWindow();
        }
        protected abstract void Initialize();
        protected abstract void LoadContent();

        protected abstract void Update();
        protected abstract void Render();
    }
}
