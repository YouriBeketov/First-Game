using UnityEngine;
using System.Collections;

public class SightRaycast : MonoBehaviour {

	private bool gotRight=false; 
	public float lookSpeed; 



	public Transform transformOfParent;
	private float minLook; 
	private float maxLook;
	private int LookD= 1; 
	private RaycastHit hit;
	private bool foundplayer= false;



	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		Debug.Log (maxLook.ToString()+"{}"+minLook.ToString());
		Debug.DrawRay(this.transform.position, this.transform.forward ,Color.red,100); 
		Debug.Log (this.transform.rotation.y.ToString());
		Debug.Log (LookD.ToString());

		foundplayer=false;

		if (Physics.Raycast(this.transform.position, this.transform.forward,out hit, 20))

		{

			if (hit.collider.gameObject.CompareTag("Player"))
			{
				foundplayer=true;
				Debug.Log("got you ");
			}
		}

		if (foundplayer==false)
		{
			minLook= transformOfParent.transform.rotation.y-0.25f;
			maxLook = (transformOfParent.transform.rotation.y +0.25f) ;

		
			
			checkAngleValues(maxLook,minLook);

		if ( maxLook>minLook)
		{

				if ( this.transform.rotation.y <= minLook )
			{
				this.transform.Rotate(Vector3.up , 10 * lookSpeed* Time.deltaTime);
				LookD=1;
					this.gameObject.renderer.material.color = Color.green;
				
			}
				else if ( this.transform.localRotation.y >= maxLook)
			{

				this.transform.Rotate(Vector3.up , 10 *lookSpeed* Time.deltaTime );
					this.gameObject.renderer.material.color = Color.black;
				LookD=-1;
			}
			else
			{
					this.gameObject.renderer.material.color = Color.cyan;
				this.transform.Rotate(Vector3.up , -10 *lookSpeed* Time.deltaTime * LookD);
			}
		}
	
		else 
			{	
				if ( this.transform.rotation.y <= minLook && this.transform.localRotation.y >0)
				{

					this.transform.Rotate(Vector3.up , -10 *lookSpeed* Time.deltaTime );
					this.gameObject.renderer.material.color = Color.magenta;
					LookD=-1;
				}

			
				else if ( this.transform.rotation.y <= maxLook&& this.transform.localRotation.y <0 ) 
				{
					this.transform.Rotate(Vector3.up , 10 * lookSpeed* Time.deltaTime);
					this.gameObject.renderer.material.color = Color.red;
					LookD=-1;
					
				}
				else
				{
					this.gameObject.renderer.material.color = Color.cyan;
					this.transform.Rotate(Vector3.up , 10 *lookSpeed* Time.deltaTime * LookD);
				}


				/*if ( this.transform.rotation.y <= minLook &&  this.transform.rotation.y >0 )
				{
					this.transform.Rotate(Vector3.up , -10 * lookSpeed* Time.deltaTime);
					LookD=1;
					this.gameObject.renderer.material.color = Color.red;
					
				}
				else if ( this.transform.rotation.y >= maxLook && this.transform.rotation.y <0 )
				{
					
					this.transform.Rotate(Vector3.up , 10 *lookSpeed* Time.deltaTime );
					LookD=-1;
					this.gameObject.renderer.material.color = Color.blue;
				}
				else if (this.transform.rotation.y <= minLook  &&this.transform.rotation.y >0)
				{
					this.transform.Rotate(Vector3.up , -10 *lookSpeed* Time.deltaTime );
					LookD=1;
				}




				else
				{
					
					this.transform.Rotate(Vector3.up , -10 *lookSpeed* Time.deltaTime * LookD);
				}
				*/
			}
		}
	}

	public void checkAngleValues(float max, float min ) 

	{
		
		if (min<=-1)
		{
			minLook= minLook+2;
		}
		
		
		if (max>1)
		{
			maxLook= (maxLook-2);
		}
	}


	





	public void pPosition() 
	{
		var x = GameObject.Find("Cube"); 

		if (x= null)
		{
			Debug.Log ("no object");
		}
		else
		{
			transformOfParent.transform.rotation = x.transform.rotation;
		}
	}

}
