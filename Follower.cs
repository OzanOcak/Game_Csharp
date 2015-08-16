using UnityEngine;
using System.Collections;

public class Follower : MonoBehaviour 
{
	private Transform camera;
	public Vector3 offSet;

	void Start () 
	{
		camera=GameObject.Find ("MainCamera").transform;
	}

	void Update () 
	{
		transform.position=camera.position+offSet;
	}
}
