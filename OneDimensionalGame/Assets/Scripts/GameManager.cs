using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	[SerializeField] private Text winText;
	[SerializeField] private Text[] introTexts;
	[SerializeField] private Text controlsText;
	[SerializeField]Camera myCam;
	public static LinePlayer bluePlayer;
	public static LinePlayer pinkPlayer;
	public static bool thereIsAWinner;
	private float t;

	void Start()
	{
//		StartCoroutine(HideText());
		thereIsAWinner = false;
		showControls = false;
	}

	private bool showControls = false;
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			showControls = !showControls;
		}

		Time.timeScale = showControls ? 1 : 0;
		
		controlsText.enabled = showControls;
		for (int i = 0; i < introTexts.Length; ++i)
		{
			introTexts[i].enabled = !showControls;
		}
		
		if (WallCounter.BlueWallsLeft <= 0)
		{
			//pink wins
			t += Time.deltaTime;
			myCam.transform.DOMove(pinkPlayer.transform.position+Vector3.back, 1f, false);
			myCam.orthographicSize = Mathf.Lerp(9.6f, 1f, t);
			winText.enabled = true;
//			DOTween.KillAll(false);

			if (!thereIsAWinner)
			{
				StartCoroutine(RestartScene());
				thereIsAWinner = true;
			}

//			SceneManager.LoadScene("main");
		} else if (WallCounter.PinkWallsLeft <= 0)
		{
			//blue wins
			t += Time.deltaTime;
			myCam.transform.DOMove(bluePlayer.transform.position + Vector3.back, 1f, false);		
			myCam.orthographicSize = Mathf.Lerp(9.6f, 1f, t);
			winText.enabled = true;
//			DOTween.KillAll(false);

			if (!thereIsAWinner)
			{
				StartCoroutine(RestartScene());
				thereIsAWinner = true;
			}
//			SceneManager.LoadScene("main");
		}
	}

	IEnumerator RestartScene()
	{
		yield return new WaitForSeconds(5f);
		DOTween.KillAll(false);
		SceneManager.LoadScene("main");
	}
	
	IEnumerator HideText()
	{
		yield return new WaitForSeconds(10f);
		for (int i = 0; i < introTexts.Length; ++i)
		{
			introTexts[i].enabled = false;
			controlsText.enabled = true;
		}
	}
}




public class WallCounter
{
	public static int BlueWallsLeft;
	public static int PinkWallsLeft;
}


