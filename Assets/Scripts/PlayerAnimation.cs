using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

    private CharacterController2D cc;
    private CharacterMotor cm;
    private Animator anim;

    public float runSpeedMultiplier = 2;

    private void Start()
    {
        cc = GetComponent<CharacterController2D>();
        cm = GetComponent<CharacterMotor>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {



        if (Input.GetAxisRaw(cm.HorizontalAxis) > 0.01f)
        {
            anim.SetBool("Running", true);
            anim.speed = (Mathf.Abs(cm.velocity.x) / cm.maxVelocity.x) *runSpeedMultiplier;
        }
        else if (Input.GetAxisRaw(cm.HorizontalAxis) < -0.01f)
        {
            anim.SetBool("Running", true);
            anim.speed = (Mathf.Abs(cm.velocity.x) / cm.maxVelocity.x) * runSpeedMultiplier;
        }
        else
        {
            anim.speed = 1;
            anim.SetBool("Running", false);
        }

        anim.SetBool("Grounded", cc.isGrounded);

        if (cm.jumping)
        {
            anim.SetBool("Jumping", true);
            anim.speed = 1;
        }
        if (anim.GetBool("Jumping") && cc.isGrounded)
        {
            anim.SetBool("Jumping", false);
            anim.speed = 1;
        }

    }

}
