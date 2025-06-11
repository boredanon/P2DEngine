using P2DEngine.Games.SceneGame;
using P2DEngine.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2DEngine.Games
{
    public class JuegoEjemplo : myGame
    {
        public JuegoEjemplo(int width, int height, int FPS, myCamera c) : base(width, height, FPS, c)
        {
            NuevaEscena escena = new NuevaEscena(c);
            Scene2 s = new Scene2(c);

            
            mySceneManager.Register(escena, "ejemplo");
            mySceneManager.Register(s, "Scene2");


            mySceneManager.SetActive("ejemplo");
        }
    }
}
