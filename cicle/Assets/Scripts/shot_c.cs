using UnityEngine;
using System.Collections;

public class shot_c : MonoBehaviour {

	GameObject myObj;

	public void OnMouseDown(){

		myObj.GetComponent<Player> ().DestroyHinge ();
	
	}
}
