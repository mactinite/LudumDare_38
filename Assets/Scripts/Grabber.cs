using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour {

    public Transform holdAbove;
    private GameObject pickedUp;
    public bool grabbed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(pickedUp == null)
        {
            if (collision.gameObject.tag == "Item")
            {
                grabbed = true;
                collision.gameObject.transform.position = holdAbove.position;
                pickedUp = collision.gameObject;
                pickedUp.GetComponent<Bomb>().grabbed = true;
            }
        }

    }
    // Update is called once per frame
    void Update () {
        if(Input.GetKeyDown(KeyCode.B))
        {
            if (pickedUp != null)
            {
                if (pickedUp.tag == "Item")
                {
                    grabbed = false;
                    pickedUp.GetComponent<Bomb>().grabbed = false;
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
            
        }
	}
}
