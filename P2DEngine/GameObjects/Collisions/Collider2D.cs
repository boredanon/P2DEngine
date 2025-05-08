using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2DEngine.GameObjects.Collisions
{
    public abstract class Collider2D
    {
        public float x;
        public float y;

        public myGameObject attachedGameObject;

        // Constructor.
        public Collider2D(myGameObject attachedGameObject)
        {
            this.attachedGameObject = attachedGameObject;
        }

        // Para actualizar el collider.
        public void PhysicsUpdate(float deltaTime)
        {
            this.x = attachedGameObject.x;
            this.y = attachedGameObject.y;
            UpdateCollider(deltaTime);
        }
        public abstract void UpdateCollider(float deltaTime);
        
        // Para ver si el collider está chocando con otro collider.
        public abstract bool IsColliding(Collider2D other);


        // Colisión punto - punto => si la posición es la misma.
        public bool PointPointCollision2D(PointCollider2D a, PointCollider2D b)
        {
            return a.x == b.x && a.y == b.y;
        }

        // Colisión punto - círculo => si la distancia entre el centro del círculo y el punto es menor al radio.
        public bool PointCircleCollision2D(PointCollider2D a, CircleCollider2D b)
        {
            var bCenter = b.GetCenter();

            var distX = bCenter.X - a.x;
            var distY = bCenter.Y - a.y;

            var distanceSquared = distX * distX + distY * distY;

            return distanceSquared <= b.radius * b.radius;
        }


        // Colisión punto - caja => si el punto está dentro de la caja.
        public bool PointBoxCollision2D(PointCollider2D a, BoxCollider2D b)
        {
            if(a.x > b.x && a.x < b.x + b.width)
            {
                if(a.y > b.y && a.y < b.y + b.height)
                {
                    return true;
                }
            }
            return false;
        }

        // Colisión círculo - círculo => Si la distancia entre los centros es menor a la suma de sus radios.
        public bool CircleCircleCollision2D(CircleCollider2D a, CircleCollider2D b)
        {
            var aCenter = a.GetCenter();
            var bCenter = b.GetCenter();

            var distX = bCenter.X - aCenter.X;
            var distY = bCenter.Y - aCenter.Y;

            var distanceSquared = distX * distX + distY * distY;
            var radiusSum = a.radius + b.radius;

            return distanceSquared <= radiusSum*radiusSum;
        }

        // Colisión círculo - caja => Si la distancia entre el punto más cercano de la caja y el centro del círculo es menor al radio.
        public bool CircleBoxCollision2D(CircleCollider2D a, BoxCollider2D b)
        {
            var aCenter = a.GetCenter();
            var testX = aCenter.X;
            var testY = aCenter.Y;

            if (aCenter.X < b.x)
            {
                testX = b.x;
            }
            else if (aCenter.X > b.x + b.width)
            {
                testX = b.x + b.width;
            }

            if (aCenter.Y < b.y)
            {
                testY = b.y;
            }
            else if (aCenter.Y > b.y + b.height)
            {
                testY = b.y + b.height;
            }

            var distX = testX - aCenter.X;
            var distY = testY - aCenter.Y;

            var distanceSquared = distX *distX + distY*distY;

            var colliding = distanceSquared <= a.radius * a.radius;


            // Esto es para ver por que lado está colisionando, dependiendo del punto más cercano, podemos deducir cual es.
            b.collidingByLeft = false;
            b.collidingByRight = false;
            b.collidingByTop = false;
            b.collidingByBottom = false;

            if (colliding)
            {
                if(testX == b.x)
                {
                    b.collidingByLeft = true;
                }
                if (testX == b.x + b.width)
                {
                    b.collidingByRight = true;
                }
                if(testY == b.y)
                {
                    b.collidingByTop = true;
                }
                if(testY == b.y + b.height)
                {
                    b.collidingByBottom = true;
                }
            }


            return colliding;
        }

        // Colisión caja-caja => Si uno de los puntos de una caja está dentro de otra.
        public bool BoxBoxCollision2D(BoxCollider2D a, BoxCollider2D b)
        {
            var colliding = false;
            if(a.x + a.width > b.x && a.x < b.x + b.width)
            {
                if (a.y + a.height > b.y && a.y < b.y + b.height)
                {
                    colliding = true;
                }
            }

            if (colliding)
            {
                // Esto es para deducir por que lado está colisionando. Se calcula la distancia entre los lados de la caja.
                var left = Math.Abs((a.x + a.width) - b.x);
                var right = Math.Abs(a.x - (b.x + b.width));
                var top = Math.Abs((a.y + a.height) - b.y);
                var bottom = Math.Abs(a.y - (b.y + b.height));

                var minHorizontal = Math.Min(left, right);
                var minVertical = Math.Min(top, bottom);
                var min = Math.Min(minHorizontal, minVertical);


                a.collidingByLeft = false;
                a.collidingByRight = false;
                a.collidingByTop = false;
                a.collidingByBottom = false;

                if (min == left)
                {
                    a.collidingByLeft = true;
                }
                if (min == right)
                {
                    a.collidingByRight = true;
                }
                if (min == top)
                {
                    a.collidingByTop = true;
                }
                if (min == bottom)
                {
                    a.collidingByBottom = true;
                }
            }
            return colliding;
        }

    }
}
