using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P2DEngine.GameObjects;
using P2DEngine.GameObjects.Collisions;
using P2DEngine.Games.SceneGame;
using P2DEngine.Managers;

namespace P2DEngine.Games
{
    public class Game : myGame
    {
        // Ahora aquí en las clase de juego se deben crear y registrar las escenas correspondientes.
        public Game(int width, int height, int FPS, myCamera c) : base(width, height, FPS, c)
        {
            Scene1 s = new Scene1(c);
            Scene2 s2 = new Scene2(c);

            mySceneManager.Register(s, "Scene1");
            mySceneManager.Register(s2, "Scene2");

            mySceneManager.SetActive("Scene1");
        }
    }
}
