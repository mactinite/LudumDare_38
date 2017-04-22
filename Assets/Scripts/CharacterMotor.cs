using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMotor : MonoBehaviour {

    public string HorizontalAxis = "Horizontal";
    public string VerticalAxis = "Vertical";

    public bool grounded;

    private Rigidbody2D rb;
    private Vector2 velocity;
    private CircularPhysics circularPhysics;

    public LayerMask affectedBy;
    public float walkSpeed = 100f;


    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        circularPhysics = GetComponent<CircularPhysics>();

    }
	
	// Update is called once per frame
	void Update () {

        velocity = rb.velocity;

        velocity.x += Input.GetAxis(HorizontalAxis) * Time.deltaTime * walkSpeed;

        RaycastHit2D groundHit = Physics2D.BoxCast(transform.position, new Vector2(0.25f, 0.25f), transform.rotation.z, -transform.up, 0.3f,affectedBy);
        Debug.DrawRay(transform.position, -transform.up * 0.3f, Color.red);
        if (groundHit)
        {
            grounded = true;
            circularPhysics.gravityOn = false;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.FromToRotation(Vector3.up, groundHit.normal), Time.time * 500); 

        }
        else
        {
            grounded = false;
            circularPhysics.gravityOn = true;
            rb.gravityScale = 0;
        }

        rb.velocity = velocity;

	}
}
