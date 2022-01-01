using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	private Transform player;
	private float yoffset= 2.5f;
	private float zoffset = -6.1f;
	void Start () {
		player = GameObject.Find("Player").transform;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = new Vector3 (player.position.x,player.position.y+yoffset,player.position.z+zoffset);
	}
}
