using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] private Transform pfPlayer;
    [SerializeField] private Transform pfEnemy;

    private static BattleSystem instance;
    private PlayerController playerController;
    private EnemyController enemyController;
    private State state;

    public static BattleSystem GetInstance()
    {
        return instance;
    }

    private enum State{
        WaitingForPlayer,
        Busy,
    }

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerController = SpawnPlayer(new Vector3(-4, 0, 0));
        enemyController = SpawnEnemy(new Vector3(1, 0, 0));
        state = State.WaitingForPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.WaitingForPlayer)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                state = State.Busy;
                playerController.Attack(enemyController);
            }
        }

        if (playerController.getAttackComplete() == true && state == State.Busy)
        {
            state = State.WaitingForPlayer;
        }
    }

    private PlayerController SpawnPlayer(Vector3 position)
    {
        Transform charactor = Instantiate(pfPlayer, position, Quaternion.identity);
        charactor.transform.localScale = new Vector3(-1, 1, 1);

        return charactor.GetComponent<PlayerController>();
    }

    private EnemyController SpawnEnemy(Vector3 position)
    {
        Transform charactor = Instantiate(pfEnemy, position, Quaternion.identity);
        return charactor.GetComponent<EnemyController>();
    }
}
