using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour {

    public class Item : MonoBehaviour
    {
        public string myName;
        public string description;
        public bool consumable;
        public Texture2D icon;
        public GameObject objectPrefab;

        public void ItemPickup()
        {
            Debug.Log("Called function");
        }
    }

}
