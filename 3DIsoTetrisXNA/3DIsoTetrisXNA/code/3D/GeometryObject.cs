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
    public class GeometryObject : Object3D
    {

        protected Color color;

        public Color getColor()
        {
            return color;
        }

        public void setColor(Color value)
        {
            this.color = value;
        }

        protected int NUM_TRIANGLES;
        protected int NUM_VERTICES;

        protected override void Construct()
        {
            return;
        }

        public override void RenderToDevice(GraphicsDevice device, BasicEffect basicEffet)
        {
            return;
        }

        public override void UpdateLogic(float time)
        {
            return;
        }

        public override void updateEffect(BasicEffect basicEffet)
        {
        //    Console.WriteLine(" ");
        //    Console.WriteLine("on affiche un cube de type" + this.Name);
        //    Console.WriteLine("la couleur est " + this.color);
        //    Console.WriteLine(" ");
            Vector3 color = new Vector3(this.color.R / 255.0f, this.color.G / 255.0f, this.color.B / 255.0f);
            basicEffet.DiffuseColor = color;
        }
    }
}
