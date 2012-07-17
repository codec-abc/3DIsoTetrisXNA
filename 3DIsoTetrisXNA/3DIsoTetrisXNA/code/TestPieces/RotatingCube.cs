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
        public override void UpdateLogic(float time)
        {
            this.Rotation = Matrix.CreateRotationY(MathHelper.ToRadians(0.4f)) * this.Rotation;
        }
    }
}
