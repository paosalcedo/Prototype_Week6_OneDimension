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
		moveVector.x = player.GetAxis("Move");

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
		transform.Translate(moveVector * speed * Time.deltaTime);
		if (i_action)
		{
//			Debug.Log(light.spotAngle);
			light.spotAngle = 30;
			StartCoroutine(ReturnToNormalLength());
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

	private bool isMovingRight;
	void OnTriggerEnter(Collider trigger)
	{
 //		if (trigger.gameObject.layer == 8)
//		{
//			if (moveState == MoveState.Horizontal)
//			{
// //				if (moveVector.x < 0)
////				{
////					transform.rotation = Quaternion.Euler(0, 0, transform.rotation.z+newRotation);				
////				} else if (moveVector.x >= 0)
////				{
////					transform.rotation = Quaternion.Euler(0, 0, transform.rotation.z-newRotation);								
////				}
//
// 				moveState = MoveState.Vertical;
//			} else if (moveState == MoveState.Vertical)
//			{
// //				if (moveVector.x < 0)
////				{
////					transform.rotation = Quaternion.Euler(0, 0, transform.rotation.z+newRotation);				
////				} else if (moveVector.x >= 0)
////				{
////					transform.rotation = Quaternion.Euler(0, 0, transform.rotation.z-newRotation);								
////				}
// 				moveState = MoveState.Horizontal;
//			}
//		}
		
		if (trigger.gameObject.layer == 8)
		{
			if (moveState == MoveState.Horizontal)
			{
				if (moveVector.x >= 0)
				{
					if (transform.position.x >= 0)
					{
						moveState = MoveState.Vertical;									
					}
					else
					{
						moveState = MoveState.InvVertical;
					}

				} else if (moveVector.x < 0)
				{
					if (transform.position.x >= 0)
					{
						moveState = MoveState.InvVertical;									
					}
					else
					{
						moveState = MoveState.Vertical;
					}
				}
			} else if (moveState == MoveState.Vertical)
			{ 
				if (moveVector.x >= 0) //moving forward on the line
				{
					if (transform.position.x >= 0) //right side
					{
						moveState = MoveState.InvHorizontal;									
					}
					else
					{
						moveState = MoveState.Horizontal;									
					}

				} else if (moveVector.x < 0) //moving backwards along the line
				{
//					moveState = MoveState.Horizontal;
					if (transform.position.x >= 0) // on the right side of the screen
					{
						moveState = MoveState.Horizontal;									
					}
					else
					{
						moveState = MoveState.InvHorizontal;									
					}
				}			
			} else if (moveState == MoveState.InvHorizontal)
			{
				if (moveVector.x >= 0)
				{
					if (transform.position.x >= 0)
					{
						moveState = MoveState.InvVertical;						
					} else if (transform.position.x < 0)
					{
						moveState = MoveState.Vertical;						
					}			
				} else if (moveVector.x < 0)
				{
					if (transform.position.x >= 0)
					{
						moveState = MoveState.Vertical;						
					} else if (transform.position.x < 0)
					{
						moveState = MoveState.InvVertical;						
					}
				}
			} else if (moveState == MoveState.InvVertical)
			{ 
				if (moveVector.x >= 0)
				{
					moveState = MoveState.InvHorizontal;				
				} else if (moveVector.x < 0)
				{
					moveState = MoveState.Horizontal;
				}			
			}
		} 
	} 

}

