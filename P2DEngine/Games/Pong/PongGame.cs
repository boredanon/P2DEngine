using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Forms;

namespace P2DEngine
{
    // En la clase "PongGame" irá toda la lógica ESPECÍFICA del Pong.
    public class PongGame : P2DGame
    {

        // Elementos necesarios para jugar Pong.
        private Block ball { get; set; }
        private Block player { get; set; }
        private Block opponent { get; set; }

        // Velocidad del pad del computador.
        private int IADy { get; set; } 

        // Velocidad del círculo tanto en X, como en Y.
        private int ballDx { get; set; }
        private int ballDy {  get; set; }

        // Lista de objetos agrupados.
        private List<Block> ObjectList { get; set; }


        // Es recomentable utilizar el constructor para inicializar las variables.
        public PongGame(int width, int height, int targetFPS) : base(width, height, targetFPS) {
            ObjectList = new List<Block>();
            ball = new Block(width/2, height/2, 20, 20, Color.Black);

            // La velocidad inicial.
            ballDx = 1;
            ballDy = 1;

            player = new Block(20, 0, 20, 100, Color.Black);
            opponent = new Block(width - 40, height / 2, 20, 100, Color.Black);

            // Añadimos a la lista 5 bloques para probar.
            ObjectList.Add(new Block(100, 100, 50, 50, Color.Black));
            ObjectList.Add(new Block(300, 300, 50, 50, Color.Black));
            ObjectList.Add(new Block(400, 400, 50, 50, Color.Black));
            ObjectList.Add(new Block(600, 500, 50, 50, Color.Black));
            ObjectList.Add(new Block(250, 250, 50, 50, Color.Black));

            IADy = 1;
        }

        // Para reiniciar el juego.
        public void Reset()
        {
            // Ponemos la pelota nuevamente en el centro.
            ball.X = windowWidth / 2;
            ball.Y = windowHeight / 2;

            // Reiniciamos las velocidades de la pelota.
            ballDx = 1;
            ballDy = 1;

            // Reiniciamos las posiciones del player y del oponente.
            player.X = 20;
            opponent.X = windowWidth - 40;
            opponent.Y = windowHeight / 2;

            IADy = 1;
        }

        protected override void RenderGame(Graphics g) // Lógica de dibujado.
        {
            // Pintamos el fondo.
            g.FillRectangle(new SolidBrush(Color.White), 0, 0, windowWidth, windowHeight);

            // Podemos dibujar objetos por separado...
            player.Draw(g);
            opponent.Draw(g);
            ball.Draw(g);


            // ... O todos los elementos que se encuentren en la lista definida anteriormente.
            foreach (Block block in ObjectList)
            {
                block.Draw(g);
            }


            // Cómo dibujar strings, se declara un objeto "Font" de la siguiente forma.
            Font font = new Font("Arial", 12);
            g.DrawString("Hola", font, new SolidBrush(Color.Black), 20, 20);

        }

        protected override void UpdateGame() // Lógica de juego.
        {
            // Cuantas unidades queremos que se mueva.
            int step = 10;
            // Movemos la pelota.
            ball.X += ballDx * step;
            ball.Y += ballDy * step;

            // Queremos que rebote en los bordes.
            if(ball.X < 0)
            {
                ballDx = 1;
            }else if(ball.X > windowWidth)
            {
                ballDx = -1;
            }
            if(ball.Y < 0)
            {
                ballDy = 1;
            }
            else if(ball.Y > windowHeight)
            {
                ballDy = -1;
            }


            /* Si nosotros definimos cualquier cosa en nuestro engine en la posición "X,Y".
             * (X, Y): Es la esquina superior izquierda.
             * (X + ancho, Y): Es la esquina superior derecha.
             * (X, Y + largo): Es la esquina inferior izquierda.
             * (X + ancho, Y + largo): Es la esquina inferior derecha.
             *  Vea la imagen acoplada en el repositorio si tiene dudas.
             */

            // Si choca con el Player, cambiamos la velocidad para que vaya hacia la derecha.
            if (ball.X < player.X + player.width && (ball.Y > player.Y && ball.Y < player.Y + player.height))
            {
                ballDx = 1;
            }


            // Esta es la lógica para eliminar elementos del juego.
            List<Block> toRemove = new List<Block>(); // Lista de elementos que vamos a eliminar.
            foreach (Block block in ObjectList) // En este caso, revisamos todos los bloques.
            {
                if(ball.X > block.X && ball.X < block.X + block.width) // Si colisionan con la pelota.
                {
                    if(ball.Y > block.Y && ball.Y < block.Y + block.height)
                    {
                        toRemove.Add(block); // Los preparamos para que sean eliminados.
                    }
                }

            }

            // Luego se recorre la lista de los elementos preparados a ser eliminados.
            foreach (Block block in toRemove)
            {
                ObjectList.Remove(block); // Los eliminamos de ObjectList.
            }


            // Lógica del oponente del pong.
            opponent.Y += step * IADy;


            // Para evitar que el oponente pase de la pantalla, le limitamos donde se puede mover
            if(opponent.Y + opponent.height > windowHeight) // Si se pasa de la pantalla, hacemos que se mueva hacia arriba.
            {
                IADy = -1;
            }
            else if(opponent.Y < 0) // Equivalente, si se pasa de la pantalla por arriba, haremos que empiece a ir hacia abajo.
            {
                IADy = 1;
            }

            // Se reinicia la pantalla si es que la pelota pasa al oponente.
            if(ball.X > opponent.X + opponent.width)
            {
                //Reset();
            }



        }

        protected override void ProcessInput() // Procesamos la entrada que realiza el usuario.
        {
            int step = 10;
            if (P2DInputManager.IsKeyPressed(Keys.Up))
            {
                player.Y -= step;
            }
            if (P2DInputManager.IsKeyPressed(Keys.Down))
            {
                player.Y += step;
            }

            if(P2DInputManager.IsKeyPressed(Keys.Space)) // Si presionamos el espacio, haremos que se dibuje un nuevo bloque.
            {
                ObjectList.Add(new Block(0, 0, 50, 50, Color.Black));
            }
        }
    }
}
