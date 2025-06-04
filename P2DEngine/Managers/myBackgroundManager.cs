using P2DEngine.GameObjects;
using P2DEngine.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2DEngine.Managers
{
    public class myBackgroundManager
    {
        // Se cambió un poco la clase myBackgroundManager para añadir soporte a fondos en distintas escenas.

        // En vez de myScene, podría ser un string para consistencia, pero es a opción de cada uno.
        // Un dato que probablemente vean en la clase de Algoritmos & Estructura de Datos es que cada clase necesita de un "hash" para poder ser usado por los contenedores (array, vector, dictionary, set, etc.). Cuando las clases implementadas
        // son muy complejas o usan C++, necesitan implementar sus propias funciones de "hash" para mejorar rendimiento. Mi punto con este parrafito es que ud. comprenda que simplemente usar la clase como referencia, como lo hacemos en este caso, no es
        // la mejor opción en cuanto a optimización.
        public static Dictionary<myScene, List<myBackgroundLayer>> backgroundLayers = new Dictionary<myScene, List<myBackgroundLayer>>(); 

        public static void AddLayerToScene(myScene scene, myBackgroundLayer layer) // Añadir una layer de fondo a la escena indicada.
        {
            backgroundLayers[scene].Add(layer);
        }

        public static List<myBackgroundLayer> GetLayers(myScene scene) // Obtener las layers de una escena, para Update y Draw.
        {
            if(backgroundLayers.ContainsKey(scene))
                return backgroundLayers[scene];
            else
                return new List<myBackgroundLayer>();
        }
    }
}
