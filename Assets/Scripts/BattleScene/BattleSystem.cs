using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] private Transform pfPlayer;
    [SerializeField] private Transform pfEnemy;

    private static BattleSystem instance;
    private PlayerController playerController;
    private PlayerStatus playerStatus;
    private EnemyController enemyController;
    private EnemyStatus enemyStatus;
    private State state;

    private BattleSystem()
    {
        if (instance == null) instance = this;
    }

    public static BattleSystem GetInstance()
    {
        return new BattleSystem();
    }

    private enum State{
        PLAYER_TURN,
        BUSY,
        ENEMY_TURN,
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
        state = State.PLAYER_TURN;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && state == State.PLAYER_TURN)
        {
            state = State.BUSY;
            int amount = GetDamageAmount(playerStatus, enemyStatus);
            playerController.Attack(enemyController.transform, () => { enemyController.TakeDamage(amount, EnemyTurn); });
        }

        if (state == State.ENEMY_TURN)
        {
            state = State.BUSY;
            int amount = GetDamageAmount(enemyStatus, playerStatus);
            enemyController.Attack(playerController.transform, () => { playerController.TakeDamage(amount, PlayerTurn); });
        }

        //for testing
        if (Input.GetKeyDown(KeyCode.D))
        {
            state = State.BUSY;
            enemyController.TakeDamage(0, null);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            state = State.BUSY;
            enemyController.Attack(playerController.transform, null);
        }
    }

    private void PlayerTurn()
    {
        Debug.Log("entering player turn");
        state = State.PLAYER_TURN;
    }

    private void EnemyTurn()
    {
        Debug.Log("entering enemy turn");
        state = State.ENEMY_TURN;
    }

    private PlayerController SpawnPlayer(Vector3 position)
    {
        Transform charactor = Instantiate(pfPlayer, position, Quaternion.identity);
        playerStatus = charactor.GetComponent<PlayerStatus>();
        return charactor.GetComponent<PlayerController>();
    }

    private EnemyController SpawnEnemy(Vector3 position)
    {
        Transform charactor = Instantiate(pfEnemy, position, Quaternion.identity);
        enemyStatus = charactor.GetComponent<EnemyStatus>();
        return charactor.GetComponent<EnemyController>();
    }

    private int GetDamageAmount(EntityStatus source, EntityStatus target)
    {
        return source.damage - target.defence;
    }
}
