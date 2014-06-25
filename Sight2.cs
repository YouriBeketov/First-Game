using UnityEngine;
using System.Collections;

public class Sight2 : MonoBehaviour {


	public Transform transformOfParent;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawRay(this.transform.position, this.transform.forward,Color.red,20); 
		pPosition();


		this.transform.Rotate(0f,10f,0f);
		Quaternion x = this.transform.rotation;

		Quaternion z =transformOfParent.transform.rotation ;

		if (x.y<z.y)
		{
			Debug.Log ("HHOOOO");
			StartCoroutine("lookRight");

			//this.transform.Rotate(0f,-180f,0f);
		}


	}

	public void pPosition() 
	{
		var x = GameObject.Find("Cube"); 
		
		if (x== null)
		{
			Debug.Log ("no object");
		}
		else
		{
			 // Look at target 
				this.transform.rotation	= Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(x.gameObject.transform.position), 1 * Time.deltaTime);



		}
	}

	public IEnumerator lookRight()
	{
		this.transform.Rotate(0f,35f,0f);
		yield return new WaitForEndOfFrame();
        
		this.transform.Rotate(0f,-90f,0f);
	}
}
