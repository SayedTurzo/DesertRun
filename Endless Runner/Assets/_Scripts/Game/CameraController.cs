using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public GameObject Player;
	public Vector3 offset;
	void Start()
	{
		offset = transform.position - Player.transform.position;
	}

	// Update is called once per frame
	void Update()
	{
		transform.position = new Vector3(Player.transform.position.x + offset.x, offset.y, offset.z);
	}
}
