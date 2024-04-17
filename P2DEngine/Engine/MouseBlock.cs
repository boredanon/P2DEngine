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
        public MouseBlock(int X, int Y, int width, int height, Color color) : base(X, Y, width, height, color)
        {
        }

        public MouseBlock(int X, int Y, int width, int height, Image image) : base(X, Y, width, height, image)
        {
        }

        public override void Update(float DeltaTime)
        {
            Position = P2DMouseManager.mouseLocation; // Hacemos que este bloque siga al mouse.
        }

        public void SetColor(Color color) // Cambiar el color.
        {
            Color = color;
        }
    }
}
