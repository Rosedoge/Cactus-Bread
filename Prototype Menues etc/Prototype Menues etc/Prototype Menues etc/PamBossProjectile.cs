using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Prototype_Menues_etc
{
    public class PamBossProjectile
    {
        // Image representing the Projectile
        public Texture2D Texture;

        // Position of the Projectile relative to the upper left side of the screen
        public Vector2 Position;
        public Vector2 Target;
        Vector2 basePos;

        // State of the Projectile
        public bool Active;
        float time = 0;
        // The amount of damage the projectile can inflict to the player
        public int Damage;

        // Represents the viewable boundary of the game
        Viewport viewport;
        public Vector2 movement;
        Vector2 target;
        public int Width
        {
            get { return Texture.Width; }
        }

        // Get the height of the projectile ship
        public int Height
        {
            get { return Texture.Height; }
        }

        // Determines how fast the projectile moves
        float projectileMoveSpeed = 7;


        public void Initialize(Viewport viewport, Texture2D texture, Vector2 position, Vector2 Target, float movSpeed)
        {
            Position = position;
            basePos = position;
            Texture = texture;
            Position = position;
            this.viewport = viewport;
            target = Target;
            movement = Target - position;
            if (movement != Vector2.Zero)
                movement.Normalize();

            Active = true;
            projectileMoveSpeed = movSpeed;
            Damage = 20;

            //projectileMoveSpeed = 5f;
        }
        public void Update(GameTime gameTime)
        {
            Position += movement * projectileMoveSpeed;


        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color.White, 0f,
            new Vector2(Width, Height), 1f, SpriteEffects.None, 0f); //Height and Width normally /2
        }
    }
}

