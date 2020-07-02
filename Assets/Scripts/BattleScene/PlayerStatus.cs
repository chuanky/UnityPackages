using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : EntityStatus
{
    void Awake()
    {
        damage = 80;
        defence = 20;
    }
}
