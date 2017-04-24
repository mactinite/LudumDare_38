using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour {

    public Transform holdAbove;
    private GameObject pickedUp;
    public string throwButton = "Attack";
    public bool grabbed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(pickedUp == null)
        {
            if (collision.gameObject.tag == "Item")
            {
                if (!collision.GetComponent<Bomb>().lit)
                {
                    grabbed = true;
                    collision.GetComponent<Bomb>().lit = true;
                    collision.gameObject.transform.position = holdAbove.position;
                    pickedUp = collision.gameObject;
                    pickedUp.GetComponent<Bomb>().grabbed = true;
                }
            }
        }

    }
    // Update is called once per frame
    void Update () {
        if(Input.GetButtonDown(throwButton))
        {
            if (pickedUp != null)
            {
                if (pickedUp.tag == "Item")
                {
                    grabbed = false;
                    pickedUp.GetComponent<Bomb>().grabbed = false;
                    pickedUp.GetComponent<Rigidbody2D>().velocity = (Vector2)transform.TransformPoint(GetComponent<CharacterController2D>().velocity);
                    pickedUp.GetComponent<Collider2D>().isTrigger = false;
                    pickedUp = null;
                    
                }
                else
                {
                    print("not a bomb");
                }
            }
        }
		if (grabbed)
        {

            pickedUp.transform.position = holdAbove.position;
            pickedUp.GetComponent<Collider2D>().isTrigger = true;
        }
	}
}
