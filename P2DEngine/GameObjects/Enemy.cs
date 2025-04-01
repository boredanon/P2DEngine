using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2DEngine
{
    // Como puede ver, esta hereda de myBlock y no de myGameObject.
    public class Enemy : myBlock
    {
        public Enemy(float x, float y, float sizeX, float sizeY, Color color) : base(x, y, sizeX, sizeY, color)
        {
        }

        // Puede tener su propio update, que lo diferencia de los otros myBlock.
        public override void Update(float deltaTime)
        {
            this.x += 10; // Cada vez que llamamos al Update, le añadimos 10 píxeles.
            // Como puede notar, si el juego corre a 60 FPS, esto se llamará aproximadamente 60 veces por segundo.
            // haciendo que se mueva 600 píxeles en un solo segundo, lo que no es ideal (véa la clase Player.cs)
        }
    }
}
