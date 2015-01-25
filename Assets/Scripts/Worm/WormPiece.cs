using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class WormPiece : WormEntity 
{
    

	public float radius = 45.0f; 
	public float Speed = 1.0f;
  	

	protected float holeEntryScale = 0.5f;

	// Use this for initialization
	void Start () 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () 
    {
		OnUpdate();
	}
	protected override void OnUpdate()
	{
		if(previous != null)
		{
			Vector3 dif = previous.transform.position - transform.position;
			dif.z=0;		
			if(dif.magnitude >= radius)
			{
				
				//this.transform.LookAt(previous.transform, new Vector3(1,0,0));
				
				var angle = Mathf.Atan2(dif.y, dif.x) * Mathf.Rad2Deg;
				transform.rotation = Quaternion.AngleAxis(angle-90, Vector3.forward);
				Vector3 test = dif.normalized * Mathf.Lerp(0,(dif.magnitude - radius)*Time.deltaTime*Speed,0.65f);
				transform.Translate(test, Space.World);
				//transform.Translate(dif.normalized * (dif.magnitude - radius), Space.World);
			}
		}
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
			color.a = Mathf.Lerp(color.b, 1.0f, 0.5f);
			spriteRenderer.color = color;
//			Vector3 holeScale = new Vector3();
//			holeScale.x = Mathf.Lerp(transform.localScale.x, 1.0f, 0.75f);
//			holeScale.y = Mathf.Lerp(transform.localScale.y, 1.0f, 0.75f);
//			holeScale.z = Mathf.Lerp(transform.localScale.z, 1.0f, 0.75f);
//			transform.localScale = holeScale;
		}
	}

    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }
}
