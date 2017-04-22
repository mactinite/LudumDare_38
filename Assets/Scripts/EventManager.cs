using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EventManager : MonoBehaviour {
	private float waitTime = 0f;

	private List<string> events;
	private string randoEvent;
	private Vector2 randoPos;
	private float planetRadius;

	// Use this for initialization
	void Start () {
		

		events = new List<string> ();
		this.getEvents ().Add ("tornado");
		this.getEvents ().Add ("lightning");
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time < this.waitTime && this.waitTime != 0.0f) {
			int chosenEvent = Random.Range (0, this.getEvents ().Count);
			this.getRandoEvent = this.getEvents () [chosenEvent];

			float randDegrees = Random.Range (0.0f, 360.0f);
			this.getRandoPos () = this.getUnitOnCircle (randDegrees, this.getPlanetRadius ());
			this.waitTime = 0.0f;
		}



	}

	private Vector2 getUnitOnCircle(float angleDegrees, float radius) {
		float x = 0;
		float y = 0;
		float angleRadians = 0;
		Vector2 result;

		angleRadians = angleDegrees * Mathf.PI / 180.0f;
		x = radius * Mathf.Cos (angleRadians);
		y = radius * Mathf.Sin (angleRadians);

		result = new Vector2 (x, y);

		return result;
	}

	private void randomizeWaitTime()
	{
		const float minimumWaitTime = 20.0f;
		const float maximumWaitTime = 100.0f;
		this.waitTime = Time.time + Random.Range(minimumWaitTime, maximumWaitTime);
	}



	public List<string> getEvents() {
		return this.events;
	}

	public string  getRandoEvent() {
		return this.randoEvent;
	}

	public Vector2 getRandoPos() {
		return this.randoPos;
	}

	public void setPlanetRadius(float pPlanetRadius) {
		this.planetRadius = pPlanetRadius;
	}

	public Vector2 getPlanetRadius() {
		return this.planetRadius;
	}

}
