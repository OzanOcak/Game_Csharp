using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour
{
	private bool loading{get;set;}
	void Awake()
	{
		Screen.orientation=ScreenOrientation.LandscapeLeft;
		loading=false;
	}


	public void InitGame () 
	{
		loading=true;
		Application.LoadLevel("scene2");
	}

	void Update()
	{

		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit ();
			loading=false;
		}
	}
}
