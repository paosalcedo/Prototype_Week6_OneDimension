using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{

	[SerializeField] private Portal portalToTeleportTo;
	public int portalNum;

	public float offsetX;
	private Vector3 offset;
	// Use this for initialization
	void Start ()
	{
		offset.x = offsetX;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider trigger)
	{
		trigger.gameObject.transform.position = portalToTeleportTo.transform.position + portalToTeleportTo.offset;
	}
}
