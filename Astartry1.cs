using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class Astartry1 : MonoBehaviour {

	
	public int GridHeight =0; 
	public int GridLength =0; 
	public int PercentageOfWalls;
	
	public List<Transform> closedSet = new List<Transform>();
	public List<Transform> openSet = new List<Transform>();

	public List<Transform> currentlist = new List<Transform>();
	public List<Transform> adjTiles = new List<Transform>();

	public List<Transform> pathList = new List<Transform>();

	public Vector3 adjustmentPathLine= new Vector3(0f,0.5f,0f); 
	public bool showPathtoUser  =false; 

	//public Transform board;
	public Transform StartPosition;
	public Transform EndPosition;

	private bool antiwall= false;
	private Transform currentTile;

	private int currentPosition;
	private int counter=0;
	// Use this for initialization
	void Start () {

		for (int i = 0; i < GridLength; i++) {
			for (int j = 0; j < GridHeight; j++) {
				
				var grid = GameObject.CreatePrimitive(PrimitiveType.Cube); 
				grid.transform.position= new Vector3(i,0,j ); 
				grid.name = i.ToString()+"|"+j.ToString();
				grid.gameObject.AddComponent("Tile");
				var info=  grid.gameObject.GetComponent<Tile>();
				info.ID.x=i;
				info.ID.y=j;
				if (Random.Range(0,100)<=PercentageOfWalls  )
				{
					info.walkable=false; 
					grid.renderer.material.color = Color.black ; 

					grid.transform.position+=adjustmentPathLine;
					 
				}
				else 
				{
					grid.gameObject.tag = "Finish";
					info.walkable=true;
					currentlist.Add (grid.transform);
				}


			}
		}
		StartPosition= currentlist[ Random.Range(0,currentlist.Count)];
		StartPosition.renderer.material.color = Color.magenta;
		var checkForWall = StartPosition.GetComponent<Tile>();

		checkForWall.walkable = true; 
		EndPosition= currentlist[Random.Range(0,currentlist.Count)];
		EndPosition.renderer.material.color = Color.magenta;
		var checkForWall2 = EndPosition.GetComponent<Tile>();
		
		checkForWall2.walkable = true; 
	}

	
	// Update is called once per frame
	void Update () {
		/*
		int pathCount = pathList.Count -1 ; 

		if( Input.GetKeyDown(KeyCode.C))
		{	
			closedSet = new List<Transform>();
		 	openSet = new List<Transform>();
			AstarPath(StartPosition,EndPosition);
			showPathtoUser= true; 
		}

		if (showPathtoUser)
		{
			//DrawPath();
		}

		//int pathcount = pathList.Count-1; 
		//Debug.DrawLine(pathList[pathcount].transform.position, pathList[pathcount-1].transform.position, Color.red);

	*/
	}
	

	public void DrawPath ()
	{
		try
		{
			Debug.DrawLine(EndPosition.transform.position + adjustmentPathLine, pathList[0].transform.position + adjustmentPathLine , Color.red);
			foreach (var item in pathList) {

				if(pathList.IndexOf(item) +1 == pathList.Count)
				{
				Debug.DrawLine(item.transform.position + adjustmentPathLine, StartPosition.transform.position + adjustmentPathLine , Color.red);
				}
				else
				{
				Debug.DrawLine(item.transform.position + adjustmentPathLine, pathList[ pathList.IndexOf(item) +1 ].transform.position + adjustmentPathLine , Color.red);
				}
				}
		}
		catch 
		{
			Debug.Log ("something fishy");
		}

	}
	public void AstarPath ( Transform start, Transform goal) 
	{
		openSet.Add(start); 

		while (openSet.Count!=0)
		{
			currentTile = getTileWithLowestCost(openSet);
			//Debug.Log (currentTile.ToString()); 
			if (currentTile == EndPosition)// some error 
			{
				//openSet.Remove(currentTile);
				Debug.Log ("FOund Target"); 
				currentTile.renderer.material.color=Color.red;
				pathList= ShowPath();
				break;
			}
			else 
			{
				openSet.Remove(currentTile);
				closedSet.Add(currentTile);
				adjTiles = checkAdjTiles(currentTile);

				foreach (var adjTile in adjTiles)
				{

					if ( !openSet.Contains(adjTile))
					{
						if ( !closedSet.Contains(adjTile))
						{
							openSet.Add(adjTile);

							var adjInfo = adjTile.gameObject.GetComponent<Tile>();
							var curInfo = currentTile.gameObject.GetComponent<Tile>();

							adjInfo.cost= curInfo.cost +1;

							adjInfo.heuristicValue = calculateManhattanDistance(adjTile);

							adjInfo.total = adjInfo.cost + adjInfo.heuristicValue;

							adjTile.gameObject.renderer.material.color = Color.green;
						}
					}
				}
			}
		}
	}



	public List<Transform> ShowPath()
	{
		bool startFound= false; 
		int crashCount =0;
		Transform currentTilePath =EndPosition;

		List<Transform> pathTitles = new List<Transform>();
		//var startInfo = StartPosition.gameObject.GetComponent<Tile>();
		while (startFound ==false) 
		{
			List<Transform> adjacentTiles = checkAdjTiles(currentTilePath); 
			crashCount++;

			foreach (var ADJTIle in adjacentTiles) 
			{
				if ( ADJTIle == StartPosition)
				{
					startFound=true; 
					ADJTIle.renderer.material.color = Color.gray;
					break;
				}
				if (openSet.Contains(ADJTIle) || closedSet.Contains(ADJTIle))
				{
					var costCheck = ADJTIle.gameObject.GetComponent<Tile>(); 
					var Endcost = currentTilePath.gameObject.GetComponent<Tile>();
					if ( costCheck.cost <= Endcost.cost && costCheck.cost >0 )
					{
						currentTilePath= ADJTIle;

//						Debug.Log (currentTilePath.ToString());
						pathTitles.Add (ADJTIle); 

						ADJTIle.gameObject.renderer.material.color = Color.yellow;
						break;
					}
					else
					{
						//ADJTIle.gameObject.renderer.material.color = Color.cyan;
					}
				}
				
			}
		}
		return(pathTitles);
	}

	public Transform getAdjTile(int xInc, int yInc, Transform CurrentTile)
	{
		var checkValue= CurrentTile.gameObject.GetComponent<Tile>();
		var checkTile = GameObject.Find (  (checkValue.ID.x + xInc).ToString()+"|"+(checkValue.ID.y + yInc).ToString());

		if ( checkTile !=null)
		{
			
			var checkifWalkable = checkTile.gameObject.GetComponent<Tile>();
			
			if ( checkifWalkable.walkable)
			{
				return checkTile.transform;
			}
		}

		return CurrentTile;
	}

	public List<Transform> checkAdjTiles (Transform CurrentTile)
	{
		List<Transform> adjecentTiles=new List<Transform>();

//		Debug.Log(checkValue.ToString());

		//if (getAdjTile(1, 1, CurrentTile) != CurrentTile) { adjecentTiles.Add (getAdjTile(1, 1, CurrentTile));}
		if (getAdjTile(0, -1, CurrentTile) != CurrentTile) { adjecentTiles.Add (getAdjTile(0, -1, CurrentTile));}
		//if (getAdjTile(-1, -1, CurrentTile) != CurrentTile) { adjecentTiles.Add (getAdjTile(-1, -1, CurrentTile));}
		//if (getAdjTile(1, -1, CurrentTile) != CurrentTile) { adjecentTiles.Add (getAdjTile(1, -1, CurrentTile));}
		//if (getAdjTile(-1, 1, CurrentTile) != CurrentTile) { adjecentTiles.Add (getAdjTile(-1, 1, CurrentTile));}
		if (getAdjTile(1, 0, CurrentTile) != CurrentTile) { adjecentTiles.Add (getAdjTile(1, 0, CurrentTile));}
		if (getAdjTile(-1, 0, CurrentTile) != CurrentTile) { adjecentTiles.Add (getAdjTile(-1, 0, CurrentTile));}
		if (getAdjTile(0, 1, CurrentTile) != CurrentTile) { adjecentTiles.Add (getAdjTile(0, 1, CurrentTile));}

	
		return (adjecentTiles);
	}
	/*
	public List<Transform> checkAdjTilesD (Transform CurrentTile)
	{
		List<Transform> adjecentTiles=new List<Transform>();
		
		//		Debug.Log(checkValue.ToString());
		
		if (getAdjTile(1, 1, CurrentTile) != CurrentTile) { adjecentTiles.Add (getAdjTile(1, 1, CurrentTile));}
		if (getAdjTile(0, -1, CurrentTile) != CurrentTile) { adjecentTiles.Add (getAdjTile(0, -1, CurrentTile));}
		if (getAdjTile(-1, -1, CurrentTile) != CurrentTile) { adjecentTiles.Add (getAdjTile(-1, -1, CurrentTile));}
		if (getAdjTile(1, -1, CurrentTile) != CurrentTile) { adjecentTiles.Add (getAdjTile(1, -1, CurrentTile));}
		if (getAdjTile(-1, 1, CurrentTile) != CurrentTile) { adjecentTiles.Add (getAdjTile(-1, 1, CurrentTile));}
		if (getAdjTile(1, 0, CurrentTile) != CurrentTile) { adjecentTiles.Add (getAdjTile(1, 0, CurrentTile));}
		if (getAdjTile(-1, 0, CurrentTile) != CurrentTile) { adjecentTiles.Add (getAdjTile(-1, 0, CurrentTile));}
		if (getAdjTile(0, 1, CurrentTile) != CurrentTile) { adjecentTiles.Add (getAdjTile(0, 1, CurrentTile));}
		
		
		return (adjecentTiles);
	}
*/
	public int  calculateManhattanDistance (Transform titleToCal )
	{
		var currentP = titleToCal.gameObject.GetComponent<Tile>();
		var endP = EndPosition.gameObject.GetComponent<Tile>();
		int ManhattanD = Mathf.Abs (((int)(endP.ID.x-currentP.ID.x)*(int)(endP.ID.x-currentP.ID.x)) +((int)(endP.ID.y- currentP.ID.y))*(int)(endP.ID.y- currentP.ID.y));
		return (ManhattanD);	
	}

	public Transform getTileWithLowestCost(List<Transform> openlist) 
	{
		Transform tileWithLowestValue= null;
		int lowestTotal = int.MaxValue; 


		foreach (var openTiles in openlist) {

			var TileInfoL= openTiles.gameObject.GetComponent<Tile>();

			if ( TileInfoL.total <=lowestTotal)
			{
				lowestTotal=TileInfoL.total;
				tileWithLowestValue=openTiles;
			}


		
//		Debug.Log(tileWithLowestValue.name.ToString());
		}
		return (tileWithLowestValue);

	}



}

	/*

			//var getvalues = openTiles.gameObject.GetComponent<Tile>();
			char[] sep = new char[] { '|' };  
			string[] name = openTiles.name.ToString().Split(sep);
			int TileTotal;
			int resultH;
			int resultL;
			if (int.TryParse(name[0], out resultH))
			{
				TileTotal = resultH; 
				if (int.TryParse(name[1],out resultL))
				{
					TileTotal= TileTotal+ resultL;
					if (TileTotal<=lowestTotal)
					{
						lowestTotal=TileTotal;

						tileWithLowestValue= openTiles.transform; 
						//Debug.Log (lowestTotal.ToString());
					}
				
				}
			}
			else 
			{
				//Debug.Log("INT EROOR");
			}		
		}
*/
	/*
	function A*(start,goal)
		closedset := the empty set    // The set of nodes already evaluated.
		openset := {start}    // The set of tentative nodes to be evaluated, initially containing the start node
came_from := the empty map    // The map of navigated nodes.
		
		g_score[start] := 0    // Cost from start along best known path.
			// Estimated total cost from start to goal through y.
			f_score[start] := g_score[start] + heuristic_cost_estimate(start, goal)
			
			while openset is not empty
				current := the node in openset having the lowest f_score[] value
					if current = goal
					return reconstruct_path(came_from, goal)
					
					remove current from openset
					add current to closedset
					for each neighbor in neighbor_nodes(current)
						if neighbor in closedset
							continue
								tentative_g_score := g_score[current] + dist_between(current,neighbor)
								
								if neighbor not in openset or tentative_g_score < g_score[neighbor] 
								came_from[neighbor] := current
								g_score[neighbor] := tentative_g_score
								f_score[neighbor] := g_score[neighbor] + heuristic_cost_estimate(neighbor, goal)
								if neighbor not in openset
									add neighbor to openset
										
										return failure
										
										function reconstruct_path(came_from, current_node)
										if current_node in came_from
											p := reconstruct_path(came_from, came_from[current_node])
												return (p + current_node)
												else
													return current_node
													*/


