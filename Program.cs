using System;
using GLFW;
using GLFW_Test;
using static GLFW_Test.OpenGL.GL;

class Program
{

    public static void Main(string[] args)
    {
        Console.WriteLine("Hello World");
        new TestGame(600,600,"Hello World").Run();
    }
}