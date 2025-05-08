using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace P2DEngine.GameObjects.Collisions
{
    public class CircleCollider2D : Collider2D
    {
        public float radius;

        public CircleCollider2D(float radius, myPhysicsGameObject attachedGameObject) : base(attachedGameObject)
        {
            this.radius = radius;
        }

        public override bool IsColliding(Collider2D other)
        {
            if (other is CircleCollider2D) // Círculo-Círculo
            {
                return CircleCircleCollision2D(this, (CircleCollider2D)other);
            }
            else if (other is PointCollider2D) // Punto-círculo
            {
                return PointCircleCollision2D((PointCollider2D)other, this);
            }
            else if(other is BoxCollider2D) // Círculo-Caja
            {
                return CircleBoxCollision2D(this, (BoxCollider2D)other);
            }
                return false;
        }

        public Vector GetCenter() // Obtener el centro del círculo.
        {
            return new Vector(x + radius, y + radius);
        }

        // Esto lo pueden usar si necesitan actualizar por alguna razón algo del collider.
        public override void UpdateCollider(float deltaTime)
        {
        }
    }
}
