using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2DEngine
{
    public class Block
    {
        public int X;
        public int Y;
        public int width;
        public int height;

        public Block(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            this.width = width;
            this.height = height;
        }

        public void Draw(Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color.Black), X, Y, width, height);
        }
    }
}
