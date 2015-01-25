using UnityEngine;
using System.Collections;

public class WormHead : WormPiece {

	public float HeadSpeed;

	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		OnUpdate();
	}
	protected override void OnUpdate()
	{
		transform.Translate(this.transform.up * HeadSpeed * Time.deltaTime * Speed, Space.World);

		if(base.isUnderground)
		{
			Color color = spriteRenderer.color;
			color.r = Mathf.Lerp(color.r, 0.2f, 0.5f);
			color.g = Mathf.Lerp(color.g, 0.2f, 0.5f);
			color.b = Mathf.Lerp(color.b, 0.2f, 0.5f);
			color.a = Mathf.Lerp(color.a, 0.2f, 0.5f);
			spriteRenderer.color = color;

//			Vector3 holeScale = new Vector3();
//			holeScale.x = Mathf.Lerp(transform.localScale.x, holeEntryScale, 0.75f);
//			holeScale.y = Mathf.Lerp(transform.localScale.y, holeEntryScale, 0.75f);
//			holeScale.z =  Mathf.Lerp(transform.localScale.z, holeEntryScale, 0.75f);
//			transform.localScale = holeScale;
		}else
		{
			Color color = spriteRenderer.color;
			color.r = Mathf.Lerp(color.r, 1.0f, 0.5f);
			color.g = Mathf.Lerp(color.g, 1.0f, 0.5f);
			color.b = Mathf.Lerp(color.b, 1.0f, 0.5f);
			color.a = Mathf.Lerp(color.a, 1.0f, 0.5f);
			spriteRenderer.color = color;
//			Vector3 holeScale = new Vector3();
//			holeScale.x = Mathf.Lerp(transform.localScale.x, 1.0f, 0.75f);
//			holeScale.y = Mathf.Lerp(transform.localScale.y, 1.0f, 0.75f);
//			holeScale.z = Mathf.Lerp(transform.localScale.z, 1.0f, 0.75f);
//			transform.localScale = holeScale;
		}
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
