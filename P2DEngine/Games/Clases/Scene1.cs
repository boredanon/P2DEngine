using P2DEngine.Engine;
using P2DEngine.Engine.GameComponents;
using P2DEngine.Engine.Managers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2DEngine.Games.Clases
{
 
    // Creamos una clase para cada escena, en este caso "Scene1".
    public class Scene1 : P2DScene
    {
        // Input.
        bool space;
        bool pressedA;
        
        public bool pressedButton;
        public bool previousPressedButton;
        
        // Objetos de esta escena.
        P2DBlock b;
        // UI Objetos de esta escena.
        P2DButton button;

        public Scene1()
        {
            // RECUERDE SIEMPRE INICIALIZAR LA LISTA.
            gameObjects = new List<P2DGameObject>();
            UIObjects = new List<P2DUIObject>();

            // Note que estos instantiates son diferentes.
            Instantiate(b = new P2DBlock(100, 100, 50, 50, 0, Color.White));

            Instantiate(button = new P2DButton(200, 200, 100, 50, 0, Color.Yellow));
        }

        public override void ProcessInput()
        {
            space = P2DInputManager.IsKeyPressed(System.Windows.Forms.Keys.Space);
            pressedA = P2DInputManager.IsKeyPressed(System.Windows.Forms.Keys.A);
        }

        public override void Render(Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color.Purple), 0, 0, P2DGame.viewportSize.X, P2DGame.viewportSize.Y);
        
            foreach(P2DGameObject obj in gameObjects) // Dibujamos todos los gameobjects
            {
                DrawObject(g, obj);
            }


            // Importante: Como se dibuja secuencial, recuerde siempre dibujar los UIObjects después
            // de los gameObjects!
            foreach(P2DUIObject obj in UIObjects) // Dibujamos todos los UIObjects
            {
                DrawUIObject(g, obj);
            }
        
        }

        // Reiniciamos la escena.
        public override void Reset()
        {
            // Desafío: Reinicie la posición del bloque "b" cuando vuelva a esta pantalla.
            // (Vea como cambiar de pantalla en el Update de las escenas)
            // Luego intente hacerlo después de cambiar el tamaño de la pantalla.
        }

        public override void Update(float deltaTime)
        {
            button.Update(deltaTime);

            pressedButton = button.isDown;

            if(!pressedButton && previousPressedButton) // Si presionamos el botón, cambiamos de escena.
            {
                P2DSceneManager.SetActive("Escena_2");
            }

            if(pressedA) // Mover el bloque b, vea desafío en método reset.
            {
                b.Position = new PointF(b.Position.X + 1, b.Position.Y);
            }
            previousPressedButton = pressedButton; // Estado antiguo del botón.  
        }
    }
}
