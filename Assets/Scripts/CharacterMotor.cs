using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMotor : MonoBehaviour
{

    public int playerNumber = 1;

    public string HorizontalAxis = "Horizontal";
    public string VerticalAxis = "Vertical";

    public bool grounded;

    private Rigidbody2D rb;
    private Vector2 acceleration;
    private Vector2 velocity;
    public Transform gravityPoint;
    public float gravity = 40f;
    public bool jumping;


    public LayerMask affectedBy;
    public float walkSpeed = 100f;
    public float jumpSpeed = 100f;
    public float friction = 0.5f;


    private CharacterController2D cc;

    // Use this for initialization
    void Start()
    {
        cc = GetComponent<CharacterController2D>();
    }

    private void Update()
    {
        acceleration = Vector2.zero;
        Vector2 planetNormal = transform.position - gravityPoint.position;
        planetNormal.Normalize();
        acceleration.x = Input.GetAxis(HorizontalAxis) * walkSpeed;
        Quaternion newRot = Quaternion.FromToRotation(transform.up, planetNormal) * transform.rotation;
        transform.rotation = newRot;

        if(!cc.isGrounded)
        acceleration.y = -gravity;

        transform.rotation = Quaternion.FromToRotation(-Vector3.up, gravityPoint.position - transform.position);


        cc.Move(acceleration * Time.deltaTime);
    }

    public void AddForce(Vector2 forceDirection)
    {
        //TODO: This method
    }


}

