using UnityEngine;

/**
 * By Matthew Ielusic
 * Written 2/15/2016
 * 
 * Update 2/16/2016 -- Made subclass of a more generic type
 */
public class Elevator_KeyPress : Elevator {


	public string activateKey = "space";

	
	void FixedUpdate () {
		IncrementElevatorMoveTime ();
		if (Input.GetKeyDown (activateKey)) {
			ActivateElevator ();
		} 
	}
}
