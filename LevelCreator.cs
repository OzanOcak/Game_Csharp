using UnityEngine;
using System.Collections;

public class LevelCreator : MonoBehaviour 
{
	public GameObject tiles{get;set;}
	public GameObject gameLayer{get;set;}
	public GameObject tilePos;
	private GameObject tmpTile4Layer;
	private Transform player;
	private string tmpEnemyToGame;

	public float startPosY{get;private set;}
	public const float tileWidth=1.98f;

	private float outOfBounceX;
	//private string lastTile="";
	public int heightLevel=0;
    LevelReader reader;
	private int x=1;
//	private GameObject enemy;
	void Awake ()
	{

		tiles=GameObject.Find ("Tiles");
		gameLayer=GameObject.Find("GameLayer");
		player=GameObject.Find("Player").transform;

		Screen.orientation=ScreenOrientation.LandscapeLeft;

		reader=GetComponent<LevelReader>();

		for(int i=0; i<81; i++){
			GameObject tmpTile00=Instantiate(Resources.Load ("tile00",typeof(GameObject)))as GameObject;
			tmpTile00.transform.parent=tiles.transform.FindChild("t00").transform;
			tmpTile00.transform.position=Vector2.zero;
		}
		for (int i=0;i<31;i++){

			GameObject tmpTile01=Instantiate(Resources.Load ("tile01",typeof(GameObject)))as GameObject;
			tmpTile01.transform.parent=tiles.transform.FindChild("t01").transform;
			tmpTile01.transform.position=Vector2.zero;

			GameObject tmpTile02=Instantiate(Resources.Load ("tile02",typeof(GameObject)))as GameObject;
			tmpTile02.transform.parent=tiles.transform.FindChild("t02").transform;
			tmpTile02.transform.position=Vector2.zero;

			GameObject tmpTile03=Instantiate(Resources.Load ("tile03",typeof(GameObject)))as GameObject;
			tmpTile03.transform.parent=tiles.transform.FindChild("t03").transform;
			tmpTile03.transform.position=Vector2.zero;

			GameObject tmpTile04=Instantiate(Resources.Load ("tile04",typeof(GameObject)))as GameObject;
			tmpTile04.transform.parent=tiles.transform.FindChild("t04").transform;
			tmpTile04.transform.position=Vector2.zero;

			GameObject tmpTile05=Instantiate(Resources.Load ("tile05",typeof(GameObject)))as GameObject;
			tmpTile05.transform.parent=tiles.transform.FindChild("t05").transform;
			tmpTile05.transform.position=Vector2.zero;

		}


		for (int i = 0; i<8; i++) {

			GameObject tmpG5 = Instantiate(Resources.Load("enemy01", typeof(GameObject))) as GameObject;
			tmpG5.transform.parent = tiles.transform.FindChild("e01").transform;
			tmpG5.transform.position=Vector2.zero;

			GameObject tmpG6 = Instantiate(Resources.Load("enemy02", typeof(GameObject))) as GameObject;
			tmpG6.transform.parent = tiles.transform.FindChild("e02").transform;
			tmpG6.transform.position = Vector2.zero;

			GameObject tmpG8 = Instantiate(Resources.Load("enemy03",typeof(GameObject)))as GameObject;
			tmpG8.transform.parent=tiles.transform.FindChild("e03").transform;
			tmpG8.transform.position=Vector2.zero;	
			GameObject tmpTile09=Instantiate(Resources.Load ("tiletree1",typeof(GameObject)))as GameObject;
			tmpTile09.transform.parent=tiles.transform.FindChild("t00t1").transform;
			tmpTile09.transform.position=Vector2.zero;
			
			GameObject tmpTile10=Instantiate(Resources.Load ("tiletree2",typeof(GameObject)))as GameObject;
			tmpTile10.transform.parent=tiles.transform.FindChild("t00t2").transform;
			tmpTile10.transform.position=Vector2.zero;
			
			GameObject tmpTile11=Instantiate(Resources.Load ("tiletree3",typeof(GameObject)))as GameObject;
			tmpTile11.transform.parent=tiles.transform.FindChild("t00t3").transform;
			tmpTile11.transform.position=Vector2.zero;
			
			GameObject tmpTile12=Instantiate(Resources.Load ("tiletree4",typeof(GameObject)))as GameObject;
			tmpTile12.transform.parent=tiles.transform.FindChild("t00t4").transform;
			tmpTile12.transform.position=Vector2.zero;
			
			GameObject tmpTile13=Instantiate(Resources.Load ("tiletree5",typeof(GameObject)))as GameObject;
			tmpTile13.transform.parent=tiles.transform.FindChild("t00t5").transform;
			tmpTile13.transform.position=Vector2.zero;

		}

		for (int i = 0; i<4; i++) {

			GameObject tmpTile06=Instantiate(Resources.Load ("tileFini",typeof(GameObject)))as GameObject;
			tmpTile06.transform.parent=tiles.transform.FindChild("tFini").transform;
			tmpTile06.transform.position=Vector2.zero;

			GameObject tmpG7 = Instantiate(Resources.Load("score100", typeof(GameObject))) as GameObject;
			tmpG7.transform.parent = tiles.transform.FindChild("s100").transform;
			tmpG7.transform.position=Vector2.zero;
		}


		tiles.transform.position=new Vector2(-80f,-40f);

		tilePos=GameObject.Find ("startingTilePos");
		startPosY=tilePos.transform.position.y;

		//outOfBounceX=tilePos.transform.position.x-10f;

		FirstSceen();
	}
//	void Start()
//	{
//		enemy=GameObject.Find ("enemy01(Clone)");
//	}

	void FixedUpdate()
	{
		//gameLayer.transform.position = new Vector2(gameLayer.transform.position.x-gameSpeed*Time.deltaTime,0f);
		outOfBounceX=player.transform.position.x-25f;

		foreach(Transform child in gameLayer.transform)
		{
			if (child.position.x<outOfBounceX)
			{
				switch(child.gameObject.name)
				{
				case"tile00(Clone)":
					child.gameObject.transform.position=tiles.transform.FindChild("t00").transform.position;
					child.gameObject.transform.parent=tiles.transform.FindChild("t00").transform;
					break;
				case"tile01(Clone)":
					child.gameObject.transform.position=tiles.transform.FindChild("t01").transform.position;
					child.gameObject.transform.parent=tiles.transform.FindChild("t01").transform;
					break;
				case"tile02(Clone)":
					child.gameObject.transform.position=tiles.transform.FindChild("t02").transform.position;
					child.gameObject.transform.parent=tiles.transform.FindChild("t02").transform;
					break;
				case"tile03(Clone)":
					child.gameObject.transform.position=tiles.transform.FindChild("t03").transform.position;
					child.gameObject.transform.parent=tiles.transform.FindChild("t03").transform;
					break;
				case"tile04(Clone)":
					child.gameObject.transform.position=tiles.transform.FindChild("t04").transform.position;
					child.gameObject.transform.parent=tiles.transform.FindChild("t04").transform;
					break;
				case"tile05(Clone)":
					child.gameObject.transform.position=tiles.transform.FindChild("t05").transform.position;
					child.gameObject.transform.parent=tiles.transform.FindChild("t05").transform;
					break;
				case"tileFini(Clone)":
					child.gameObject.transform.position=tiles.transform.FindChild("tFini").transform.position;
					child.gameObject.transform.parent=tiles.transform.FindChild("tFini").transform;
					break;
				case"Gold":
					GameObject.Find("Gold").GetComponent<GoldScript>().inPlay=false;
					break;
				case"enemy01(Clone)":
					child.gameObject.transform.position = tiles.transform.FindChild("e01").transform.position;
					child.gameObject.transform.parent = tiles.transform.FindChild("e01").transform;
					break;
				case"enemy02(Clone)":
					child.gameObject.transform.position = tiles.transform.FindChild("e02").transform.position;
					child.gameObject.transform.parent = tiles.transform.FindChild("e02").transform;
					break;
				case"enemy03(Clone)":
					child.gameObject.transform.position=tiles.transform.FindChild("e03").transform.position;
					child.gameObject.transform.parent=tiles.transform.FindChild("e03").transform;
					break;

				case"tiletree1(Clone)":
					child.gameObject.transform.position=tiles.transform.FindChild("t00t1").transform.position;
					child.gameObject.transform.parent=tiles.transform.FindChild("t00t1").transform;
					break;
				case"tiletree2(Clone)":
					child.gameObject.transform.position=tiles.transform.FindChild("t00t2").transform.position;
					child.gameObject.transform.parent=tiles.transform.FindChild("t00t2").transform;
					break;
				case"tiletree3(Clone)":
					child.gameObject.transform.position=tiles.transform.FindChild("t00t3").transform.position;
					child.gameObject.transform.parent=tiles.transform.FindChild("t00t3").transform;
					break;
				case"tiletree4(Clone)":
					child.gameObject.transform.position=tiles.transform.FindChild("t00t4").transform.position;
					child.gameObject.transform.parent=tiles.transform.FindChild("t00t4").transform;
					break;
				case"tiletree5(Clone)":
					child.gameObject.transform.position=tiles.transform.FindChild("t00t5").transform.position;
					child.gameObject.transform.parent=tiles.transform.FindChild("t00t5").transform;
					break;
				default:
					Destroy(child.gameObject);
					break;
				}
			}
		}

	}

	void LateUpdate()
	{

		if(gameLayer.transform.childCount<50)
		{
			reader.loadLevel(x);
			x++;
		}
	}
	
 	private void FirstSceen()
	{
		for(int i=0;i<10;i++){
			SetTile("zero");
		}
	}

	public void SetTile(string type)
	{

		switch (type)
		{
		case "zero":
		    tmpTile4Layer=tiles.transform.FindChild("t00").transform.GetChild(0).gameObject;
		    break;
		case "inc":
			heightLevel+=2;
			tmpTile4Layer=tiles.transform.FindChild("t01").transform.GetChild(0).gameObject;
			break;
		case "dec":
			tmpTile4Layer=tiles.transform.FindChild("t02").transform.GetChild(0).gameObject;
			heightLevel-=2;
			break;
		case "w3":
			tmpTile4Layer=tiles.transform.FindChild("t03").transform.GetChild(0).gameObject;
			break;
		case "down":
			tmpTile4Layer=tiles.transform.FindChild("t00").transform.GetChild(0).gameObject;
			heightLevel--;
			break;
		case "down2":
			tmpTile4Layer=tiles.transform.FindChild("t00").transform.GetChild(0).gameObject;
			heightLevel-=2;
			break;
		case"up":
			tmpTile4Layer=tiles.transform.FindChild("t00").transform.GetChild(0).gameObject;
			heightLevel++;
			break;
		case"up2":
		    tmpTile4Layer=tiles.transform.FindChild("t00").transform.GetChild(0).gameObject;
			heightLevel+=2;
			break;
		case "uper":
			tmpTile4Layer=tiles.transform.FindChild("t04").transform.GetChild(0).gameObject;
			break;
		case "w2":
			tmpTile4Layer=tiles.transform.FindChild("t05").transform.GetChild(0).gameObject;
			break;
		case "tree1":
			tmpTile4Layer=tiles.transform.FindChild("t00t1").transform.GetChild(0).gameObject;
			break;
		case "tree2":
			tmpTile4Layer=tiles.transform.FindChild("t00t2").transform.GetChild(0).gameObject;
			break;
		case "tree3":
			tmpTile4Layer=tiles.transform.FindChild("t00t3").transform.GetChild(0).gameObject;
			break;
		case "tree4":
			tmpTile4Layer=tiles.transform.FindChild("t00t4").transform.GetChild(0).gameObject;
			break;
		case "tree5":
			tmpTile4Layer=tiles.transform.FindChild("t00t5").transform.GetChild(0).gameObject;
			break;
		case"fini":
			tmpTile4Layer=tiles.transform.FindChild("tFini").transform.GetChild(0).gameObject;
			break;
			
			
			//		case"upBack":
//			tmpTile4Layer=tiles.transform.FindChild("t00").transform.GetChild(0).gameObject;
//			heightLevel++;
//			tmpTile4Layer.transform.parent=gameLayer.transform;
//			tmpTile4Layer.transform.position= new Vector2(tilePos.transform.position.x-(tileWidth),startPosY+(heightLevel*tileWidth));
//			tilePos=tmpTile4Layer;
//			break;
		case"e1":
			tmpTile4Layer=tiles.transform.FindChild("t00").transform.GetChild(0).gameObject;
			tmpEnemyToGame="e01";
			AddEnemy(tmpEnemyToGame);
			break;
		case"e2":
			tmpTile4Layer=tiles.transform.FindChild("t00").transform.GetChild(0).gameObject;
			tmpEnemyToGame="e02";
			AddEnemy(tmpEnemyToGame);
			break;
		case "e3":
			tmpTile4Layer=tiles.transform.FindChild("t00").transform.GetChild(0).gameObject;
			tmpEnemyToGame="e03";
			AddEnemy(tmpEnemyToGame);
			break;
		case"down9":

		    tmpTile4Layer=tiles.transform.FindChild("t05").transform.GetChild(0).gameObject;
			heightLevel-=9;

			break;

		}
		tmpTile4Layer.transform.parent=gameLayer.transform;
		tmpTile4Layer.transform.position= new Vector2(tilePos.transform.position.x+(tileWidth),startPosY+(heightLevel*tileWidth));// pour -x direction, j'ai beson -tileWidth d'ici 
		tilePos=tmpTile4Layer;
		//lastTile=type;
	}
	private void AddEnemy(string type)
	{

		GameObject newEnemy = tiles.transform.FindChild(type).transform.GetChild(0).gameObject;
		newEnemy.transform.parent = gameLayer.transform;
		newEnemy.transform.position = new Vector2(tilePos.transform.position.x+tileWidth, startPosY + (heightLevel*tileWidth + (tileWidth*3)));
	}

	
}
