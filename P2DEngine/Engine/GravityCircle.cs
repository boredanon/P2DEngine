using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2DEngine.Engine
{
    public class GravityCircle : P2DCircle
    {
        private float gravity;
        private bool useDeltaTime; // Si queremos que use delta time o no.

        public GravityCircle(int X, int Y, int radius, Color color, bool useDeltaTime) : base(X, Y, radius, color)
        {
            gravity = 9.81f;
            this.useDeltaTime = useDeltaTime;
        }

        public override void Update(float DeltaTime)
        {
            base.Update(DeltaTime);

            // Desafío: ¿Cuál es la diferencia entre estas dos formas de mover el círculo? Juegue con los FPS en el archivo Program.cs para notar la diferencia.
            if (useDeltaTime)
                Position = new PointF(Position.X, Position.Y + gravity * DeltaTime);
            else
                Position = new PointF(Position.X, Position.Y + gravity);
        }
        public override void Draw(Graphics g)
        {
            base.Draw(g);
        }
    }
}
