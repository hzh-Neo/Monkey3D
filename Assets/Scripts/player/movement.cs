using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody body;
    public float speed = 10f;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 3f;

    [SerializeField]
    private float rotateSpeed = 30f;

    [SerializeField]
    private PlayerState playerState;

    [SerializeField]
    private InputCotroller inputCotroller;

    private Vector3 velocity;

    private void Awake()
    {
        if (!playerState)
        {
            playerState = GetComponent<PlayerState>();
        }
    }

    void Update()
    {

        Vector3 newMove = inputCotroller.getMoveVector(speed);
        if (playerState.isGround && inputCotroller.isJump())
        {
            Vector3 jumpVector = new Vector3(0, Mathf.Sqrt(jumpHeight * -2f * gravity), 0);
            body.velocity = jumpVector;
        }
        if (newMove.x != 0 || newMove.z != 0)
        {
            body.transform.forward = Vector3.Slerp(body.transform.forward, newMove, rotateSpeed * Time.deltaTime);
            body.position += newMove;
        }
        playerState.isWalking = newMove != Vector3.zero;


        // 应用重力
        if (!playerState.isGround)
        {
            playerState.airY = body.velocity.y;
            body.velocity += Vector3.up * gravity * Time.deltaTime;
        }

    }
}
