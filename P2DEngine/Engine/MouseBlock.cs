using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2DEngine.Engine
{
    internal class MouseBlock : P2DBlock
    {
        public MouseBlock(int X, int Y, int width, int height, float angle, Color color) : base(X, Y, width, height, angle, color)
        {
        }

        public MouseBlock(int X, int Y, int width, int height, float angle, Image image) : base(X, Y, width, height, angle, image)
        {
        }

        public override void Update(float DeltaTime)
        {
            Position = P2DMouseManager.mouseLocation; // Hacemos que este bloque siga al mouse. Desafío: Mueva la cámara y vea lo que pasa. ¿Cómo podría hacer que al mover la cámara este bloque aún siga al mouse?
            // Pista: Vea el método RenderGame de P2DGame.
            Angle += 10 * DeltaTime; // Podemos cambiar el ángulo dinámicamente.
        
        }

        public void SetColor(Color color) // Cambiar el color.
        {
            Color = color;
        }
    }
}
