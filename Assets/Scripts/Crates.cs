using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crates : MonoBehaviour {


    [System.Serializable]
    public class Items
    {
        public string name;
        public GameObject item;
    }

    public List<Items> DropList = new List<Items>();

    public GameObject dropLoot()
    {

        int chosenOne = Random.Range(0, DropList.Count);

        GameObject newOne = Instantiate(DropList[chosenOne].item, transform.position, Quaternion.identity);
        Destroy(gameObject);
        return newOne;
    }
	// Use this for initialization
	void Start () {
        gameObject.name = "Crate";
	}
	
	// Update is called once per frame
	void Update () {	
	}
}
