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
    public abstract class Object3D
    {

        protected const int NUM_TRIANGLES = 12;
        protected const int NUM_VERTICES = 36;

        public Vector3 Size { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Texture { get; set; }

        // Array of vertex information - contains position, normal and texture data
        protected VertexPositionNormalTexture[] _vertices;

        // The vertex buffer where we load the vertices before drawing the shape
        protected VertexBuffer _shapeBuffer;

        // Lets us check if the data has been constructed or not to improve performance
        protected bool _isConstructed = false;

        protected abstract void Construct();

        public abstract void RenderToDevice(GraphicsDevice device);
    }
}
