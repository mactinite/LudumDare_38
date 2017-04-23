using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMotor : MonoBehaviour
{

    public int playerNumber = 1;

    public string HorizontalAxis = "Horizontal";
    public string VerticalAxis = "Vertical";

    public bool grounded;

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
        if(Input.GetAxisRaw(HorizontalAxis) > 0f)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if(Input.GetAxisRaw(HorizontalAxis) < 0f)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }

        Quaternion newRot = Quaternion.FromToRotation(transform.up, planetNormal) * transform.rotation;
        transform.rotation = newRot;

        acceleration.y = -gravity;

        if(Input.GetButtonDown("Jump") && cc.isGrounded)
        {
            acceleration.y = jumpSpeed;
            jumping = true;
        }

        if(jumping && cc.isGrounded)
        {
            jumping = false;
        }

        cc.Move(acceleration * Time.deltaTime);
    }

    public void AddForce(Vector2 forceDirection)
    {
        //TODO: This method
    }


}

