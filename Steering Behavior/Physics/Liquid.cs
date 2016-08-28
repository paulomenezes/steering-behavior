using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steering_Behavior.Physics
{
    class Liquid
    {
        public float x, y, w, h, c;

        public Liquid(float x, float y, float w, float h, float c)
        {
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;
            this.c = c;
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            spriteBatch.Draw(texture, new Rectangle((int)x, (int)y, (int)w, (int)h), Color.LightGray);
        }
    }
}
