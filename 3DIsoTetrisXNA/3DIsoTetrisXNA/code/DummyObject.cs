using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TetrisGame
{
    public class DummyObject : Object3D
    {
        public DummyObject()
        {
            this.Position = Vector3.Zero;
            this.Size = Vector3.One;
            this.Rotation = Matrix.Identity;
        }
        protected override void Construct()
        {
            return;
        }

        public override void RenderToDevice(Microsoft.Xna.Framework.Graphics.GraphicsDevice device)
        {
            return;
        }

        public override void UpdateLogic(float time)
        {
            return;
        }


    }
}
