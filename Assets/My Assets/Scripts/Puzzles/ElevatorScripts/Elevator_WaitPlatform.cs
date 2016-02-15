using UnityEngine;
using System.Collections;

/**
 * By Matthew Ielusic
 * Written 2/15/2016
 */
public class Elevator_WaitPlatform : WaitPlatform {

	public enum ElevatorState {ASCENDING, DESCENDING, STATIONARY_TOP, STATIONARY_BOTTOM};

	public ElevatorState state;
	public string activateKey = "space";

	public int movementTimeMax; //The length of movement = speed * time
	public int movementSpeed; //Movement_speed should always be positive

	private int timeSpentMoving = 0;


	
	void FixedUpdate () {


		//Check which state we are in.  If ascending/descending, increment time-in-motion and check if we should stop;
		//If stationary, check if we should begin moving.
		if (state == ElevatorState.ASCENDING) {
			timeSpentMoving++;
			if (timeSpentMoving == movementTimeMax) {
				state = ElevatorState.STATIONARY_TOP;
				GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
				timeSpentMoving = 0;
			}
			for (int i = 0; i < heldCharacters.Count; i++) {
				heldCharacters[i].GetComponent<Rigidbody2D> ().velocity = GetComponent<Rigidbody2D> ().velocity;
			}
		} else if (state == ElevatorState.DESCENDING) {
			timeSpentMoving++;
			if (timeSpentMoving == movementTimeMax) {
				state = ElevatorState.STATIONARY_BOTTOM;
				GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
				timeSpentMoving = 0;
			}
			for (int i = 0; i < heldCharacters.Count; i++) {
				heldCharacters[i].GetComponent<Rigidbody2D> ().velocity = GetComponent<Rigidbody2D> ().velocity;
			}
		}


		if (state == ElevatorState.STATIONARY_BOTTOM && Input.GetKeyDown (activateKey)) {
			state = ElevatorState.ASCENDING;
			GetComponent<Rigidbody2D> ().velocity = new Vector2(0, movementSpeed);
		} else if (state == ElevatorState.STATIONARY_TOP && Input.GetKeyDown (activateKey)) {
			state = ElevatorState.DESCENDING;
			GetComponent<Rigidbody2D> ().velocity = new Vector2(0, -1*movementSpeed);
		} 
	}


}
