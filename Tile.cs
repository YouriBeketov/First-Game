using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	public Vector2 ID;
	public Transform cube;
	public int cost =0; 
	public int heuristicValue =0; 
	public int total =0; 
	public bool walkable; 

	void Start () {
		//this.gameObject.renderer.material.color = Color.red;
		//this.walkable=true;
		//this.gameObject.collider.isTrigger = true; 
	}

	void Update()
	{

	}

	void OnMouseOver()
	{
		if(Input.GetKey(KeyCode.B))
		   {
			this.renderer.material.color= Color.black;
		this.walkable=false;
		}
		else if(Input.GetKey(KeyCode.N))
		{
			this.renderer.material.color= Color.white;
			this.walkable=true;
		}
		else 
		{}



	}

	/*
	public Tile (Vector2 ID, bool walkable) 
	{
		this.ID = ID; 
		this.walkable=walkable;

		if (!this.walkable)
		{
			this.gameObject.renderer.material.color = Color.black; 

	}
		else 
		{
			this.gameObject.renderer.material.color = Color.yellow; 
		}
}
*/
}