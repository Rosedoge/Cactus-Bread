using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Prototype_Menues_etc
{
    public class PowerUp
    {
        public PowerUpAnimation PowerUpAnimation;
        // Image representing the PowerUp
        public Texture2D Texture;
        Random Rando = new Random();
        // Position of the PowerUp relative to the upper left side of the screen
        public Vector2 Position;
        TimeSpan TotalTime;
        TimeSpan SpawnTime;
        // The PowerUp's name
        public string Name;
        public bool Active = true;
        
        public int Width
        {
            get { return PowerUpAnimation.spriteWidth; }
        }

        // Get the height of the PowerUp
        public int Height
        {
            get { return PowerUpAnimation.spriteHeight; }
        }

        // Determines how fast the PowerUp



        public void Initialize(PowerUpAnimation animation, Vector2 position, GameTime gameTime)
        {
            /// <summary>
            /// Places the object and  determines what type it is
            /// Uses Name as Type
            /// </summary>
            PowerUpAnimation = animation;
            Position = position;
            DetermineType();
            SpawnTime = gameTime.TotalGameTime;
            TotalTime = TimeSpan.FromSeconds(7.0f);

            
        }
        void DetermineType()
        {
            /// <summary>
            /// Determines the name of the power up, therefore type
            /// 
            /// </summary>
            int type = Rando.Next(0, 2);
            switch (type)
            {
                case 0:
                    Name = "Ammo";
                    break;
                case 1:
                    Name = "Health";
                    break;

            }
             
        }


        public void Update(GameTime gameTime)
        {
            /// <summary>
            /// Makes sure the Power Up fades at the right time
            /// </summary>
            /// 
            // 
            if (gameTime.TotalGameTime - SpawnTime > TotalTime)
            {
                SpawnTime = gameTime.TotalGameTime;
                Active = false;

            }
            PowerUpAnimation.Update(Name, Position);
        }
        //Will get called by the main game Collision
        public void PickUp(Game1 game1)
        {

            switch (Name)
            {
                case "Ammo":
                    //Gives 30 ammo to the maingun and 15 to the sub weapon
                    if (game1.CurGun == "winchester")
                    {
                        game1.WinAmmo += 30;
                        game1.RevolverAmmo += 15;
                    }
                    else
                    {
                        game1.WinAmmo += 15;
                        game1.RevolverAmmo += 30;
                    }
                    break;
                case "Health":
                    if (game1.player.Health <= 50)
                    {
                        game1.player.Health += 50;
                    }
                    else
                        game1.player.Health = 100;
                    break;

            }
            


            //Ends with Active = False;
            Active = false;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            PowerUpAnimation.Draw(spriteBatch);
           // spriteBatch.Draw(Texture, Position, null, Color.White, 0f,
            //new Vector2(Width, Height), 1f, SpriteEffects.None, 0f); //Height and Width normally /2
        }
    }
}
