using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class InteligentAI : MonoBehaviour {

	public Transform transformOfParent;
	public Transform target; 
	public Transform pathObject;

	public List<Transform> PatrolPoints = new List<Transform>();

	public float speed;
	public float EnAttackDamage;
	public float sightRange = 50;

	public float movementSpeed; // same during patrols ans attack !
	public float rotationSpeed;
	
	private int currentPositionPatrol;
	//private int counterPatrol=0;

	private int currentPosition;
	private int counter=0;
	
	private bool foundPLayer=false;
	private bool attackingPlayer= false;
	private bool startWalking= false; 

	public Vector3 adjustmentPathLine= new Vector3(0f,1.4f,0f); 
	
	
	// Use this for initialization
	void Start () {	
	}
	
	// Update is called once per frame
	void Update () {
		
		//Debug.DrawRay(this.transform.position,this.transform.right,Color.red);
		this.transform.forward = transformOfParent.transform.forward;
		
		Vector3 direction = target.transform.position -  this.transform.position;
		float Distance = Vector3.Distance(target.transform.position ,transformOfParent.transform.position);
		
		if (Distance < sightRange && Vector3.Dot(direction.normalized, this.transform.forward)>0.25f)
		{
			RaycastHit[] hit; 
			hit = Physics.RaycastAll(this.transform.position,direction,Distance);
			Debug.DrawRay(this.transform.position,direction,Color.magenta,Distance);
			

			string checker = hit[0].transform.gameObject.name;
			
			if (checker == target.transform.gameObject.name)
			{
				foundPLayer=true; 

				if (Distance < 1)
				{
					//Debug.Log("BOOM");
				}
				else
				{
					/*
					transformOfParent.transform.LookAt(target.transform);
					
					transformOfParent.position += transformOfParent.forward*speed*Time.deltaTime;
					*/
					RaycastHit[] hit2; 
					hit2 = Physics.RaycastAll(hit[0].transform.position, - hit[0].transform.up,10);
					RaycastHit[] hit3; 
					hit3 = Physics.RaycastAll(this.transform.position, - this.transform.up,10);
					
					foreach (var item in hit2) {
						
						if (item.transform.tag =="Finish")
						{
							//Debug.Log(item.transform.tag.ToString());
							foreach (var item2 in hit3) {

								if (item2.transform.tag =="Finish")
								{
									var astar=  this.gameObject.GetComponent<InteligentEnemy>();
									astar.EndPosition= (item.transform);
									astar.StartPosition=(item2.transform);
									astar.AStarStar(item2.transform, item.transform);
									PatrolPoints= astar.pathList;
									counter=PatrolPoints.Count-1;
									currentPosition=  PatrolPoints.Count-1;
							}
							
							}}
					}
				}
				
			}
			else 
			{
				foundPLayer=false;

				/*
				var getpathlist = pathObject.GetComponent<InteligentEnemy>();
				
				PatrolPoints= getpathlist.pathList;
				counter=PatrolPoints.Count-1;
				currentPosition=  PatrolPoints.Count-1;
				PatrolPointsAI();
				*/
			}
		}
		else 
		{
			//var getpathlist = pathObject.GetComponent<InteligentEnemy>();
			
			//PatrolPoints= getpathlist.pathList;


			if( PatrolPoints.Count>0)
			{
			//PatrolPointsAI();
			}
			foundPLayer=false;
			//PatrolPointsAI();
		}

		if( PatrolPoints.Count>0)
		{
			//Debug.Log ("WALK");
			PatrolPointsAI();



		//transformOfParent.transform.position=Vector3.MoveTowards(transformOfParent.transform.position, PatrolPoints[currentPosition].position + adjustmentPathLine, movementSpeed * Time.deltaTime);
		}
	}
	
	public IEnumerator AttackMotion(bool checkattack,GameObject targetAttack, float DamageAmount ) 
	{
		attackingPlayer = true; 
		float timeRange = Random.Range(1.0f,2.5f);
		this.gameObject.renderer.material.color = Color.red;
		yield return new WaitForSeconds(timeRange);
		
		if (checkattack==true)
		{this.gameObject.renderer.material.color = Color.magenta;
			Attack(targetAttack.gameObject,DamageAmount);
		}
		attackingPlayer =false; 
		
		
	}
	public void Attack (GameObject attackTarget, float damageValue)
	{
		
		PLayerDefense IsHedefending = target.gameObject.GetComponent<PLayerDefense>();
		if (IsHedefending.defending==false)
		{
			HealthAI health = attackTarget.gameObject.GetComponent<HealthAI> (); 
			
			health.health -=damageValue;
		}
		else
		{
			Debug.Log("Miss");
		}
		
	}
	
	public void PatrolPointsAI() 
	{

		if (transformOfParent.transform.position== PatrolPoints[currentPosition].position + adjustmentPathLine )
		{

			if ( counter == 0)
			{
				
				currentPosition=PatrolPoints.Count-1;
				counter =PatrolPoints.Count-1;
				//var getpathlist = pathObject.GetComponent<Astartry1>();
				//this.transform.position = getpathlist.StartPosition.transform.position+adjustmentPathLine;
				//startWalking=false;
				
			}
			
			else
			{

				counter --;
				currentPosition --;
			}
		
		// Move at patrol point
		transformOfParent.transform.position=Vector3.MoveTowards(transformOfParent.transform.position, PatrolPoints[currentPosition].position + adjustmentPathLine, movementSpeed * Time.deltaTime);
		
		//Look at patrol point
		transformOfParent.transform.rotation = Quaternion.Slerp(transformOfParent.transform.rotation,Quaternion.LookRotation(PatrolPoints[currentPosition].position + adjustmentPathLine - transformOfParent.transform.position), rotationSpeed * Time.deltaTime);


	}
		else 
		{
			//Debug.Log ("going the worin way");
		
		// Move at patrol point
		transformOfParent.transform.position=Vector3.MoveTowards(transformOfParent.transform.position, PatrolPoints[currentPosition].position + adjustmentPathLine, movementSpeed * Time.deltaTime);
		
		//Look at patrol point
		transformOfParent.transform.rotation = Quaternion.Slerp(transformOfParent.transform.rotation,Quaternion.LookRotation(PatrolPoints[currentPosition].position + adjustmentPathLine - transformOfParent.transform.position), rotationSpeed * Time.deltaTime);
		}
}
}