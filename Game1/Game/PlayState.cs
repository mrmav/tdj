﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TDJGame.Engine;
using TDJGame.Engine.Tiled;
using TDJGame.Utils;

namespace TDJGame
{
    public class PlayState : GameState
    {

        #region [Porperties]

        Camera2D camera;

        SpriteFont font;
        Texture2D ball;
        Texture2D tilemapTexture;

        Level level;

        Player player;

        #endregion

        public PlayState(string key, Game game)
        {
            this.Key = key;
            this.Game = game;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void LoadContent()
        {
            base.LoadContent();

            camera = new Camera2D(Vector2.Zero);
            
            font = this.content.Load<SpriteFont>("Font");
            ball = this.content.Load<Texture2D>("ball");
            tilemapTexture = this.content.Load<Texture2D>("tilemap");

            this.player = new Player(
                ball,
                new Vector2(
                    this.Game.graphics.PreferredBackBufferWidth / 2,
                    this.Game.graphics.PreferredBackBufferHeight / 2)
                );

            XMLLevelLoader XMLloader = new XMLLevelLoader();
            this.level = XMLloader.LoadLevel("C:\\Users\\Noro\\Desktop\\TestMap\\test_map.tmx", tilemapTexture);
            
            this.ContentLoaded = true;

        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState kState = Keyboard.GetState();
            MouseState mState = Mouse.GetState();

            //if (kState.IsKeyDown(Keys.Enter))
            //{
            //    StateManager.Instance.StartGameState("MenuState");
            //}

            float inc = 0.1f;

            if (kState.IsKeyDown(Keys.Q))
            {
                this.camera.Zoom += inc;
            }

            if (kState.IsKeyDown(Keys.E))
            {
                this.camera.Zoom -= inc;
            }

            this.player.Update(gameTime, kState);

            this.camera.Position = this.player.position;
            
            this.camera.GetTransform(this.Game.GraphicsDevice);

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            graphicsDevice.Clear(Color.CornflowerBlue);

            //spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

            //world render
            spriteBatch.Begin(
                samplerState: SamplerState.PointClamp,                
                transformMatrix: this.camera.Transform
            );
            this.level.Draw(spriteBatch);
            spriteBatch.DrawString(font, $"This state key is {this.Key}! Play with WASD!\nIf you wnat to go back to the menu, press Enter.\nQ & E to zoom in and out!", Vector2.Zero, Color.OrangeRed);
            this.player.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }
    }
}