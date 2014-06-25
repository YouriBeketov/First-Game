using UnityEngine;
using System.Collections;

public class Lightview : MonoBehaviour {

	public Transform Head; 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		this.transform.forward= Head.transform.forward;;
	
	}
}
