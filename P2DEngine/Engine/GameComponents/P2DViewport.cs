using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2DEngine.Engine
{
    public class P2DViewport
    {
        // Sorpresa: El viewport es simplemente un rectángulo. Piense en el mundo ahora como infinito y el viewport como el pedazo de mundo que estamos viendo actualmente.
        // Desafío: ¿Cómo podría implementar zoom? Pista: El tamaño de la pantalla es independiente al tamaño del viewport, coincidentemente estamos trabajando con las mismas dimensiones. (Vea los métodos ChangeScreen en P2DGame)
        public float X;
        public float Y;
        public float Width;
        public float Height;

        public P2DViewport(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
    }
}
