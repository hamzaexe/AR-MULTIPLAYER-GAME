using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityStandardAssets.CrossPlatformInput;

public class TankController : NetworkBehaviour {
	public float MoveSpeed = 150.0f;
	public float RotateSpeed = 3.0f;
	public Color LocalPlayerColor;//make a variable of color type setting to change it in inspector for our local player
	public GameObject ShotPrefab;
	public Transform ShotSpawnTransform;//for instantiate bullet(projectile)
	public float ShotSpeed;
	public float ReloadRate = 0.5f;
	private float _nextShotTime;

	public bool isEnabled = true;
//	private Rigidbody rb;


	// Use this for initialization
	void Start () {
		//rb = GetComponent<Rigidbody> ();

	}

	public override void OnStartLocalPlayer(){
		var tankParts = GetComponentsInChildren<MeshRenderer> ();//get array of all the mesh renderers of game object that is tank parts
		foreach (var part in tankParts) {
			part.material.color = LocalPlayerColor;//loop through all the mesh renderers and change there color
		}
	}
	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer) {//if input not coming from local player then just return and below code dont run
			return;
		}
		var x = CrossPlatformInputManager.GetAxis("Horizontal") * Time.deltaTime * MoveSpeed;
		var z = CrossPlatformInputManager.GetAxis("Vertical") * Time.deltaTime * RotateSpeed;

		transform.Rotate(0, x, 0);
		transform.Translate(0, 0, z);

	
		if (Input.GetButtonDown ("Fire1") && Time.time > _nextShotTime  ) //compare game time to nextshot time
		{
		CmdFire ();
			AudioSource shootclip = GetComponent<AudioSource> ();
			shootclip.Play ();
		}




	}

		
		


	[Command]
	void CmdFire(){
		_nextShotTime = Time.time + ReloadRate;//for reload bullet we use this
		var bullet = Instantiate (ShotPrefab, ShotSpawnTransform.position, Quaternion.identity) as GameObject;//make object of projectile and fire forward
		bullet.GetComponent<Rigidbody> ().velocity = transform.forward * ShotSpeed;

		NetworkServer.Spawn (bullet);//respawning bullet over network
	}


	void OnCollisionEnter(Collision collision){//collision method handler
		var other = collision.gameObject;//give reference to a gameobject that we collided
		try{//try to get the component of collided objects
			var causeDamageScript = other.GetComponent<CauseDamage>();
			var totalDamage = causeDamageScript.GetDamage();
			var healthScript = GetComponent<Health>();//store reference of health script
			healthScript.TakeDamage(totalDamage);
		} catch {
			Debug.Log ("Something hit us but didn't do any damage.");
	
	}
	}

}
