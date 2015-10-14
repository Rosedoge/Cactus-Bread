using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Prototype_Menues_etc
{
    class SniperEnemy
    {
        SniperEnemyAnimation SniperAnimation;
        public int Health = 25; //Should be low HP
        public int Damage = 100; //Damage should be a 1 shot kill, and avoidable
        public int Value = 200;
        int MoveSpeed = 5, count = 0;
        
 
        bool Aiming = false;
        public bool Active = true;
        String Action = "Moving";
        public Vector2 Position;
        
        //TimeSpan SpawnTime, ShootTime;



        public void Initialize(SniperEnemyAnimation animation, Vector2 position, Vector2 target)
        {
            // Set the position of the enemy
            SniperAnimation = animation;
            Position = position;

            // We initialize the enemy to be active so it will be update in the game
            Active = true;

            // Set the health of the enemy
            Health = 25;

            // Set the amount of damage the enemy can do
            //Very Weak
            Damage = 100;

            // Set how fast the enemy moves
            

            //Direction = direction;
            // Set the score value of the enemy
            //More value due to more danger
            Value = 150;


        }

        public void Update(GameTime gameTime, Game1 game1)
        {

            /// <summary>
            /// Moves 100 pixel ix x or y, and in the middle of the screen otherwise. 1280x720
            /// </summary>
            //AI
            // The enemy always moves to the correct position for it's placement
            if (Action == "Moving")
            {
                if (Position.X <= 1000)
                    Position.X += MoveSpeed;
                else
                    Action = "Shooting";
            } // End Moving Action While/IF
            if (Action == "Shooting")
            {
                //Vector2 target;
                //count for 5 bullets per update

                if (count == 60)
                {
                    SniperProjectile projectileS = new SniperProjectile();
                    projectileS.Initialize(game1.GraphicsDevice.Viewport, game1.EnemyBulletTexture, Position, game1.player.Position);
                    game1.sniperBullets.Add(projectileS);
                    //public void Initialize(Viewport viewport, Texture2D texture, Vector2 position, Vector2 Target)
                    //Projectile projectile = new Projectile();
                    //projectile.Initialize(GraphicsDevice.Viewport, projectileTexture, position, new Vector2(ms.X, ms.Y));
                    //projectiles.Add(projectile);
                    count = 0;
                    //end 5 bullet count
                }
                count += 1;
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
            SniperAnimation.Update(gameTime, Action, Position);

            if (SniperAnimation.currentFrame > 2)
            {
                // By setting the Active flag to false, the game will remove this objet fromthe
                // active game list
                Active = false;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            //Draw this AND Draw a crosshair
            //2birds1stone.jpg
            //if (Aiming == true)
            //{
            //    spriteBatch.Draw(Crosshair, PlayerLoc, null, Color.White, 0f,
            //    new Vector2(30, 30), 1f, SpriteEffects.None, 0f); //H
            //}
            // Draw the Projectiles
            
            SniperAnimation.Draw(spriteBatch);
        }
    }
}
