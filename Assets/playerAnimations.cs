using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationString
{
    public static string IsWalking = "IsWalking";
}

public class playerAnimations : MonoBehaviour
{
    private Animator anim;
    public PlayerState playerState;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        if (!playerState)
        {
            playerState = GetComponent<PlayerState>();
        }
    }

    public void ChangeIdle()
    {
        anim.SetBool(AnimationString.IsWalking, playerState.isWalking);
    }

    public void ChangeWalk()
    {
        anim.SetBool(AnimationString.IsWalking, playerState.isWalking);
    }

    private void Update()
    {
        if (playerState.isWalking)
        {
            ChangeWalk();
        }
        else
        {
            ChangeIdle();
        }
    }
}
