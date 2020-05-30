using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public float speed;

    private Animator animator;
    private Vector3 targetPosition;
    private Vector3 initPosition;
    private bool attackComplete;
    // Start is called before the first frame update
    void Start()
    {
        speed = 8f;
        initPosition = transform.position;
        targetPosition = transform.position;
        animator = GetComponent<Animator>();
        attackComplete = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetBool("Attacking") == true && attackComplete == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            if (transform.position == targetPosition) StopAttack();
        } else
        {
            transform.position = Vector3.MoveTowards(transform.position, initPosition, speed * Time.deltaTime);
            if (transform.position == initPosition) attackComplete = true;
        }
    }

    public void Attack(EnemyController enemyController)
    {
        attackComplete = false;
        targetPosition = enemyController.transform.position - new Vector3(1, 0, 0);
        animator.SetBool("Attacking", true);
    }

    public void StopAttack()
    {
        animator.SetBool("Attacking", false);
    }

    public bool getAttackComplete()
    {
        return attackComplete;
    }
}
