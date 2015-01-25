using UnityEngine;
using System.Collections;

public class GainSizeOnStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D(Collider2D c){
		if(c.transform.tag == "WormHead" || c.transform.tag == "WormBody")
		{
			Worm w = c.transform.parent.gameObject.GetComponent<Worm>();

			for (int i = 0; i < 10; i++) {
				w.AddBodyChunk();	
			}


		}
	}
}
