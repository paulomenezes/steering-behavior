using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steering_Behavior
{
    interface IBoid
    {
        Vector2 GetVelocity();
        float GetMaxVelocity();
        Vector2 GetPosition();
        float GetMass();
    }
}
