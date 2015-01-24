using UnityEngine;
using System.Collections;

public class WormHead : WormPiece {

	public float HeadSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		OnUpdate();
	}
	protected override void OnUpdate()
	{
		transform.Translate(this.transform.up * HeadSpeed * Time.deltaTime * Speed, Space.World);
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
		Speed = value * HeadSpeed;
	}

}
