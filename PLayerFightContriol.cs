using UnityEngine;
using System.Collections;

public class PLayerFightContriol : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.E))
		   {
			PLayerDefense ProtectPlayer = this.gameObject.GetComponent<PLayerDefense>(); 
			ProtectPlayer.defending = true;
		}
		else if (Input.GetKeyUp(KeyCode.E))
		{
			PLayerDefense ProtectPlayer = this.gameObject.GetComponent<PLayerDefense>(); 
			ProtectPlayer.defending = false;
		}
	
	}
}
