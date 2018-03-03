using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float direction;
	[SerializeField]private float speed;

	public Material blueMat;
	public Material pinkMat;
	// Use this for initialization
	void Start () {
		Destroy(gameObject, 0.25f);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(transform.right * speed * direction * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<Enemy>() != null)
		{
			Destroy(other.gameObject);
			Destroy(gameObject);
		}
	}
}
