using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game01.GameObjects {

    internal class Player {
        private Vector2 _position;
        private int _health;

        public Player() {
        }

        public void Update() {
        }

        public void Draw(SpriteBatch spriteBatch) {
        }

        #region Getters and Setters

        public Vector2 Position {
            get { return _position; }
            set { _position = value; }
        }

        public int Health {
            get { return _health; }
            set { _health = value; }
        }

        #endregion Getters and Setters
    }
}