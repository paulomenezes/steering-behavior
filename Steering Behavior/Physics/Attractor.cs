using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steering_Behavior.Physics
{
    class Attractor
    {
        Texture2D texture;

        public float mass;
        public Vector2 location;

        float G = 0.4f;

        public Attractor()
        {
            location = new Vector2(Game1.WIDTH / 2, Game1.HEIGHT / 2);
            mass = 20;

            texture = Game1.CreateCircle((int)mass);
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, location, Color.LightGray);
        }
    }
}
