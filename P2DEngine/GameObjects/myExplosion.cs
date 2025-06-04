using P2DEngine.GameObjects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace P2DEngine
{
    // Sistema de partículas de ejemplo.
    public class myExplosion : myParticleSystem
    {
        public myExplosion(float x, float y, float maxLife, int numParticles, Color particleColor) : base(x, y, maxLife, numParticles, particleColor)
        {
        
        }

        public override Particle InstantiateParticle()
        {
            float speed = (float)random.NextDouble() * 100f; // Velocidad entre 0 y 100
            float randomDirX = (float)random.NextDouble() * 2f - 1f; // Dirección en X entre -1 y 1.
            float randomDirY = (float)random.NextDouble() * 2f - 1f; // Dirección en Y entre -1 y 1

            var direction = new Vector(randomDirX, randomDirY);

            if (direction.X != 0f && direction.Y != 0f)
            {
                direction.Normalize();
            }

            var velocity = speed * direction;
            var life = (float)random.NextDouble(); // Vida entre 0 y 1.

            Particle p = new Particle(this.x, this.y, life, (float)velocity.X, (float)velocity.Y, 10f, particleColor, fadeParticles);
            return p;
        }
    }
}
