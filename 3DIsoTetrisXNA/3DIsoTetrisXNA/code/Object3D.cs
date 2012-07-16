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

        protected int NUM_TRIANGLES;
        protected int NUM_VERTICES;

        protected List<Object3D> childs = new List<Object3D>();

        protected Vector3 Size = new Vector3(1.0f, 1.0f, 1.0f);
        public Vector3 getSize()
        {
            return this.Size;
        }
        public void setSize(Vector3 size)
        {
            this.Size = size;
        }

        protected Vector3 Position = new Vector3(0.0f, 0.0f, 0.0f);
        public Vector3 getPosition()
        {
            return this.Position;
        }
        public void setPosition(Vector3 position)
        {
            this.Position = position;
        }

        protected Matrix Rotation = Matrix.Identity;
        public Matrix getRotation()
        {
            return this.Rotation;
        }
        public void setRotation(Matrix rotation)
        {
            this.Rotation = rotation;
        }

        protected bool isConstructed = false;

        protected Matrix rotation = Matrix.Identity;

        // Array of vertex information - contains position, normal and texture data
        protected VertexPositionNormalTexture[] _vertices;

        // The vertex buffer where we load the vertices before drawing the shape
        protected VertexBuffer _shapeBuffer;

        // Lets us check if the data has been constructed or not to improve performance
        
        protected abstract void Construct();

        public abstract void RenderToDevice(GraphicsDevice device);

        public void render(BasicEffect basicEffet ,GraphicsDevice device)
        {
            Matrix backup1 = basicEffet.World;
            Matrix temp = backup1 * Matrix.CreateTranslation(this.Position);
            temp = temp * Matrix.CreateScale(this.Size);
            temp = temp * this.Rotation;
            basicEffet.World = temp;
            Matrix backup2 = basicEffet.World;
            this.RenderToDevice(device);
            foreach (Object3D obj in childs) // Loop through List with foreach
            {
                basicEffet.World = backup2;
                obj.render(basicEffet, device);
            }
        }

        public void Add(Object3D obj)
        {
            this.childs.Add(obj);
        }
    }
}
