using P2DEngine.GameObjects;
using P2DEngine.Managers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2DEngine.Games
{
    // Clase base de cada escena. Funciona relativamente igual que como funcionaba antes una clase de tipo myGame.cs
    public abstract class myScene
    {
        public List<myGameObject> gameObjects; // Cada escena tiene su lista de objetos. Si usted quiere hacer un objeto global (es decir, que funcione entre escenas), tendría que buscar la forma de implementarlo. La pista es que ud. puede obtener
        // referencias a otras escenas con la clase mySceneManager.

        // Cámaras
        protected myCamera mainCamera;
        public myCamera currentCamera;

        public myScene(myCamera camera) // Constructor.
        {
            mainCamera = camera;
            currentCamera = mainCamera;

            gameObjects = new List<myGameObject>();

            if (!myBackgroundManager.backgroundLayers.ContainsKey(this))
            {
                myBackgroundManager.backgroundLayers[this] = new List<myBackgroundLayer>();
            }


            Init();
        }

        // Métodos que cada escena debe implementar.
        public abstract void Init();
        public abstract void ProcessInput();
        public abstract void Update(float deltaTime);
        public abstract void Render(Graphics g);
        

        // Instanciar y desinstanciar objetos.
        public myGameObject Instantiate(myGameObject go)
        {
            if (!gameObjects.Contains(go))
            {
                gameObjects.Add(go);
            }
            return go;
        }

        public myGameObject Destroy(myGameObject go)
        {
            if(gameObjects.Contains(go))
            {
                gameObjects.Remove(go); 
            }
            return go;
        }
    }
}
   