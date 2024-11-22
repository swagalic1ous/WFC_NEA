using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace button2 {
    public class Button : Component {
        #region Fields

        private SpriteFont _font;

        private bool _isHovering; //if mouse is hovering over button

        private MouseState _previousMouse; //last mouse state

        private MouseState _currentMouse; //current mouse state

        private Texture2D _texture; //button PNG_

        private Vector2 _position;

        private string _message;

        private bool _isHelpButton = false;

        public int default_Y = 350;
        #endregion

        #region Properties

        public event EventHandler Click; //event for if clicked

        public bool Clicked { get; private set; }

        public Color PenColour { get; set; }

        public Rectangle Rectangle {
            get {
                return new Rectangle((int)_position.X, (int)_position.Y, _texture.Width*2 , _texture.Height*2); // getting PNG as rectangle, and scaling it by s.f. 2
            }
        }

        public string Text { get; set; }

        #endregion

        #region Methods

        public Button(Texture2D texture, SpriteFont font, int posX, int buttonNum) { //posX (X coord of button) is varaible, posY is dependent on buttonNum (how many other buttons there are)
            _texture = texture;

            _position.X = posX;

            _font = font;

            PenColour = Color.Black; //font colour

            switch (buttonNum) {
                case 1:
                    _position.Y = default_Y;
                    break;

                case 2:
                    _position.Y = Convert.ToInt16(default_Y * 1.25);
                    break;

                case 3:
                    _position.Y = Convert.ToInt16(default_Y * 1.56);
                    break;

                case 4:
                    _position.Y = Convert.ToInt16(default_Y * 2);
                    break;

                case 5:
                    _position.Y = Convert.ToInt16(default_Y * 2.4);
                    break;
            }
        }

        public Button(Texture2D texture, SpriteFont font, Vector2 position) { //buttons for top left of screen
            _texture = texture;

            _font = font;

            _position = position;

            PenColour = Color.Black; //font colour
        }

        public Button(Texture2D texture, SpriteFont font, GraphicsDeviceManager graphics, string message) { //help button, if hovered over, information will display
            _isHelpButton = true;

            _texture = texture;

            _font = font;

            _message = message; //help text

            _position = new Vector2((graphics.PreferredBackBufferWidth/2) - texture.Width/2,950);

            PenColour = Color.Black;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) { //abstract method, for each button
            Color colour = Color.White; //colour of button

            if (_isHovering) {
                colour = Color.Gray; //if on button, turns grey
                if(_isHelpButton) { spriteBatch.DrawString(_font, _message, new Vector2(200, 400), PenColour); }
            }

            spriteBatch.Draw(_texture, Rectangle, colour); //drawing button

            if (!string.IsNullOrEmpty(Text)) { //position of the text inside the button
                float x = (Rectangle.X + (Rectangle.Width / 2)) - (_font.MeasureString(Text).X); //ensuring text is center of box
                float y = (Rectangle.Y + (Rectangle.Height / 2)) - (_font.MeasureString(Text).Y);

                spriteBatch.DrawString(_font, Text, new Vector2(x, y), PenColour, 0f, new Vector2(0,0), new Vector2(2,2), SpriteEffects.None,1f); //drawing text
            }
        }

        public override void Update(GameTime gameTime) {
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            Rectangle mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);

            _isHovering = false;

            if (mouseRectangle.Intersects(Rectangle)) {
                _isHovering = true;

                if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed) {
                    Click?.Invoke(this, new EventArgs()); //if not null, invoke
                }
            }
        }

        #endregion
    }
}