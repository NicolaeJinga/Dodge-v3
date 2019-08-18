using UnityEngine;
using System.Collections;

public class AcceptBash : MonoBehaviour {

	private bool acceptBash;
	public float mass;
	public float radius;
	private Vector3 bashVelocity;
	public float dragAmplification;
	public float gameUnitsToMeters;
	private float bashTime;
	
	public float BashTime
	{
		get
		{
			return bashTime;
		}
	}

	// Use this for initialization
	void Start () 
	{
		gameUnitsToMeters = (2.0f * radius) / transform.localScale.x;
		acceptBash = true;
		bashVelocity = new Vector3 ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(bashVelocity.magnitude>0.01f)
		{
			Vector3 dragForce = dragAmplification * 0.5f * Mathf.Pow (bashVelocity.magnitude, 2.0f) * Mathf.PI * radius * radius * -bashVelocity.normalized;
			Vector3 acceleration = dragForce / mass;
			bashVelocity += acceleration * Time.deltaTime;
			transform.position += bashVelocity * Time.deltaTime;
		}
	}	

	public void Bash(Vector3 bashOrigin, float bashForce)
	{
		if (!acceptBash)
			return;
		bashTime = Time.time;
		Vector3 Bash2Jelly = transform.position - bashOrigin;
		float radius = Bash2Jelly.magnitude * gameUnitsToMeters;
		float bashForceOnJelly = bashForce / Mathf.Pow (radius, 2.0f);
		Vector3 acceleration = Bash2Jelly.normalized * bashForceOnJelly / mass;
		bashVelocity = acceleration;
	}
}
