using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Monster
{
    /// <summary>
    /// this is a class that right now only just returns the type of monster it is
    /// but it can be used to further go into depth with enemy types for example the Skeleton could have a special trait where they can randomly come back to life after dying
    /// so basically its used for Skeleton specific methods for example a crumbling of bones particle effect or some other game mechanic maybe
    /// </summary>
    /// <returns></returns>
    public override MonsterType GetMonsterType()
    {
        return MonsterType.Skeleton;
    }
}
