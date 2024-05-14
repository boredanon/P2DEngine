using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2DEngine.Engine.GameComponents
{
    public class P2DSprite
    {
        private List<Image> frames;
        private int frame_index;

        private float delta;
        private float elapsedTime;

        public P2DSprite(float delta) // Recibe cuanto tiempo queremos que se demore en cada frame.
        {
            this.frames = new List<Image>();
            this.frame_index = 0;
            this.delta = delta;
            this.elapsedTime = 0;
        }

        public void Add(Image image) // Añadir nuevas frames a la animación.
        {
            this.frames.Add(image);
        }

        public Image GetCurrentFrame() // Obtener la frame actual, para dibujar (vea Player.cs)
        {
            return this.frames[this.frame_index];
        }

        public void Update(float DeltaTime) { 
            this.elapsedTime += DeltaTime; // Timer.
            
            if(this.elapsedTime > this.delta) // Si pasa el tiempo suficiente en la frame actual
            {
                this.elapsedTime = 0;
                this.frame_index++; // Pasa de frame.
                if(this.frame_index >= this.frames.Count) // Volver al frame inicial una vez pasa por todas las frames.
                {
                    this.frame_index = 0;
                }

            }
        }
    }
}
