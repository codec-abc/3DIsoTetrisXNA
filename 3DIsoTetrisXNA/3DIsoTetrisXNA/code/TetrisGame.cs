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
        public static BasicEffect shadedEffect;
        public static BasicEffect mateEffect;

        // Create a cube with a size of 1 (all dimensions) at the origin
        DummyObject rootObject = new DummyObject();
        
        float aspectRatio = 0.0f;

        public TetrisGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferMultiSampling = true;

            rootObject.setName("rootObject");
            rootObject.setPosition(new Vector3(0,0,0));

            TetrisLine line = new TetrisLine(Vector3.One, new Vector3(0, 0, 0));
            line.setName("TetrisLine1");
            rootObject.Add(line);

            DummyObject toto = new DummyObject();
            toto.setPosition(new Vector3(3, 0, 0));
            toto.setName("dummy object2");
            line.Add(toto);
            toto.setVisible(true);

            rootObject.setVisible(true);
            
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
            shadedEffect = new BasicEffect(GraphicsDevice);
            mateEffect = new BasicEffect(GraphicsDevice);
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
            rootObject.UpdateLogicGame(time);
          //  move = Matrix.CreateTranslation(new Vector3(1f * time, 0, 1f * time)) *move ;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            int screenWidth = Window.ClientBounds.Width;
            int screenHeight = Window.ClientBounds.Height;

            // Set the World matrix which defines the position of the cube

            shadedEffect.World = Matrix.Identity;
            shadedEffect.View = Matrix.CreateLookAt(new Vector3(-50,50,-50),Vector3.Zero,new Vector3(0,1,0)) * Matrix.CreateScale(1);
            
            shadedEffect.Projection = Matrix.CreateOrthographicOffCenter(-screenWidth / 30, screenWidth / 30, -screenHeight / 30, screenHeight / 30, 0.01f, 1000.0f);

            // Enable textures on the Cube Effect. this is necessary to texture the model
            shadedEffect.TextureEnabled = true;
            shadedEffect.Texture = cubeTexture;

            // Enable some pretty lights
            shadedEffect.EnableDefaultLighting();



            mateEffect.World = Matrix.Identity;
            mateEffect.View = Matrix.CreateLookAt(new Vector3(-50, 50, -50), Vector3.Zero, new Vector3(0, 1, 0)) * Matrix.CreateScale(1);

            mateEffect.Projection = Matrix.CreateOrthographicOffCenter(-screenWidth / 30, screenWidth / 30, -screenHeight / 30, screenHeight / 30, 0.01f, 1000.0f);

            // Enable textures on the Cube Effect. this is necessary to texture the model
            mateEffect.TextureEnabled = false;
            mateEffect.VertexColorEnabled = true;
            mateEffect.Texture = cubeTexture;

            // Enable some pretty lights
           // mateEffect.EnableDefaultLighting();

            
          //  rootObject.render(shadedEffect,GraphicsDevice,0);
            rootObject.render(mateEffect, GraphicsDevice, 0); 
            base.Draw(gameTime);
        }
    }
}



 