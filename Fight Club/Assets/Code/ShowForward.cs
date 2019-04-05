using UnityEngine;
using System.Collections;

public class ShowForward : MonoBehaviour
{
    public Color ForwardColor = Color.red;
    public Color UpColor = Color.green;
    public int LineLength = 1000;
	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        var forward = transform.TransformDirection(Vector3.forward) * LineLength;
        Debug.DrawRay(transform.position, forward, ForwardColor);
        var upward = transform.TransformDirection(Vector3.up) * LineLength;
        Debug.DrawRay(transform.position, upward, UpColor);
    }
}
