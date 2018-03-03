using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using Rewired;
using UnityEngine.SceneManagement;

public class LinePlayer : MonoBehaviour
{
	private Player player;
	public int playerId;
	[SerializeField] private float speed;
	private bool i_move;
	

	private bool i_action;
	// Use this for initialization
	private Light light;
 	
	private Vector3 moveVector;

	public enum MoveState
	{
		Vertical,
		Horizontal,
		InvVertical,
		InvHorizontal
	}

	[SerializeField] private MoveState moveState;

	void Awake () {
		player = ReInput.players.GetPlayer(playerId);
 	}

	void Start()
	{
//		Cursor.visible = false;
//		Cursor.lockState = CursorLockMode.Locked;
		light = GetComponentInChildren<Light>();
	}

	// Update is called once per frame
	void Update () {
		GetInput();
		ProcessInput();
	}

	private void FixedUpdate()
	{
//		GetInput();
//		ProcessInput();
	}

	void GetInput()
	{
//		if (transform.position.x > -11f)
//		{
//			moveVector.y = 0;
//			moveVector.x = player.GetAxis("Move");
//		} else if (transform.position.x <= -11f)
//		{
//			moveVector.x = 0;
//			moveVector.y = -player.GetAxis("Move");
//		} 
		if (playerId == 0)
		{
			moveVector.x = player.GetAxis("Move");
		}

		else if (playerId == 1)
		{
			moveVector.x = player.GetAxis("P2 Move");
		}

		switch (moveState)
		{
			case MoveState.Horizontal:				
				transform.rotation = Quaternion.Euler(0, 0, 0);

 				break;
			case MoveState.Vertical:
				transform.rotation = Quaternion.Euler(0, 0, -90);

				break;
			case MoveState.InvHorizontal:
				transform.rotation = Quaternion.Euler(0, 0, 180);
				break;
			case MoveState.InvVertical:
				transform.rotation = Quaternion.Euler(0, 0, 90);
				break;
			default:
				break;
		}		
		i_action = player.GetButtonDown("Action");
	}

	void ProcessInput()
	{
		if (playerId == 0)
		{
			transform.Translate(moveVector * speed * Time.deltaTime);
			if (i_action)
			{
	//			Debug.Log(light.spotAngle);
				light.spotAngle = 18;
				StartCoroutine(ReturnToNormalLength());
				if (moveVector.x >= 0)
				{
					SpawnBullet(1);			
				}
				else
				{
					SpawnBullet(-1);
				}
			}			
		}

		if (playerId == 1)
		{
			transform.Translate(moveVector * speed * Time.deltaTime);
			if (player.GetButtonDown("P2 Action"))
			{
	//			Debug.Log(light.spotAngle);
				light.spotAngle = 18;
				StartCoroutine(ReturnToNormalLength());
				if (moveVector.x <= 0)
				{
					SpawnBullet(-1);			
				}
				else
				{
					SpawnBullet(1);
				}
			}			
		}


		if (Input.GetKeyDown(KeyCode.R))
		{
			SceneManager.LoadScene("main");
		}
	}

	IEnumerator ReturnToNormalLength()
	{
		yield return new WaitForSeconds(0.5f);
		light.spotAngle = 9;
	}

	public void SpawnBullet(float direction)
	{
		if (playerId == 1)
		{
			GameObject bullet = Instantiate(Resources.Load("Prefabs/bullet"), transform.position, Quaternion.identity) as GameObject;
			bullet.GetComponent<Bullet>().direction = direction;
//			bullet.GetComponent<MeshRenderer>()
			bullet.GetComponent<MeshRenderer>().material = bullet.GetComponent<Bullet>().pinkMat;
			bullet.GetComponent<Bullet>().teamNum = 1;
		}
		
		if(playerId == 0)
		{
			GameObject bullet = Instantiate(Resources.Load("Prefabs/bullet"), transform.position, Quaternion.identity) as GameObject;
			bullet.GetComponent<Bullet>().direction = direction;
//			bullet.GetComponent<MeshRenderer>()
			bullet.GetComponent<MeshRenderer>().material = bullet.GetComponent<Bullet>().blueMat;
			bullet.GetComponent<Bullet>().teamNum = 0;
		}
	}

}

