using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2DEngine
{
    public class Player : myBlock
    {
        public Player(float x, float y, float sizeX, float sizeY, Color color) : base(x, y, sizeX, sizeY, color)
        {
        }

        public override void Update(float deltaTime)
        {
            this.x += 10 * deltaTime; // Le añadimos 10*deltaTime cada vez que llama al método.

            // Considere que el juego corre a 60 FPS, entonces este método se llamará 60 veces en un segundo.
            // Lo que implica que en la primera frame se movería 10*0.016, en la segunda otros 10*0.016 y así
            // 60 veces, osea se moverá alrededor de 10 píxeles por segundo a 60 FPS (vea la clase Enemy.cs)
        }
    }
}
