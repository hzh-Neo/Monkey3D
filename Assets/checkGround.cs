using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkGround : MonoBehaviour
{
    public PlayerState playerState;
    public bool isGrounded;
    public float groundDistance = 0.4f;
    public CapsuleCollider cb;
    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(cb.transform.position, Vector3.down);
        RaycastHit hit;
        isGrounded = Physics.Raycast(ray, out hit, groundDistance);
        playerState.isGround = isGrounded;
    }
}
