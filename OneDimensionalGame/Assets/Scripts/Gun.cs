using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;

public class Gun : MonoBehaviour
{

	private int myDirection;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void SpawnBullet()
	{
		GameObject bullet = Instantiate(Resources.Load("Prefabs/bullet"), transform.position, Quaternion.identity) as GameObject;
	}
}
