using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TetrisGame
{
    class DummyObject : Object3D
    {
        protected override void Construct()
        {
            return;
        }

        public override void RenderToDevice(Microsoft.Xna.Framework.Graphics.GraphicsDevice device)
        {
            return;
        }
    }
}
