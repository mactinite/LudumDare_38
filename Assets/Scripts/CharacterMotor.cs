using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMotor : MonoBehaviour {

    public int playerNumber = 1;

    public string HorizontalAxis = "Horizontal";
    public string VerticalAxis = "Vertical";

    public bool grounded;

    private Rigidbody2D rb;
    private Vector2 acceleration;
    public Transform gravityPoint;
    public float gravity = 40f;
    public bool jumping;


    public LayerMask affectedBy;
    public float walkSpeed = 100f;
    public float jumpSpeed = 100f;
    public float jumpTime = 0.5f;
    [Range(0,1)]
    public float skinWidth = 0.1f;
    public float rotationDamping = 15f;
    public float drag = 5f;


    public Vector2 maxVelocity;
    

    private Vector2 velocity;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();

    }

    private float jumpTimer; 

	// Update is called once per frame
	void FixedUpdate () {

        Gravity();
        //reset acceleration every frame
        acceleration = Vector2.zero;

        acceleration.x = Input.GetAxis(HorizontalAxis) * walkSpeed;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.FromToRotation(Vector3.up, transform.position - gravityPoint.position), Time.fixedDeltaTime * rotationDamping);
        



        if (grounded)
        {
            jumping = false;
            jumpTimer = jumpTime;
        }

        if(Input.GetButtonDown("Jump") && grounded)
        {
            jumping = true;
            acceleration.y = jumpSpeed;
        }
        CalculateDrag();

        velocity += acceleration * Time.fixedDeltaTime;

        if(velocity.x > maxVelocity.x)
        {
            velocity.x = maxVelocity.x;
        }
        else if(velocity.x < -maxVelocity.x)
        {
            velocity.x = -maxVelocity.x;
        }


        if (velocity.y > maxVelocity.y)
        {
            velocity.y = maxVelocity.y;
        }
        else if (velocity.y < -maxVelocity.y)
        {
            velocity.y = -maxVelocity.y;
        }


        Debug.DrawRay(transform.position, transform.TransformDirection(velocity), Color.blue);
        rb.velocity =  (transform.TransformDirection(velocity));

	}





    void Gravity()
    {

        RaycastHit2D groundHit = Physics2D.Raycast(transform.position, -transform.up, 0.25f + skinWidth, affectedBy);
       
        if (groundHit)
        {
            if (groundHit.collider.gameObject.CompareTag("World"))
            {
                grounded = true;
                Debug.DrawRay(transform.position, -transform.up * (0.25f + skinWidth), Color.green);
            }
            else if (groundHit.collider.gameObject.CompareTag("Obstacle"))
            {
                grounded = true;
                Debug.DrawRay(transform.position, -transform.up * (0.25f + skinWidth), Color.green);
            }
            else
            {
                Debug.DrawRay(transform.position, -transform.up * (0.25f + skinWidth), Color.red);
                grounded = false;
            }
        }
        else
        {
            Debug.DrawRay(transform.position, -transform.up * 0.25f, Color.red);
            grounded = false;
        }

        //apply gravity if we are not touching the ground.
        if (!grounded)
            velocity.y -= gravity * Time.deltaTime;
        else
            velocity.y = 0;
    }


    void CalculateDrag()
    {
        if (velocity.x > 0)
        {
            velocity.x -= Time.fixedDeltaTime * drag;
        }
        else
        {
            velocity.x += Time.fixedDeltaTime * drag;

        }
    }


    Vector2 getPerpendicularVector(Vector2 inVector)
    {
        Vector2 vector = inVector;
        vector.y = -inVector.x;
        vector.x = inVector.y;
        
        return vector.normalized;
    }
}
