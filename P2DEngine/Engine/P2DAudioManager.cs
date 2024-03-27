using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace P2DEngine.Engine
{
    public class P2DAudioManager // Esta es una clase para cargar y hacer escuchar SFX.
    {
        // Recuerde que todos los recursos tienen que ir en la carpeta bin/Debug/ y lo que diga esta variable, es decir, en este caso sería bin/Debug/Assets/Audio/
        static private string path = "Assets/Audio/"; 
        // Guardaremos en un diccionario los sonidos, a través de un ID asignado, accederemos al sonido correspondiente.
        private static Dictionary<string, SoundPlayer> sounds = new Dictionary<string, SoundPlayer>();

        // Este método es para cargar sonidos dentro del programa
        public static void Load(string filename, string soundId)
        {
            if (File.Exists(path + filename)) // Si existe el archivo.
            {
                if (!sounds.ContainsKey(soundId)) // Si no lo hemos cargado anteriormente.
                {
                    SoundPlayer newSound = new SoundPlayer(path + filename); // Creamos un nuevo archivo y lo guardamos.
                    sounds.Add(soundId, newSound);
                }
                else // En cambio, si lo hemos cargado anteriormente.
                {
                    throw new Exception("Resource already added: " + filename);
                }
            }
            else // En cambio, si no existe el archivo.
            {
                throw new Exception("File not found " + filename);
            }
        }

        public static void Play(string soundId) // Este es para hacer sonar los SFXs cargados anteriormente.
        {
            if (sounds.ContainsKey(soundId)) // Si se encuentra dentro del diccionario.
            {
                sounds[soundId].Play(); // Hace sonar el SFX.
            }
            else // En cambio, si no está.
            {
                throw new Exception("Resource not found" + soundId);
            }
        }

    }
}
