using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Prototype_Menues_etc
{
    class ToasterEnemy
    /// <summary>
    /// This enemy is designed to reach one of four spots on the screen, stand there, and shoot rapidly in the opposite direction.
    /// Stronger than the normal enemy, yet has less hp in order to stop the player from dying
    /// </summary>
    {
        // Animation representing the enemy
        public ToasterAnimation ToasterAnimation;

        Random Rando = new Random();
        public string Direction;
        // The position of the enemy ship relative to the top left corner of thescreen
        public Vector2 Position;
        public string Action = "Moving";
        // The states of the enemy
        public bool Active, Dodged = false;
        public bool move1 = false, move2 = false; // Variables that will see if the enemy is in place
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
            get { return ToasterAnimation.spriteWidth; } // 50
        }

        // Get the height of the enemy ship
        public int Height
        {
            get { return ToasterAnimation.spriteHeight; } // 100
        }

        // The speed at which the enemy moves
        float enemyMoveSpeed;

        public void Initialize(ToasterAnimation animation, Vector2 position, string direction)
        {
            // Set the position of the enemy
            ToasterAnimation = animation;
            Position = position;

            // We initialize the enemy to be active so it will be update in the game
            Active = true;

            // Set the health of the enemy
            Health = 50;

            // Set the amount of damage the enemy can do
            //Very Weak
            Damage = 5;

            // Set how fast the enemy moves
            enemyMoveSpeed = 5f;
            
            Direction = direction;
            // Set the score value of the enemy
            //More value due to more danger
            Value = 125;


        }

        public void Update(GameTime gameTime,  Game1 game1)
        {
            /// <summary>
            /// Moves 100 pixel ix x or y, and in the middle of the screen otherwise. 1280x720
            /// </summary>
            //AI
            // The enemy always moves to the correct position for it's placement
            if (Action == "Moving")
            {
                if (Direction == "Top") //640x100
                {
                    if (Position.X != 640)
                    {
                        if (Position.X > 640)
                        {
                            Position.X -= enemyMoveSpeed;

                        }
                        else if (Position.X < 640)
                        {
                            Position.X += enemyMoveSpeed;
                        }
                    }
                    else
                        move1 = true;

                    if (Position.Y <= 100 || Position.Y > 110)
                    {
                        if (Position.Y > 100)
                        {
                            Position.Y -= enemyMoveSpeed;
                        }
                        else if (Position.Y < 100)
                        {
                            Position.Y += enemyMoveSpeed;
                        }

                    }
                    else
                        move2 = true;
                }

                if (Direction == "Bottom") //640x620
                {
                    if (Position.X <= 640 || Position.X > 650)
                    {
                        if (Position.X > 640)
                        {
                            Position.X -= enemyMoveSpeed;
                        }
                        else if (Position.X < 640)
                        {
                            Position.X += enemyMoveSpeed;
                        }
                    }
                    else
                        move1 = true;
                    if (Position.Y <= 620 || Position.Y > 630)
                    {
                        if (Position.Y > 620)
                        {
                            Position.Y -= enemyMoveSpeed;
                        }
                        else if (Position.Y < 620)
                        {
                            Position.Y += enemyMoveSpeed;
                        }
                    }
                    else
                        move2 = true;
                }

                // Left And Right 1280x720
                if (Direction == "Left") //100x360
                {
                    if (Position.X <= 100 || Position.X >= 110)
                    {
                        if (Position.X > 100)
                        {
                            Position.X -= enemyMoveSpeed;
                        }
                        else if (Position.X < 100)
                        {
                            Position.X += enemyMoveSpeed;
                        }
                    }
                    else
                        move1 = true;
                    if (Position.Y <= 360 || Position.Y >= 370)
                    {
                        if (Position.Y > 360)
                        {
                            Position.Y -= enemyMoveSpeed;
                        }
                        else if (Position.Y < 360)
                        {
                            Position.Y += enemyMoveSpeed;
                        }
                    }
                    else
                        move2 = true;
                }
                if (Direction == "Right") //1180x360
                {
                    if ((Position.X <= 1180 || Position.X >= 1190) && (Position.Y <= 360 || Position.Y >= 370))
                    {
                        if (Position.X > 1180)
                        {
                            Position.X -= enemyMoveSpeed;
                        }
                        else if (Position.X < 1180)
                        {
                            Position.X += enemyMoveSpeed;
                        }
                        if (Position.Y > 360)
                        {
                            Position.Y -= enemyMoveSpeed;
                        }
                        else if (Position.Y < 360)
                        {
                            Position.Y += enemyMoveSpeed;
                        }
                    }
                    else
                        Action = "Shooting";
                }
                if (move1 == true && move2 == true) // stops the moving of the machine
                    Action = "Shooting";

            } // End Moving Action While/IF
            if(Action == "Shooting"){
                Vector2 target;
                 //count for 5 bullets per update
                
                target = Targeting();
                ToastProjectile projectile = new ToastProjectile();
                projectile.Initialize(game1.GraphicsDevice.Viewport, game1.toastTexture, Position, target);
                game1.ToastProjectiles.Add(projectile);
                //public void Initialize(Viewport viewport, Texture2D texture, Vector2 position, Vector2 Target)
                //Projectile projectile = new Projectile();
                //projectile.Initialize(GraphicsDevice.Viewport, projectileTexture, position, new Vector2(ms.X, ms.Y));
                //projectiles.Add(projectile);
                    
                 //end 5 bullet count

            } //End If Shooting

            
            // If the enemy is past the screen or its health reaches 0 then deactivate it
            if (Health <= 0)
            {
                // By setting the Active flag to false, the game will remove this objet fromthe
                // active game list
                Damage = 0;
                //game1.score += Value;
                Action = "Dying";
                
            }
            ToasterAnimation.Update(gameTime, Action, Position, game1);

            if (ToasterAnimation.currentFrame > 8)
            {
                // By setting the Active flag to false, the game will remove this objet fromthe
                // active game list
                Active = false;
            }
        }

        Vector2 Targeting()
        { //sets up a target for each toast bullet
            
            Vector2 target = Vector2.Zero;
            if (Direction == "Top" || Direction == "Bottom")
            {

                int xpos = Rando.Next(0, 1280);
                target.X = xpos;
                if (Direction == "Top")
                    target.Y = 700;
                else
                    target.Y = 100;
            }
            else //if (Direction == "Left" || Direction == "Right") // Bad Coordinates: Probably
            {
                int xpos = Rando.Next(0, 1280);
                target.Y = xpos;
                if (Direction == "Left")
                    target.X = 700;
                else
                    target.X = 100;
            }

            return target;
        }



        public void Draw(SpriteBatch spriteBatch)
        {
            ToasterAnimation.Draw(spriteBatch);
        }
    }
}
