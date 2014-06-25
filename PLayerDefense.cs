using UnityEngine;
using System.Collections;

public class PLayerDefense : MonoBehaviour {

	public  bool defending = false; 
	
	public bool Defending
	{
		get
		{return defending;}
		set
		{ defending = value;}
	}
}
