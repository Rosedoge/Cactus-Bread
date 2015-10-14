using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Prototype_Menues_etc
{
    class BossToaster

    {
        Random Rando = new Random();
        public int count = 0, attackCount = 0;
        // Animation representing the boss
        public BossToasterAnimation BossToasterAnimation;
        public int Health = 900;
        float BossMoveSpeed = 2f;
        string Action;
        public bool shootStop = false;
        public int Damage = 10;
        public int Value = 1500;
        public TimeSpan Interval;
        public bool Active = false, dyingStop = false;
        
        public Vector2 Position;
        // Get the width of the boss
        public int Width
        {
            get { return BossToasterAnimation.spriteWidth; }
        }

        // Get the height of the boss 
        public int Height
        {
            get { return BossToasterAnimation.spriteHeight; }
        }


        public void Initialize(BossToasterAnimation animation, Vector2 position)
        {
            BossToasterAnimation = animation;
            Position = position;
            Interval = TimeSpan.FromSeconds(10000005.0f);
            //Interval = TimeSpan.FromSeconds(9.0f);
            Active = true;
        }

        public void Update(GameTime gameTime, Game1 game1)
        {
            if (Health >= 1)
            {
                if (gameTime.TotalGameTime - game1.BossSpawnTime > Interval && shootStop == false)
                {

                    Action = "Shooting";
                    if (Health >= 450)
                    {
                        if (count > 20)
                        {
                            count = 0;
                            Vector2 target;
                            //count for 5 bullets per update

                            target = game1.player.Position;
                            BossToasterProjectile projectile = new BossToasterProjectile();
                            projectile.Initialize(game1.GraphicsDevice.Viewport, game1.bossToasterProjectile, Position, target, 7);
                            game1.bossToasterProjectiles.Add(projectile);
                            attackCount++;
                        }
                    }
                    else
                    {
                        Vector2 target = Vector2.Zero;

                        target.X = Rando.Next(0, 1280);
                        target.Y = Rando.Next(0, 680);
                        BossToasterProjectile projectile = new BossToasterProjectile();
                        projectile.Initialize(game1.GraphicsDevice.Viewport, game1.bossToasterProjectile, Position, target, 3);
                        game1.bossToasterProjectiles.Add(projectile);
                    }
                    count++;

                }

                else // slow crawl after player
                {
                    if (count >= 1)
                    {
                        count = 0;
                        game1.BossSpawnTime = gameTime.TotalGameTime;
                        shootStop = false;
                    }
                    if (attackCount > 50)
                        attackCount = 0;
                    Action = "Moving";
                    if (Position.X > game1.player.Position.X)
                    {
                        Position.X -= BossMoveSpeed;
                    }
                    else
                    {
                        Position.X += BossMoveSpeed;
                    }
                    if (Position.Y > game1.player.Position.Y)
                    {
                        Position.Y -= BossMoveSpeed / 2;
                    }
                    else
                    {
                        Position.Y += BossMoveSpeed / 2;
                    }
                    BossToasterAnimation.Position = Position;
                    Interval = TimeSpan.FromSeconds(5.0f);
                    //Action = "Moving"
                } // End Else Timer


            }
            else
            {

                Action = "Dying";
                if (dyingStop == true)
                {
                    game1.BossKill = true;
                    game1.BossSpawn = false;
                    game1.score += Value;
                    Active = false;
                    
                }
            }
            BossToasterAnimation.Update(gameTime, Position, Action, game1, ref shootStop, ref dyingStop);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            BossToasterAnimation.Draw(spriteBatch);
        }
    }
}
