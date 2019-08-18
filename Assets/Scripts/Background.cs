using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour 
{
	public GameObject algaePrefab;
	public GameObject gameArea;
	// Use this for initialization
	void Start () 
	{
		/*
		float adx = algaePrefab.transform.localScale.x;
		//float n = quad.transform.localScale.x / adx;
		Vector3 prevPos = new Vector3 (-gameArea.transform.localScale.x / 2.0f, 10, gameArea.transform.localScale.y / 2.0f - 0.10f * adx);
		Vector3 newPos = prevPos;
		while(newPos.x < gameArea.transform.localScale.x / 2.0f)
		{
			Quaternion rotation = Quaternion.Euler (90,180+Random.Range (-40,+40),0);
			GameObject alg = GameObject.Instantiate(algaePrefab,newPos,rotation) as GameObject;
			alg.transform.parent = transform;
			alg.transform.localPosition += new Vector3 (0,Random.Range (1.0f,10.0f),Random.Range(-0.05f,0.05f)*adx);
			prevPos = newPos;
			newPos = prevPos + new Vector3 (adx*0.66f,0,0);
			//Debug.Log (alg.transform.position);
		}
		*/
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
