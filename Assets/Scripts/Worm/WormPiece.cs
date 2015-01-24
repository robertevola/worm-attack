using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class WormPiece : WormEntity 
{
    private SpriteRenderer spriteRenderer;

	public float radius = 45.0f; 
	public float Speed = 1.0f;
  	

	float holeEntryScale = 2f;

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
		//		if(base.isUnderground)
		//		{
		//			//Debug.Log ("BOOYEAH");
		//			Vector3 holeScale = new Vector3();
		//			holeScale.x = Mathf.Lerp(transform.localScale.x, holeEntryScale, 0.75f);
		//			holeScale.y = Mathf.Lerp(transform.localScale.y, holeEntryScale, 0.75f);
		//			holeScale.z =  Mathf.Lerp(transform.localScale.z, holeEntryScale, 0.75f);
		//			transform.localScale = holeScale;
		//		}else
		//		{
		//			Vector3 holeScale = new Vector3();
		//			holeScale.x = Mathf.Lerp(transform.localScale.x, 3.0f, 0.75f);
		//			holeScale.y = Mathf.Lerp(transform.localScale.y, 3.0f, 0.75f);
		//			holeScale.z = Mathf.Lerp(transform.localScale.z, 3.0f, 0.75f);
		//			transform.localScale = holeScale;
		//		}
	}

    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }
}
