using P2DEngine.GameObjects;
using P2DEngine.Managers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P2DEngine.Games.SceneGame
{
    // Una de las escenas del juego.
    public class Scene1 : myScene
    {
        // Cuatro explosiones para demostrar las diferentes opciones del sistema de partículas.
        myExplosion particleSystem;
        myExplosion particleSystem2;
        myExplosion particleSystem3;
        myExplosion particleSystem4;
        myExplosion particleSystem5;
        myExplosion particleSystem6;

        public Scene1(myCamera camera) : base(camera){} // Constructor.

        public override void ProcessInput()
        {
            if(myInputManager.IsKeyPressed(Keys.Z)) // Cambiar a la escena 2.
            {
                mySceneManager.SetActive("Scene2");
            }
        }

        public override void Render(Graphics g) // Algo de dibujado especial, como UI, etc.
        {
        }

        public override void Init() // Para inicializar/reiniciar la escena.
        {
            // Limpiamos todos los valores.
            gameObjects.Clear();
            currentCamera.x = 0f;
            currentCamera.y = 0f;
            myBackgroundManager.backgroundLayers[this].Clear();


            // Esto lo puse aquí para probar si no rompí el parallax, pero aparentemente funciona correctamente.
            var testLayer = new myBackgroundLayer(Color.White, new PointF(myGame.windowWidth, myGame.windowHeight), 0.0f);
            var testLayer2 = new myBackgroundLayer(Color.Black, new PointF(myGame.windowWidth * 0.9f, myGame.windowHeight * 3f/4f), 0.25f);
            var testLayer3 = new myBackgroundLayer(Color.LightBlue, new PointF(myGame.windowWidth * 0.8f, myGame.windowHeight * 2f/4f), 0.5f);
            var testLayer4 = new myBackgroundLayer(Color.Green, new PointF(myGame.windowWidth * 0.7f, myGame.windowHeight * 1f/4f), 0.75f);

            myBackgroundManager.AddLayerToScene(this, testLayer);
            myBackgroundManager.AddLayerToScene(this, testLayer2);
            myBackgroundManager.AddLayerToScene(this, testLayer3);
            myBackgroundManager.AddLayerToScene(this, testLayer4);


            // Instanciamos los sistemas de partículas con distintos parámetros.
            particleSystem = new myExplosion(100, 100, 1f, 100, Color.Red);
            particleSystem.oneShot = true;

            particleSystem2 = new myExplosion(300, 100, 1f, 100, Color.Blue);
            particleSystem2.oneShot = true;
            particleSystem2.fadeParticles = true;
            
            particleSystem3 = new myExplosion(500, 100, 1f, 100, Color.Gold);
            particleSystem3.oneShot = false;
            particleSystem3.fadeParticles = true;
            particleSystem3.regenerate = true;


            particleSystem4 = new myExplosion(100, 300, 1f, 100, Color.White);
            particleSystem4.oneShot = false;
            particleSystem4.fadeParticles = true;
            particleSystem4.regenerate = false;

            particleSystem5 = new myExplosion(0, 0, 1f, 100, Color.Black);
            particleSystem5.oneShot = false;
            particleSystem5.fadeParticles = true;
            particleSystem5.regenerate = true;


            particleSystem6 = new myExplosion(0, 0, 1f, 100, Color.Brown);
            particleSystem6.oneShot = false;
            particleSystem6.fadeParticles = true;
            particleSystem6.regenerate = false;


            Instantiate(particleSystem);
            Instantiate(particleSystem2);
            Instantiate(particleSystem3);
            Instantiate(particleSystem4);
            Instantiate(particleSystem5);
            Instantiate(particleSystem6);
        }

        public override void Update(float deltaTime)
        {
            // Para mover la cámara (estaba probando el parallax).
            var right = myInputManager.IsKeyPressed(Keys.Right);
            var left = myInputManager.IsKeyPressed(Keys.Left);

            if (right)
            {
                currentCamera.x += 10f;
            }
            else if(left)
            {
                currentCamera.y -= 10f;
            }

            var mousePosition = myInputManager.mousePosition;
            particleSystem5.x = mousePosition.X + currentCamera.x; // El mouse está en coordenadas de pantalla, la debo mover a coordenadas de mundo o si no se vería con un "offset" cuando dibujo el sistema en base a la cámara.
            particleSystem5.y = mousePosition.Y + currentCamera.y;

            particleSystem6.x = mousePosition.X + currentCamera.x;
            particleSystem6.y = mousePosition.Y + currentCamera.y;
        }
    }
}
