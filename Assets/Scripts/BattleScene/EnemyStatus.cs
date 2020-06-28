using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : EntityStatus
{
    void Awake()
    {
        damage = 30;
        defence = 10;
    }
}
