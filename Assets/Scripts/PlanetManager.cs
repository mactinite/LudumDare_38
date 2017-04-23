using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetManager : MonoBehaviour {

	public float decayRate = 5.0f;
    private float nextDecay = 0.0f;
	public float maxRadius = 150.0f;
	public float minRadius = 50.0f;
	public float currRadius = 150.0f;
	public float radiusDecayInterval = 1.0f;
	public float maxGravity = 60.0f;
	public float minGravity = 10.0f;
	public float currGravity = 60.0f;
	public float gravityDecayInterval = 0.5f;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > nextDecay) {
			nextDecay = Time.time + decayRate;
			if (currGravity != minGravity) {
				currGravity = currGravity - gravityDecayInterval;
			}
			if (currRadius != minRadius) {
				currRadius = currRadius - radiusDecayInterval;
			}
		}
	}


		

}
