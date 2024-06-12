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
    public class Escenita : P2DScene
    {
        bool enter;

        public Escenita() {
            gameObjects = new List<P2DGameObject>();
            UIObjects = new List<P2DUIObject>();

            // Recuerde como funciona el parallax.
            P2DBackgroundManager.AddLayer("bg_1", 0.1f);
            P2DBackgroundManager.AddLayer("bg_2", 0.25f);
            P2DBackgroundManager.AddLayer("bg_3", 0.5f);
            P2DBackgroundManager.AddLayer("bg_4", 1.0f);
        }
        public override void ProcessInput()
        {
            //throw new NotImplementedException();
            enter = P2DInputManager.IsKeyPressed(System.Windows.Forms.Keys.Enter);



        }

        public override void Render(Graphics g)
        {
            DrawBackground(g);
        }

        public override void Reset()
        {

        }

        public override void Update(float deltaTime)
        {
            foreach (var layer in P2DBackgroundManager.layers)
            {
                layer.Update(deltaTime);
            }

            if (P2DInputManager.IsKeyPressed(System.Windows.Forms.Keys.L))
            {
                P2DGame.ChangeScreenSize(400, 300);
            }


            P2DBackgroundManager.SetParallaxSpeed(100);
            if(enter)
            {
                P2DSceneManager.ResetScene("Escena_1");
                P2DSceneManager.SetActive("Escena_1");
            }
            //throw new NotImplementedException();
        }
    }
}
