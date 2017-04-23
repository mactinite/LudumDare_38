using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour {

    //private variables

    //private references
    private Sprite sprite;

    //public variables
    public int verticalRays = 4;
    public int horizontalRays = 4;
    public float skinWidth = 0.03f;
    public float slopeLimit = 45.0f;
    public LayerMask platformMask;
    public Vector2 velocity;

    public bool isGrounded = false;

    private Vector2[] downRayPositions;
    private Vector2[] leftRayPositions;
    private Vector2[] rightRayPositions;
    private Vector2[] upRayPositions;

    private Vector2 leftMin, rightMin, leftMax, rightMax;
    public float jumpThreshold = 0.07f;
    public AnimationCurve slopeSpeedMultiplier = new AnimationCurve(new Keyframe(-90f, 1.5f), new Keyframe(0f, 1f), new Keyframe(90f, 0f));

    bool wasGroundedLastFrame, isGoingUpSlope, movingDownSlope;


    float slopeAngle;
    float slopeLimitTangent = Mathf.Tan(75f * Mathf.Deg2Rad);

    // Use this for initialization
    void Start () {
        sprite = GetComponent<SpriteRenderer>().sprite;
        UpdateRayPositions();
    }

    private void Update()
    {
       
        leftMin = sprite.bounds.min;
        rightMin = new Vector2(sprite.bounds.center.x + sprite.bounds.extents.x, sprite.bounds.center.y - sprite.bounds.extents.y);
        leftMax = new Vector2(sprite.bounds.center.x - sprite.bounds.extents.x, sprite.bounds.center.y + sprite.bounds.extents.y);
        rightMax = sprite.bounds.max;
    }

    void UpdateRayPositions()
    {
        downRayPositions = new Vector2[verticalRays];
        leftRayPositions = new Vector2[horizontalRays];
        rightRayPositions = new Vector2[horizontalRays];
        upRayPositions = new Vector2[verticalRays];
        float bottomWidth = rightMin.x - leftMin.x;
        float bottomIncrement = bottomWidth / verticalRays;
        for (int i = 0; i < verticalRays; i++)
        {
            Vector2 offset = new Vector2(leftMin.x + (bottomIncrement * i), leftMin.y);
            Vector2 offsetTop = new Vector2(leftMax.x + (bottomIncrement * i), leftMax.y);
            downRayPositions[i] = offset;
            upRayPositions[i] = offsetTop;
        }

        float sideHeight = rightMax.y - rightMin.y;
        float sideIncrement = sideHeight / horizontalRays;
        for (int i = 0; i < horizontalRays; i++)
        {
            Vector2 offsetLeft = new Vector2(leftMin.x, leftMin.y + (sideIncrement * i));
            Vector2 offsetRight = new Vector2(rightMin.x, rightMin.y + (sideIncrement * i));
            leftRayPositions[i] = offsetLeft;
            rightRayPositions[i] = offsetRight;
        }

    }

    // Update is called once per frame
    void FixedUpdate () {

    }

    public void MoveVertically(ref Vector2 delta)
    {
        bool isMovingUp = delta.y > 0;
        Vector2[] rayPositions = isMovingUp ? upRayPositions : downRayPositions;
        float rayDistance = Mathf.Abs(delta.y) + skinWidth;
        Vector2 rayDirection = isMovingUp ? transform.up : -transform.up;


        foreach (Vector2 pos in rayPositions)
        {
            Debug.DrawRay(transform.TransformPoint(pos), rayDirection * (rayDistance), Color.green);
            RaycastHit2D hit = Physics2D.Raycast(transform.TransformPoint(pos), rayDirection, rayDistance, platformMask);

            if (hit)
            {

                delta.y = hit.point.y - transform.TransformPoint(pos).y;
                rayDistance = Mathf.Abs(delta.y);


                if (isMovingUp)
                {
                    delta.y -= skinWidth;
                }
                else
                {
                    delta.y += skinWidth;
                    isGrounded = true;
                }
                
            }
            else
            {
                isGrounded = false;
            }

            if (rayDistance < skinWidth + 0.001f)
                break;

        }

    }

    private void HandleVerticalSlope(ref Vector3 deltaMovement)
    {
        // slope check from the center of our collider
        var centerOfCollider = (leftRayPositions[0].x + rightRayPositions[0].x) * 0.5f;
        var rayDirection = -Vector2.up;

        // the ray distance is based on our slopeLimit
        var slopeCheckRayDistance = slopeLimitTangent * (rightRayPositions[0].x - centerOfCollider);

        var slopeRay = new Vector2(centerOfCollider, leftRayPositions[0].y);
        Debug.DrawRay(slopeRay, rayDirection * slopeCheckRayDistance, Color.yellow);
        RaycastHit2D raycastHit = Physics2D.Raycast(slopeRay, rayDirection, slopeCheckRayDistance, platformMask);
        if (raycastHit)
        {
            // bail out if we have no slope
            var angle = Vector2.Angle(raycastHit.normal, Vector2.up);
            if (angle == 0)
                return;

            // we are moving down the slope if our normal and movement direction are in the same x direction
            var isMovingDownSlope = Mathf.Sign(raycastHit.normal.x) == Mathf.Sign(deltaMovement.x);
            if (isMovingDownSlope)
            {
                // going down we want to speed up in most cases so the slopeSpeedMultiplier curve should be > 1 for negative angles
                var slopeModifier = slopeSpeedMultiplier.Evaluate(-angle);
                // we add the extra downward movement here to ensure we "stick" to the surface below
                deltaMovement.y += raycastHit.point.y - slopeRay.y - skinWidth;
                deltaMovement.x *= slopeModifier;
                movingDownSlope = true;
                slopeAngle = angle;
            }
        }
    }

    public void MoveHorizontally(ref Vector2 delta)
    {

        bool isMovingRight = delta.x > 0;
        Vector2[] rayPositions = isMovingRight ? rightRayPositions : leftRayPositions;
        float rayDistance = Mathf.Abs(delta.x) + skinWidth;
        Vector2 rayDirection = isMovingRight ? transform.right : -transform.right;
        foreach (Vector2 pos in rayPositions)
        {

            RaycastHit2D hit = Physics2D.Raycast(transform.TransformPoint(pos), rayDirection, rayDistance, platformMask);      
            Debug.DrawRay(transform.TransformPoint(pos), rayDirection * (rayDistance), Color.green);

            if (hit)
            {
                delta.x = hit.point.x - transform.TransformPoint(pos).x;
                rayDistance = Mathf.Abs(delta.x);


                if (isMovingRight)
                {
                    delta.x -= skinWidth;

                }
                else
                {
                    delta.x += skinWidth;
                }

                if (rayDistance < skinWidth + 0.001f)
                    break;

            }

        }

    }

    bool HandleHorizontalSlope(ref Vector3 delta, float angle)
    {
        // disregard 90 degree angles (walls)
        if (Mathf.RoundToInt(angle) == 90)
            return false;

        // if we can walk on slopes and our angle is small enough we need to move up
        if (angle < slopeLimit)
        {
            // we only need to adjust the deltaMovement if we are not jumping
            // TODO: this uses a magic number which isn't ideal! The alternative is to have the user pass in if there is a jump this frame
            if (delta.y < jumpThreshold)
            {
                // apply the slopeModifier to slow our movement up the slope
                var slopeModifier = slopeSpeedMultiplier.Evaluate(angle);
                delta.x *= slopeModifier;

                // we dont set collisions on the sides for this since a slope is not technically a side collision.
                // smooth y movement when we climb. we make the y movement equivalent to the actual y location that corresponds
                // to our new x location using our good friend Pythagoras
                delta.y = Mathf.Abs(Mathf.Tan(angle * Mathf.Deg2Rad) * delta.x);
                var isGoingRight = delta.x > 0;

                // safety check. we fire a ray in the direction of movement just in case the diagonal we calculated above ends up
                // going through a wall. if the ray hits, we back off the horizontal movement to stay in bounds.
                Vector2 rayPosition = isGoingRight ? rightRayPositions[0] : leftRayPositions[0];
                RaycastHit2D raycastHit;
                raycastHit = Physics2D.Raycast(rayPosition, delta.normalized, delta.magnitude, platformMask);

                if (raycastHit)
                {
                    // we crossed an edge when using Pythagoras calculation, so we set the actual delta movement to the ray hit location
                    delta = (Vector2)raycastHit.point - rayPosition;
                    if (isGoingRight)
                        delta.x -= skinWidth;
                    else
                        delta.x += skinWidth;
                }

                isGoingUpSlope = true;
                isGrounded = true;
            }
        }
        else // too steep. get out of here
        {
            delta.x = 0;
        }

        return true;
    }

    public void Move( Vector2 delta)
	{
        wasGroundedLastFrame = isGrounded;
        UpdateRayPositions();

        if (delta.y < 0f && wasGroundedLastFrame)
            HandleVerticalSlope(ref delta);

        // now we check movement in the horizontal dir
        if (delta.x != 0f )
			MoveHorizontally(ref delta);

		// next, check movement in the vertical dir
		if(delta.y != 0f )
			MoveVertically( ref delta);

		// move then update our state
		transform.Translate(delta);

		// only calculate velocity if we have a non-zero deltaTime
		if( Time.deltaTime > 0f )
			velocity = transform.InverseTransformDirection(delta) / Time.deltaTime;
	}

}
