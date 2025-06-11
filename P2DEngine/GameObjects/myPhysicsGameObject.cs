using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using P2DEngine.GameObjects.Collisions;
using P2DEngine.Managers;

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
        public Rigidbody2D body;

        public List<myPhysicsGameObject> collidingObjects = new List<myPhysicsGameObject>();
        public List<myPhysicsGameObject> prevCollidingObjects = new List<myPhysicsGameObject>();


        public myPhysicsGameObject(float x, float y, float sizeX, float sizeY, Color color) : base(x, y, sizeX, sizeY, color)
        {
            CreateCollider(sizeX, sizeY);
            body = new Rigidbody2D(this);
        }

        public myPhysicsGameObject(float x, float y, float sizeX, float sizeY, Image image) : base(x, y, sizeX, sizeY, image)
        {
            CreateCollider(sizeX, sizeY);
            body = new Rigidbody2D(this);
        }

        public abstract void CreateCollider(float sizeX, float sizeY);

        public override void Update(float deltaTime)
        {
            if (body != null)
            {
                body.Update(deltaTime); // Actualizar el rigidbody.
            }

            collidingObjects = new List<myPhysicsGameObject>();
            var gameObjects = mySceneManager.GetActiveScene().gameObjects;

            // Verificamos si algún objeto está colisionando.
            foreach (var gameObject in gameObjects)
            {
                if (gameObject == this)
                {
                    continue;
                }

                if (gameObject is myPhysicsGameObject)
                {
                    if (IsColliding((myPhysicsGameObject)gameObject)) // Lo añadimos a una lista.
                    {
                        collidingObjects.Add((myPhysicsGameObject)gameObject);
                    }
                }

            }

            foreach (var collidingObject in collidingObjects)
            {
                if (prevCollidingObjects.Contains(collidingObject)) // Si está colisionando en este frame y en el anterior.
                {
                    OnCollisionStay2D(collidingObject); // Mantiene la colisión.
                }
                else // Si está colisionando en este frame, pero no en el anterior.
                {
                    OnCollisionEnter2D(collidingObject); // Entra a la colisión.
                }
            }

            foreach (var collidingObject in prevCollidingObjects) // Si está colisionando en el frame anterior
            {
                if (!collidingObjects.Contains(collidingObject)) // pero no en el actual.
                {
                    OnCollisionExit2D(collidingObject); // Sale de la colisión.
                }
            }
            collider.PhysicsUpdate(deltaTime); // Actualizamos el collider.
            UpdateGameObject(deltaTime);  // Actualizamos el objeto.

            prevCollidingObjects = new List<myPhysicsGameObject>(collidingObjects); // Copiamos la lista.
        }
        public abstract void UpdateGameObject(float deltaTime);

        // Para ver si dos objetos físicos están colisionando, debemos ver si sus collider están colisionando.
        public virtual bool IsColliding(myPhysicsGameObject other)
        {
            return collider.IsColliding(other.collider);
        }

        public abstract void OnCollisionEnter2D(myPhysicsGameObject other);
        public abstract void OnCollisionStay2D(myPhysicsGameObject other);
        public abstract void OnCollisionExit2D(myPhysicsGameObject other);
    }
}
