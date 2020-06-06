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
    private bool moveEventFired;

    private AnimatorData animatorData;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        defaultClipName = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        animatorData = GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<AnimatorData>();
        targetPos = transform.position;
        moveEventFired = false;
    }

    public void PlayAnim(string clipName, Action onAnimFinished)
    {
        this.onAnimFinished = onAnimFinished;
        animator.speed = animatorData.speedRate;
        animator.Play(clipName, 0, 0);
    }

    public void MoveAnim(Vector3 targetPos, Action onMoveFinished)
    {
        this.targetPos = targetPos;
        this.onMoveFinished = onMoveFinished;
        moveEventFired = false;

        float speed = animatorData.moveSpeed * animatorData.speedRate * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed);
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
            MoveAnim(targetPos, onMoveFinished);
        }
    }


}
