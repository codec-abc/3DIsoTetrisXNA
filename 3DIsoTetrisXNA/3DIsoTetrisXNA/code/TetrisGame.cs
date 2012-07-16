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
using System.Collections;
using TetrisGame;


namespace TetrisGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class TetrisGame : Microsoft.Xna.Framework.Game
    {
        public static bool debug=false;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Matrix move = Matrix.Identity;
        // Content
        Texture2D cubeTexture;
        BasicEffect basicEffet;

        // Create a cube with a size of 1 (all dimensions) at the origin
        DummyObject rootObject = new DummyObject();
        
        float aspectRatio = 0.0f;

        public TetrisGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferMultiSampling = true;
            rootObject.setName("rootObject");
            /*
            RotatingCube rotatingCube1 = new RotatingCube(Vector3.One, Vector3.Zero);
            rotatingCube1.setName("rotatingCube1");
            rootObject.Add(rotatingCube1);

            RotatingCube RotatingCube2 = new RotatingCube(Vector3.One, new Vector3(1, 0, 1));
            RotatingCube2.setName("RotatingCube2");
            rotatingCube1.Add(RotatingCube2);
            */
            TetrisLine line = new TetrisLine(Vector3.One, new Vector3(2, 0, 2));
            line.setName("TetrisLine1");
            rootObject.Add(line);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            cubeTexture = Content.Load<Texture2D>("uvGrid3");
            aspectRatio = GraphicsDevice.Viewport.AspectRatio;
            basicEffet = new BasicEffect(GraphicsDevice);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            float time =gameTime.ElapsedGameTime.Milliseconds/1000.0f;
            rootObject.UpdateLogic();
            move = Matrix.CreateTranslation(new Vector3(1f * time, 0, 1f * time)) *move ;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // Set the World matrix which defines the position of the cube
            basicEffet.World = Matrix.Identity;
            basicEffet.View = Matrix.CreateLookAt(new Vector3(-50,50,-50),Vector3.Zero,new Vector3(0,1,0)) * Matrix.CreateScale(1);
            
            int screenWidth = Window.ClientBounds.Width;
            int screenHeight = Window.ClientBounds.Height;
         
            basicEffet.Projection = Matrix.CreateOrthographicOffCenter(-screenWidth / 30, screenWidth / 30, -screenHeight / 30, screenHeight / 30, 0.01f, 1000.0f);

            // Enable textures on the Cube Effect. this is necessary to texture the model
            basicEffet.TextureEnabled = true;
            basicEffet.Texture = cubeTexture;

            // Enable some pretty lights
            basicEffet.EnableDefaultLighting();

            
            rootObject.render(basicEffet,GraphicsDevice,0); 
            base.Draw(gameTime);
        }
    }
}



 