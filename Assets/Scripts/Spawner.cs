using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private const int MaxNumberOfSpawners = 1;
    private const string SpawnerPrefabName = "Spawner";
    private MonsterFactory monsterFactory;

    private readonly Transform[] spawner = new Transform[MaxNumberOfSpawners];
    public Spawner(MonsterFactory pMonsterFactory)
    {
        monsterFactory = pMonsterFactory;
    }

    //public void SpawnMonsters()
    //{
    //    //Zombie zombie = monsterFactory.CreateZombie();
    //}

    Spawner waveOneSpawner = new Spawner(new WaveOneFactory());
    //waveOneSpawner.SpawnMonsters();
}


