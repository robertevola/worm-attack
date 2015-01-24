using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class WormPiece : MonoBehaviour 
{
    private SpriteRenderer spriteRenderer;
    public WormPiece next, previous;

    public bool isUnderground;

	// Use this for initialization
	void Start () 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () 
    {

	}

    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }
}
