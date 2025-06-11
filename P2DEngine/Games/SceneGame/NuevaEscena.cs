using P2DEngine.GameObjects.Collisions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2DEngine.Games.SceneGame
{
    public class NuevaEscena : myScene
    {
        myPhysicsBlock block1;
        myPhysicsBlock block2;


        public NuevaEscena(myCamera camera) : base(camera){}

        public override void Init()
        {
            gameObjects.Clear();

            block1 = new myPhysicsBlock(200, 200, 20, 20, Color.Red);
            block1.body.bodyType = RigidBodyType2D.Dynamic;
            block1.body.affectedByGravity = true;
            block1.body.AddImpulse(9.8f, 0f);

            block2 = new myPhysicsBlock(0, 400, 2000, 20, Color.Blue);
            block2.body.bodyType = RigidBodyType2D.Static;

            

            Instantiate(block1);
            Instantiate(block2);

        }

        public override void ProcessInput()
        {
        }

        public override void Render(Graphics g)
        {
        }

        public override void Update(float deltaTime)
        {
        }
    }
}
