using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleObserver : MonoBehaviour
{
    List<EnemyStatus> subscribers;
    public int EnemyCount { get; set; }

    private void Awake()
    {
        subscribers = new List<EnemyStatus>();
        EnemyCount = 0;
    }

    public bool BattleEnd()
    {
        return EnemyCount == 0;
    }

    public void addSubscriber(EnemyStatus enemyStatus)
    {
        subscribers.Add(enemyStatus);
    }
}
