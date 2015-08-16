using UnityEngine;
using System.Collections;

public class scoreTransformer : MonoBehaviour {

	private Vector2 scorePos;
	public LevelCreator level{get;set;}
	private GameObject score;

	void Start()
	{
		level=GameObject.Find ("MainCamera").GetComponent<LevelCreator>();

	}

	public void AddScore100(Vector2 enemyPos)
	{
	    if(level!=null){
		GameObject newScore = level.tiles.transform.FindChild("s100").transform.GetChild(0).gameObject;
		newScore.transform.parent=level.gameLayer.transform;
		newScore.transform.position=enemyPos;
		
		Vector2 newScorePos=enemyPos;
		newScorePos.y+=1.5f;
		newScore.transform.position=newScorePos;
		}


		StartCoroutine(dieTransformer());
		
	}


	IEnumerator dieTransformer()
	{
		yield return new WaitForSeconds(1f);

		if(level!=null){
		    gameObject.transform.position=level.tiles.transform.FindChild("s100").transform.position;
			gameObject.transform.parent=level.tiles.transform.FindChild("s100").transform;
	    }
	}

}
