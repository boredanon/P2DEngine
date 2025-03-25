using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2DEngine
{
    public class myCamera
    {
        // La cámara es simplemente un cuadrado.
        public float x;
        public float y;
        public float width;
        public float height;

        public float aspectRatio;
        public float zoom;

        public myCamera(int x, int y, float width, float height, float zoom)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;


            this.aspectRatio = width / height;
            this.zoom = zoom;
        }

    }
}
