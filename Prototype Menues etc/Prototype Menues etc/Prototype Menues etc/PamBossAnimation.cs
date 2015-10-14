using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Prototype_Menues_etc
{
    class PamBossAnimation
    {
        Texture2D spriteTexture;
        float timer = 0f;
        float interval = 200f;
        public int currentFrame = 0;
        public int spriteWidth = 50;
        public int spriteHeight = 100;

        // Position;
        Rectangle sourceRect;
        Vector2 position;
        Vector2 origin;
        bool Active;
        int Row = 0;
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
        public void Update(GameTime gameTime, String Action, int Health, Vector2 Position)
        {
            if (Active == false)
                return;
            this.position = Position;
            sourceRect = new Rectangle(currentFrame * spriteWidth, Row * spriteHeight, spriteWidth, spriteHeight);

            //Rectan = new Rectangle(100,
            timer += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (Action == "Up")
            {

                Row = 2;
                if (currentFrame < 5 && currentFrame > 9)
                {
                    currentFrame = 5;
                }
                if (timer > interval)
                {
                    currentFrame++;
                    if (currentFrame > 9)
                    {
                        currentFrame = 5;

                    }
                    timer = 0f;
                }

            }
            if (Action == "Down")
            {
                Row = 2;
                if (currentFrame < 0 && currentFrame > 4)
                {
                    currentFrame = 0;
                }
                if (timer > interval)
                {
                    currentFrame++;
                    if (currentFrame > 4)
                    {
                        currentFrame = 0;

                    }
                    timer = 0f;
                }
            }
            if (Health >= 900)
            {
                if (Action == "Left")
                {

                    Row = 1;
                    if (currentFrame < 5 && currentFrame > 9)
                    {
                        currentFrame = 4;
                    }
                    if (timer > interval)
                    {
                        currentFrame++;
                        if (currentFrame > 8)
                        {
                            currentFrame = 4;

                        }
                        timer = 0f;
                    }

                }
                if (Action == "Right")
                {
                    Row = 1;
                    if (currentFrame < 0 && currentFrame > 4)
                    {
                        currentFrame = 0;
                    }
                    if (timer > interval)
                    {
                        currentFrame++;
                        if (currentFrame > 3)
                        {
                            currentFrame = 0;

                        }
                        timer = 0f;
                    }

                }
            }
            else
            {
                if (Action == "Left")
                {

                    Row = 0;
                    if (currentFrame < 4 && currentFrame > 8)
                    {
                        currentFrame = 4;
                    }
                    if (timer > interval)
                    {
                        currentFrame++;
                        if (currentFrame > 8)
                        {
                            currentFrame = 4;

                        }
                        timer = 0f;
                    }

                }
                if (Action == "Right")
                {
                    Row = 0;
                    if (currentFrame < 0 && currentFrame > 3)
                    {
                        currentFrame = 0;
                    }
                    if (timer > interval)
                    {
                        currentFrame++;
                        if (currentFrame > 3)
                        {
                            currentFrame = 0;

                        }
                        timer = 0f;
                    }

                }

            }
            if (Action == "Dying")
            {
                Row = 3;
                if (currentFrame < 0 && currentFrame > 4)
                {
                    currentFrame = 0;
                }
                interval = 500f;
                if (timer > interval)
                {
                    currentFrame++;
                    if (currentFrame > 4) // Shouldn't loop though
                    {
                        currentFrame = 0;

                    }
                    timer = 0f;
                }

            }

            origin = new Vector2(sourceRect.Width / 2, sourceRect.Height / 2);
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
