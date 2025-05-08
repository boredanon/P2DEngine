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
    // Esta clase es funcionalmente igual a myGameObject, excepto que considera el uso de físicas.
    public abstract class myPhysicsGameObject : myGameObject
    {
        // Ver si está afectado por la gravedad, su collider etc.
        // Si ud. quiere irse al chancho puede hacer la clase RigidBody2D, poner cosas como masa, viscosidad, pero yo no se lo
        // voy a pedir :^)
        // Note también que en este engine estamos haciendo unas pequeñas suposiciones que usted puede modificar si es que le apetece:
        // 1. Los objetos tienen solo 1 collider. (Podría tener varios!)
        // 2. Los colliders son del mismo tamaño que el objeto (No siempre cierto!) De hecho, este fue el opcional del Proyecto 1.

        public bool affectedByGravity = false; 
        public Collider2D collider;

        public myPhysicsGameObject(float x, float y, float sizeX, float sizeY, Color color) : base(x, y, sizeX, sizeY, color)
        {
            CreateCollider(sizeX, sizeY);
        }

        public myPhysicsGameObject(float x, float y, float sizeX, float sizeY, Image image) : base(x, y, sizeX, sizeY, image)
        {
            CreateCollider(sizeX, sizeY);
        }

        public abstract void CreateCollider(float sizeX, float sizeY);

        public override void Update(float deltaTime)
        {
            if (affectedByGravity) // Si es que se mueve con la gravedad.
            {
                velocityY += 9.8f * deltaTime;
                y += velocityY * deltaTime;
            }
            collider.PhysicsUpdate(deltaTime); // Actualizamos el collider.
            UpdateGameObject(deltaTime);  // Actualizamos el objeto.
        }
        public abstract void UpdateGameObject(float deltaTime);

        // Para ver si dos objetos físicos están colisionando, debemos ver si sus collider están colisionando.
        public virtual bool IsColliding(myPhysicsGameObject other)
        {
            return collider.IsColliding(other.collider);
        }
    }
}
