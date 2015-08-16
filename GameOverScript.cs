using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour {

	private int curScore;
	private int highScore;
	public Text scoreText;
	public Text highScoreText;

	void Start () 
	{
		curScore=GameManager.instance.score;
	}
	

	void Update () 
	{
		if (curScore > PlayerPrefs.GetInt ("HighScore"))
		{
			highScore = curScore;
			PlayerPrefs.SetInt("HighScore", highScore);
		}
//		else if(curScore > highScore)
//			highScore=PlayerPrefs.GetInt("HighScore",0);

		scoreText.text=""+curScore;
		highScoreText.text=""+PlayerPrefs.GetInt("HighScore");
	}
}
