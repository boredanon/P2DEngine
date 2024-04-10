using P2DEngine.Engine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;

namespace P2DEngine.Games.Arkanoid
{
    public class MouseControl : P2DGame
    {
        // Evaluar los botones del mouse.
        bool mouseLeft; 
        bool mouseRight;

        // Evaluar los botones del mouse *EN EL FRAME ANTERIOR*
        bool previousMouseLeft;
        bool previousMouseRight;
        
        // Posición del mouse.
        int mouseX;
        int mouseY;

        // Lista de bloques.
        List<Block> blocks;

        // Esta fue a pedido de un alumno para entender como hacer algo parecido a una pausa, no es necesario que la use.
        bool play;

        Block block_Image;
  
        // Fondo.
        Image bg;

        public MouseControl(int width, int height, int targetFPS) : base(width, height, targetFPS)
        {
            play = false;
            blocks = new List<Block>();

            // Podemos crear bloques con imágenes: No es necesario que el bloque tenga la misma resolución que la imagen.
            block_Image = new Block(width/2, height/2, 38, 39, P2DImageManager.Get("Green"));
            
            // O también podemos crearlas con colores como lo veníamos haciendo hasta ahora.
            blocks.Add(new Block(0, 0, 20, 20, Color.Red));
            blocks.Add(new Block(0, 100, 20, 20, Color.Blue));


            bg = P2DImageManager.Get("Background"); // Obtenemos la imagen anteriormente guardada

            P2DAudioManager.Play("background_music");
        }

        protected override void ProcessInput()
        {
            // Obtenemos los datos correspondientes al mouse a través de P2DMouseManager.
            mouseLeft = P2DMouseManager.isLeftButtonDown; 
            mouseRight = P2DMouseManager.isRightButtonDown;

            mouseX = P2DMouseManager.mouseLocation.X;
            mouseY = P2DMouseManager.mouseLocation.Y;

        }

        protected override void RenderGame(Graphics g)
        {
            // Dibujamos el fondo.
            g.DrawImage(bg, 0, 0, mainWindow.Width, mainWindow.Height);

            //g.FillRectangle(new SolidBrush(Color.White), 0, 0, mainWindow.ClientSize.Width, mainWindow.ClientSize.Height);

            foreach (Block block in blocks) // Dibujamos cada bloque en la lista de bloques, ud. ya conoce esto.
            {
                block.Draw(g);
            }
            block_Image.Draw(g); // Dibujamos el bloque con la imagen también.
        }

        protected override void UpdateGame()
        {
            if (mouseLeft && !previousMouseLeft) // Si presionamos el botón izquierdo *Y NO LO TENÍAMOS PRESIONADO EN EL FRAME ANTERIOR*
            {
                P2DAudioManager.Play("bounce"); // Tocamos el sonido.
                blocks.Add(new Block(mouseX, mouseY, 50, 50, Color.Black)); // Creamos un bloque en la posición del click.
            }


            List<Block> toRemove = new List<Block>(); // Lista de objetos a eliminar.
            if (mouseRight && !previousMouseRight) // Si presionamos el botón derecho *Y NO LO TENÍAMOS PRESIONADO EN EL FRAME ANTERIOR*
            {
                foreach (Block block in blocks) // Para cada bloque de la lista
                {
                    if (mouseX > block.X && mouseX < block.X + block.width) // Si se hizo click DENTRO DE LOS LÍMITES DEL BLOQUE.
                    {
                        if (mouseY > block.Y && mouseY < block.Y + block.height)
                        {
                            toRemove.Add(block); // Lo preparamos para ser eliminado.
                        }
                    }
                }

                // Si el click derecho es en cambio al bloque que tiene una imagen, cambiaremos la imagen.
                if(mouseX > block_Image.X && mouseX < block_Image.X + block_Image.width) 
                {
                    if(mouseY > block_Image.Y && mouseY < block_Image.Y + block_Image.height)
                    {
                        block_Image.ChangeImage(P2DImageManager.Get("Blue"));
                    }
                }
            }

            foreach (var block in toRemove) // Eliminar los bloques que fueron preparados para ser eliminados.
            {
                blocks.Remove(block);
            }

            foreach (Block block in blocks) // Podemos llamar un Update a cada bloque de la lista también si lo ven necesario.
            {
                block.Update();
            }


            if(P2DInputManager.IsKeyPressed(Keys.Space))
            {
                play = true;
            }
            if (play) // Hacemos que el bloque con imagen siga al mouse.
            {
                block_Image.X = mouseX - (block_Image.width / 2);
                block_Image.Y = mouseY;
            }

            // Al final, actualizamos los valores de estos booleanos para que en la frame siguiente podamos consultar el estado del botón correspondiente en el frame anterior.
            previousMouseLeft = mouseLeft;
            previousMouseRight = mouseRight;
        }
    }
}
