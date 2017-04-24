using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour{
	public float waitTime = 0.0f;

	public List<Transform> items = new List<Transform>();
	private Transform randoItem;
	public PlanetManager planetManager;
	private Vector2 randPos;

    public float minimumWaitTime = 15.0f;
    public float maximumWaitTime = 30.0f;

    // Use this for initialization
    void Start () {
		
		this.planetManager = GetComponent<PlanetManager> ();
		this.randomizeWaitTime ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Time.time > this.waitTime && this.waitTime != 0.0f) {
			int chosenItem = Random.Range (0, this.items.Count);
			if (this.items.Count != 0) {
				this.randoItem = this.items [chosenItem];
			}
			float randDegrees = Random.Range (0.0f, 360.0f);
			this.randPos =  this.getUnitOnCircle (randDegrees, this.planetManager.currRadius);
            this.PlaceItem(this.randPos, this.randoItem);
            this.randomizeWaitTime ();
            
		}
	}



    public void PlaceItem(Vector2 Position, Transform itemObj)
    {

        Transform obj = Instantiate(itemObj, Position, Quaternion.identity);
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
		
		
		this.waitTime = Time.time + Random.Range(minimumWaitTime, maximumWaitTime);
	}
		
}
