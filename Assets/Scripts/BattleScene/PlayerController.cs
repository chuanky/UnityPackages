using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    private Vector3 initPosition;
    private Vector3 targetPosition;
    private AnimController animController;
    private PlayerStatus playerStatus;

    // Start is called before the first frame update
    void Awake()
    {
        initPosition = transform.position;
        targetPosition = transform.position;
        animController = GetComponent<AnimController>();
        playerStatus = GetComponent<PlayerStatus>();
    }

    public void Attack(Transform target, Action OnAttackFinished)
    {
        targetPosition = target.position - new Vector3(1, 0, 0);

        animController.MoveAnim(targetPosition, () =>
        {
            animController.PlayAnim("Player_Attack", () =>
            {
                animController.PlayAnim("Player_Idle", null);
                animController.MoveAnim(initPosition, () =>
                {
                    OnAttackFinished?.Invoke();
                });
            });
        });
    }

    public void TakeDamage(int amount, Action OnTakeDamageFinished)
    {
        animController.PlayAnim("Player_TakeDmg", () => {
            playerStatus.TakeDamage(amount);
            animController.PlayAnim("Player_Idle", null);
            OnTakeDamageFinished?.Invoke();
        });
    }
}
