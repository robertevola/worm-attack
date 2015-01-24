using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WormHeadAnimation : MonoBehaviour {

	public List<Sprite> Frames = new List<Sprite>();
	public int FramesPerSec = 3;
	public int currentFrame = 0;

	// Use this for initialization
	SpriteRenderer sr;
	void Start () {
		sr = GetComponent<SpriteRenderer>();

		StartCoroutine(WormAnima());
	}
	IEnumerator WormAnima()
	{
		while(true)
		{
			sr.sprite = Frames[currentFrame];
			currentFrame++;
			if(currentFrame >= Frames.Count)
			{
				currentFrame = 0;
			}
			yield return new WaitForSeconds(1.0f/FramesPerSec);
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
