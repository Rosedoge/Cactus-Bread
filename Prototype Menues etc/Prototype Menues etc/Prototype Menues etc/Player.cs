using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Prototype_Menues_etc
{
    public class Player : Microsoft.Xna.Framework.Game
    {
        // Animation representing the player
        public Animation PlayerAnimation;

        // Position of the Player relative to the upper left side of the screen
        public Vector2 Position;

        // State of the player
        public bool Active;

        // Amount of hit points that player has
        public int Health;

       
        public int Width
        {
            get { return PlayerAnimation.spriteWidth; } // 50
        }

        
        public int Height
        {
            get { return PlayerAnimation.spriteHeight; } // 100
        }


        public void Initialize(Animation animation, Vector2 position)
        {
            PlayerAnimation = animation;

            // Set the starting position of the player around the middle of the screen and to the back
            Position = position;

            // Set the player to be active
            Active = true;

            // Set the player health
            Health = 100;
        }
        public void Update(GameTime gameTime, String Action)
        {
            PlayerAnimation.Position = Position;
            PlayerAnimation.Update(gameTime, Action);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            PlayerAnimation.Draw(spriteBatch);
        }
    }
}
