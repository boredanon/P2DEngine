using P2DEngine.Engine.Managers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace P2DEngine.Engine.GameComponents
{
    public abstract class P2DScene
    {
        public List<P2DGameObject> gameObjects; // Ahora cada escena maneja sus gameObjects
        public List<P2DUIObject> UIObjects; // Lo mismo con los UIObjects.

        // Cada escena debe implementar esto.
        public abstract void ProcessInput();
        public abstract void Update(float deltaTime);
        public abstract void Render(Graphics g);
        public abstract void Reset(); // Solo si quiere reiniciar el estado de algunos elementos.

        protected void Instantiate(P2DGameObject gameObject) // Añadir un objeto a la lista.
        {
            gameObjects.Add(gameObject);
        }

        protected void Destroy(P2DGameObject gameObject) // Remover un objeto de la lista.
        {
            gameObjects.Remove(gameObject);
        }

        protected void Instantiate(P2DUIObject UIObject) // Añadir un objeto a la lista.
        {
            UIObjects.Add(UIObject);
        }

        protected void Destroy(P2DUIObject UIObject) // Remover un objeto de la lista.
        {
            UIObjects.Remove(UIObject);
        }


        // Dibujar cada objeto con respecto a la pantalla.
        public void DrawObject(Graphics g, P2DGameObject go)
        {
            var originalPosition = go.Position;
            go.Position = P2DGame.ObjectToScreen(go);
            go.Draw(g);
            go.Position = originalPosition;
        }

        public void DrawUIObject(Graphics g, P2DUIObject go)
        {
            var originalPosition = go.Position;
            go.Position = P2DGame.ObjectToScreen(go);
            go.Draw(g);
            go.Position = originalPosition;
        }

        // Lo mismo, para las BackgroundLayer.
        public void DrawBackground(Graphics g)
        {
            foreach (var layer in P2DBackgroundManager.layers)
            {
                var originalPosition = layer.Position;
                //layer.Position = P2DGame.LayerToScreen(layer);
                layer.Draw(g);
                layer.Position = originalPosition;
            }
        }

    }
}
