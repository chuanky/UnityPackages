using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyController : MonoBehaviour
{
    private Vector3 initPosition;
    private Vector3 targetPosition;
    private AnimController animController;
    private EnemyStatus enemyStatus;

    // Start is called before the first frame update
    void Start()
    {
        initPosition = transform.position;
        targetPosition = transform.position;
        animController = GetComponent<AnimController>();
        enemyStatus = GetComponent<EnemyStatus>();
    }

    public void Attack(Transform target, Action OnAttackFinished)
    {
        targetPosition = target.position + new Vector3(1, 0, 0);

        animController.MoveAnim(targetPosition, () =>
        {
            animController.PlayAnim("Enemy_Attack", () =>
            {
                animController.PlayAnim("Enemy_Idle", null);
                animController.MoveAnim(initPosition, () =>
                {
                    OnAttackFinished?.Invoke();
                });
            });
        });
    }

    public void TakeDamage(int amount, Action OnTakeDamageFinished)
    {
        animController.PlayAnim("Enemy_TakeDmg", () => {
            enemyStatus.TakeDamage(amount);
            OnTakeDamageFinished?.Invoke();
            if (enemyStatus.ShouldDie())
            {
                Die();
            } else
            {
                animController.PlayAnim("Enemy_Idle", null);
            }
        });
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
