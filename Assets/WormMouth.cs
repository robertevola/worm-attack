using UnityEngine;
using System.Collections;

public class WormMouth : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag == "WormBody")
		{
			col.gameObject.transform.parent.GetComponent<Worm>().KillWorm();
		}
	}
}
