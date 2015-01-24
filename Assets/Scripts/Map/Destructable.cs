﻿using UnityEngine;
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
		Instantiate (explosion, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}
