using UnityEngine;
using System.Collections;

public class AIClass : MonoBehaviour {

	public Transform transformOfParent;
	public Transform target; 
	public Transform[] PatrolPoints;

	public float speed;
	public float EnAttackDamage;
	public float sightRange = 50;


	public float movementSpeed; // same during patrols ans attack !
	public float rotationSpeed;

	private int currentPositionPatrol;
	private int counterPatrol=0;
	
	private bool foundPLayer=false;
	private bool attackingPlayer= false;




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

			/*
			foreach (var item in hit) {
				Debug.Log(item.transform.gameObject.name.ToString());
			}
			*/
			string checker = hit[0].transform.gameObject.name;
			//Debug.DrawLine(this.transform.position, hit[hit.Length-1].transform.position, Color.red);

			if (checker == target.transform.gameObject.name)
			{
				foundPLayer=true; 
				//Debug.Log("foundplayer");
				Debug.DrawLine(this.transform.position, hit[0].transform.position, Color.blue);

			if (Distance < 3)
			{

					PLayerDefense IsHedefending = target.gameObject.GetComponent<PLayerDefense>();

					if (IsHedefending.defending == true) 
					{
						this.gameObject.renderer.material.color = Color.yellow;
					}
					else 
					{
						if(attackingPlayer==false)
						{
				
				//attack ! 
						StartCoroutine(AttackMotion(foundPLayer, target.gameObject,EnAttackDamage));
					//Attack(target.gameObject,EnAttackDamage);
					}
						else 
						{
							this.gameObject.renderer.material.color = Color.green;
						}
					}
			}
			else
			{
			transformOfParent.transform.LookAt(target.transform);

			transformOfParent.position += transformOfParent.forward*speed*Time.deltaTime;
			}
			}
			else 
			{
				foundPLayer=false;
				PatrolPointsAI();
			}
		}
		else 
		{
			foundPLayer=false;
			PatrolPointsAI();
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
		if (transformOfParent.transform.position== PatrolPoints[currentPositionPatrol].position)
		{
			if ( counterPatrol == PatrolPoints.Length-1)
			{
				currentPositionPatrol=0;
				counterPatrol =0;
			}
			else
			{
				counterPatrol ++;
				currentPositionPatrol ++;
			}		
		}
		
		// Move at patrol point
		transformOfParent.transform.position=Vector3.MoveTowards(transformOfParent.transform.position, PatrolPoints[currentPositionPatrol].position, movementSpeed * Time.deltaTime);
		
		//Look at patrol point
		transformOfParent.transform.rotation = Quaternion.Slerp(transformOfParent.transform.rotation,Quaternion.LookRotation(PatrolPoints[currentPositionPatrol].position - transformOfParent.transform.position), rotationSpeed * Time.deltaTime);
		
	}
}
