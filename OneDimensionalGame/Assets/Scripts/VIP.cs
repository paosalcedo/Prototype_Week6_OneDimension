using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VIP : MonoBehaviour
{

	private float speed = 1f;
	private Vector3 myScale;
	private float scaleLoss;
	[SerializeField]private float health = 100f;

	public Team myTeam;
	// Use this for initialization
	void Start ()
	{
		myScale = transform.localScale;
		health = 100f;
		scaleLoss = myScale.x / 10;
		Debug.Log(scaleLoss);
	}
	
	// Update is called once per frame
	void Update () {
//		transform.Translate(Vector3.right * speed * Time.deltaTime);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.GetComponent<Enemy>() != null)
		{
			if (myTeam == Team.Left)
			{
				if (other.gameObject.layer == 11)
				{
					health -= other.gameObject.GetComponent<Enemy>().damage;
					myScale.x -= scaleLoss;	
					transform.localScale = new Vector3(myScale.x, myScale.y, myScale.z);
					Destroy(other.gameObject);					
				}
			}
			else
			{
				if (other.gameObject.layer == 10)
				{
					health -= other.gameObject.GetComponent<Enemy>().damage;
					myScale.x -= scaleLoss;	
					transform.localScale = new Vector3(myScale.x, myScale.y, myScale.z);
					Destroy(other.gameObject);					
				}
			}

		}
	}
}
