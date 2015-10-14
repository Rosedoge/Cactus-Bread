using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
namespace Prototype_Menues_etc
{
    class EnemyAnimation 
    {
        Texture2D spriteTexture;
        float timer = 0f;
        float interval = 400f;
        public int currentFrame = 0;
        public int spriteWidth = 50;
        public int spriteHeight = 50;
        int timesDead = 0;
        // Position;
        Rectangle sourceRect;
        Vector2 position;
        Vector2 origin;
        bool Active;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Vector2 Origin
        {
            get { return origin; }
            set { origin = value; }
        }

        public Texture2D Texture
        {
            get { return spriteTexture; }
            set { spriteTexture = value; }
        }

        public Rectangle SourceRect
        {
            get { return sourceRect; }
            set { sourceRect = value; }
        }


        public void Initialize(Texture2D texture, int currentFrame, int spriteWidth, int spriteHeight)
        {
            this.spriteTexture = texture;
            this.currentFrame = currentFrame;
            this.spriteWidth = spriteWidth;
            this.spriteHeight = spriteHeight;
            this.Active = true;
        }

        public void Update(GameTime gameTime, string Action, Vector2 Position, Game1 game1)
        {
            if (Active != false)
            {
                this.position = Position;
                sourceRect = new Rectangle(currentFrame * spriteWidth, 0, spriteWidth, spriteHeight);

                if (Action == "Dying")
                {
                    if (timesDead == 0)
                    {
                        game1.score += 100;
                        timesDead = 1;
                    }

                    interval = 1000f;
                    if (currentFrame < 6)
                        currentFrame = 6;
                    timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                    if (timer > interval)
                    {

                        currentFrame++;

                        if (currentFrame > 7)
                        {
                            //game1.score += 100;  
                            Active = false;
                            
                        }
                        timer = 0f;
                    }
                }
                else if (Action == "Dodging")
                {
                    if (currentFrame < 3 && currentFrame > 5)
                    {
                        currentFrame = 3;
                    }
                    timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    interval = 100f;
                    if (timer > interval)
                    {

                        currentFrame++;

                        if (currentFrame >= 5)
                        {
                            currentFrame = 3;
                        }
                        timer = 0f;
                    }
                }
                else if (Action == "Moving")
                {
                    if (currentFrame > 2)
                    {
                        currentFrame = 0;
                    }
                    timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                    if (timer > interval)
                    {

                        currentFrame++;

                        if (currentFrame >= 3)
                        {
                            currentFrame = 0;
                        }
                        timer = 0f;
                    }

                } //end Action Ifs

                //sourceRect.Y = 0;
                //sourceRect.X = currentFrame * 50;
                origin = new Vector2(sourceRect.Width / 2, sourceRect.Height / 2);
            }
        }//End Initialize

        public void Draw(SpriteBatch spriteBatch)
        {
            // Only draw the animation when we are active
            if (Active)
            {
                spriteBatch.Draw(spriteTexture, position, sourceRect, Color.White, 0f, Origin, 1.0f, SpriteEffects.None, 0);
            }
        }
    }
}
