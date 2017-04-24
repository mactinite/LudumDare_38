using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterDelay : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 0.1f);
	}

    float timer;
	// Update is called once per frame
	void Update () {

        Vector2 transformScale;
        transformScale = transform.localScale;
        timer += Time.deltaTime;


        transform.localScale *= 1 + timer; 

	}
}
