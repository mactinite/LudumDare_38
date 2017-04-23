using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMotor : MonoBehaviour
{
    // movement config
    public float gravity = -25f;
    public float runSpeed = 8f;
    public float groundDamping = 20f; // how fast do we change direction? higher means faster
    public float inAirDamping = 5f;
    public float jumpForce = 3f;

    [HideInInspector]
    private float normalizedHorizontalSpeed = 0;

    private CharacterController2D _controller;
    private Animator _animator;
    private RaycastHit2D _lastControllerColliderHit;
    public Vector2 velocity;
    private Vector2 acceleration;
    private Vector2 hitForce;
    public Transform gravityPoint;
    public float rotationDamping = 10f;
    public float friction = 5;


    public string HorizontalAxis = "Horizontal";
    public string VerticalAxis = "Vertical";
    public bool jumping;

    public Vector2 maxVelocity;
    bool isMoving = false;

    public bool dashing;
    public float dashSpeed = 25f;


    void Awake()
    {
        _animator = GetComponent<Animator>();
        _controller = GetComponent<CharacterController2D>();
    }


    // the Update loop contains a very simple example of moving the character around and controlling the animation
    void Update()
    {

        if (_controller.isGrounded)
        {
            acceleration = Vector2.zero;
            velocity.y = 0;
            dashing = false;
        }
        Vector2 planetNormal = transform.position - gravityPoint.position;
        planetNormal.Normalize();

        Quaternion newRot = Quaternion.FromToRotation(transform.up, planetNormal) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, newRot, Time.deltaTime * rotationDamping);


        acceleration.x = Input.GetAxisRaw(HorizontalAxis) * runSpeed;
        


        if (velocity.x > maxVelocity.x)
            velocity.x = maxVelocity.x;
        else if (velocity.x < -maxVelocity.x)
            velocity.x = -maxVelocity.x;

        if (velocity.y > maxVelocity.y)
            velocity.y = maxVelocity.y;
        else if (velocity.y < -maxVelocity.y)
            velocity.y = -maxVelocity.y;



        // Friction and Drag
        if (Mathf.Abs(Input.GetAxisRaw(HorizontalAxis)) < 0.001f)
        {
            if (velocity.x - (friction * Time.deltaTime) > 0)
                velocity.x -= friction * Time.deltaTime;
            else if (velocity.x + (friction * Time.deltaTime) < 0)
                velocity.x += friction * Time.deltaTime;
            else
                velocity.x = 0;
        }

        if (jumping)
        {
            jumping = false;
        }

        acceleration.y -= gravity * Time.deltaTime;

        if (Input.GetButton("Jump") && _controller.isGrounded && !jumping)
        {
            acceleration.y = Mathf.Sqrt(2f * jumpForce * gravity);
            jumping = true;
        }
        if (_controller.isGrounded)
        {
            velocity.y -= gravity * Time.deltaTime;
        }
        //HandleDash();
        if (hitForce.x != 0)
            acceleration.x += hitForce.x * Time.deltaTime;
        if (hitForce.y != 0)
            acceleration.y += hitForce.y * Time.deltaTime;
        hitForce = Vector2.zero;
        float dampingSpeed = _controller.isGrounded ? groundDamping : inAirDamping;
        velocity.x = Mathf.Lerp(velocity.x, velocity.x + acceleration.x, Time.deltaTime * dampingSpeed);
        velocity.y += acceleration.y;
        
        _controller.Move((velocity * Time.deltaTime));
        velocity = _controller.velocity;
    }


    void HandleDash()
    {
        if (Input.GetButtonDown("Fire1") && !dashing)
        {
            dashing = true;
            Vector2 dashForce = acceleration;
            AddForce(dashForce * dashSpeed);                                                                                                                                                                             
        }
    }


    public void AddForce(Vector2 forceVector)
    {
        hitForce = forceVector;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Explosion"))
        {
            Vector2 force = transform.position - collision.transform.position;
            Debug.Log(force.normalized * 1000f);
            AddForce(transform.InverseTransformDirection(force.normalized) * 5000f);
        }
    }

}
