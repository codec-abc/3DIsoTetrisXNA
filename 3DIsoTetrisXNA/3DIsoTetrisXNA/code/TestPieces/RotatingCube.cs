using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TetrisGame;

namespace TetrisGame
{
    class RotatingCube : Cube
    {
        public RotatingCube(Vector3 size, Vector3 position) :base(size, position)
        {
            
        }
        public override void UpdateLogic()
        {
           // this.Rotation = Matrix.CreateRotationY(MathHelper.ToRadians(40)) * this.Rotation;
            this.Position = this.Position + new Vector3(1, 0, 1);
        }
    }
}
