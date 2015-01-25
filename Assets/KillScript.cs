using UnityEngine;
using System.Collections;

public class KillScript : MonoBehaviour {

	public float timeToDie = 1.0f;
	// Use this for initialization
	void Start () {
		Destroy(gameObject, timeToDie);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
