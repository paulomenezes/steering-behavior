using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steering_Behavior
{
    enum Decelaration
    {
        slow = 3,
        normal = 2,
        fast = 1
    }

    class SteeringManager
    {
        public Vector2 steering;
        public MovingEntity vehicle;

        public float wanderDistance = 10;
        public float wanderRadius = 5;
        public float wanderJitter = 2;
        public Vector2 wanderTarget;

        private Random random;

        public SteeringManager(MovingEntity vehicle)
        {
            this.vehicle = vehicle;
            this.steering = Vector2.Zero;

            this.wanderTarget = Vector2.Zero;
            random = new Random();
        }

        public void Seek(Vector2 target)
        {
            steering += DoSeek(target);
        }

        public void Flee(Vector2 target)
        {
            steering += DoFlee(target);
        }

        public void Wander()
        {
            steering += DoWander();
        }

        public void Evade(MovingEntity target)
        {
            steering -= DoEvade(target);
        }

        public void Pursuit(MovingEntity target)
        {
            steering += DoPursuit(target);
        }

        public void Arrive(Vector2 target, Decelaration decelaration)
        {
            steering += DoArrive(target, decelaration);
        }

        public void Update()
        {

        }

        public void Reset() { }
        
        public Vector2 DoSeek(Vector2 target)
        {
            Vector2 desiredVelocity = Vector2.Normalize(target - vehicle.Position);
            desiredVelocity *= new Vector2((float)vehicle.MaxSpeed);

            return desiredVelocity - vehicle.Velocity;
        }

        public Vector2 DoFlee(Vector2 target)
        {
            double PanicDistanceSq = 100 * 100;

            if (Vector2.DistanceSquared(vehicle.Position, target) > PanicDistanceSq)
                return Vector2.Zero;

            Vector2 desiredVelocity = Vector2.Normalize(vehicle.Position - target);
            desiredVelocity *= new Vector2((float)vehicle.MaxSpeed);

            return desiredVelocity - vehicle.Velocity;
        }

        public Vector2 DoWander()
        {
            //float r1 = -1 + 2 * (float)random.NextDouble();
            //float r2 = -1 + 2 * (float)random.NextDouble();

            //wanderTarget += new Vector2(r1 * wanderJitter, r2 * wanderJitter);
            //wanderTarget.Normalize();

            //wanderTarget *= wanderRadius;

            //Vector2 targetLocal = wanderTarget + new Vector2(wanderDistance, 0);

            //return targetLocal - vehicle.Position;

            Vector2 center = vehicle.Velocity;
            center.Normalize();
            center *= wanderDistance;

            Vector2 displacement = new Vector2(0, -1);
            displacement *= wanderDistance;

            displacement = SetAngle(displacement, wanderRadius);

            Random rnd = new Random();
            wanderRadius += (float)rnd.NextDouble() * wanderJitter - wanderJitter * 0.5f;

            Vector2 wanderForce = center + displacement;

            return wanderForce;
        }

        public Vector2 DoEvade(MovingEntity pursuer)
        {
            Vector2 ToPusuer = pursuer.Position - vehicle.Position;

            double LookAheadTime = ToPusuer.Length() / (vehicle.MaxSpeed + pursuer.Speed());
            
            return DoFlee(Vector2.Multiply(pursuer.Position + pursuer.Velocity, (float)LookAheadTime));
        }

        public Vector2 DoPursuit(MovingEntity evader)
        {
            Vector2 ToEvader = evader.Position - vehicle.Position;

            double RelativeHeading = Vector2.Dot(vehicle.Heading, evader.Heading);

            if (Vector2.Dot(ToEvader, vehicle.Heading) > 0 && RelativeHeading < -0.95)
                return DoSeek(evader.Position);

            double LookAheadTime = ToEvader.Length() / (vehicle.MaxSpeed + evader.Speed());

            return DoSeek(Vector2.Multiply(evader.Position + evader.Velocity, (float)LookAheadTime));
        }

        public Vector2 DoArrive(Vector2 target, Decelaration decelaration)
        {
            Vector2 ToTarget = target - vehicle.Position;

            double dist = ToTarget.Length();

            if (dist > 0)
            {
                double DecelerationTweker = 0.3;
                double speed = dist / ((double)decelaration * DecelerationTweker);
                speed = Math.Min(speed, vehicle.MaxSpeed);

                Vector2 desiredVelocity = Vector2.Multiply(ToTarget, (float)(speed / dist));

                return desiredVelocity - vehicle.Velocity;
            }

            return Vector2.Zero;
        }

        private Vector2 SetAngle(Vector2 vector, float value)
        {
            float len = vector.Length();

            return new Vector2((float)Math.Cos(value) * len, (float)Math.Sin(value) * len);
        }

        public Vector2 Truncate(Vector2 vector, float max)
        {
            float i = 0;
            i = max / vector.Length();
            i = i < 1.0f ? i : 1.0f;

            vector *= i;

            return vector;
        }
    }
}
