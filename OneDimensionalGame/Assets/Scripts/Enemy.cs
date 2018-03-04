using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public float speed = 5f;

	public float health;

	public float damage;

	public float direction;
	public int teamNum;
	public Material pinkMat;
	public Material blueMat;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(transform.right * speed * direction * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.GetComponent<Enemy>() != null)
		{
			if (other.GetComponent<Enemy>().teamNum != teamNum)
			{
				Destroy(other.gameObject);
				Destroy(gameObject);
			}
		}
	}

}
