using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EventManager : MonoBehaviour {
	public float waitTime = 0.0f;

	public List<Transform> events = new List<Transform>();
	private Transform randoEvent;
	private Vector2 randoPos;
	public PlanetManager planetManager;

	// Use this for initialization
	void Start () {
		
		this.planetManager = GetComponent<PlanetManager> ();
		this.randomizeWaitTime ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Time.time > this.waitTime && this.waitTime != 0.0f) {
			int chosenEvent = Random.Range (0, this.events.Count);
			if (this.events.Count != 0) {
				this.randoEvent = this.events[chosenEvent];
			}
			float randDegrees = Random.Range (0.0f, 360.0f);
			this.randoPos = this.getUnitOnCircle (randDegrees, this.planetManager.currRadius);
			this.randomizeWaitTime ();
            this.PlaceEvent(this.randoPos, this.randoEvent);
		}
			
	}


    public void PlaceEvent(Vector2 Position, Transform eventObj)
    {

        Transform obj = Instantiate(eventObj, Position, Quaternion.identity);
        obj.GetComponent<CircularPhysics>().gravityCenter = transform;
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
		const float minimumWaitTime = 0.25f;
		const float maximumWaitTime = 1f;
		this.waitTime = Time.time + Random.Range(minimumWaitTime, maximumWaitTime);
	}





}
