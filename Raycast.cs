using UnityEngine;
using System.Collections;

public class Raycast : MonoBehaviour {

	public GameObject xCharacter;
	private RaycastHit hit; 

	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 AIPosition= xCharacter.transform.forward;

		if (Physics.Raycast(this.transform.position,AIPosition,out hit,10))
		{
			if (hit.collider.gameObject.CompareTag("Finish"))
			 {
				Debug.Log("wall");
			}
			else if (hit.collider.gameObject.CompareTag("Player"))
			{
				Debug.Log("got you ");
			}
		}

		}
	
	}

