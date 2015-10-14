using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Prototype_Menues_etc
{
    public class SniperEnemyAnimation
    {
        Texture2D spriteTexture;
        float timer = 0f;
        float interval = 400f;
        public int currentFrame = 0;
        public int spriteWidth = 50;
        public int spriteHeight = 100;
        
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

        public void Update(GameTime gameTime, string Action, Vector2 Position)
        {
            if (Active != false)
            {
                this.position = Position;
                sourceRect = new Rectangle(currentFrame * spriteWidth, 0, spriteWidth, spriteHeight);
                if (Action == "Dying")
                { 
                    if (currentFrame != 2)
                        currentFrame = 2;
                    timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                    if (timer > interval)
                    {
                        currentFrame++;
                        if (currentFrame > 2)
                        {
                            Active = false;
                        }
                        timer = 0f;
                    }
                }
                else if (Action == "Shooting")
                {
                    if (currentFrame != 1)
                    {
                        currentFrame = 1;
                    }
                }
                else if (Action == "Moving")
                {
                    if (currentFrame != 0)
                    {
                        currentFrame = 0;
                    }
                } //end Action Ifs

                //sourceRect.Y = 0;
                //sourceRect.X = currentFrame * 50;
                origin = new Vector2(sourceRect.Width / 2, sourceRect.Height / 2);

            }
        }

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