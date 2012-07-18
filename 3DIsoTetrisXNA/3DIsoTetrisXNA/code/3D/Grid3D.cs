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
    public class Grid3D : Object3D
    {
        VertexPositionColor[] pointList;
        short[] lineListIndices;
        int points = 21*2 + 11 * 2;

        public Grid3D()
        {
            this.Position = Vector3.Zero;
            this.Size = Vector3.One;
            this.Rotation = Matrix.Identity;
            this.isConstructed = false;
            pointList = new VertexPositionColor[this.points];
            for (int i = 0; i < points; i++)
            {
                pointList[i] = new VertexPositionColor(new Vector3(0, 0, 0), Color.White);
            }
        }

        protected override void Construct()
        {
            
            
                float zoffset = 0.75f;
                float yoffset = -0.5f;
                float xoffset = 0;
                int maxValue = 0;
                for (int j = -1; j < 20; j++)
                {
                    int i = j + 1;
                    VertexPositionColor vertex1 = new VertexPositionColor(new Vector3(-0.75f, yoffset, (1 + GameLogic.offset) * j + zoffset), Color.White);
                    VertexPositionColor vertex2 = new VertexPositionColor(new Vector3(-0.75f+(1 + GameLogic.offset) * 10, yoffset, (1 + GameLogic.offset) * j + zoffset), Color.White);

                    //VertexPositionColor vertex3 = new VertexPositionColor(new Vector3(3 * 10, yoffset, 3.0f * j + zoffset), Color.White);
                    //VertexPositionColor vertex4 = new VertexPositionColor(new Vector3(3 * 10, -yoffset, 3.0f * i + zoffset), Color.White);

                    //VertexPositionColor vertex5 = new VertexPositionColor(new Vector3(3 * 10, -yoffset, 3.0f * j + zoffset), Color.White);
                    //VertexPositionColor vertex6 = new VertexPositionColor(new Vector3(-3, -yoffset, 3.0f * j + zoffset), Color.White);

                    //VertexPositionColor vertex7 = new VertexPositionColor(new Vector3(-3, -yoffset, 3.0f * j + zoffset), Color.White);
                    //VertexPositionColor vertex8 = new VertexPositionColor(new Vector3(-3, yoffset, 3.0f * j + zoffset), Color.White);
                    maxValue = i * 2;
                    pointList[i * 2 + 0] = vertex1;
                    pointList[i * 2 + 1] = vertex2;
                    //pointList[i * 8 + 2] = vertex3;
                    //pointList[i * 8 + 3] = vertex4;
                    //pointList[i * 8 + 4] = vertex5;
                    //pointList[i * 8 + 5] = vertex6;
                    //pointList[i * 8 + 6] = vertex7;
                    //pointList[i * 8 + 7] = vertex8;
                }



                xoffset = 0.75f;
                zoffset = 0.75f;
                yoffset = -0.5f;
                for (int j = -1; j < 10; j++)
                {
                    int i = j + 1;

                    VertexPositionColor vertex1 = new VertexPositionColor(new Vector3((1 + GameLogic.offset) * j + 0.75f, yoffset, -(1 + GameLogic.offset) + zoffset), Color.White);
                    VertexPositionColor vertex2 = new VertexPositionColor(new Vector3((1 + GameLogic.offset) * j + 0.75f, yoffset, (1 + GameLogic.offset) * 19 + zoffset), Color.White);

                 //   VertexPositionColor vertex1 = new VertexPositionColor(new Vector3(-1 + i * (1 + GameLogic.offset) + xoffset, yoffset, -(1 ) + zoffset), Color.White);
                 //   VertexPositionColor vertex2 = new VertexPositionColor(new Vector3(-1 + i * (1 + GameLogic.offset) + xoffset, yoffset, (1 +GameLogic.offset ) * 19 + zoffset), Color.White);

                    //VertexPositionColor vertex3 = new VertexPositionColor(new Vector3(3.0f * j + xoffset, yoffset, 3 * 20), Color.White);
                    //VertexPositionColor vertex4 = new VertexPositionColor(new Vector3(3.0f * j + xoffset, -yoffset, 3 * 20f), Color.White);

                    //VertexPositionColor vertex5 = new VertexPositionColor(new Vector3(3.0f * j + xoffset, -yoffset, 3 * 20f), Color.White);
                    //VertexPositionColor vertex6 = new VertexPositionColor(new Vector3(3.0f * j + xoffset, -yoffset, 0f), Color.White);

                    //VertexPositionColor vertex7 = new VertexPositionColor(new Vector3(3.0f * j + xoffset, -yoffset, 0f), Color.White);
                    //VertexPositionColor vertex8 = new VertexPositionColor(new Vector3(3.0f * j + xoffset, yoffset, 0f), Color.White);
                    int currentvalue = 21 * 2 + i * 2;
                    pointList[21 * 2 + i * 2 + 0] = vertex1;
                    pointList[21 * 2 + i * 2 + 1] = vertex2;
                    //pointList[20 * 8 + i * 8 + 2] = vertex3;
                    //pointList[20 * 8 + i * 8 + 3] = vertex4;
                    //pointList[20 * 8 + i * 8 + 4] = vertex5;
                    //pointList[20 * 8 + i * 8 + 5] = vertex6;
                    //pointList[20 * 8 + i * 8 + 6] = vertex7;
                    //pointList[20 * 8 + i * 8 + 7] = vertex8;
                
            }
            
            lineListIndices = new short[points];
            for (int i = 0; i < points; i++)
            {
                lineListIndices[i] = (short)i;
            }

            isConstructed = true;
        }

        public override void RenderToDevice(GraphicsDevice device, BasicEffect basicEffet)
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
                    device.DrawUserIndexedPrimitives<VertexPositionColor>(PrimitiveType.LineList, pointList, 0, points, lineListIndices, 0, points/2);
                }
            }
        }

        public override void UpdateLogic(float time)
        {
            return;
        }

        public override void updateEffect(BasicEffect basicEffet)
        {
            Vector3 color = new Vector3(1,1,1);
            basicEffet.DiffuseColor = color;
        }

    }
}
