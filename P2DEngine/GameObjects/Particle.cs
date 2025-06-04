using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace P2DEngine.GameObjects
{
    // Clase para las partículas.
    public class Particle : myGameObject
    {
        // Parámetros: vida, tamaño color y si va desapareciendo gradualmente o no.
        public float maxLife;
        public float currentLife;
        public float size;
        public Color color;
        public bool fading;

        public Particle(float x, float y, float maxLife, float velX, float velY, float size, Color color, bool fading) : base(x, y, size, size, color)
        {
            this.brush = new SolidBrush(color);
            this.color = color;
            this.maxLife = maxLife;
            this.velocityX = velX;
            this.velocityY = velY;
            this.fading = fading;
            this.size = size;
        }

        public override void Draw(Graphics g, Vector position, Vector size)
        {
            if (fading) // Sacamos la proporción del alfa para ver que tanto debería "desaparecer" si es que activamos ese parámetro.
            {
                var proportion = (int)Math.Round((float)((maxLife - currentLife) / (float)maxLife) * 255, 0);
                brush.Color = Color.FromArgb(proportion, color.R, color.G, color.B);
            }
            else
            {
                brush.Color = Color.FromArgb(255, color.R, color.G, color.B);
            }

                g.FillEllipse(brush,
                        (float)position.X,
                        (float)position.Y,
                        (float)size.X,
                        (float)size.Y);
        }

        public override void Update(float deltaTime)
        {
            currentLife += deltaTime;

            this.x += velocityX * deltaTime;
            this.y += velocityY * deltaTime;
        }
    }
}
