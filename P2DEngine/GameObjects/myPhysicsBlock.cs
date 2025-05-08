using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using P2DEngine.GameObjects;
using P2DEngine.GameObjects.Collisions;

namespace P2DEngine
{
    public class myPhysicsBlock : myPhysicsGameObject
    {
        public myPhysicsBlock(float x, float y, float sizeX, float sizeY, Color color) : base(x, y, sizeX, sizeY, color)
        {
        }

        public myPhysicsBlock(float x, float y, float sizeX, float sizeY, Image image) : base(x, y, sizeX, sizeY, image)
        {
        }

        // Crear el collider de caja. Esto se llamará automático.
        public override void CreateCollider(float sizeX, float sizeY)
        {
            collider = new BoxCollider2D(sizeX, sizeY, this);
        }

        // Se dibuja igual que un game object.
        public override void Draw(Graphics g, Vector position, Vector size)
        {
            if(image == null)
            {
                g.FillRectangle(brush, (float)position.X, (float)position.Y, (float)size.X, (float)size.Y);
            }
            else
            {
                g.DrawImage(image, (float)position.X, (float)position.Y, (float)size.X, (float)size.Y);
            }
        }


        // Me dio flojera cambiarlo para que el nombre se quedase en Update, pero este se supone que es el Update que conocen
        // de todo el semestre :)
        public override void UpdateGameObject(float deltaTime)
        {
        }
    }
}
