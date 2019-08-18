using UnityEngine;
using System.Collections;

public class Bash : MonoBehaviour 
{
	public float bashForce;
	public float cooldown;

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
	}
	
	public void StartBash()
	{
		Debug.Log ("Bash");
		foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")) 
		{
			AcceptBash ab = enemy.GetComponent<AcceptBash>();
			if(ab != null)
			{
				ab.Bash(transform.position, bashForce);
			}

		}
	}

}
