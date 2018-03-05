using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float direction;
	public float damage;
	[SerializeField]private float speed;
	[SerializeField] private float lifeTime;

	public int teamNum;
	public Material blueMat;
	public Material pinkMat;

	public Team myTeam;
	// Use this for initialization
	void Start () {
		Destroy(gameObject, lifeTime);
		if (teamNum == 0)
		{
			myTeam = Team.Left;
			gameObject.layer = 14;
		}
		else
		{
			myTeam = Team.Right;
			gameObject.layer = 15;
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(transform.right * speed * direction * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<Enemy>() != null)
		{
			if (other.GetComponent<Enemy>().teamNum != teamNum)
			{
				Destroy(other.gameObject);
				Destroy(gameObject);
			}
		} 

	}
}
