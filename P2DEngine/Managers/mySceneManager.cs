using P2DEngine.GameObjects;
using P2DEngine.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2DEngine.Managers
{
    // Clase utilizada para el manejo de escenas dentro de un "myGame.cs"
    // Nota: Dentro de este módulo y en contexto Unity utilizamos el concepto de "Escena". Godot, por ejemplo, maneja todo en base a "Nodos", pero tienen una funcionalidad similar. Unreal si mal no recuerdo usa el concepto "Levels".

    public static class mySceneManager
    {
        // Diccionario de escenas, al igual que con los assets.
        public static Dictionary<string, myScene> scenes = new Dictionary<string, myScene>();
        private static string ActiveIndex = null; // Índice de la escena activa.

        // Método para registrar escenas dentro del juego.
        public static void Register(myScene scene, string sceneId)
        {
            if (scenes.ContainsKey(sceneId))
            {
                throw new Exception(sceneId + " ya existe dentro del juego."); // No metamos escenas duplicadas.
            }
            else
            {
                scenes.Add(sceneId, scene); // Añadir la escena correctamente al diccionario.
            }
        }

        public static string GetActiveIndex() // Obtener el nombre de la escena actual.
        {
            return ActiveIndex;
        }

        public static myScene GetScene(string sceneId) // Obtener una escena en específico.
        {
            if (scenes.ContainsKey(sceneId))
            {
                return scenes[sceneId];
            }
            else
                throw new Exception(sceneId + " no existe.");

        }

        public static myScene GetActiveScene() // Obtener la referencia a la escena actual.
        {
            if (ActiveIndex != null)
            {
                return scenes[ActiveIndex];
            }
            throw new Exception("No existe una escena activa.");
                }

        public static void SetActive(string sceneId, bool reset = false) // Cambiar la escena actual.
        {
            ActiveIndex = sceneId; 
            if(reset) // Si queremos que cuando cargue, se reinicie.
            {
                scenes[ActiveIndex].Init();
            }
        }
    }

}
