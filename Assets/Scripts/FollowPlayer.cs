using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour 
{
	public GameObject objectToFollow;
	

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position = new Vector3 (objectToFollow.transform.position.x, transform.position.y, objectToFollow.transform.position.z);
	}
}
