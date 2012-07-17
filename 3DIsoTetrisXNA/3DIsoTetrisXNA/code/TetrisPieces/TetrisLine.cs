﻿using System;
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
    class TetrisLine : GeometryObject
    {
        Cube cube1;
        Cube cube2;
        Cube cube3;
        Cube cube4;

        public TetrisLine(Vector3 size, Vector3 position)
        {

            Vector3 pos;
            pos = new Vector3((float)(position.X - 3.0f * size.X), position.Y + 0.0f, position.Z + 0.0f);
            cube1 = new Cube(size, pos);

            pos = new Vector3((float)(position.X - 1.0f * size.X), position.Y + 0.0f, position.Z + 0.0f);
            cube2 = new Cube(size, pos);

            pos = new Vector3((float)(position.X + 1.0f * size.X), position.Y + 0.0f, position.Z + 0.0f);
            cube3 = new Cube(size, pos);

            pos = new Vector3((float)(position.X + 3.0f * size.X), position.Y + 0.0f, position.Z + 0.0f);
            cube4 = new Cube(size, pos);

    //        Size = size;
    //        Position = position;
            isConstructed = false;
        }
        protected override void Construct()
        {

        }

        public override void RenderToDevice(GraphicsDevice device)
        {

            cube1.RenderToDevice(device);
            cube2.RenderToDevice(device);
            cube3.RenderToDevice(device);
            cube4.RenderToDevice(device);
        }

        public override void UpdateLogic(float time)
        {
            this.Rotation = Matrix.CreateRotationY(MathHelper.ToRadians(0.4f)) * this.Rotation;
        }

    }
}
