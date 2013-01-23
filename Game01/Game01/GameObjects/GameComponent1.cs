using System;
using System.Collections.Generic;
using Game01.Other;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game01.GameObjects {

    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class GameComponent1 : DrawableGameComponent {
        private Keys _upKey;
        private Keys _downKey;
        InputHelper _inputhelper = new InputHelper();

        public SpriteFont _font;

        private int _selectedIndex;
        private List<String> _menuItems;

        public GameComponent1(Game game)
            : base(game) {
            _menuItems = new List<string>();
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize() {
            _menuItems.Add("Start Game");
            _menuItems.Add("Options (unavailible)");
            _menuItems.Add("Exit");


            _upKey = Keys.Up;
            _downKey = Keys.Down;

            base.Initialize();
        }

        protected override void LoadContent() {
            _font = Game.Content.Load<SpriteFont>("Segoe");

            base.LoadContent();
        }

        protected override void UnloadContent() {
            base.UnloadContent();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime) {
            _inputhelper.Update();

            // get key's
            if (_inputhelper.IsNewPress(_downKey))
                _selectedIndex++;
            if (_inputhelper.IsNewPress(_upKey))
                _selectedIndex--;

            // fix pressing up on top element, and pressing down on bottom element
            if (_selectedIndex == _menuItems.Count)
                _selectedIndex = 0;
            if (_selectedIndex < 0)
                _selectedIndex = _menuItems.Count - 1;


            if (_inputhelper.IsNewPress(Keys.Enter)){
                switch (_selectedIndex) {
                    case 0:
                        break;
                    case 1:
                        
                        break;

                    case 2:
                        Game.Exit();

                        break;
                }
            }
            base.Update(gameTime);

        }

        public override void Draw(GameTime gameTime) {
            SpriteBatch spriteBatch = Game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;

            spriteBatch.Begin();

            Vector2 FontOrigin;
            int heightAdjust = (int) (_font.MeasureString("Hello").Y * _menuItems.Count/2);
            Vector2 FontPos = new Vector2(Game.GraphicsDevice.Viewport.Width / 2, Game.GraphicsDevice.Viewport.Height / 2 - heightAdjust);


            foreach (string item in _menuItems) {
                FontOrigin = _font.MeasureString(item) / 2;
                spriteBatch.DrawString(_font, item, FontPos, _menuItems[_selectedIndex] == item ? Color.Red : Color.Black, 0f, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
                FontPos.Y += heightAdjust;
            }


            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}