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

	void Awake()
	{
		if (myTeam == Team.Left)
		{
			WallCounter.BlueWallsLeft += 1;
		} else if (myTeam == Team.Right)
		{
			WallCounter.PinkWallsLeft += 1;
		}
	}

	void Start ()
	{
		myScale = transform.localScale;
		health = 100f;
		scaleLoss = (myScale.x / 20)+ 0.0001f;
	}
	// Update is called once per frame
	void Update () {
//		transform.Translate(Vector3.right * speed * Time.deltaTime);

		if (myScale.x <= 0)
		{
 			Destroy(gameObject);
		}
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
		else
		{
			if (other.gameObject.GetComponent<Bullet>() != null)
			{
				Bullet bullet = other.gameObject.GetComponent<Bullet>();
				if (bullet.myTeam != myTeam)
				{
					health -= other.gameObject.GetComponent<Bullet>().damage;
					myScale.x -= scaleLoss;	
					transform.localScale = new Vector3(myScale.x, myScale.y, myScale.z);
					Destroy(other.gameObject);		
				}
			}
		}
	}

	private void OnDestroy()
	{
		if (myTeam == Team.Left)
		{
			WallCounter.BlueWallsLeft -= 1;
		} else if (myTeam == Team.Right)
		{
 			WallCounter.PinkWallsLeft -= 1;
		}
	}
}
