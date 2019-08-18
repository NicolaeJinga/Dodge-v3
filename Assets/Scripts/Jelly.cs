using UnityEngine;
using System.Collections;

public class Jelly : MonoBehaviour {
	public float maxSpeed;
	private Vector3 velocity;
	private Vector3 target;
	private Vector3 oldEnemyPosition;
	public float targetSpawnPercent;
	public float targetSpawnRadiusMax;
	public float targetSpawnRadiusMin;
	public float acceleration;
	private GameObject gameArea;
	private float appliedBashTime;

	// Use this for initialization
	void Start () 
	{
		velocity = new Vector3(0.0f, 0.0f, 0.0f);
		Vector3 auxiliaryVector = Random.insideUnitCircle * Random.Range(targetSpawnRadiusMin, targetSpawnRadiusMax);
		auxiliaryVector.z = auxiliaryVector.y;
		auxiliaryVector.y = 0.0f;
		target = transform.position + auxiliaryVector;
		oldEnemyPosition = transform.position;
		gameArea = GameObject.FindWithTag ("GameArea") as GameObject;
		appliedBashTime = 0.0f;
	}

	bool pointInsideQuad(Vector3 point)
	{
		Transform Quad = gameArea.transform;
		Vector3 A = new Vector3 ();
		A.x = Quad.position.x - Quad.localScale.x / 2;
		A.y = point.y;
		A.z = Quad.position.z + Quad.localScale.y / 2;
		Vector3 B = new Vector3 ();
		B.x = Quad.position.x + Quad.localScale.x / 2;
		B.y = point.y;
		B.z = Quad.position.z - Quad.localScale.y / 2;
		return point.x >= A.x && point.x <= B.x && point.z <= A.z && point.z >= B.z;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//vector from the old enemy position to the current target;
		Vector3 distOepT = target - oldEnemyPosition;
		//vector from old enemy position to the current position of this enemy (Me);
		Vector3 distOepMe = transform.position - oldEnemyPosition;
		Vector3 distOepTNorm = distOepT.normalized;
		float percent = Vector3.Dot (distOepMe, distOepTNorm)/distOepT.magnitude;
		//if the enemy surpasses the given threshold, spawn new target
		//Debug.DrawLine (oldEnemyPosition, target);
		bool hasBeenBashed = false;
		AcceptBash ab = gameObject.GetComponent<AcceptBash> ();
		if(ab!=null)
		{
			if(appliedBashTime < ab.BashTime)
			{
				hasBeenBashed = true;
				appliedBashTime = ab.BashTime;
			}
		}
		if (percent >= targetSpawnPercent || hasBeenBashed)
		{
			//Debug.Log (target+"    "+percent);

			Vector3 newTarget = new Vector3();
			Vector3 auxiliaryVector = new Vector3();
			bool forward = true;
			int nrTries = 0;
			do
			{
				auxiliaryVector = Random.insideUnitCircle * Random.Range(targetSpawnRadiusMin, targetSpawnRadiusMax);
				auxiliaryVector.z = auxiliaryVector.y;
				auxiliaryVector.y = 0.0f;
				newTarget = target + auxiliaryVector;
				nrTries++;
				forward = Vector3.Dot((target-transform.position).normalized, newTarget-transform.position) >= 0;
				if(nrTries>10)
					forward = true;
				//newTarget.y = transform.position.y;
			}while((!pointInsideQuad(newTarget) || !forward) && (nrTries < 20 ));
			if(nrTries == 20)
				newTarget = target;
			oldEnemyPosition = transform.position;
			target = newTarget;
		}


		//move towards target
		Vector3 vectorAcceleration = (target - transform.position).normalized * acceleration;
		velocity = velocity + vectorAcceleration * Time.deltaTime;
		if (velocity.magnitude > maxSpeed)
		{
			velocity = velocity.normalized * maxSpeed;
		}
		transform.position += velocity * Time.deltaTime;


		//rotate picture alongside the vector
		float alfa = Mathf.Atan2 (velocity.z, velocity.x);
		float beta = Mathf.PI / 2.0f - alfa;
		//Debug.DrawLine (transform.position, transform.position + 20.0f * velocity.normalized);
		transform.rotation = Quaternion.Euler (0, beta*180.0f/Mathf.PI, 0);
	}
}
















