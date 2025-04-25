using NAudio.Wave;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2DEngine.Managers
{
    public class myAudioManager
    {
        // IMPORTANTE: RECUERDEN QUE ESTA CARPETA TIENE QUE ESTAR DENTRO DE LA CARPETA bin/Debug/ DE SU PROYECTO.
        static private string AudioPath = "Assets/Audio/"; // Carpeta para almacenar archivos de audio.

        // Leemos los audios como un flujo de bits.
        static private Dictionary<string, MemoryStream> AudioMap = new Dictionary<string, MemoryStream>();

        // Audios que están sonando.
        static private int PlayingIndex = 0;
        static private Dictionary<int, (WaveOutEvent player, StreamMediaFoundationReader reader)> PlayingAudioMap 
            = new Dictionary<int, (WaveOutEvent player, StreamMediaFoundationReader reader)>();
        
        // Cargar audios dentro del sistema.
        // Les recomiendo en formato .mp3 o .wav. La librería soporta alguna otra extensión de audio, pero no todas.
        public static void Load(string fileName, string audioId)
        {
            var filePath = AudioPath + fileName;
            if (File.Exists(filePath)) // Si existe el archivo.
            {
                if(!AudioMap.ContainsKey(audioId)) // No ha sido cargado un archivo con la misma ID.
                {
                    MemoryStream stream = new MemoryStream(File.ReadAllBytes(AudioPath + fileName));
                    AudioMap.Add(audioId, stream);
                }
                else // Sino, tira una excepción y se cae.
                {
                    throw new Exception("Repeated audio file: " + audioId);
                }
            }
            else // Sino, tira una excepción y se cae.
            {
                throw new Exception("Archivo" + fileName + " no existe.");
            }

        }

        // Hacer sonar un audio en base a una id.
        public static int Play(string audioId, float volume = 1.0f)
        {
            if (AudioMap.ContainsKey(audioId)) // Si existe el audio correspondiente.
            {

                // Creamos el objeto para que suene.
                MemoryStream stream = new MemoryStream(AudioMap[audioId].ToArray());
                
                var reader = new StreamMediaFoundationReader(stream);
                var player = new WaveOutEvent();

                var volumeProvider = new VolumeWaveProvider16(reader)
                {
                    Volume = volume
                };

                player.Init(volumeProvider);
                player.Play();

                int index;
                index = PlayingIndex++;
                PlayingAudioMap.Add(index, (player, reader));

                return index; // Retornamos el índice, por si queremos pausarlo o detenerlo.
            }
            else // Sino, tira una excepción y se cae.
            {
                throw new Exception(audioId + " no existe.");
            }
        }

        // Versión asíncrona según lo conversado en clases. No es necesario que la usen, pero hace lo mismo que Play,
        // solo que en otro hilo.
        public static async Task<int> PlayAsync(string audioId, float volume = 1.0f)
        {
            if (AudioMap.ContainsKey(audioId))
            {
                MemoryStream stream = new MemoryStream(AudioMap[audioId].ToArray());
                return await Task.Run(() =>
                {
                    var reader = new StreamMediaFoundationReader(stream);
                    var volumeProvider = new VolumeWaveProvider16(reader);

                    var player = new WaveOutEvent();

                    player.Init(volumeProvider);
                    player.Play();

                    int index;
                    lock (PlayingAudioMap)
                    {
                        index = PlayingIndex++;
                        PlayingAudioMap.Add(index, (player, reader));
                    }

                    return index;

                });
            }
            throw new Exception(audioId + " no encontrado.");
        }

        // Detener un audio de acuerdo a un índice numérico.
        public static void Stop(int idx)
        {
            if (PlayingAudioMap.TryGetValue(idx, out var audioData))
            {
                audioData.player.Stop();

                audioData.reader.Dispose();
                audioData.player.Dispose();

                lock (PlayingAudioMap)
                {
                    PlayingAudioMap.Remove(idx);
                }
            }
        }

        // Pausar y despausar.
        public static void Pause(int idx)
        {
            if (PlayingAudioMap.TryGetValue(idx, out var audioData))
            {
                audioData.player.Pause();
            }
        }
        
        public static void Resume(int idx)
        {
            if(PlayingAudioMap.TryGetValue(idx, out var audioData))
            {
                audioData.player.Play();
            }
        }
    }
}
