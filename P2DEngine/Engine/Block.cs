using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2DEngine
{
    public class Block // Si hizo cambios a la clase Block durante el LAB2, le recomiendo copiar dichos cambios aquí.
    {
        // Variables del bloque
        public int X;
        public int Y;
        public int width;
        public int height;

        // Esto es para pintar el mouse de cierto color.
        private SolidBrush colorBrush;

        // Esto es para que se dibuje con una imagen en vez de un color.
        private Image image;

        // Tenemos dos constructores: El primero es para dibujar solo con colores.
        public Block(int x, int y, int width, int height, Color color)
        {
            X = x;
            Y = y;
            this.width = width;
            this.height = height;
            this.image = null;
            colorBrush = new SolidBrush(color);
        }

        // Este dibujará la imagen que le asociemos.
        public Block(int x, int y, int width, int height, Image image)
        {
            X = x;
            Y = y;
            this.width = width;
            this.height = height;
            this.image = image;
        }

        // Método para dibujar el bloque.
        public void Draw(Graphics g)
        {
            if (image == null) // Si no tiene una imagen, dibujaremos con FillRectangle (el que hemos usado hasta ahora).
            {
                g.FillRectangle(colorBrush, X, Y, width, height);
            }
            else // En cambio, si le hemos asignado una imagen, dibujará la imagen correspondiente.
            {
                g.DrawImage(image, X, Y, width, height);
            }
        }

        // Por si quieren usar un update para los bloques.
        public void Update()
        {

        }


        // Este es un método que hicimos en clase para poder cambiar la imagen de forma dinámica, haga lo que quiera con él. Bórrelo, cámbielo, etc.
        public void ChangeImage(Image newImage)
        {
            image = newImage;
        }
    }
}
