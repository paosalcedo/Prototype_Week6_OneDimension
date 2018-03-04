using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{

	[SerializeField] private int numPlayers = 0;
	// Use this for initialization
	void Start () {
		for (int i = 0; i < numPlayers; ++i)
		{
			GameObject player = Instantiate(Resources.Load("Prefabs/player"), Vector3.zero, Quaternion.identity) as GameObject;
			player.GetComponent<LinePlayer>().playerId = i;
			player.GetComponent<MeshRenderer>().material = player.GetComponent<LinePlayer>().mats[i];
			if (i == 0)
			{
				player.transform.position = new Vector3(-13.83f, 4.7f, 0f);
			}
			else
			{
				player.transform.position = new Vector3(14.302f, -4.979f, 0f);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
