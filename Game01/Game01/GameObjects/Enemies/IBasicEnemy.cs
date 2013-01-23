namespace Game01.GameObjects.Enemies {

    internal interface IBasicEnemy {

        void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch);

        int Health { get; set; }

        Microsoft.Xna.Framework.Vector2 Position { get; set; }

        void Update();
    }
}