using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2DEngine.GameObjects.Collisions
{
    public class PointCollider2D : Collider2D
    {
        public PointCollider2D(myGameObject attachedGameObject) : base(attachedGameObject)
        {
        }

        public override bool IsColliding(Collider2D other)
        {
            if(other is PointCollider2D) // Punto-punto
            {
                return PointPointCollision2D(this, (PointCollider2D)other);
            }
            else if(other is CircleCollider2D) // ´Punto - Círculo
            {
                return PointCircleCollision2D(this, (CircleCollider2D)other);
            }
            else if(other is BoxCollider2D) // Punto - Caja.
            {
                return PointBoxCollision2D(this, (BoxCollider2D)other);
            }
            return false;
        }

        public override void UpdateCollider(float deltaTime)
        {
        }
    }
}
