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
		this.getEvents ().Add ("asteroid");
	}
	
	// Update is called once per frame
	void Update () {
		this.randomizeWaitTime ();
		if (Time.time < this.getWaitTime() && this.getWaitTime() != 0.0f) {
			int chosenEvent = Random.Range (0, this.getEvents ().Count);
			this.setRandoEvent(this.getEvents () [chosenEvent]);

			float randDegrees = Random.Range (0.0f, 360.0f);
			this.setRandoPos (this.getUnitOnCircle (randDegrees, this.getPlanetRadius ()));
			this.setWaitTime(0.0f);
		}



	}

	public Vector2 getUnitOnCircle(float angleDegrees, float radius) {
		float x = 0.0f;
		float y = 0.0f;
		float angleRadians = 0.0f;
		Vector2 result;

		angleRadians = angleDegrees * Mathf.PI / 180.0f;
		x = radius * Mathf.Cos (angleRadians);
		y = radius * Mathf.Sin (angleRadians);

		result = new Vector2 (x, y);

		return result;
	}

	public void randomizeWaitTime()
	{
		const float minimumWaitTime = 20.0f;
		const float maximumWaitTime = 100.0f;
		this.setWaitTime(Time.time + Random.Range(minimumWaitTime, maximumWaitTime));
	}



	public List<string> getEvents() {
		return this.events;
	}

	public string  getRandoEvent() {
		return this.randoEvent;
	}

	public void setRandoEvent(string pRandoEvent) {
		this.randoEvent = pRandoEvent;
	}

	public Vector2 getRandoPos() {
		return this.randoPos;
	}

	public void setRandoPos(Vector2 pRandoPos) {
		this.randoPos = pRandoPos;
	}


	public void setPlanetRadius(float pPlanetRadius) {
		this.planetRadius = pPlanetRadius;
	}

	public float getPlanetRadius() {
		return this.planetRadius;
	}

	public void setWaitTime(float pWaitTime) {
		this.waitTime = pWaitTime;
	}

	public float getWaitTime() {
		return this.waitTime;
	}

}
