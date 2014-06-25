using UnityEngine;
using System.Collections;

public class RayCastAI : MonoBehaviour {

	public float movementSpeed;
	public float rotationSpeed;

	private int direction =1; 
	private RaycastHit hit; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//Debug.DrawRay(this.transform.position, this.transform.forward,Color.blue,20); 

		if (Physics.Raycast(this.transform.position, this.transform.forward,out hit, 5))
		{

			if (hit.collider.gameObject.tag =="Finish")
			{
				if (Physics.Raycast(this.transform.position, - this.transform.right,5))
				{
					direction=1;
				}
				else if (Physics.Raycast(this.transform.position, - this.transform.right,5))
				 {
					direction=-1;
				}
				else {
				StartCoroutine("PickDirection");
				}
				this.transform.Rotate(Vector3.up , 90 *rotationSpeed * Time.deltaTime * direction);
			}
			else if (hit.collider.gameObject.CompareTag("Player"))
			{
				StopCoroutine("PickDirection");
				Debug.Log("got you ");
			}
		}
		else 
		{
			this.transform.Translate(Vector3.forward*movementSpeed * Time.deltaTime); 
		}
	
	}

	public IEnumerator PickDirection ()
	{
		yield  return new WaitForEndOfFrame();
		direction = Random.Range (-1,2);

	}
}
