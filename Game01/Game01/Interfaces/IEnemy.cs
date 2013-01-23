using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game01.Interfaces {

    internal interface IEnemy {

        void Update();

        void Draw(SpriteBatch spriteBatch);
    }
}