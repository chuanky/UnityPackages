using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    public Action onAnimFinished;
    public Action onMoveFinished;
    private Animator animator;
    private float animTimer;
    private bool animEventFired;
    private Vector3 targetPos;
    private float moveSpeed;
    private bool moveEventFired;

    public void PlayAnim(string clipName, Action onAnimFinished)
    {
        this.animator = GetComponent<Animator>();
        this.animTimer = this.animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        this.onAnimFinished = onAnimFinished;
        this.animEventFired = false;

        this.animator.Play(clipName, 0, 0);
    }

    public void MoveAnim(Vector3 targetPos, float moveSpeed, Action onMoveFinished)
    {
        this.targetPos = targetPos;
        this.moveSpeed = moveSpeed;
        this.onMoveFinished = onMoveFinished;
        this.moveEventFired = false;

        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        animTimer = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        if (animTimer >= 1 && animEventFired == false)
        {
            animEventFired = true;
            onAnimFinished?.Invoke();
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
