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
        public string Name = "3DObject";
        public String getName()
        {
            return this.Name;
        }
        public void setName(string name)
        {
            this.Name = name;
        }

        // Array of vertex information - contains position, normal and texture data
        protected VertexPositionNormalTexture[] _vertices;

        // The vertex buffer where we load the vertices before drawing the shape
        protected VertexBuffer _shapeBuffer;


        protected List<Object3D> Childs = new List<Object3D>();
        public List<Object3D> getChilds()
        {
            return this.Childs;
        }
        public void setChilds(List<Object3D> childs)
        {
            this.Childs = childs;
        }

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




        // Lets us check if the data has been constructed or not to improve performance
        
        protected abstract void Construct();

        public abstract void RenderToDevice(GraphicsDevice device, BasicEffect basicEffet);

        public void render(BasicEffect basicEffet ,GraphicsDevice device ,int depth)
        {
            
            Matrix backup = basicEffet.World;
            Vector3 antiOrigin = backup.Translation;
            

            Matrix temp = basicEffet.World;

            Matrix backToOrigin = Matrix.CreateTranslation(-antiOrigin);
            temp = backToOrigin * temp;

            Vector3 center = temp.Translation;

          /*  
            if (TetrisGame.debug)
            {
                
                Console.WriteLine("object to display : " + this.getName());
                Console.WriteLine("object world position : " + center);
                Console.WriteLine("object world position : " + (-antiOrigin));
                Console.WriteLine(" ");
                Console.WriteLine(" ");
                 
            }
          */
            Matrix translateBackToParent = Matrix.CreateTranslation(antiOrigin);
            temp = translateBackToParent * temp;

            Matrix scale = Matrix.CreateScale(this.Size);
            temp = scale * temp;

            Matrix rotate = this.Rotation;
            temp = rotate * temp;


            Matrix translate = Matrix.CreateTranslation(this.Position);
            temp = translate * temp;

            basicEffet.World = temp;
       /*     
            if (TetrisGame.debug)
            {
                Console.WriteLine("object to display : " + this.getName());
                Console.WriteLine("object depth : " + depth);
                Console.WriteLine("object parent relative position : " + this.Position);
                Console.WriteLine("object parent relative size : " + this.Size);
                Console.WriteLine("object parent relative rotation : " + this.Rotation);
                Console.WriteLine("world matrix : " + basicEffet.World);
                Console.WriteLine("childs to display : " + this.Childs.Count);
                Console.WriteLine(" ");
                Console.WriteLine(" ");
            }
            
        */
            
            
         
   
            foreach (Object3D obj in this.Childs) // Loop through List with foreach
            {
                obj.render(basicEffet, device, depth +1);
            }

            foreach (EffectPass pass in basicEffet.CurrentTechnique.Passes)
            {
                pass.Apply();
                this.RenderToDevice(device, basicEffet);
            }
            basicEffet.World = backup;
            
        }

        public void Add(Object3D obj)
        {
            this.Childs.Add(obj);
        }

        public abstract void UpdateLogic(float time);

        public void UpdateLogicGame(float time)
        {
            foreach (Object3D obj in Childs) // Loop through List with foreach
            {
                obj.UpdateLogicGame(time);
                obj.UpdateLogic(time);
            }
        }
    }
}
