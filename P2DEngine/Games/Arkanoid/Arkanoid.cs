using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2DEngine
{
    public class Arkanoid : P2DGame // Le recomiendo copiar y pegar el código de su LAB 2 aquí (y obviamente, adaptarlo para que funcione con las nuevas implementaciones hechas en clase)
    {
        public Arkanoid(int width, int height, int targetFPS) : base(width, height, targetFPS)
        {
        }

        protected override void ProcessInput()
        {
            
        }

        protected override void RenderGame(Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color.Black), 0, 0, windowWidth, windowHeight);
        }

        protected override void UpdateGame()
        {

        }
    }
}
