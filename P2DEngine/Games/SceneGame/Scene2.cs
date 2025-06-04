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
    // Otra escena que solo tiene un fondo de color.
    public class Scene2 : myScene
    {
        public Scene2(myCamera camera) : base(camera)
        {
        }

        public override void Init()
        {
            var layer = new myBackgroundLayer(Color.Aquamarine, new PointF(myGame.windowWidth, myGame.windowHeight), 0.0f);
            myBackgroundManager.AddLayerToScene(this, layer);
        }

        public override void ProcessInput()
        {
            if (myInputManager.IsKeyPressed(Keys.X)) // Volver a la escena 1, pero reiniciando los valores.
            {
                mySceneManager.SetActive("Scene1", true);
            }
        }

        public override void Render(Graphics g)
        {
        }

        public override void Update(float deltaTime)
        {
        }
    }
}
