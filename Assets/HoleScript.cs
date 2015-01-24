using UnityEngine;
using System.Collections;

public class HoleScript : WormEntity {

	public bool Entry;

	// Use this for initialization
	void Start () {
		//Destroy(this.gameObject,5);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D(Collider2D col)
	{

		if(col.gameObject.tag == "WormBody")
		{
			//Debug.Log("Hole Trigger");
			WormEntity wp = col.gameObject.GetComponent<WormEntity>();
			wp.previous = base.next;
			if(wp.next.gameObject.tag == "WormBody")
			wp.next.previous = this; 
			base.next = wp;

			if(Entry)
			wp.isUnderground = true;
			else wp.isUnderground = false;
		}
		if(col.gameObject.tag == "WormTail")
		{
			WormEntity wp = col.gameObject.GetComponent<WormEntity>();
			wp.previous = base.next;
			Destroy(this.gameObject);	
		}
	}

}
