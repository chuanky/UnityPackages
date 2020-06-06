using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    public Action onAnimFinished;
    public Action onMoveFinished;

    private Animator animator;
    private String defaultClipName;

    private Vector3 targetPos;
    private float moveSpeed;
    private bool moveEventFired;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        defaultClipName = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        targetPos = transform.position;
        moveSpeed = 0f;
        moveEventFired = false;
    }

    public void PlayAnim(string clipName, Action onAnimFinished)
    {
        this.onAnimFinished = onAnimFinished;
        animator.Play(clipName, 0, 0);
    }

    public void MoveAnim(Vector3 targetPos, float moveSpeed, Action onMoveFinished)
    {
        this.targetPos = targetPos;
        this.moveSpeed = moveSpeed;
        this.onMoveFinished = onMoveFinished;
        moveEventFired = false;

        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            if (onAnimFinished != null)
            {
                onAnimFinished.Invoke();
            } else
            {
                animator.Play(defaultClipName, 0, 0);
            }
        }

        if (transform.position == targetPos && moveEventFired == false)
        {
            moveEventFired = true;
            onMoveFinished?.Invoke();
        }

        if (transform.position != targetPos)
        {
            MoveAnim(targetPos, moveSpeed, onMoveFinished);
        }
    }


}
