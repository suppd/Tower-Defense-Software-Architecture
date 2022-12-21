using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Monster
{
    public override MonsterType GetMonsterType()
    {
        return MonsterType.Zombie;
    }
}
