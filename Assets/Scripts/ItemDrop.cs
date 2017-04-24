using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour {


    [System.Serializable]
    public class Items
    {
        public string name;
        public GameObject item;
        public int dropChance;
    }

    public List<Items> DropList = new List<Items>();
    public bool isCrateDestroyed = false;

    public void dropLoot()
    {
        if(isCrateDestroyed)
        {
            int itemChance = 0;

            for (int i = 0; i < DropList.Count; i++)
            {
                itemChance += DropList[i].dropChance;
            }

            int chosenOne = Random.Range(0, itemChance);

            for (int j = 0; j < DropList.Count; j++)
            {
                if (chosenOne <= DropList[j].dropChance)
                {
                    Instantiate(DropList[j].item, transform.position, Quaternion.identity);
                    return;
                }
                chosenOne -= DropList[j].dropChance;
            }
        }
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.dropLoot();	
	}
}
