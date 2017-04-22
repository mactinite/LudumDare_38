using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularPhysics : MonoBehaviour {


    public float gravitySpeed;

    public List<Rigidbody2D> physicsObjects = new List<Rigidbody2D>();



    private void Update()
    {
        Physics();
    }

    void Physics()
    {
        foreach(Rigidbody2D rb in physicsObjects)
        {
            rb.velocity = transform.position - rb.transform.position * gravitySpeed * Time.deltaTime;
        }
    }

    public void RegisterRigidbody(Rigidbody2D rigidBody)
    {
        physicsObjects.Add(rigidBody);
    }

}
