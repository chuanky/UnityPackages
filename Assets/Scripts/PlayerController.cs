using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    private Vector3 targetPosition;
    private AnimController animController;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = transform.position;
        animController = GetComponent<AnimController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Attack(Transform target, Action OnAttackFinished)
    {
        Vector3 initPosition = transform.position;
        targetPosition = target.position - new Vector3(1, 0, 0);

        animController.MoveAnim(targetPosition, () =>
        {
            Debug.Log("move towards enemy finished");
            animController.PlayAnim("Player_Attack", () =>
            {
                Debug.Log("attack finished");
                animController.PlayAnim("Player_Idle", null);
                animController.MoveAnim(initPosition, () =>
                {
                    Debug.Log("move back finished");
                    OnAttackFinished?.Invoke();
                });
            });
        });
    }
}
