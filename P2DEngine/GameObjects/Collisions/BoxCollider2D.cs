using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2DEngine.GameObjects.Collisions
{
    // Colisiones rectangulares.
    public class BoxCollider2D : Collider2D
    {
        //Ancho y alto.
        public float width;
        public float height;

        //Para reconocer por que lado colisiona.
        public bool collidingByTop;
        public bool collidingByBottom;
        public bool collidingByRight;
        public bool collidingByLeft;

        public BoxCollider2D(float width, float height, myGameObject attachedGameObject) : base(attachedGameObject)
        {
            this.width = width;
            this.height = height;
        }

        public override bool IsColliding(Collider2D other)
        {
            if (other is BoxCollider2D) // Colisión caja-caja
            {
                return BoxBoxCollision2D(this, (BoxCollider2D)other);
            }
            else if (other is PointCollider2D) // Colisión caja-punto
            {
                return PointBoxCollision2D((PointCollider2D)other, this);
            }
            else if(other is CircleCollider2D) // Colisión caja-círculo
            {
                return CircleBoxCollision2D((CircleCollider2D)other, this);
            }
            return false;
        }

        public override void UpdateCollider(float deltaTime)
        {
        }
    }
}
