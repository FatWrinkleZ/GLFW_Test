using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GLFW_Test.Rendering.Camera
{
    internal interface Camera
    {

        public abstract Matrix4x4 GetProjectionMatrix();

    }
}
