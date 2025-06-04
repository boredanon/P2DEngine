using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace P2DEngine.GameObjects
{
    public class myBackgroundLayer
    {
        Image image;
        SolidBrush color;

        public PointF position;
        public PointF size;
        public float layerSpeed;

        private float scaledLayerSpeed;

        public myBackgroundLayer(Image image, PointF size, float layerSpeed)
        {
            this.image = image;
            this.size = size;
            this.layerSpeed = layerSpeed;
        }

        public myBackgroundLayer(Color color, PointF size, float layerSpeed)
        {
            this.color = new SolidBrush(color);
            this.image = null;
            this.size = size;
            this.layerSpeed = layerSpeed;
        }

        public void Update(float deltaTime)
        {
            scaledLayerSpeed = layerSpeed * deltaTime;
        }

        
        public void Draw(Graphics g, myCamera c)
        {
            //Versión trucha, dibujar la misma imagen tres veces.
            /*
            var pos = c.GetViewPosition((float)position.X, (float)position.Y);
            var siz = c.GetViewSize((float)size.X, (float)size.Y);

            g.DrawImage(image, (float)(pos.X - siz.X + 1), (float)pos.Y, (float)siz.X, (float)siz.Y);
            g.DrawImage(image, (float)pos.X,               (float)pos.Y, (float)siz.X, (float)siz.Y);
            g.DrawImage(image, (float)(pos.X + siz.X - 1), (float)pos.Y, (float)siz.X, (float)siz.Y);
            */


            // Versión pulenta que parece como si supiese programar. Pero es prácticamente el mismo efecto.

            // Definimos el x donde empezar a dibujar, basándonos en la x de la cámara.
            float positionRelativeToCameraX = -c.x * scaledLayerSpeed; // Cuánto se ha movido la layer. 
            float baseX = positionRelativeToCameraX % (size.X); // Para poder la imagen repetir cada size.X unidades.
            if(baseX > 0)
            {
                baseX -= size.X;
            }

            for(float x = baseX; x < c.width / c.zoom + size.X; x+=size.X)
            {
                if (image != null)
                {
                    g.DrawImage(image,
                        x * c.zoom,
                        0,
                        size.X * c.zoom,
                        size.Y * c.zoom);
                }
                else
                {
                    g.FillRectangle(color,
                        x * c.zoom,
                        0,
                        size.X * c.zoom,
                        size.Y * c.zoom);
                }
            }

            
        }
    }
}
