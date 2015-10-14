using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Prototype_Menues_etc
{
    class PamBoss
    {
        int x = 1, y = 1, shootCounter = 0;
        Random Rando = new Random();
        public int count = 2, attackCount = 0;
        // Animation representing the boss
        public PamBossAnimation PamBossAnimation;
        public int Health = 1400;
        float BossMoveSpeed = 2f;
        string Action;
        public bool shootStop = false;
        public int Damage = 15;
        public int Value = 3000;
        public TimeSpan Interval;
        public bool Active = false, dyingStop = false;

        public Vector2 Position;
        // Get the width of the boss
        public int Width
        {
            get { return PamBossAnimation.spriteWidth; }
        }

        // Get the height of the boss 
        public int Height
        {
            get { return PamBossAnimation.spriteHeight; }
        }


        public void Initialize(PamBossAnimation animation, Vector2 position)
        {
            PamBossAnimation = animation;
            Position = position;
            Interval = TimeSpan.FromSeconds(5.0f);
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
                    if (Health >= 900)
                    {
                        if (count > 15)
                        {
                            count = 0;
                            Vector2 target;
                            //count for 5 bullets per update

                            target = game1.player.Position;
                            PamBossProjectile projectile = new PamBossProjectile();
                            projectile.Initialize(game1.GraphicsDevice.Viewport, game1.pamBossProjectile, Position, target, 7);
                            game1.pamBossProjectiles.Add(projectile);
                            shootCounter++;
                        }
                    }
                    else
                    {
                        Interval = TimeSpan.FromSeconds(3.0f);
                        PamBossProjectile projectile1 = new PamBossProjectile();
                        PamBossProjectile projectile2 = new PamBossProjectile();
                        PamBossProjectile projectile3 = new PamBossProjectile();
                        PamBossProjectile projectile4 = new PamBossProjectile();
                        //Shoots in an + then x pattern
                        Vector2 target = Vector2.Zero;
                        if (attackCount == 0) //+
                        {
                            target = new Vector2(Position.X, Position.Y + 1000);
                            projectile1.Initialize(game1.GraphicsDevice.Viewport, game1.pamBossProjectile, Position, target, 8); // Add Texture to game1
                            target = new Vector2(Position.X, Position.Y - 1000);
                            projectile2.Initialize(game1.GraphicsDevice.Viewport, game1.pamBossProjectile, Position, target, 8); // Add Texture to game1
                            target = new Vector2(Position.X + 1000, Position.Y);
                            projectile3.Initialize(game1.GraphicsDevice.Viewport, game1.pamBossProjectile, Position, target, 8); // Add Texture to game1
                            target = new Vector2(Position.X - 1000, Position.Y);
                            projectile4.Initialize(game1.GraphicsDevice.Viewport, game1.pamBossProjectile, Position, target, 8); // Add Texture to game1
                            game1.pamBossProjectiles.Add(projectile1);
                            game1.pamBossProjectiles.Add(projectile2);
                            game1.pamBossProjectiles.Add(projectile3);
                            game1.pamBossProjectiles.Add(projectile4);
                            shootCounter++;
                            attackCount = 1;
                        }
                        else if (attackCount == 1) //x
                        {
                            target = new Vector2(Position.X + 1000, Position.Y + 1000);
                            projectile1.Initialize(game1.GraphicsDevice.Viewport, game1.pamBossProjectile, Position, target, 8); // Add Texture to game1
                            target = new Vector2(Position.X - 1000, Position.Y - 1000);
                            projectile2.Initialize(game1.GraphicsDevice.Viewport, game1.pamBossProjectile, Position, target, 8); // Add Texture to game1
                            target = new Vector2(Position.X + 1000, Position.Y - 1000);
                            projectile3.Initialize(game1.GraphicsDevice.Viewport, game1.pamBossProjectile, Position, target, 8); // Add Texture to game1
                            target = new Vector2(Position.X - 1000, Position.Y + 1000);
                            projectile4.Initialize(game1.GraphicsDevice.Viewport, game1.pamBossProjectile, Position, target, 8); // Add Texture to game1
                            game1.pamBossProjectiles.Add(projectile1);
                            game1.pamBossProjectiles.Add(projectile2);
                            game1.pamBossProjectiles.Add(projectile3);
                            game1.pamBossProjectiles.Add(projectile4);
                            attackCount = 2;
                            //shootCounter++;


                        }
                        else if (attackCount == 2) //swirl hopefully
                        //should adjust by a counter that increments by 1 or 5

                            //Scratch that, add a random element to target

                        {
                            x = Rando.Next(0,1280); y = Rando.Next(0,640);
                            target = new Vector2(Position.X + x, Position.Y + y);
                            projectile1.Initialize(game1.GraphicsDevice.Viewport, game1.pamBossProjectile, Position, target, 8); // Add Texture to game1
                            x = Rando.Next(0, 1280); y = Rando.Next(0, 640);
                            target = new Vector2(Position.X - x, Position.Y - y);
                            projectile2.Initialize(game1.GraphicsDevice.Viewport, game1.pamBossProjectile, Position, target, 8); // Add Texture to game1
                            x = Rando.Next(0, 1280); y = Rando.Next(0, 640);
                            target = new Vector2(Position.X + x, Position.Y - y);
                            projectile3.Initialize(game1.GraphicsDevice.Viewport, game1.pamBossProjectile, Position, target, 8); // Add Texture to game1
                            x = Rando.Next(0, 1280); y = Rando.Next(0, 640);
                            target = new Vector2(Position.X - x, Position.Y + y);
                            projectile4.Initialize(game1.GraphicsDevice.Viewport, game1.pamBossProjectile, Position, target, 8); // Add Texture to game1
                            game1.pamBossProjectiles.Add(projectile1);
                            game1.pamBossProjectiles.Add(projectile2);
                            game1.pamBossProjectiles.Add(projectile3);
                            game1.pamBossProjectiles.Add(projectile4);
                            attackCount = 0;
                            //shootCounter++;
                            x += 5;
                            y += 5;
                            if (x > 100)
                            {
                                x = -100;
                            }
                            if (y > 100)
                            {
                                y = -100;
                            }
                        }
                    }
                    count++;
                    if (shootCounter >= 10)
                    {
                        shootStop = true;

                    }
                }

                else // slow crawl after player
                {
                    if (shootCounter >= 1)
                    {
                        shootCounter = 0;
                        count = 0;
                        game1.BossSpawnTime = gameTime.TotalGameTime;
                        shootStop = false;
                    }
                    if (attackCount > 50)
                        attackCount = 0;
                    //Action = "Moving";
                    if (Position.X > game1.player.Position.X)
                    {
                        Position.X -= BossMoveSpeed;
                        Action = "Down";
                    }
                    else
                    {
                        Position.X += BossMoveSpeed;
                        Action = "Up";
                    }
                    if (Position.Y > game1.player.Position.Y)
                    {
                        Position.Y -= BossMoveSpeed / 2;
                        Action = "Right";
                    }
                    else
                    {
                        Position.Y += BossMoveSpeed / 2;
                        Action = "Left";
                    }
                    PamBossAnimation.Position = Position;
                    //Action = "Moving"
                } // End Else Timer


            }
            else
            {

                Action = "Dying";
                game1.GameState = "Victory";
                if (dyingStop == true)
                {
                    game1.BossKill = true;
                    game1.BossSpawn = false;
                    game1.score += Value;
                    Active = false;

                }
            }
            PamBossAnimation.Update(gameTime, Action, Health, Position);
        }
       

        public void Draw(SpriteBatch spriteBatch)
        {
            PamBossAnimation.Draw(spriteBatch);
        }
    }
}
