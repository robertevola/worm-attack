using UnityEngine;
using System.Collections;

public class WormEntity : MonoBehaviour {
	public WormEntity next, previous;
	public bool isUnderground;
	protected SpriteRenderer spriteRenderer;
	bool dieing;
	// Use this for initialization
	void Start () {
	
	}
	protected virtual void OnUpdate()
	{

	}
	// Update is called once per frame
	void Update () {
		OnUpdate();
	}
	public void KillSelf()
	{
		Invoke("KillNext", 0.1f);
		if(next!=null)Camera.main.gameObject.GetComponent<CameraController>().objectToFollow = next.transform;
		Destroy(gameObject, 0.15f);			
	}
	void KillNext()
	{
		if(next!= null)
		next.KillSelf();
	}

}
