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


namespace CubeBuilder
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class TetrisGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Content
        Texture2D cubeTexture;
        BasicEffect basicEffet;

        // Create a cube with a size of 1 (all dimensions) at the origin
        List<Object3D> ObjectsList = new List<Object3D>();
        
        

        // Position related variables
        Vector3 cameraPosition = new Vector3(0, 3, 4);
        Vector3 modelPosition = Vector3.Zero;
        float rotation = 0.0f;
        float aspectRatio = 0.0f;

        public TetrisGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferMultiSampling = true;

            
            Cube cubeToDraw = new Cube(new Vector3(1, 1, 1), Vector3.Zero);
            Cube cubeToDraw2 = new Cube(new Vector3(1, 1, 1), new Vector3(1,2,1));
            ObjectsList.Add(cubeToDraw);
            ObjectsList.Add(cubeToDraw2);
            // Frame rate is 30 fps by default for Windows Phone.
          //  TargetElapsedTime = TimeSpan.FromTicks(333333);
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

            cubeTexture = Content.Load<Texture2D>("328425");
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

            rotation += 0.5f;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // Set the World matrix which defines the position of the cube
            basicEffet.World = Matrix.Identity;
            basicEffet.World = Matrix.CreateRotationY(MathHelper.ToRadians(rotation)) *
                Matrix.CreateRotationX(MathHelper.ToRadians(rotation)) * Matrix.CreateTranslation(modelPosition);

            // Set the View matrix which defines the camera and what it's looking at
            basicEffet.View = Matrix.CreateLookAt(cameraPosition, modelPosition, Vector3.Up);
            basicEffet.View = Matrix.CreateLookAt(cameraPosition, modelPosition, Vector3.Up) * Matrix.CreateScale(20);
            
            int screenWidth = Window.ClientBounds.Width;
            int screenHeight = Window.ClientBounds.Height;
            basicEffet.Projection = Matrix.CreateOrthographic(screenWidth, screenHeight, 1.0f, 100000.0f);
            // Set the Projection matrix which defines how we see the scene (Field of view)
          //  cubeEffect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, 1.0f, 1000.0f);

            // Enable textures on the Cube Effect. this is necessary to texture the model
            basicEffet.TextureEnabled = true;
            basicEffet.Texture = cubeTexture;

            // Enable some pretty lights
            basicEffet.EnableDefaultLighting();

            // apply the effect and render the cube
            foreach (EffectPass pass in basicEffet.CurrentTechnique.Passes)
            {
                pass.Apply();

                foreach (Object3D obj in ObjectsList)
                {
                    obj.RenderToDevice(GraphicsDevice);
                }
            }

            base.Draw(gameTime);
        }
    }
}
