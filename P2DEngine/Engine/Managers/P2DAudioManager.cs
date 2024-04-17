using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace P2DEngine.Engine
{
    public class P2DAudioManager // Esta es una clase para cargar y hacer escuchar SFX.
    {
        // Recuerde que todos los recursos tienen que ir en la carpeta bin/Debug/ y lo que diga esta variable, es decir, en este caso sería bin/Debug/Assets/Audio/
        static private string path = "Assets/Audio/";

        /* Versión anterior:
         * private static Dictionary<string, SoundPlayer> sounds = new Dictionary<string, SoundPlayer>();
         */

        // Guardaremos en un diccionario los sonidos, a través de un ID asignado, accederemos al sonido correspondiente.
        private static Dictionary<string, MemoryStream> audios = new Dictionary<string, MemoryStream>();

        // Este método es para cargar sonidos dentro del programa
        public static void Load(string filename, string soundId)
        {
            if (File.Exists(path + filename)) // Si existe el archivo.
            {
                if (!audios.ContainsKey(soundId)) // Si no lo hemos cargado anteriormente.
                {
                    /* Versión anterior.
                     * SoundPlayer newSound = new SoundPlayer(path + filename); // Creamos un nuevo archivo y lo guardamos.
                     */

                    MemoryStream memoryStream = new MemoryStream(File.ReadAllBytes(path + filename));
                    
                    //sounds.Add(soundId, newSound); Versión anterior.
                    audios.Add(soundId, memoryStream);
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
            if (audios.ContainsKey(soundId)) // Si se encuentra dentro del diccionario.
            {
                MemoryStream stream = audios[soundId];
                StreamMediaFoundationReader reader = new StreamMediaFoundationReader(stream); // Lo transforma a un formato que el WaveOutEvent puede correr.
                WaveOutEvent sound = new WaveOutEvent();
                sound.Init(reader);
                sound.Play();

                // Versión anterior.
                //audios[soundId].Play(); // Hace sonar el SFX.
            }
            else // En cambio, si no está cargado el sonido que queremos.
            {
                throw new Exception("Resource not found" + soundId);
            }
        }

    }
}
