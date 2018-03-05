using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq.Expressions;
using DG.Tweening;
using UnityEngine;
using Rewired;
using UnityEngine.SceneManagement;

public class LinePlayer : MonoBehaviour
{
	private Player player;
	public int playerId;
	[SerializeField] private float health;
	[SerializeField] private float speed;
	private float scaleLoss;
	[SerializeField] private Vector3 myScale;
	private Vector3 originalScale;
	private Vector3 spawnPos;

	private bool i_move;
	[SerializeField] private float gunCooldown = 0;
	[SerializeField] private float gunTimer = 0;
	public Material[] mats;
	private bool i_action;
	// Use this for initialization
//	private Light light;

	private GameManager gameManager;
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
		 tweenIsActive = false;
		if (playerId == 0)
		{
			gameObject.layer = 14;
			GameManager.bluePlayer = this;

		}
		else
		{
			gameObject.layer = 15;
			GameManager.pinkPlayer = this;
		}

		spawnPos = transform.position;
		myScale = transform.localScale;
		originalScale = transform.localScale;
		scaleLoss = (myScale.x / 10) + 0.001f;
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
//		light = GetComponentInChildren<Light>();
		
	}

	// Update is called once per frame
	void Update()
	{
		if (!GameManager.thereIsAWinner)
		{
			GetInput();
			ProcessInput();
			if (Input.GetKeyDown(KeyCode.R))
			{
				SceneManager.LoadScene("main");
			}
		}
	}

	private void FixedUpdate()
	{
//		GetInput();
//		ProcessInput();
	}

	void GetInput()
	{
		if (playerId == 0)
		{
			moveVector.x = player.GetAxis("Move");
		}

		else if (playerId == 1)
		{
			moveVector.x = player.GetAxis("P2 Move");
		}
	
		i_action = player.GetButtonDown("Action");
	}

	private bool hasFired = false;
	void ProcessInput()
	{

		if (hasFired)
		{
			gunTimer += Time.deltaTime;
			if (gunTimer >= gunCooldown)
			{
				hasFired = false;
			}
		}

		if (playerId == 0)
		{
			transform.Translate(moveVector * speed * Time.deltaTime);
			if (i_action)
			{
	//			Debug.Log(light.spotAngle);
//				light.spotAngle = 12;
//				StartCoroutine(ReturnToNormalLength());
				if (moveVector.x >= 0 && !hasFired)
				{
					SpawnBullet(1);
					myScale.x *= 1.5f;
					transform.localScale = new Vector3(myScale.x, transform.localScale.y, transform.localScale.z);
					Sequence sequence = DOTween.Sequence();
					sequence.OnPlay(() => SetTweenToActive());
					sequence.Append(transform.DOScaleX(myScale.x/1.5f, 0.5f));
					sequence.OnComplete(() => SetTweenToInactive());
					hasFired = true;
					gunTimer = 0;
				}
				else if(moveVector.x < 0 && !hasFired)
				{
					SpawnBullet(-1);
					myScale.x *= 1.5f;
					transform.localScale = new Vector3(myScale.x, transform.localScale.y, transform.localScale.z);
					Sequence sequence = DOTween.Sequence();
					sequence.OnPlay(() => SetTweenToActive());
					sequence.Append(transform.DOScaleX(myScale.x/1.5f, 0.5f));
					sequence.OnComplete(() => SetTweenToInactive());
					hasFired = true;
					gunTimer = 0;
				}
			}			
		}

		if (playerId == 1)
		{
			transform.Translate(moveVector * speed * Time.deltaTime);
			if (player.GetButtonDown("P2 Action"))
			{
	//			Debug.Log(light.spotAngle);
//				light.spotAngle = 12;
//				StartCoroutine(ReturnToNormalLength());
				if (moveVector.x <= 0 && !hasFired && !tweenIsActive)
				{
					SpawnBullet(-1);
 					myScale.x *= 1.5f;
					transform.localScale = new Vector3(myScale.x, transform.localScale.y, transform.localScale.z);
					Sequence sequence = DOTween.Sequence();
					sequence.OnPlay(() => SetTweenToActive());
					sequence.Append(transform.DOScaleX(myScale.x/1.5f, 0.5f));
					sequence.OnComplete(() => SetTweenToInactive());
					hasFired = true;
					gunTimer = 0;
				}
				else if(moveVector.x > 0 && !hasFired && !tweenIsActive)
				{
					SpawnBullet(1);
					myScale.x *= 1.5f;
					transform.localScale = new Vector3(myScale.x, transform.localScale.y, transform.localScale.z);
					Sequence sequence = DOTween.Sequence();
					sequence.OnPlay(() => SetTweenToActive());
					sequence.Append(transform.DOScaleX(myScale.x/1.5f, 0.5f));
					sequence.OnComplete(() => SetTweenToInactive());
					hasFired = true;
					gunTimer = 0;
				}
			}			
		}


	
	}

	IEnumerator ReturnToNormalLength()
	{
		yield return new WaitForSeconds(0.5f);
//		light.spotAngle = 9;
 	}

	public void SpawnBullet(float direction)
	{
		switch (playerId)
		{
			case 0:
				GameObject bullet0 = Instantiate(Resources.Load("Prefabs/bullet"), transform.position, Quaternion.identity) as GameObject;
				bullet0.GetComponent<Bullet>().direction = direction;
				bullet0.GetComponent<MeshRenderer>().material = bullet0.GetComponent<Bullet>().blueMat;
				bullet0.GetComponent<Bullet>().teamNum = 0;
				break;
			case 1: 
				GameObject bullet = Instantiate(Resources.Load("Prefabs/bullet"), transform.position, Quaternion.identity) as GameObject;
				bullet.GetComponent<Bullet>().direction = direction;
 				bullet.GetComponent<MeshRenderer>().material = bullet.GetComponent<Bullet>().pinkMat;
				bullet.GetComponent<Bullet>().teamNum = 1;
				break;
			default:
				break;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		
		if (other.GetComponent<Enemy>() != null)
		{
			Enemy bullet = other.GetComponent<Enemy>();
			if (other.GetComponent<Enemy>().teamNum != playerId)
			{
				myScale.x -= scaleLoss;	
				transform.localScale = new Vector3(myScale.x, myScale.y, myScale.z);
				Destroy(other.gameObject);
				if (myScale.x <= 0)
				{
					transform.position = spawnPos;
					transform.localScale = originalScale;
					myScale.x = transform.localScale.x;
				}
			}
		}	
	}

	private bool hitTaken = false;

	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.GetComponent<Bullet>() != null)
		{
			Bullet bullet = other.gameObject.GetComponent<Bullet>();
			if (other.gameObject.GetComponent<Bullet>().teamNum != playerId)
			{
 				myScale.x -= scaleLoss;	
				transform.localScale = new Vector3(myScale.x, myScale.y, myScale.z);
				Destroy(other.gameObject);	
				if (myScale.x <= 0)
				{
					transform.position = spawnPos;
					transform.localScale = originalScale;
					myScale.x = transform.localScale.x;
				}
			}
		}
	}

	public bool tweenIsActive = false;

	private void SetTweenToInactive()
	{
		myScale.x = myScale.x / 1.5f;
		tweenIsActive = false;
	}

	private void SetTweenToActive()
	{
		tweenIsActive = true;
	}
}

