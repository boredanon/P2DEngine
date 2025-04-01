using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace P2DEngine
{
    public class myCamera
    {
        // La cámara es simplemente un rectángulo.
        public float x;
        public float y;
        public float width;
        public float height;

        public float zoom;

        public myCamera(int x, int y, float width, float height, float zoom)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;

            this.zoom = zoom;
        }


        // Pasamos las coordenadas de mundo a coordenadas de cámara, para que puedan ser dibujadas.
        public Vector GetViewPosition(float x, float y)
        {
            Vector viewPosition = new Vector();
            
            viewPosition.X = (x - this.x) * this.zoom;
            viewPosition.Y = (y - this.y) * this.zoom;

            return viewPosition;
        }

        public Vector GetViewSize(float sizeX, float sizeY)
        {
            Vector viewSize = new Vector();

            viewSize.X = (sizeX) * this.zoom;
            viewSize.Y = (sizeY) * this.zoom;
            return viewSize;
        }


    }
}
