using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
	public float speed;
	private Joystick joy;
	private bool isDead;
	private GameController gc;
	private GameObject gameArea;


	// Use this for initialization
	void Start () 
	{
		joy = GameObject.FindGameObjectWithTag("Joystick").GetComponent<Joystick>();
		gc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController>();
		gameArea = GameObject.FindGameObjectWithTag ("GameArea");
		isDead = false;
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Enemy")
		{
			isDead = true;
			gc.PlayerHasDied();
		}
	}
	// Update is called once per frame
	void Update () 
	{
		if(!isDead)
		{
			//update location
			Vector3 velocity = new Vector3 (joy.value.x, 0, joy.value.y)* speed;
			transform.position = transform.position + velocity * Time.deltaTime;
			//update graphics orientation
			float angle=0;
			if((velocity.x<0 && transform.localScale.x > 0)||(velocity.x>0 && transform.localScale.x < 0))
			{
				transform.localScale = new Vector3 (-transform.localScale.x,transform.localScale.y,transform.localScale.z);
			}
			//obtaining angle and converting it in degrees
			//Debug.Log (joy.value.x.ToString() + ", " + joy.value.y.ToString());
			joy.value.y = -joy.value.y;
			if (velocity.x < 0)
			{
				velocity.x = -velocity.x;
				angle = 180;
			}
			angle += 180.0f/3.141592f* Mathf.Atan2 (joy.value.y, joy.value.x);
			transform.localRotation = Quaternion.Euler(0,angle,0);

			//check outside the box
			float gahx = gameArea.transform.localScale.x/2.0f;
			float gahy = gameArea.transform.localScale.y/2.0f;
			float hx = transform.localScale.x/2.0f;
			float hy = transform.localScale.y/2.0f;

			Vector3 A;
			A.x = gameArea.transform.position.x - gahx;
			A.y = transform.position.y;
			A.z = gameArea.transform.position.z - gahy;
			Vector3 B;
			B.x = gameArea.transform.position.x + gahx;
			B.y = transform.position.y;
			B.z = gameArea.transform.position.z + gahy;

			if(transform.position.x - hx < A.x)
			{
				transform.position = new Vector3 (A.x + hx, transform.position.y, transform.position.z);
			}
			if(transform.position.x + hx > B.x)
			{
				transform.position = new Vector3 (B.x - hx, transform.position.y, transform.position.z);
			}
			if(transform.position.z - hy < A.z)
			{
				transform.position = new Vector3 (transform.position.x, transform.position.y, A.z + hy);
			}
			if(transform.position.z + hy > B.z)
			{
				transform.position = new Vector3 (transform.position.x, transform.position.y, B.z - hy);
			}
		}
	}

	public void Revive()
	{
		isDead = false;

	}
}

















