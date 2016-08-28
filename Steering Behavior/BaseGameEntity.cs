using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steering_Behavior
{
    class BaseGameEntity
    {
        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private int entityType;

        public int EntityType
        {
            get { return entityType; }
            set { entityType = value; }
        }

        private bool tag;

        public bool Tag
        {
            get { return tag; }
            set { tag = value; }
        }

        private Vector2 pos;

        public Vector2 Position
        {
            get { return pos; }
            set { pos = value; }
        }

        private Vector2 scale;

        public Vector2 Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        private double boundingRadius;

        public double BoundingRadius
        {
            get { return boundingRadius; }
            set { boundingRadius = value; }
        }

        private int NextValidID()
        {
            id = 0;
            return id++;
        }

        public bool IsTagget() { return tag; }

        public void SetScale(Vector2 val)
        {
            boundingRadius *= Math.Max(val.X, val.Y) / Math.Max(scale.X, scale.Y);
            scale = val;
        }

        public void SetScale(float val)
        {
            boundingRadius *= (val / Math.Max(scale.X, scale.Y));
            scale = new Vector2(val, val);
        }

        public BaseGameEntity()
        {
            id = NextValidID();
            boundingRadius = 0;
            pos = Vector2.Zero;
            scale = Vector2.One;
            entityType = -1;
            tag = false;
        }

        public BaseGameEntity(int entityType)
        {
            id = NextValidID();
            boundingRadius = 0;
            pos = Vector2.Zero;
            scale = Vector2.One;
            this.entityType = entityType;
            tag = false;
        }

        public BaseGameEntity(int entityType, Vector2 pos, double r)
        {
            id = NextValidID();
            boundingRadius = r;
            this.pos = pos;
            scale = Vector2.One;
            this.entityType = entityType;
            tag = false;
        }

        public BaseGameEntity(int entityType, int forceID)
        {
            id = forceID;
            boundingRadius = 0;
            pos = Vector2.Zero;
            scale = Vector2.One;
            this.entityType = entityType;
            tag = false;
        }
    }
}
