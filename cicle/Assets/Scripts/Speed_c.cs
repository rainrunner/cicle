using UnityEngine;
using System.Collections;

public class Speed_c : MonoBehaviour {
	
	GameObject startF;
	
	public void OnMouseOver(){

		Debug.Log ("test");
		startF.GetComponent<Player> ().SpeedUp ();
		
	}
}
