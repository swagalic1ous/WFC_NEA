using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace button2.Controls {
    public class Title : Component {

        #region Fields
        private GraphicsDeviceManager _graphics;

        private SpriteFont _font;

        readonly int _titleNo;

        private Vector2 _position;

        private Vector2 _scale;

        private string _text;

        public int titleDefaultY = 350; //subheading default Y value
        public const int titleDefaultX = 600; //subheading default X value

        #endregion


        #region Properties
        public Color PenColour { get; set; }
        #endregion


        #region Methods 
        //overloading constuctor based on if i want title or a subheading
        public Title(SpriteFont font, GraphicsDeviceManager graphics, string text) { //for main title
            #region Fields
            _graphics = graphics;

            _text = text;

            _font = font;

            _scale = new Vector2(5, 5);

            _position = new Vector2((float)graphics.PreferredBackBufferHeight/2 + (font.MeasureString(text).X/2), 150);
            #endregion
        }

        public Title(SpriteFont font, int titleNo, GraphicsDeviceManager graphics, string text) { //for subheading, int parameter changes where it is
            #region Fields
            _font = font;

            _graphics = graphics;

            _titleNo = titleNo;

            _text = text;

            _scale = new Vector2(3, 3);

            _position.X = titleDefaultX - font.MeasureString(text).X*3;
            #endregion

            switch (titleNo) { // working out positioning of subheading based on number of subheadings on page.
                case 1:
                    _position.Y = titleDefaultY;
                    break;

                case 2:
                    _position.Y =  titleDefaultY + font.MeasureString(text).Y*5;
                    break;

                case 3:
                    _position.Y = titleDefaultY + font.MeasureString(text).Y * 10;
                    break;

                case 4:
                    _position.Y = titleDefaultY + font.MeasureString(text).Y * 15;
                    break;

                case 5:
                    _position.Y = titleDefaultY + font.MeasureString(text).Y * 20;
                    break;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            spriteBatch.DrawString(_font, _text, _position, PenColour, 0f, new Vector2(0, 0), _scale, SpriteEffects.None, 1f); //drawing the string
        }

        public override void Update(GameTime gameTime) {

        }
        #endregion
    }
}
