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
    class LinePiece : DummyObject
    {
        Cube cube1;
        Cube cube2;
        Cube cube3;
        Cube cube4;

        public LinePiece(Vector3 size, Vector3 position)
        {

            Vector3 pos;
            pos = new Vector3((float)(position.X - 3.6f * size.X), position.Y + 0.0f, position.Z + 0.0f);
            cube1 = new Cube(size, pos);

            pos = new Vector3((float)(position.X - 1.2f * size.X), position.Y + 0.0f, position.Z + 0.0f);
            cube2 = new Cube(size, pos);

            pos = new Vector3((float)(position.X + 1.2f * size.X), position.Y + 0.0f, position.Z + 0.0f);
            cube3 = new Cube(size, pos);

            pos = new Vector3((float)(position.X + 3.6f * size.X), position.Y + 0.0f, position.Z + 0.0f);
            cube4 = new Cube(size, pos);

            cube1.setName("CubeLine1");
            cube2.setName("CubeLine2");
            cube3.setName("CubeLine3");
            cube4.setName("CubeLine4");

            this.Add(cube1);
            this.Add(cube2);
            this.Add(cube3);
            this.Add(cube4);

            Size = size;
            Position = position;
            isConstructed = true;
        }
        protected override void Construct()
        {

        }

        public override void RenderToDevice(GraphicsDevice device, BasicEffect basicEffet)
        {

        }

        public override void UpdateLogic(float time)
        {

        }

    }
}
