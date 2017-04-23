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
    public Vector2 leftPos;
    public Vector2 rightPos;



    private Vector2 velocity;
    private Quaternion facing;
    private BoxCollider2D collider;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
    }

    private float jumpTimer;


    private void Update()
    {

    }

    private void LateUpdate()
    {
        Quaternion headingDelta = Quaternion.AngleAxis(acceleration.x, transform.up);

        
    }

    // Update is called once per frame
    void FixedUpdate () {

        //reset acceleration every frame
        acceleration = Vector2.zero;
        Vector2 planetNormal = transform.position - gravityPoint.position;
        planetNormal.Normalize();
        acceleration.x = Input.GetAxis(HorizontalAxis) * walkSpeed;
        Quaternion newRot = Quaternion.FromToRotation(transform.up, planetNormal) * transform.rotation;
        transform.rotation = newRot;
        Gravity();
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
        

        velocity += acceleration * Time.deltaTime;

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
        CalculateDrag();

        Debug.DrawRay(transform.position, transform.TransformDirection(velocity), Color.blue);
        rb.velocity =  (transform.TransformDirection(velocity));

	}





    void Gravity()
    {
        RaycastHit2D leftHit = Physics2D.Raycast(transform.TransformPoint((Vector3)leftPos), -transform.up, skinWidth, affectedBy);
        RaycastHit2D rightHit = Physics2D.Raycast(transform.TransformPoint((Vector3)rightPos), -transform.up, skinWidth, affectedBy);


        if(leftHit)
        {
            grounded = true;
            Debug.DrawRay(transform.TransformPoint((Vector3)leftPos), -transform.up * skinWidth, Color.green);
        }
        else
        {
            Debug.DrawRay(transform.TransformPoint((Vector3)leftPos), -transform.up * skinWidth, Color.red);
        }

        if (rightHit)
        {
            grounded = true;
            Debug.DrawRay(transform.TransformPoint((Vector3)rightPos), -transform.up * skinWidth, Color.green);
        }
        else
        {
            Debug.DrawRay(transform.TransformPoint((Vector3)rightPos), -transform.up * skinWidth, Color.red);
        }

        if(!leftHit && !rightHit)
        {
            grounded = false;
        }

        if (!grounded)
            acceleration.y = -gravity;
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

    void OnDrawGizmosSelected ()
    {
        Gizmos.DrawIcon(transform.TransformPoint((Vector3)leftPos), "leftPosition.png", true);
        Gizmos.DrawIcon(transform.TransformPoint((Vector3)rightPos), "rightPosition.png", true);
    }
}
