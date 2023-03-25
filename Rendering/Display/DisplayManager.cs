using GLFW;
using System.Numerics;
using System.Xml.Serialization;
using Monitor = GLFW.Monitor;
using static GLFW_Test.OpenGL.GL;
using System.Drawing;

namespace GLFW_Test.Rendering.Display
{
    internal class DisplayManager
    {

        public static Window Window { get; set; }
        public static Vector2 WindowSize { get; set; }

        public static void CreateWindow(int width, int height, string title)
        {
            WindowSize = new Vector2(width, height);
            Glfw.Init();
            Glfw.WindowHint(Hint.ContextVersionMajor, 3);
            Glfw.WindowHint(Hint.ContextVersionMinor, 3);
            Glfw.WindowHint(Hint.OpenglProfile, Profile.Core);

            Glfw.WindowHint(Hint.Focused, true);
            Glfw.WindowHint(Hint.Resizable, false);

            Window = Glfw.CreateWindow(width, height, title, Monitor.None, Window.None);
            if(Window == Window.None) { Console.WriteLine("Something went wrong creating the window"); return; }

            Rectangle screen = Glfw.PrimaryMonitor.WorkArea;
            int x = (screen.Width - width) / 2, y = (screen.Height - height) / 2;
            Glfw.SetWindowPosition(Window,x, y);
            
            Glfw.MakeContextCurrent(Window);
            Import(Glfw.GetProcAddress);
            glViewport(0, 0, width, height);
            Glfw.SwapInterval(0);
            
        }

        public static void CloseWindow()
        {
            Glfw.Terminate();
        }
    }
}
