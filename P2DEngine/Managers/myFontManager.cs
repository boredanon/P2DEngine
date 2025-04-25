using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2DEngine.Managers
{
    public class myFontManager
    {
        // IMPORTANTE: RECUERDEN QUE ESTA CARPETA TIENE QUE ESTAR DENTRO DE LA CARPETA bin/Debug/ DE SU PROYECTO.
        static private string FontsPath = "Assets/Fonts/"; // Carpeta para almacenar las fuentes en formato .ttf

        // Estructuras de datos que se usan para almacenar las fuentes.
        static private int FontIndex = 0;
        static private PrivateFontCollection FontCollection = new PrivateFontCollection();


        // Usamos dos diccionarios por si, por ejemplo, queremos cargar el mismo .ttf dos veces, pero usarlo en contextos
        // distintos (ej. myFontManager.Load("fuente.ttf", "UIText"), myFontManager.Load("fuente.ttf", "GameText")
        // no es completamente necesario pero puede servir en algunos casos.
        // Aparte, PrivateFontCollection lo utilizamos en base a índices (vea el método Get)
        static private Dictionary<string, int> FontIndexMap = new Dictionary<string, int>();
        static private Dictionary<string, int> FontFilenameMap = new Dictionary<string, int>();

        // Cargado de fuentes en el programa.
        public static void Load(string fileName, string fontId)
        {
            var filePath = FontsPath + fileName;
            if (File.Exists(filePath)) // Si el archivo existe.
            {
                if (FontIndexMap.ContainsKey(fontId)) // Si ya está cargada una fuente con el ID.
                {
                    throw new Exception(fontId + " ya fue cargado.");
                }
                else
                {
                    if(FontFilenameMap.ContainsKey(fileName)) // Si ya se había cargado la fuente anteriormente.
                    {
                        FontIndexMap.Add(fontId, FontFilenameMap[fileName]);
                    }
                    else // Sino, cargamos la fuente.
                    {
                        FontCollection.AddFontFile(FontsPath + fileName);
                        FontIndexMap.Add(fontId, FontIndex);
                        FontFilenameMap.Add(fileName, FontIndex);
                        FontIndex++;
                    }
                }
                     
            }
            else // Sino, tira una excepción y se cae.
            {
                throw new Exception("Archivo" + fileName + " no existe.");
            }

        }

        // Obtener fuentes cargadas en el sistema.
        public static Font Get(string fontId, int fontSize)
        {
            if (FontIndexMap.ContainsKey(fontId)) // Si está la fuente dentro del programa.
            {
                int idx = FontIndexMap[fontId];
                return new Font(FontCollection.Families[idx], fontSize); // Retornamos la fuente solicitada.
            }
            else // Sino, tira una excepción y se cae.
            {
                throw new Exception(fontId + " no existe.");
            }
        }
    }
}
