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
        public static DummyObject rootObject = new DummyObject();

        public static List<KeyboardState> keyboardStates = new List<KeyboardState>();
        protected int numberOfRecordedStates = 10;

        private int updateNumber = 0;

        protected GameLogic gameLogic = null;

        Matrix world;
        Matrix view;
        Matrix projection;


        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Matrix move = Matrix.Identity;
        // Content
        Texture2D cubeTexture;

        public static BasicEffect shadedEffect;
        public static BasicEffect mateEffect;

        // Create a cube with a size of 1 (all dimensions) at the origin
       
        
        float aspectRatio = 0.0f;
        private SpriteFont Font1;
        private Vector2 FontPos;

        public void newGame()
        {
            this.gameLogic = null;
        }

        public TetrisGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferMultiSampling = true;
            for (int i = 0; i <this.numberOfRecordedStates; i++)
            {
                TetrisGame.keyboardStates.Add(Keyboard.GetState());
            }
            this.IsMouseVisible = true;

            rootObject.setName("rootObject");
            rootObject.setPosition(new Vector3(0,0,0));
            rootObject.setVisible(true);
            if (debug)
            {
                
                DummyObject dummy1 = new DummyObject();
                dummy1.setPosition(new Vector3(0, 0, 0));
                dummy1.setName("Dummy1");
                dummy1.setVisible(true);
                rootObject.Add(dummy1);

                LinePiece rotatingcube1 = new LinePiece(Vector3.One, new Vector3(0, 0, 0));
                rotatingcube1.setName("RotatingCube1");
                dummy1.Add(rotatingcube1);

                DummyObject toto = new DummyObject();
                toto.setPosition(new Vector3(0, 0, 0));
                toto.setName("dummy object2");
                rotatingcube1.Add(toto);
                toto.setVisible(true);
            }
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

            Font1 = Content.Load<SpriteFont>("Courier New");

            // TODO: Load your game content here            
            FontPos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 20,
                graphics.GraphicsDevice.Viewport.Height / 20);

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
            for (int i = this.numberOfRecordedStates-1; i > 0; i--)
            {
                TetrisGame.keyboardStates[i] = TetrisGame.keyboardStates[i - 1]; 
            }
            TetrisGame.keyboardStates[0]=Keyboard.GetState();
            if(this.gameLogic == null)
            {
                this.gameLogic = new GameLogic(gameTime,this);
            }
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            
            float time = gameTime.ElapsedGameTime.Milliseconds/1000.0f;
          //  Console.WriteLine("iteration : " + this.updateNumber);
          //  Console.WriteLine("time ellapsed since last iteration : " +time);
          //  Console.WriteLine("updating game...");
            gameLogic.updateGame(gameTime);
            this.updateNumber++;

           // Console.WriteLine("");
           // Console.WriteLine("");
          //  rootObject.UpdateLogicGame(time);
          //  move = Matrix.CreateTranslation(new Vector3(1f * time, 0, 1f * time)) *move ;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightSkyBlue);

            spriteBatch.Begin();
            //////////////////////////SCORE//////////////////////////
            string output = "score : " +GameLogic.score;
            FontPos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 20,
                graphics.GraphicsDevice.Viewport.Height / 20);
            // Find the center of the string
            Vector2 FontOrigin = Font1.MeasureString(output) / 2;
            FontOrigin.Y = 0;
            // Draw the string
            spriteBatch.DrawString(Font1, output, FontPos, Color.White,
                0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);
            //////////////////////////SCORE//////////////////////////


            //////////////////////////LEVEL//////////////////////////
            output = "level : " + GameLogic.level;
            FontPos = new Vector2(graphics.GraphicsDevice.Viewport.Width -graphics.GraphicsDevice.Viewport.Width / 20,
                graphics.GraphicsDevice.Viewport.Height / 20);
            // Find the center of the string
            FontOrigin = Font1.MeasureString(output);
            FontOrigin.Y = 0;
            // Draw the string
            spriteBatch.DrawString(Font1, output, FontPos, Color.White,
                0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
            //////////////////////////LEVEL//////////////////////////


            //////////////////////////TIME//////////////////////////
            output = "time : " + (int) (GameLogic.timeSinceBeginning);
            FontPos = new Vector2(graphics.GraphicsDevice.Viewport.Width/2 ,
                graphics.GraphicsDevice.Viewport.Height / 20);
            // Find the center of the string
            FontOrigin = Font1.MeasureString(output);
            FontOrigin.Y = 0;
            // Draw the string
            spriteBatch.DrawString(Font1, output, FontPos, Color.White,
                0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
            //////////////////////////TIME//////////////////////////

            //////////////////////////GAME OVER//////////////////////////
            if (GameLogic.gameOver)
            {
                output = "game over, press n to play again";
                FontPos = new Vector2(graphics.GraphicsDevice.Viewport.Width/2,
                   graphics.GraphicsDevice.Viewport.Height / 8 +  graphics.GraphicsDevice.Viewport.Height / 20);
                // Find the center of the string
                FontOrigin = Font1.MeasureString(output)/2;
                FontOrigin.Y = 0;
                // Draw the string
                spriteBatch.DrawString(Font1, output, FontPos, Color.White,
                    0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
            }
            //////////////////////////GAME OVER//////////////////////////
            spriteBatch.End();

            int screenWidth = Window.ClientBounds.Width;
            int screenHeight = Window.ClientBounds.Height;

            world = Matrix.Identity;
            view = Matrix.CreateLookAt(new Vector3(50, 50, 50), Vector3.Zero, new Vector3(0, 1, 0));
            view = Matrix.CreateTranslation(new Vector3(-5, 0, -12)) * view;
           
           //  *
            projection = Matrix.CreateOrthographicOffCenter(-screenWidth / 30, screenWidth / 30, -screenHeight / 30, screenHeight / 30, 0.01f, 1000.0f);


            // Set the World matrix which defines the position of the cube

            shadedEffect.World = world;
            shadedEffect.View = view;
            shadedEffect.Projection = projection;

            
            // Enable textures on the Cube Effect. this is necessary to texture the model
            shadedEffect.TextureEnabled = false;
            shadedEffect.Texture = cubeTexture;
            shadedEffect.SpecularColor = new Vector3(Color.DarkGray.R / 255.0f, Color.DarkGray.G / 255.0f, Color.DarkGray.B / 255.0f);

            // Enable some pretty lights
            shadedEffect.EnableDefaultLighting();


            mateEffect.World = world;
            mateEffect.View = view;
            mateEffect.Projection = projection;

            // Enable textures on the Cube Effect. this is necessary to texture the model
            mateEffect.TextureEnabled = false;
            mateEffect.VertexColorEnabled = true;

            rootObject.render(mateEffect, GraphicsDevice, 0);
            rootObject.render(shadedEffect,GraphicsDevice,0);
            


            
            base.Draw(gameTime);
        }
    }
}