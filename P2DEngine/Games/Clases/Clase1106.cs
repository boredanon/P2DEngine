using P2DEngine.Engine;
using P2DEngine.Engine.Managers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace P2DEngine.Games.Clases
{
    public class Clase1106 : P2DGame
    {
        public Clase1106(int width, int height, P2DViewport vp, int targetFPS) : base(width, height, vp, targetFPS)
        {
            // Registramos todas las escenas de nuestro juego.
            P2DSceneManager.Register(new Scene1(), "Escena_1");
            P2DSceneManager.Register(new Escenita(), "Escena_2");

            // Debemos dejar una escena como activa.
            P2DSceneManager.SetActive("Escena_1");
        }
    }
}
