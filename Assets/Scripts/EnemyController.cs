using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyController : MonoBehaviour
{
    public float speed;

    private Vector3 initPosition;
    private Vector3 targetPosition;
    private AnimController animController;

    // Start is called before the first frame update
    void Start()
    {
        speed = 10f;
        initPosition = transform.position;
        targetPosition = transform.position;
        animController = GetComponent<AnimController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Attack(Transform target, Action OnAttackFinished)
    {
        targetPosition = target.position + new Vector3(1, 0, 0);

        animController.MoveAnim(targetPosition, speed, () =>
        {
            animController.PlayAnim("Enemy_Attack", () =>
            {
                animController.PlayAnim("Enemy_Idle", null);
                animController.MoveAnim(initPosition, speed, () =>
                {
                    Debug.Log("enemy attack finished");
                    OnAttackFinished?.Invoke();
                });
            });
        });
    }

    public void TakeDamage(Action OnTakeDamageFinished)
    {
        animController.PlayAnim("Enemy_TakeDmg", () => {
            animController.PlayAnim("Enemy_Idle", OnTakeDamageFinished);
        });
    }
}
