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
            if (sr != null)
            {
                sr.sprite = Frames[currentFrame];
                currentFrame++;
                if (currentFrame >= Frames.Count)
                {
                    currentFrame = 0;
                }
            }
			yield return new WaitForSeconds(1.0f/FramesPerSec);
		}
	}

    public void ToggleAnimation(bool state)
    {
        sr.enabled = state;
        this.enabled = state;
    }

    //void OnDisable()
    //{
    //    if(sr != null)
    //        sr.enabled = false;

    //    StopCoroutine(WormAnima());
    //}

    //void OnEnable()
    //{
    //    if (sr != null)
    //        sr.enabled = true;

    //    StartCoroutine(WormAnima());
    //}

}
