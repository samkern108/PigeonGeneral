using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour {

	public void SubmitMessage(Message msg) {
		Debug.Log("Message received at " + Board.GetCellPosition(this.transform.position));
	}
	
}
