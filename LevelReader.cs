using UnityEngine;
using System.Collections;
using System.Xml;
using System.Collections.Generic;

public class LevelReader : MonoBehaviour {
	
	XmlDocument levelDoc;
	XmlNodeList levelist;
	List<string> levelArray;
	//private string level{get;set;}
	
	
	// Use this for initialization
	void Start () {
		
		levelArray = new List<string> ();
		levelDoc = new XmlDocument ();
		
		TextAsset xmlfile = Resources.Load ("levels", typeof(TextAsset)) as TextAsset;
		levelDoc.LoadXml (xmlfile.text);
		levelist = levelDoc.GetElementsByTagName ("level");
		foreach( XmlNode leveldata in levelist){
			XmlNodeList levelinfo = leveldata.ChildNodes;
			foreach(XmlNode data in levelinfo){
				if(data.Name == "setup"){
					levelArray.Add(data.InnerText);
//				if(data.Name=="levelname")
//				    level=data.InnerText;
				}
			}	
		}	
	}
	
	public void loadLevel(int nr){

		
		string[] levString = levelArray[nr -1].Split(',');
		//string sub = levString.Substring(0, levString.Length - 5);
		foreach(string tile in levString)
		{
			    GameObject.Find("MainCamera").GetComponent<LevelCreator>().SetTile(tile);
		}
	}
}
