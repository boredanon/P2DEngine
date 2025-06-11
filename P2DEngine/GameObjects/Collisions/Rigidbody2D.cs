using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2DEngine.GameObjects.Collisions
{
    public enum RigidBodyType2D
    {
        Static, // No afectado por fuerzas.
        Kinematic, // Ustedes manejan el movimiento directamente.
        Dynamic // Afectado por fuerzas.
    }


    public class Rigidbody2D
    {
        public Rigidbody2D(myPhysicsGameObject attachedGameObject, RigidBodyType2D type = RigidBodyType2D.Kinematic) { 
            this.attachedGameObject = attachedGameObject;
        }

        public RigidBodyType2D bodyType;
        public myPhysicsGameObject attachedGameObject;
        
        public float mass = 1f;
        public float velocityX;
        public float velocityY;
        public bool affectedByGravity;

        // Fuerza.
        public float accForceX = 0f;
        public float accForceY = 0f;

        // Impulso.
        public float accImpulseX = 0f;
        public float accImpulseY = 0f;

        public void AddForce(float forceX, float forceY)
        {
            if(bodyType != RigidBodyType2D.Dynamic) // Los rigidbody no dinámicos no son afectados por la fuerza.
            {
                return;
            }

            accForceX += forceX;
            accForceY += forceY;
        }

        public void AddImpulse(float impulseX, float impulseY)
        {
            if(bodyType != RigidBodyType2D.Dynamic) // Los rigidbody no dinámicos no son afectados por el impulso.
            {
                return;
            }

            accImpulseX += impulseX;
            accImpulseY += impulseY;
        }


        public void Update(float deltaTime)
        {
            switch(bodyType)
            {
                case RigidBodyType2D.Static: // No afectado por la fuerza.
                    return;
                    break;
                case RigidBodyType2D.Kinematic: // Cambios de acuerdo a la velocidad.
                    attachedGameObject.x += velocityX * deltaTime;
                    attachedGameObject.y += velocityY * deltaTime;
                    break;
                case RigidBodyType2D.Dynamic:

                    if(affectedByGravity) 
                    {
                        AddForce(0, 9.8f); // Fuerza de gravedad.
                    }

                    velocityX += accImpulseX * mass;
                    velocityY += accImpulseY * mass; // El impulso es un cambio instantáneo de la velocidad en este caso.

                    accImpulseX = 0f;
                    accImpulseY = 0f;


                    // La fuerza, en cambio, cambia la aceleración (F = m * a)

                    float accelerationX = accForceX / mass;
                    float accelerationY = accForceY / mass;

                    accForceX = 0f;
                    accForceY = 0f; // Lo reiniciamos a cero, ya que si no lo hacemos, añadiremos la suma de las fuerzas múltiples veces en
                    // cada update.

                    velocityX += accelerationX * deltaTime;
                    velocityY += accelerationY * deltaTime;

                    attachedGameObject.x += velocityX * deltaTime;
                    attachedGameObject.y += velocityY * deltaTime;

                    break;
            }
        }


    }
}
