using UnityEngine;
using System.Collections;

public class WormHeadWes : MonoBehaviour {
	public float SpeedUpMul = 1f;
	public float SpeedDownMul = 0.025f;
	public float IdleSpeed = 0.05f;
	public float TurnSpeed = 2.0f; 
	public float Speed;
	// Use this for initialization
	void Start () {
		//this.transform.LookAt(this.transform.position + new Vector3(1.0f, 0.0f, 0.0f), new Vector3(0, 1, 0));	
		Speed = IdleSpeed;
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKey(KeyCode.A))
		{
			TurnLeft();
		}
		if(Input.GetKey(KeyCode.D))
		{
			TurnRight();
		}
		if(Input.GetKey(KeyCode.W))
		{
			SpeedUp();
		}
		else if(Input.GetKey(KeyCode.S))
		{
			SlowDown();
		}else Idle();

		transform.Translate(this.transform.up * Speed, Space.World);

	}
	void Idle()
	{
		Mathf.Lerp(Speed, IdleSpeed, 0.25f);
	}

	void TurnLeft()
	{
		transform.Rotate(new Vector3(0, 0, 1), TurnSpeed);
	}
	void TurnRight()
	{
		transform.Rotate(new Vector3(0, 0, 1), -TurnSpeed);
	}
	void SpeedUp()
	{
		Speed = Mathf.Lerp(Speed, SpeedUpMul, 0.25f);

	}
	void SlowDown()
	{	
		Speed = Mathf.Lerp(Speed, SpeedDownMul, 0.25f);
	}
}
