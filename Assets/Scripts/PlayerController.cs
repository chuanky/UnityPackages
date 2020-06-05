using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public float speed;

    private Vector3 targetPosition;
    private AnimController animController;

    private Action OnAttackFinished;
    // Start is called before the first frame update
    void Start()
    {
        speed = 10f;
        targetPosition = transform.position;
        animController = GetComponent<AnimController>();
        animController.PlayAnim("Player_Idle", null);
        animController.MoveAnim(targetPosition, speed, null);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Attack(Transform target, Action OnAttackFinished)
    {
        Vector3 initPosition = transform.position;
        targetPosition = target.position - new Vector3(1, 0, 0);

        animController.MoveAnim(targetPosition, speed, () =>
        {
            Debug.Log("move towards enemy finished");
            animController.PlayAnim("Player_Attack", () =>
            {
                Debug.Log("attack finished");
                animController.PlayAnim("Player_Idle", null);
                animController.MoveAnim(initPosition, speed, () =>
                {
                    Debug.Log("move back finished");
                    OnAttackFinished?.Invoke();
                });
            });
        });
    }
}
