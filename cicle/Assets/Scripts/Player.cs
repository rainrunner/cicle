using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour {

	private JointMotor2D motor;
	private float smoothTime = 0.3F;			// for Camera
	public GameObject Player_Obj;
	public Camera cam;
	private Vector3 velo, target;				//for camera
	//TEST
	private int counter;
	public bool Trigger;
	public Image img;
	public float speeed;

	// Use this for initialization
	void Start () {
		velo = new Vector3 (0,(float)0,0);
		target = new Vector3 (0, 5, -10);
		SetMotorSpeed ();
		SetHingeParas(CreateHinge(), 1, 1, -2, 2);
		/*if (newJoint != null) {
			
			JointMotor2D motor = newJoint.motor;
			motor.motorSpeed = 0;
			newJoint.motor = motor;
			newJoint.useMotor = true;
			
		}*/


	}
	public void OnMouseOver(){
		

		SpeedUp ();
		
	}

	                  

	//SpeedUP
	public void SpeedUp(){

		Debug.Log ("test");

		if(motor.motorSpeed <= 350){
			motor.motorSpeed +=10;
			Debug.Log ("speed ="+ motor.motorSpeed);
		}
		HingeJoint2D newJoint = FindObjectOfType(typeof(HingeJoint2D)) as HingeJoint2D;
		//SetSpeed ();
		if (newJoint != null) {
			//motor.maxMotorTorque = 10000; 
			newJoint.motor = motor;
		}

	}



	//Destroy Hinge
	public void DestroyHinge(){

		
		Debug.Log ("shot");
		
		Destroy(gameObject.GetComponent("HingeJoint2D"));

	}


	//Collision Detection
	void OnCollisionEnter2D (Collision2D col)
	{
		Trigger = true;
		Debug.Log ("collision with " + col.gameObject.name);
		//cam.transform.position = new Vector3 (0, (float)2.6, -10);
		if(col.gameObject.name == "target")
		{
			Debug.Log("target");
			Destroy(col.gameObject);								//Destroy target
			SetMotorSpeed();										//Motor auf 10 setzen

			SetHingeParas(CreateHinge(), 1, 1, 2, 5);				//NeuesHinge erzeugen und Parameter setzen
														//Move Camera
		}else if(col.gameObject.name == "Center"){

			Debug.Log("Center = " +col.gameObject.name);
		}
	}

	//Neues Hinge
	HingeJoint2D CreateHinge(){			

		HingeJoint2D newJoint = (HingeJoint2D)gameObject.AddComponent("HingeJoint2D");
		return newJoint;
	}


	//Parameter für Hinge setzen
	void SetHingeParas(HingeJoint2D newJoint, float anchor_x, float anchor_y, float conanchor_x, float con_anchor_y){

		newJoint.anchor = new Vector2 ((float)anchor_y, (float)anchor_y);
		newJoint.connectedAnchor = new Vector2 (conanchor_x,con_anchor_y);
		newJoint.useMotor = true;
		newJoint.collideConnected = true;
		JointMotor2D motor = newJoint.motor;
		motor.motorSpeed = 10;
		newJoint.motor = motor;

	}

	//Camera movement
	void MoveCamera(){

		//cam.transform.position = new Vector3.Lerp (0, 5, -10);
		Vector3 targetPosition = new Vector3 (0, 5, -10);
		cam.transform.position = Vector3.SmoothDamp(cam.transform.position, targetPosition, ref velo, smoothTime);

	}

	void SetSpeed(){

		HingeJoint2D newJoint = CreateHinge ();

		if (newJoint != null) {
			motor.maxMotorTorque = 10000; 
			newJoint.motor = motor;
		}

	}

	void AddHingetoObj(){

		Player_Obj.gameObject.AddComponent("newJoint");

	}

	void CreateJointMotor(HingeJoint2D newJoint){

		//JointMotor2D motor = newJoint.motor;

	}

	void SetMotorSpeed(){

		motor.motorSpeed = 10;
		motor.maxMotorTorque = 10000; 
		//newJoint.motor = motor;
	}


	float Remap (this float value, float from1, float to1, float from2, float to2) {
		return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
	
	
	}
	


	// Update is called once per frame
	void Update () {
		
		counter += 1;


		if (Trigger == true ) {

			//Trigger = false;
			MoveCamera ();
			//Debug.Log("movecam");
			//Debug.Log ("cam = "+ cam.transform.position + "target ="+target);
			if (cam.transform.position == target) {
				
				//Debug.Log ("trigger = false");
				Trigger = false;
			}
		}


		//Debug.Log ("counter = " + counter);
		if(counter > 25 && motor.motorSpeed > 0){

			HingeJoint2D newJoint = FindObjectOfType(typeof(HingeJoint2D)) as HingeJoint2D;



			if (newJoint != null) {
			//newJoint.tag = "Hingejoin";
				speeed = motor.motorSpeed;
				speeed = (speeed/360);
				Debug.Log ("speed = "+speeed);

				//change color of health bar
				if(speeed > 0.5){
					Debug.Log ("grün!!! color =" +Remap(speeed, 0, (float)1, 0, 255));
					img.color = new Color32 ((byte)Remap(speeed, 0, (float)1, 255, 0), 255, 100, 255); // R,G,B,Trans

				}else{
					Debug.Log ("rot!!!color =" +Remap(speeed, 0, (float)1, 0, 255));
					img.color = new Color32 (255, (byte)Remap(speeed, 0, (float)1, 0, 255), 100, 255); // R,G,B,Trans

				}

				img.fillAmount = speeed;

				motor.motorSpeed -=10;
				motor.maxMotorTorque = 10000; 
				newJoint.motor = motor;
				counter = 0;
			}
		}
		
	}
}
