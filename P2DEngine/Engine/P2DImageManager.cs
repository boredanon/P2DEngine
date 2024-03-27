using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2DEngine.Engine
{
    public class P2DImageManager // Clase para el manejo y cargado de imágenes.
    {
        // Recuerde que todos los recursos tienen que ir en la carpeta bin/Debug/ y lo que diga esta variable, es decir, en este caso sería bin/Debug/Assets/Images/
        static private string path = "Assets/Images/";
        // Guardaremos en un diccionario las imágenes, a través de un ID asignado, accederemos a la imagen correspondiente.
        private static Dictionary<string, Image> images = new Dictionary<string, Image>();

        // Este método es para cargar imágenes dentro del programa
        public static void Load(string filename, string imageId)
        {
            if(File.Exists(path + filename)) // Si existe el archivo.
            {
                if(!images.ContainsKey(imageId)) // Si no lo hemos cargado anteriormente.
                {
                    Image newImage = Image.FromFile(path + filename); // Guardaremos la imagen dentro de nuestro diccionario.
                    images.Add(imageId, newImage);
                }
                else // En cambio, si hemos cargado anteriormente el archivo.
                {
                    throw new Exception("Resource already added: " + filename);
                }
            }
            else // En cambio, si no existe el archivo.
            {
                throw new Exception("File not found " + filename);
            }
        }

        // Obtener la imagen a través de la ID asignada.
        public static Image Get(string imageId)
        {
            if(images.ContainsKey(imageId)) // Si contiene la imagen.
            {
                return images[imageId]; // Retornamos la imagen.
            }
            else // Si no hemos cargado la imagen pedida.
            {
                throw new Exception("Resource not found" + imageId);
            }
        }
    
    }
}
