  using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//ui framework
using UnityEngine.Networking;

public class Health : NetworkBehaviour {
	public int maxHealth;

	[SyncVar(hook="OnHealthChanged")]
	public int currentHealth;
	public Text HealthScore;//field to hold the text box

	// Use this for initialization
	void Start () {
		HealthScore.text = currentHealth.ToString();	//to show health score when game instantiate
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void TakeDamage(int howMuch){//function for how much health damage and update health

		if (!isServer) {
			return;
		}
		var newHealth = currentHealth - howMuch;
		if (newHealth <= 0) {
			//Debug.Log ("Dead");
			currentHealth = maxHealth;
			RpcDeath();
		} else {
			currentHealth = newHealth;

		}
	}

	void OnHealthChanged(int updatedHealth){
		HealthScore.text = updatedHealth.ToString();
	}

	[ClientRpc]//remote procedure call issued on server but execute on client
	void RpcDeath(){
		if (isLocalPlayer) {
			//transform.position = Vector3.zero;
			var spawnPoints = FindObjectsOfType<NetworkStartPosition>();
			var chosenPoint = Random.Range (0, spawnPoints.Length);
			transform.position = spawnPoints [chosenPoint].transform.position;
		}
	}
}
