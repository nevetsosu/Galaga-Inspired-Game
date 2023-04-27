using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mob : Entity
{
    public abstract void take_damage(int damage);
    public abstract void attack();
}
