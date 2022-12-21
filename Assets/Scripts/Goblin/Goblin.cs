using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Monster
{
    public override MonsterType GetMonsterType()
    {
        return MonsterType.Goblin;
    }
}
