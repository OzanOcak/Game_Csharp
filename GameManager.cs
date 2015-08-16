using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager instance=null;
	public int score=0;
	public int stage=1;
	public Text guiText;
	public float startDelay=.5f;
	private Text levelText;
	public GameObject levelImage;
	public int level=1;
	//private bool enemyMove;
	//private bool doingSetUp;

	private Player player;
	public Text stageText;

	void Awake()
	{
		if (instance==null)
			instance=this;
		else if(instance !=this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
		InitGame ();


	}


	void OnLevelWasLoaded(int index){

		InitGame();
		//level++;
	}
    void InitGame(){
		//doingSetUp=true;
		levelImage=GameObject.Find ("Canvas").transform.FindChild("LevelImage").gameObject;
		levelText=GameObject.Find("Canvas").transform.FindChild("LevelImage").transform.FindChild("LevelText").GetComponent<Text>();
		//stageText=GameObject.Find ("stageText").GetComponent<Text>();
		levelImage.SetActive (true);
		levelText.text="Level "+level;

		Invoke ("HideLevelImage",startDelay);
	}
	void HideLevelImage(){
		levelImage.SetActive(false);
		//doingSetUp=false;
	}
	void Update(){
		guiText.text=" "+ score;
		stageText.text="Stage :"+stage;
//		if(enemyMove|| doingSetUp)
//			return;
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit ();
		}


	}
//	public void GameOver()
//	{
//		levelText.text="game over";
//		levelImage.SetActive(true);
//		enabled=false;
//	}
	
}
