using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : EntityStatus
{
    void Awake()
    {
        damage = 40;
        defence = 20;
    }
}
