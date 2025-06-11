using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using P2DEngine.GameObjects.Collisions;

namespace P2DEngine.GameObjects
{
    public class myPhysicsCircle : myPhysicsGameObject
    {
        // Todo esto es funcionalmente igual a lo que está en myCircle.cs, excepto que esta clase considera tener un collider.

        public float radius;
        public myPhysicsCircle(float x, float y, float radius, Color color) : base(x, y, radius*2, radius*2, color)
        {
            this.radius = radius;
        }

        public myPhysicsCircle(float x, float y, float radius, Image image) : base(x, y, radius*2, radius*2, image)
        {
            this.radius = radius; 
        }

        public override void CreateCollider(float sizeX, float sizeY)
        { 
            collider = new CircleCollider2D(sizeX/2, this);
        }

        public override void Draw(Graphics g, Vector position, Vector size)
        {
            if (image == null)
            {
                g.FillEllipse(brush, (float)position.X, (float)position.Y, (float)size.X, (float)size.Y);
            }
            else
            {
                g.DrawImage(image, (float)position.X, (float)position.Y, (float)size.X, (float)size.Y);
            }
        }

        public override void OnCollisionEnter2D(myPhysicsGameObject other)
        {
        }

        public override void OnCollisionExit2D(myPhysicsGameObject other)
        {
        }

        public override void OnCollisionStay2D(myPhysicsGameObject other)
        {
        }

        public override void UpdateGameObject(float deltaTime)
        {
            
        }
    }
}
