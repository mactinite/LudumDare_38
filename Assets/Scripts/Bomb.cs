using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    public float bombTime = 1f;
    public Transform explosion;
    public bool exploded;
    public bool grabbed;
    private float timer = 0f;
    public bool lit = false;
    private Animator anim;

	// Use this for initialization
	void Start () {
        grabbed = false;
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        anim.SetBool("Lit", lit);

        if(grabbed == false && lit)
        {
            if (timer > bombTime && !exploded)
            {
                Explode();
            }

            timer += Time.deltaTime;
        } else
        {
            timer = 0;
        }

	}

    public void Explode()
    {

        Instantiate(explosion,transform.position,Quaternion.identity);
        Destroy(gameObject);
        exploded = true;
    }

}
