using System.Collections.Generic;
using Game01.Interfaces;
using Game01.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game01.GameObjects {

    internal class GameManager {
        private List<ILevel> _levelList;
        private List<IEnemy> _EnemyList;
        private int _currentLevel;

        private enum state { NA, Game, Loading };
        private state _currentState;
        private string _LoadingMessage;

        public GameManager() {

            // set everything to default values to check on
            _levelList = null;
            _currentState = state.NA;
            _LoadingMessage = "";
        }

        public void Initialize() {
            _levelList = new List<ILevel>();
            _levelList.Add(new Level1());
        }

        public void PaintLevel(SpriteBatch spriteBatch) {
            switch (_currentState) {
                case state.NA:
                    break;
                case state.Game:
                    break;
                case state.Loading:
                    break;
                default:
                    break;
            }
        }

        private void PaintLoading(SpriteBatch spriteBatch) {
            // GraphicsDevice.Clear(Color.Black);                        
            
        }


        public void SwitchToLevel(int level) {
            _currentState = state.Loading;
            // unload previous level

            // load next level

            // Insert Enemies, objects, ...

            // if succesfull update level
            _currentLevel = level;
            _currentState = state.Game;
        }
    }
}