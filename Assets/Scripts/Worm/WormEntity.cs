using UnityEngine;
using System.Collections;

public class WormEntity : MonoBehaviour {
	public WormEntity next, previous;
	public bool isUnderground;
	protected SpriteRenderer spriteRenderer;
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
}
