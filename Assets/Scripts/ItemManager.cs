using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : EventManager{
	public List<string> items = new List<string>();
	private string randoItem;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.randomizeWaitTime ();
		if (Time.time > this.getWaitTime() && this.getWaitTime() != 0.0f) {
			int chosenItem = Random.Range (0, this.getItems ().Count);
			this.setRandoItem(this.getItems () [chosenItem]);

			float randDegrees = Random.Range (0.0f, 360.0f);
			this.setRandoPos (this.getUnitOnCircle (randDegrees, this.getPlanetRadius ()));
			this.setWaitTime(0.0f);
		}
	}


	public List<string> getItems() {
		return this.items;
	}

	public string  getRandoItem() {
		return this.randoItem;
	}

	public void setRandoItem(string pRandoItem) {
		this.randoItem = pRandoItem;
	}
}
