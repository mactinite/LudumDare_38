using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularPhysics : MonoBehaviour {


    public float gravitySpeed;
    public Transform gravityCenter;
    private Rigidbody2D rb;
    public bool gravityOn = true;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Physics();
    }

    void Physics()
    {
        if(gravityOn)
            rb.velocity = gravityCenter.position - transform.position * gravitySpeed * Time.deltaTime;
    }

}
