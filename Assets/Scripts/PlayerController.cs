using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public float speed;

    private Vector3 targetPosition;
    private Vector3 initPosition;
    private AnimController animController;
    // Start is called before the first frame update
    void Start()
    {
        speed = 8f;
        initPosition = transform.position;
        targetPosition = transform.position;
        animController = GetComponent<AnimController>();
        animController.PlayAnim("Player_Idle", null);
        animController.MoveAnim(targetPosition, null);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Attack()
    {
        animController.PlayAnim("Player_Action", () => {
            Debug.Log("Attack Finished");
        });
    }
}
