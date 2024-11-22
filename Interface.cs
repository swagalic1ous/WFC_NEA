using button2.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;

namespace button2 {
    public class Interface : Game {
        #region Fields
        private GraphicsDeviceManager graphics;

        private SpriteBatch spriteBatch;

        private Color backgroundColour = Color.GhostWhite;

        private List<Component> gameComponents; //replaces list of sprites
        #endregion

        public Interface() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize() {

            #region graphics
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;

            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            graphics.ToggleFullScreen(); //setting to full screen, depenidng on the device's res.
            #endregion

            base.Initialize();
        }

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            #region Buttons 
            // B = button
            var defaultB = new Button(Content.Load<Texture2D>("Controls/Button"), Content.Load<SpriteFont>("Fonts/Font"), graphics.PreferredBackBufferWidth / 2 + 100, 2) { //loading files from pipeline
                Text = "Default",
            };

            defaultB.Click += DefaultB_Click; //if clicked

            var quitB = new Button(Content.Load<Texture2D>("Controls/Button"), Content.Load<SpriteFont>("Fonts/Font"), new Vector2(50,50)) {
                Text = "Quit",
            };

            quitB.Click += quitB_Click;

            var customB = new Button(Content.Load<Texture2D>("Controls/Button"), Content.Load<SpriteFont>("Fonts/Font"), (graphics.PreferredBackBufferWidth / 2 - 250) , 2) { 
                Text = "Custom",
            };

            var helpB = new Button(Content.Load<Texture2D>("Controls/Button"), Content.Load<SpriteFont>("Fonts/Font"), graphics, Content.Load<String>("Controls/page_one_help")) {
                Text = "Help",
            };
            #endregion

            #region Titles
            var startTitle = new Title(Content.Load<SpriteFont>("Fonts/Font"), graphics, "Procedural Generation") {
                PenColour = Color.DarkBlue,
            };

            var settingText = new Title(Content.Load<SpriteFont>("Fonts/Font"), 2, graphics, "Setting:") {
                PenColour = Color.Black,
            };
            #endregion

            gameComponents = new List<Component>() { //list of all components to be drawn on this page
              defaultB, quitB, customB, helpB, startTitle, settingText,
            };
        }

        private void quitB_Click(object sender, System.EventArgs e) {
            Exit();           
        }


        private void DefaultB_Click(object sender, System.EventArgs e) {
            //if clicked, run appropriate screen
        }

        protected override void Update(GameTime gameTime) {
            foreach (var component in gameComponents) { //checking mouse position for all buttons
                component.Update(gameTime);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(backgroundColour);

            spriteBatch.Begin();

            foreach (var component in gameComponents) { //draws all buttons
                component.Draw(gameTime, spriteBatch); //calls draw function from component
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}