using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Prototype_Menues_etc
{
    public class Button
    {
       
        public Vector2 Position;
        public Texture2D Texture;
        bool Activated = false;
        //Set up variables
        public int Width
        {
            get { return Texture.Width; }
        }

        // Get the height of the enemy ship
        public int Height
        {
            get { return Texture.Height; }
        }


        public void Initialize(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;
        }
        public bool Update(Vector2 MosPos) //Called on Mouseclick When 
            //game is not in playmode
        {
            if ((MosPos.X >= Position.X && MosPos.Y >= Position.Y) && 
                (MosPos.X <= Position.X + Texture.Width && MosPos.Y <= Position.Y + Texture.Height))
            {
                Activated = true;
            }
            return Activated;
        }
        public void Draw(SpriteBatch spriteBatch) //Draw function, same as mousehandler one.
        {
            spriteBatch.Draw(Texture, Position, null, Color.White, 0f,
            new Vector2(Width, Height), 1f, SpriteEffects.None, 0f);
        }
    }
}