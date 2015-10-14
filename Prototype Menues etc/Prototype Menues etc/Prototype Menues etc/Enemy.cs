using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Prototype_Menues_etc
{
    class Enemy
    {
        // Animation representing the enemy
        public EnemyAnimation EnemyAnimation;
        public string Direction;
        // The position of the enemy ship relative to the top left corner of thescreen
        public Vector2 Position;
        public string Action = "Moving";
        // The state of the Enemy Ship
        public bool Active, Dodged = false;
        public float timer;
        public float interval = 2000;
        const float TIMER = 2;
        // The hit points of the enemy, if this goes to zero the enemy dies
        public int Health;

        // The amount of damage the enemy inflicts on the player ship
        public int Damage;

        // The amount of score the enemy will give to the player
        public int Value;

        // Get the width of the enemy ship
        public int Width
        {
            get { return EnemyAnimation.spriteWidth; }
        }

        // Get the height of the enemy ship
        public int Height
        {
            get { return EnemyAnimation.spriteHeight; }
        }

        // The speed at which the enemy moves
        float enemyMoveSpeed;

        public void Initialize(EnemyAnimation animation, Vector2 position)
        {
            // Set the position of the enemy
            EnemyAnimation = animation;
            Position = position;

            // We initialize the enemy to be active so it will be update in the game
            Active = true;


            // Set the health of the enemy
            Health = 75;

            // Set the amount of damage the enemy can do
            Damage = 10;

            // Set how fast the enemy moves
            enemyMoveSpeed = 4f;


            // Set the score value of the enemy
            Value = 100;

        
        }

        public void Update(GameTime gameTime, Game1 game1)
        {

            //AI
            // The enemy always moves to the left so decrement it's xposition
           

            //// Update the position of the Animation
            //Enemy dodges based on time
            //Every 5 seconds
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timer -= elapsed;
            if (timer < 0)
            {
                //Timer expired, execute action
                timer = TIMER;   //Reset Timer

                Action = "Dodging";
            }else{
                Action = "Moving";
            }





            // Update Animation
            if (Health <= 0 )
            {
                Action = "Dying";
                Damage = 0;

            }
            else if (Action == "Moving")
            {
                if (Position.X > game1.player.Position.X)
                {
                    Position.X -= enemyMoveSpeed;
                }
                else
                {
                    Position.X += enemyMoveSpeed;
                }
                if (Position.Y > game1.player.Position.Y)
                {
                    Position.Y -= enemyMoveSpeed/2;
                }
                else
                {
                    Position.Y += enemyMoveSpeed/2;
                }
                EnemyAnimation.Position = Position;
                //Action = "Moving"
            }
            else if (Action == "Dodging")
            {
                if (game1.player.Position.Y > Position.Y/* && game1.player.Position.Y > Position.Y*/)
                {
                    Position.Y += 50;
                    EnemyAnimation.Position = Position;
                }
                if (game1.player.Position.Y < Position.Y/* && game1.player.Position.Y > Position.Y*/)
                {
                    Position.Y -= 50;
                    EnemyAnimation.Position = Position;
                }
            }

            EnemyAnimation.Update(gameTime, Action, Position, game1);
            // If the enemy is past the screen or its health reaches 0 then deactivateit
            if (Position.X < -Width || EnemyAnimation.currentFrame == 8)
            {
                // By setting the Active flag to false, the game will remove this objet fromthe
                // active game list
                Active = false;
            }
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            EnemyAnimation.Draw(spriteBatch);
        }
    }
}
