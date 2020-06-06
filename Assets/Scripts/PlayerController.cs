using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    private Vector3 initPosition;
    private Vector3 targetPosition;
    private AnimController animController;

    // Start is called before the first frame update
    void Start()
    {
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

    public void TakeDamage(Action OnTakeDamageFinished)
    {
        animController.PlayAnim("Player_TakeDmg", () => {
            animController.PlayAnim("Player_Idle", OnTakeDamageFinished);
        });
    }
}
