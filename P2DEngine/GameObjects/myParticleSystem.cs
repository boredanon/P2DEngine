using P2DEngine.Managers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace P2DEngine.GameObjects
{
    // Clase para el manejo de las partículas.
    public abstract class myParticleSystem : myGameObject
    {
        protected Random random = new Random();

        // Una lista de partículas, la cantidad de partículas en el sistema.
        List<Particle> particles;
        public int numParticles;
       
        // Parametros: oneshot (se ejecuta solo una vez), regenerate (continuamente se emiten partículas), fadeParticles (el alfa de las partículas va cambiando)
        public bool oneShot;
        public bool fadeParticles;
        public bool regenerate;
        public Color particleColor;

        int generatedParticles = 0;

        public myParticleSystem(float x, float y, float maxLife, int numParticles, Color particleColor) : base(x, y, 0, 0, Color.White)
        {
            this.numParticles = numParticles;   
            particles = new List<Particle>();
            this.particleColor = particleColor;

            /*if(oneShot)
            {
                for (int i = 0; i < numParticles; i++)
                {
                    particles.Add(InstantiateParticle());
                }
            }*/
            
        }

        // Cada sistema de partículas tiene su propia forma de instanciar partículas.
        public abstract Particle InstantiateParticle();

        // Dibujamos cada partícula en la lista.
        public override void Draw(Graphics g, Vector position, Vector size)
        {
            var camera = mySceneManager.GetActiveScene().currentCamera;
            foreach (var particle in particles)
            {
                var particlePosition = camera.GetViewPosition(particle.x, particle.y);
                var particleSize = camera.GetViewSize(particle.sizeX, particle.sizeY);
                particle.Draw(g, particlePosition, particleSize);
            }
        }

        public override void Update(float deltaTime)
        {
            bool generate;
            if (oneShot && (generatedParticles >= numParticles))
            {
                generate = false;
            }
            else
            {
                generate = true;
            }


            if (generate)
            {
                if (regenerate)
                {

                    if (particles.Count < numParticles)
                    {
                        int difference = numParticles - particles.Count;
                        for (int i = 0; i < difference; i++)
                        {
                            particles.Add(InstantiateParticle());
                        }
                        generatedParticles += difference;
                    }
                }
                else
                {
                    if (particles.Count == 0)
                    {
                        for (int i = 0; i < numParticles; i++)
                        {
                            particles.Add(InstantiateParticle());
                        }
                    }
                }
            }
            



            /*if (regenerate) // Si se regeneran continuamente.
            { }

            else // Si vuelve a ocurrir el efecto tras finalizar.
            {
                if (particles.Count == 0)
                {
                    for (int i = 0; i < numParticles; i++)
                    {
                        particles.Add(InstantiateParticle());
                    }
                }
            }*/
            
            // Nótese que aquí falta algo importante: si no se regenera y se acaban las partículas, ud. debería destruir el sistema para que no ocupe recursos.

            List<Particle> toRemove = new List<Particle>();
            foreach(var particle in particles)
            {
                particle.Update(deltaTime);

                if(particle.currentLife >= particle.maxLife)
                {
                    toRemove.Add(particle);
                }
            }

            foreach(var particle in toRemove)
            {
                particles.Remove(particle);
            }
        }
    }
}
