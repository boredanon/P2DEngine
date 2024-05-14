using P2DEngine.Engine.GameComponents;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2DEngine.Engine.Managers
{
    public class P2DBackgroundManager
    {
        // Lista de capas del fondo.
        public static List<P2DBackgroundLayer> layers = new List<P2DBackgroundLayer>();
        public static float parallaxSpeed;


        // Añadir a la lista de capas.
        public static void AddLayer(string imageID, float speedScale)
        {
            Image image = P2DImageManager.Get(imageID);
            P2DBackgroundLayer layer = new P2DBackgroundLayer(image, speedScale);
            layers.Add(layer);
        }

        public static void SetParallaxSpeed(float newSpeed)
        {
            parallaxSpeed = newSpeed;

            foreach(var layer in layers) { 
                layer.speed = parallaxSpeed * layer.speedScale;
            }
        }
    }
}
