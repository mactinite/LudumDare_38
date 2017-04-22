using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMotor : MonoBehaviour {

    public int playerNumber = 1;

    public string HorizontalAxis = "Horizontal";
    public string VerticalAxis = "Vertical";

    public bool grounded;

    private Rigidbody2D rb;
    private Vector2 velocity;
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



    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();

    }

    private float jumpTimer; 

	// Update is called once per frame
	void FixedUpdate () {

        velocity = Vector2.zero;
        velocity.x = Input.GetAxis(HorizontalAxis) * walkSpeed * Time.fixedDeltaTime;
        velocity.y = -(gravity * Time.fixedDeltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.FromToRotation(Vector3.up, transform.position - gravityPoint.position), Time.fixedDeltaTime * rotationDamping);

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


        if (!grounded)
        {
                
        }
        else
        {
            jumping = false;
            jumpTimer = jumpTime;
        }

        if(Input.GetButton("Jump") && grounded)
        {
            velocity.y = jumpSpeed;
        }

        rb.velocity =  (transform.TransformDirection(velocity));

	}


    Vector2 getPerpendicularVector(Vector2 inVector)
    {
        Vector2 vector = inVector;
        vector.y = -inVector.x;
        vector.x = inVector.y;
        
        return vector.normalized;
    }
}
