using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] private Transform pfPlayer;
    [SerializeField] private Transform pfEnemy;

    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = SpawnCharactor(true);
        SpawnCharactor(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerController.Attack();
        } 

        if (Input.GetKeyUp(KeyCode.Space))
        {
            playerController.StopAttack();
        }
    }

    private PlayerController SpawnCharactor(bool onPlayerSide)
    {
        Vector3 position;
        Transform charactor;
        if (onPlayerSide)
        {
            position = new Vector3(-4, 0, 0);
            charactor = Instantiate(pfPlayer, position, Quaternion.identity);
            charactor.transform.localScale = new Vector3(-1, 1, 1);
        } else
        {
            position = new Vector3(1, 0, 0);
            charactor = Instantiate(pfEnemy, position, Quaternion.identity);
        }

        return charactor.GetComponent<PlayerController>();
    }
}
