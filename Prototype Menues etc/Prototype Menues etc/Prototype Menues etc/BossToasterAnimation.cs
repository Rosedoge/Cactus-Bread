using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Prototype_Menues_etc
{
    class BossToasterAnimation

    {
        Texture2D spriteTexture;
        float timer = 0f;
        float interval = 400f;
        public int currentFrame = 0;
        public int spriteWidth = 154; // Change
        public int spriteHeight = 151; // Change
        int timesDead = 0;
        // Position;
        Rectangle sourceRect;
        Vector2 position;
        Vector2 origin;
        bool Active;
        int row = 0;
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

        public void Update(GameTime gameTime, Vector2 position, string Action, Game1 game1, ref bool shootStop, ref bool dyingStop)
        {

            if (Active != false)
            {
                this.position = Position;
                sourceRect = new Rectangle(currentFrame * spriteWidth, row * spriteHeight, spriteWidth, spriteHeight);

                if (Action == "Moving")
                {
                    row = 0;
                    interval = 250f;
                    if (currentFrame >= 6)
                        currentFrame = 0;
                    timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                    if (timer > interval)
                    {

                        currentFrame++;

                        timer = 0f;
                    }
                }
                else if (Action == "Shooting")
                {
                    row = 1;
                    if (currentFrame < 0 || currentFrame > 13)
                    {
                        currentFrame = 0;
                    }
                    timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    interval = 200f;
                    if (timer > interval)
                    {

                        currentFrame++;

                        if (currentFrame >= 13)
                        {
                            currentFrame = 0;
                            shootStop = true;
                        }
                        timer = 0f;
                    }
                }
                else if (Action == "Dying")
                {
                    row = 2;
                    interval = 500f;
                    if (currentFrame >= 6)
                    {
                        currentFrame = 0;
                        dyingStop = true;
                    }
                            
                    timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                    if (timer > interval)
                    {

                        currentFrame++;

                        timer = 0f;
                    }



                }
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
