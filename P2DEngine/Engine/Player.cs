using P2DEngine.Engine.GameComponents;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2DEngine.Engine
{
    public class Player : P2DBlock
    {
        // Sprite
        public P2DSprite currentSprite;

        public Player(int X, int Y, int width, int height, float angle, P2DSprite sprite) : base(X, Y, width, height, angle, Color.White)
        {
            this.currentSprite = sprite;
        }

        public override void Draw(Graphics g)
        {
            //Vea P2DSprite.cs
            g.DrawImage(currentSprite.GetCurrentFrame(), Position.X, Position.Y, Size.X, Size.Y);
        }

        public override void Update(float DeltaTime)
        {
            //Actualizamos el sprite actual, vea P2DSprite.cs
            currentSprite.Update(DeltaTime);
        }
    }
}
