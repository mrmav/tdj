﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Engine;

namespace TDJGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {

        public GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            IsMouseVisible = true;

        }

        protected override void Initialize()
        {

            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.ApplyChanges();

            //StateManager.Instance.AddGameState(new StartupState("StartupState", graphics));
            //StateManager.Instance.AddGameState(new MenuState("MenuState", graphics));
            StateManager.Instance.AddGameState(new PlayState("PlayState", this));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            StateManager.Instance.LoadContent(Content);

            StateManager.Instance.StartGameState("PlayState");

        }

        protected override void UnloadContent()
        {
            StateManager.Instance.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            StateManager.Instance.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            StateManager.Instance.Draw(gameTime, spriteBatch, GraphicsDevice);

            base.Draw(gameTime);
        }

    }
}
