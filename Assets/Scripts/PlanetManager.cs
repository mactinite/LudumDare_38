using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetManager : MonoBehaviour {

	public float decayRate; 
	private float nextDecay;

	public float currRadius;
	public float maxRadius;
	public float minRadius;
	public float radiusDecayInterval;
	public float currGravity;
	public float maxGravity;
	public float minGravity;
	public float gravityDecayInterval;


	// Use this for initialization
	void Start () {
		decayRate = 5.0f;
		nextDecay = 0.0f;
		maxRadius = 150.0f;
		minRadius = 50.0f;
		currRadius = maxRadius;
		radiusDecayInterval = 1.0f;
		maxGravity = 60.0f;
		minGravity = 10.0f;
		currGravity = maxGravity;
		gravityDecayInterval = 0.5f;

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
