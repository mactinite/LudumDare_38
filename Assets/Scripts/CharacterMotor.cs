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
    public Vector2 velocity;
    public Transform gravityPoint;
    public float gravity = 40f;
    public bool jumping;


    public LayerMask affectedBy;
    public float walkSpeed = 100f;
    public float jumpSpeed = 100f;
    public float friction = 0.5f;
    public float drag = 0.5f;
    public Vector2 maxVelocity;
    public Vector2 minVelocity;


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
        acceleration.x = Input.GetAxisRaw(HorizontalAxis) * walkSpeed;
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
        if (jumping && cc.isGrounded)
        {
            jumping = false;
        }

        if (Input.GetButtonDown("Jump") && cc.isGrounded)
        {
             acceleration.y = gravity + jumpSpeed;
            jumping = true;
        }



        
        velocity += acceleration;
        velocity.x = Mathf.Clamp(velocity.x, minVelocity.x, maxVelocity.x);
        velocity.y = Mathf.Clamp(velocity.y, minVelocity.y, maxVelocity.y);

        if (velocity.x > 0.1f)
        {
            velocity.x -= friction * Time.deltaTime;
        }
        else if (velocity.x < -0.1f)
        {
            velocity.x += friction * Time.deltaTime;
        }
        else
        {
            velocity.x = 0;
        }

        if (velocity.y > Mathf.Epsilon)
        {
            velocity.y -= drag * Time.deltaTime;
        }
        else if (velocity.y < -Mathf.Epsilon)
        {
            velocity.y += drag * Time.deltaTime;
        }
        else
        {
            velocity.y = 0;
        }

        cc.Move(velocity * Time.deltaTime);
    }

    public void AddForce(Vector2 forceDirection)
    {
        velocity += (Vector2)(transform.up * gravity) + forceDirection;
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Explosion"))
        {
            AddForce(transform.up * 35f);
            Debug.DrawRay(collider.transform.position, transform.up  * 35f);
        }
    }


}

