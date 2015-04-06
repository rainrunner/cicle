using UnityEngine;
using System.Collections;

public class buttonpressed : MonoBehaviour
{

	public bool pressedState;
	
	// Use this for initialization
	void Start () 
	{
		pressedState = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
	}
	
	void OnMouseDown(bool pressed)
	{
		if (pressed)
		{
			Debug.Log("Now is pressed");
			pressedState = true;
		}			
		else
		{
			pressedState = false;
			Debug.Log("Now is released");
		}			
	}
}
