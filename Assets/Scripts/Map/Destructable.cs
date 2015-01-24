using UnityEngine;
using System.Collections;

public class Destructable : MonoBehaviour {
	public int points = 100;
	public int health;
	private int currentHealth;
	public GameObject explosion;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter2D(Collision2D c){
		if (transform.parent.transform.childCount <= 1) {
			Destroy(transform.parent.gameObject);
		}
		explosion.transform.RotateAround (Vector3.zero, new Vector3(0,0,1), Random.Range(-360, 360));
		GameObject rubble = Instantiate (explosion, transform.position, explosion.transform.rotation) as GameObject;
		Destroy (rubble,  Random.Range(4, 8));
		Destroy(gameObject);
	}
}
