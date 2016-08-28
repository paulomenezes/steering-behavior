using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steering_Behavior
{
    class Unit : MovingEntity
    {
        public SteeringManager steering;

        public Unit(Vector2 pos, float totalMass) : base(pos, 6, new Vector2(0, -2), 150, new Vector2(1), totalMass, new Vector2(3), 1, 0.1)
        {
            this.Position = pos;

            Mass = totalMass;
            steering = new SteeringManager(this);
        }

        public void Update(float timeElapsed, Vector2 target)
        {
            Vector2 steeringForce = steering.steering;

            Vector2 acceleration = Vector2.Divide(steeringForce, (float)Mass);

            Velocity += acceleration * timeElapsed;
            Velocity = Truncate(Velocity, MaxSpeed);

            Position += Velocity * timeElapsed;

            if (Velocity.LengthSquared() > 0.00000001)
            {
                Heading = Vector2.Normalize(Velocity);
                Side = new Vector2(-Heading.Y, Heading.X);
            }

            if (Position.X < 0)
                Position = new Vector2(Game1.WIDTH, Position.Y);
            else if (Position.X > Game1.WIDTH)
                Position = new Vector2(0, Position.Y);

            if (Position.Y < 0)
                Position = new Vector2(Position.X, Game1.HEIGHT);
            else if (Position.Y > Game1.HEIGHT)
                Position = new Vector2(Position.X, 0);
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture, Color color)
        {
            spriteBatch.Draw(texture, Position, new Rectangle(0, 0, 10, 10), color);

            if (steering.wanderTarget != Vector2.Zero)
                spriteBatch.Draw(texture, steering.wanderTarget, new Rectangle(0, 0, 5, 5), Color.White);
        }

        private Vector2 Truncate(Vector2 v, double max)
        {
            double i = max / v.Length();
            i = i < 1.0 ? i : 1.0;

            return Vector2.Multiply(v, (float)i);
        }
    }
}
