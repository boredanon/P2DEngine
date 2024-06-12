using P2DEngine.Engine.GameComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2DEngine.Engine.Managers
{
    // Para el manejo de escenas.
    public class P2DSceneManager
    {
        // Aquí se almacenarán todas las escenas del juego.
        private static Dictionary<string, P2DScene> scenes = new Dictionary<string, P2DScene>();

        // Identificador de la escena activa.
        private static string ActiveIndex = null;

        // Almacenar una escena con el identificador "index".
        public static void Register(P2DScene scene, string index)
        {
            if(scenes.ContainsKey(index))
            {
                throw new Exception("Scene already in dictionary");
            }
            else
            {
                scenes.Add(index, scene);
            }
        }

        // Obtener una escena mediante su "index".
        public static P2DScene Get(string index) 
        {
            return scenes[index];
        }

        // Obtener la escena activa.
        public static P2DScene GetActive()
        {
            if(ActiveIndex == null)
            {
                throw new Exception("No scene is active.");
            }
            return scenes[ActiveIndex];
        }

        // Asignar una escena activa.
        public static void SetActive(string index)
        {
            if(!scenes.ContainsKey(index))
            {
                throw new Exception("No scene exists named " + index);
            }
            ActiveIndex = index;
        }

        // Obtener todas las escenas.
        public static Dictionary<string, P2DScene> GetScenes()
        {
            return scenes;
        }

        // Reiniciar una escena en particular.
        public static void ResetScene(string index)
        {
            Get(index).Reset();
        }
    }
}
