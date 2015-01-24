using UnityEngine;
using System.Collections;

public class WormBody : MonoBehaviour {

	public GameObject thingIFollow;
	public float radius =2.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 dif = thingIFollow.transform.position - transform.position;
		dif.z=0;		
		if(dif.magnitude >= radius)
		{

			this.transform.LookAt(thingIFollow.transform.position, new Vector3(0,0,-1));

			transform.Translate(dif.normalized * (dif.magnitude - radius), Space.World);
		}
	}
}
