using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Destroy (gameObject, 2);//destroy gameobject i.e projectile after 2 sec/
	}

	void OnCollisionEnter(Collision other){
		Destroy (gameObject);//destroy gameobject after collision
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
