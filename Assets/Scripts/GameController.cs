using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour 
{

	public GameObject enemyPrefab;
	public int nrEnemies;
	public GameObject gameArea;
	private int state;
	public GameObject gameover;
	private float nextJellySpawnTime;
	public GameObject player;
	private float timeOfDeath;
	public GUIText nrJellies;


	private Vector3 GeneratePointInsideGameArea(float height)
	{
		Transform Quad = gameArea.transform;
		Vector3 A = new Vector3 ();
		A.x = Quad.position.x - Quad.localScale.x / 2;
		A.y = height;
		A.z = Quad.position.z + Quad.localScale.y / 2;
		Vector3 B = new Vector3 ();
		B.x = Quad.position.x + Quad.localScale.x / 2;
		B.y = height;
		B.z = Quad.position.z - Quad.localScale.y / 2;
		return new Vector3 (Random.Range (A.x, B.x), height, Random.Range (A.z, B.z));
	}
	// Use this for initialization
	void Start () 
	{
		gameover.SetActive (false);
		state = 0;
		for (int i = 0; i < nrEnemies; i++)
		{
			Vector3 randPos;
			do
			{
				randPos = GeneratePointInsideGameArea(5);
			}while((randPos-player.transform.position).magnitude < 20);
			GameObject uJelly = GameObject.Instantiate (enemyPrefab, randPos, Quaternion.identity) as GameObject;  
			uJelly.GetComponent<Jelly>().maxSpeed = Random.Range (1.0f,20.0f);
		}
		nextJellySpawnTime = Time.time + 1.0f;
	}

	void UnspawnJelly()
	{
		foreach(GameObject jelly in GameObject.FindGameObjectsWithTag("Enemy"))
		{
			Destroy(jelly);
		}

	}
	void Restart()
	{
		UnspawnJelly ();
		player.transform.position = new Vector3 (0.0f, player.transform.position.y, 0.0f);
		player.GetComponent<Movement>().Revive ();
		gameover.SetActive (false);
		state = 0;
		for (int i = 0; i < nrEnemies; i++)
		{
			Vector3 randPos;
			do
			{
				randPos = GeneratePointInsideGameArea(5);
			}while((randPos-player.transform.position).magnitude < 20);
			GameObject uJelly = GameObject.Instantiate (enemyPrefab, randPos, Quaternion.identity) as GameObject;  
			uJelly.GetComponent<Jelly>().maxSpeed = Random.Range (1.0f,20.0f);
		}
		nextJellySpawnTime = Time.time + 1.0f;

	}
	
	// Update is called once per frame
	void Update () 
	{
		if(state==0)
		{
			if(Time.time >= nextJellySpawnTime)
			{
				nextJellySpawnTime = Time.time+1.0f;
				Vector3 randPos;
				do
				{
					randPos = GeneratePointInsideGameArea(5);
				}while((randPos-player.transform.position).magnitude < 20);
				GameObject.Instantiate (enemyPrefab, randPos, Quaternion.identity);  
			}
		}
		if(state==1)
		{
			if(Time.time > timeOfDeath + 2)
				Restart ();
		}
		nrJellies.text = GameObject.FindGameObjectsWithTag("Enemy").Length.ToString();
		if (Input.GetKeyDown(KeyCode.Escape)) { Application.Quit(); }
	}

	public void PlayerHasDied()
	{
		state = 1;
		gameover.SetActive (true);
		timeOfDeath = Time.time;
	}



}
