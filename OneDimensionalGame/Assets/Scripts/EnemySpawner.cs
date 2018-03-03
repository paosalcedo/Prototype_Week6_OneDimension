using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField]private float timer;
	[SerializeField]private float timeBeforeNextEnemy;

	public Team team;
	
	// Use this for initialization
	void Start () {
		SpawnEnemy();
	}
	
	// Update is called once per frame
	void Update ()
	{
		timer += Time.deltaTime;
		if (timer >= timeBeforeNextEnemy)
		{
			SpawnEnemy();
			timer = 0;
		}
	}

	void SpawnEnemy()
	{
		switch (team)
		{
			case Team.Left:
				List<GameObject> leftCreeps = new List<GameObject>();
				leftCreeps.Add(Instantiate(Resources.Load("Prefabs/enemy"), new Vector3(-13.83f, 4.7f, 0), Quaternion.identity) as GameObject);
				leftCreeps.Add(Instantiate(Resources.Load("Prefabs/enemy"), new Vector3(-12.83f, 4.7f, 0), Quaternion.identity) as GameObject);
				leftCreeps.Add(Instantiate(Resources.Load("Prefabs/enemy"), new Vector3(-11.83f, 4.7f, 0), Quaternion.identity) as GameObject);
				foreach (var creep in leftCreeps)
				{
					creep.GetComponent<Enemy>().direction = 1f;
					creep.layer = 10;
				}
				break;
			
			case Team.Right:
				List<GameObject> rightCreeps = new List<GameObject>();
				rightCreeps.Add(Instantiate(Resources.Load("Prefabs/enemy"), new Vector3(15.21f, -4.969f, 0), Quaternion.identity) as GameObject);
				rightCreeps.Add(Instantiate(Resources.Load("Prefabs/enemy"), new Vector3(14.21f, -4.969f, 0), Quaternion.identity) as GameObject);
				rightCreeps.Add(Instantiate(Resources.Load("Prefabs/enemy"), new Vector3(13.21f, -4.969f, 0), Quaternion.identity) as GameObject);
				foreach (var creep in rightCreeps)
				{
					creep.GetComponent<Enemy>().direction = -1f;
					creep.layer = 11;
				}
				break;
			
			default:
				break;
		}

	}
}
