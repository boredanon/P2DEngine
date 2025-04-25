using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P2DEngine.Managers
{
    public class myImageManager
    {
        // IMPORTANTE: RECUERDEN QUE ESTA CARPETA TIENE QUE ESTAR DENTRO DE LA CARPETA bin/Debug/ DE SU PROYECTO.
        // Hay que almacenar en esta carpeta las imagenes.
        static private string ImagesPath = "Assets/Images/"; 
        // Diccionario que guarda las imágenes cargadas en el engine.
        static private Dictionary<string, Image> images = new Dictionary<string, Image>(); 

        // Método para cargar imágenes.
        public static void Load(string fileName, string imageId)
        {
            var filePath = ImagesPath + fileName;
            if (File.Exists(filePath)) // Si el archivo existe.
            {
                if (!images.ContainsKey(imageId)) // Si no hay una imagen con el mismo ID.
                {
                    Image image = Image.FromFile(filePath);
                    images.Add(imageId, image);
                    Console.WriteLine("Added " + imageId + " to the engine.");
                }
                else // Sino, tira una excepción y se cae.
                {
                    throw new Exception("Archivo duplicado.");
                }
            }
            else // Sino, tira una excepción y se cae.
            {
                throw new Exception("Archivo " + fileName + " no existe.");
            }

        }

        // Obtener una imagen en base a una ID.
        public static Image Get(string imageId)
        {
            if (images.ContainsKey(imageId)) // Si es que está cargada dentro del sistema, la retornamos.
            {
                return images[imageId];
            }
            else // Sino, tira una excepción y se cae.
            {
                throw new Exception(imageId + " no existe.");
            }
        }
    }
}
