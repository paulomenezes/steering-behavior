using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steering_Behavior.Physics
{
    class Mover
    {
        public Vector2 location;
        public Vector2 velocity;
        Vector2 acceleration;

        float radius;
        float maxspeed = 4;
        float maxforce = 0.1f;

        Texture2D texture;

        public Mover(float x, float y)
        {
            location = new Vector2(x, y);
            velocity = Vector2.Zero;
            acceleration = Vector2.Zero;

            radius = 6 * 2;
            texture = Game1.CreateCircle(6);
        }

        public void ApplyForce(Vector2 force)
        {
            acceleration += force;
        }

        public void Update()
        {
            velocity += acceleration;
            velocity = Limit(velocity, maxspeed);

            location += velocity;
            acceleration *= 0;

            CheckEdges();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, location, Color.Red);
        }

        public void Seek(Vector2 target)
        {
            Vector2 desired = target - location;
            desired.Normalize();
            desired *= maxspeed;

            Vector2 steer = desired - velocity;
            steer = Limit(steer, maxforce);

            ApplyForce(steer);
        }

        public void Flee(Vector2 target)
        {
            Vector2 desired = location - target;
            desired.Normalize();
            desired *= maxspeed;

            Vector2 steer = desired - velocity;
            steer = Limit(steer, maxforce);

            ApplyForce(steer);
        }

        private void CheckEdges()
        {
            if (location.X + radius > Game1.WIDTH)
            {
                location.X = Game1.WIDTH - radius;
                velocity.X *= -1;
            }
            else if (location.X < 0)
            {
                velocity.X *= -1;
                location.X = 0;
            }

            if (location.Y + radius > Game1.HEIGHT)
            {
                velocity.Y *= -1;
                location.Y = Game1.HEIGHT - radius;
            }
        }

        private Vector2 Limit(Vector2 v, float f)
        {
            v.X = Math.Min(v.X, f);
            v.Y = Math.Min(v.Y, f);

            return v;
        }
    }
}
