using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steering_Behavior
{
    class MovingEntity : BaseGameEntity
    {
        private Vector2 velocity;

        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        private Vector2 heading;

        public Vector2 Heading
        {
            get { return heading; }
            set { heading = value; }
        }

        private Vector2 side;

        public Vector2 Side
        {
            get { return side; }
            set { side = value; }
        }

        private double mass;

        public double Mass
        {
            get { return mass; }
            set { mass = value; }
        }

        private double maxSpeed;

        public double MaxSpeed
        {
            get { return maxSpeed; }
            set { maxSpeed = value; }
        }

        private double maxForce;

        public double MaxForce
        {
            get { return maxForce; }
            set { maxForce = value; }
        }

        private double maxTurnRate;

        public double MaxTurnRate
        {
            get { return maxTurnRate; }
            set { maxTurnRate = value; }
        }

        public MovingEntity(Vector2 pos, double radius, Vector2 vel, double maxSpeed, 
                            Vector2 heading, double mass, Vector2 scale, double turnRate, 
                            double maxForce) : base(0, pos, radius)
        {
            this.velocity = vel;
            this.maxSpeed = maxSpeed;
            this.heading = heading;
            this.mass = mass;
            this.Scale = scale;
            this.maxTurnRate = turnRate;
            this.maxForce = maxForce;
        }

        public float Speed()
        {
            return velocity.Length();
        }

        public float SpeedSq()
        {
            return velocity.LengthSquared();
        }

        public bool IsSpeedMaxOut()
        {
            return MaxSpeed * MaxSpeed >= velocity.LengthSquared();
        }
    }
}
