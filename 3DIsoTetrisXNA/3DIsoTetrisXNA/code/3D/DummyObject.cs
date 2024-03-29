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
    public class DummyObject : Object3D
    {
        
        public bool Visible;
        public bool isVisible() { return Visible; }
        public void setVisible(bool value) { Visible = value; }
        

        VertexPositionColor[] pointList;
        short[] lineListIndices;
        int points = 6;

        public DummyObject()
        {
            this.Position = Vector3.Zero;
            this.Size = Vector3.One;
            this.Rotation = Matrix.Identity;
            this.isConstructed = false;
            this.Visible = false;
        }

        public override void updateEffect(BasicEffect basicEffet)
        {
          //  if (!basicEffet.LightingEnabled)
         //   {
                basicEffet.VertexColorEnabled = true;
         //   }
        }

        protected override void Construct()
        {
            pointList = new VertexPositionColor[this.points];

            VertexPositionColor vertex1 = new VertexPositionColor(new Vector3(0, 0, 0), Color.Red);
            VertexPositionColor vertex2 = new VertexPositionColor(new Vector3(3, 0, 0), Color.Red);

            VertexPositionColor vertex3 = new VertexPositionColor(new Vector3(0, 0, 0), Color.Blue);
            VertexPositionColor vertex4 = new VertexPositionColor(new Vector3(0, 0, 3), Color.Blue);

            VertexPositionColor vertex5 = new VertexPositionColor(new Vector3(0, 0, 0), Color.Green);
            VertexPositionColor vertex6 = new VertexPositionColor(new Vector3(0, 3, 0), Color.Green);

            pointList[0] = vertex1;
            pointList[1] = vertex2;

            pointList[2] = vertex3;
            pointList[3] = vertex4;

            pointList[4] = vertex5;
            pointList[5] = vertex6;

            // Initialize an array of indices of type short.

            // Populate the array with references to indices in the vertex buffer
            lineListIndices = new short[points];
            for (int i = 0; i < points; i++)
            {
                lineListIndices[i] = (short)i;
            }

            isConstructed = true;
        }


        public override void RenderToDevice(GraphicsDevice device, BasicEffect basicEffet)
        {

            if (Visible)
            {
                if (!basicEffet.LightingEnabled)
                {
                    if (isConstructed == false)
                        Construct();

                    using
                        (
                        VertexBuffer buffer = new VertexBuffer(
                        device,
                        VertexPositionColor.VertexDeclaration,
                        points,
                        BufferUsage.WriteOnly)
                      )
                    {
                        // Load the buffer
                        buffer.SetData(pointList);

                        // Send the vertex buffer to the device
                        device.SetVertexBuffer(buffer);
                        device.DrawUserIndexedPrimitives<VertexPositionColor>(PrimitiveType.LineList, pointList, 0, 6, lineListIndices, 0, 3);
                    }
                }
            }
        }

        public override void UpdateLogic(float time)
        {
            return;
        }
    }
}
