using UnityEngine;
using System.Collections;

public class WormHead : WormPiece {

	float speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Translate(this.transform.up * speed, Space.World);
	}
	void OnTriggerEnter(Collider col)
	{

	}
	public void Turn(float value)
	{
		transform.Rotate(new Vector3(0, 0, -1), value);
	}
	public void AdjustSpeed(float value)
	{
		speed = value;
	}

}
