using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterDelay : MonoBehaviour {

    public float explosionDuration = 0.25f;
    private Vector2 scale;
	// Use this for initialization
	void Start () {
        scale = transform.localScale;
        Destroy(gameObject, 0.5f);
	}

    float timer;
	// Update is called once per frame
	void Update () {

        if (timer < explosionDuration)
        {
            Vector2 transformScale;
            transformScale = transform.localScale;
            timer += Time.deltaTime;
            transform.localScale = scale + Vector2.one * ((timer / explosionDuration));
        }
        else
        {
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
        }
	}
}
