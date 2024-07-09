using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody body;
    public float speed = 12f;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 3f;
    public CapsuleCollider cb;
    public float groundDistance = 0.4f;
    [SerializeField]
    private float rotateSpeed = 10f;

    public PlayerState playerState;

    private Vector3 velocity;

    private bool isGrounded;

    private void Awake()
    {
        if (!playerState)
        {
            playerState = GetComponent<PlayerState>();
        }
    }

    void Update()
    {
        Ray ray = new Ray(cb.transform.position, Vector3.down);
        RaycastHit hit;
        isGrounded = Physics.Raycast(ray, out hit, groundDistance);

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 newVelocity = Vector3.zero;
        if (x != 0 || z != 0)
        {
            Vector3 move = transform.right * x + transform.forward * z;
            newVelocity = new Vector3(move.x * speed, body.velocity.y, move.z * speed);
            body.transform.forward = Vector3.Slerp(body.transform.forward, newVelocity, rotateSpeed * Time.deltaTime);

        }
        playerState.isWalking = newVelocity != Vector3.zero;
        /*if (isGrounded && Input.GetAxis("Jump") > 0)
            {
                newVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }*/

        body.velocity = newVelocity;
        // 应用重力
        if (!isGrounded)
        {
            body.velocity += Vector3.up * gravity * Time.deltaTime;
        }

    }
}
