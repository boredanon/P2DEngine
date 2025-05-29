using P2DEngine.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2DEngine.Managers
{
    public class myBackgroundManager
    {
        public static List<myBackgroundLayer> backgroundLayers = new List<myBackgroundLayer>();

        public static void AddLayer(myBackgroundLayer layer)
        {
            backgroundLayers.Add(layer);
        }
    }
}
