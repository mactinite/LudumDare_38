using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    public float bombTime = 1f;
    public Transform explosion;
    public bool exploded;
    private float timer = 0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(timer > bombTime && !exploded)
        {
            Explode();
        }

        timer += Time.deltaTime;
	}

    void Explode()
    {

        Instantiate(explosion,transform.position,Quaternion.identity);
        Destroy(gameObject);
        exploded = true;
    }

}
