using UnityEngine;
using System.Collections;

public class Sight3 : MonoBehaviour {
	public Transform transformOfParent;
	private RaycastHit hit; 

	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {

		if (Physics.Raycast(this.transform.position, this.transform.forward,out hit, 5))
		{
			if (hit.collider.gameObject.CompareTag("Player"))
			{

				Debug.Log("got you ");
			}
		}

		StartCoroutine(lookRight());
		Debug.Log (this.transform.rotation.y.ToString());



		
	}
	public IEnumerator lookRight()
	{
		Debug.DrawRay(this.transform.position, this.transform.forward,Color.red,20);
		this.renderer.material.color=Color.red;
		this.transform.forward= transformOfParent.transform.forward;
		yield return new  WaitForEndOfFrame();
		this.renderer.material.color=Color.blue;
		Debug.DrawRay(this.transform.position, this.transform.forward,Color.red,20);
		//this.transform.forward= transformOfParent.transform.right;
		//StartCoroutine(lookLeft());

	}
	public IEnumerator lookLeft()
	{
		
		yield return new WaitForSeconds(0.1f);
		this.transform.forward = -transformOfParent.transform.right;
		
	}
}



