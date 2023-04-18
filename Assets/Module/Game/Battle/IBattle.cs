using System.Collections;
using System.Collections.Generic;

namespace Module.Game
{
    internal interface IBattle
    {
        IEnumerable<Entity> GetUnits();
    }
}