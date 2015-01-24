using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class WormPiece : MonoBehaviour 
{
    private SpriteRenderer spriteRenderer;
    public WormPiece next, previous;
	public float radius = 45.0f; 
	
    public bool isUnderground;

	// Use this for initialization
	void Start () 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () 
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

				transform.Translate(dif.normalized * (dif.magnitude - radius), Space.World);
			}
		}
	}

    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }
}
