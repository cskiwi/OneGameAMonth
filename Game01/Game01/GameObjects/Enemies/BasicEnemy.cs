using System;
using Game01.Interfaces;
using Microsoft.Xna.Framework;

namespace Game01.GameObjects.Enemies {

    internal class BasicEnemy : IEnemy {
        private Vector2 _position;
        private int _health;

        public void Update() {
            throw new NotImplementedException();
        }

        public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch) {
            throw new NotImplementedException();
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