using UnityEngine;
using System.Collections;
//using System.Collections.Generic.HashSet;

/**
 * By Matthew Ielusic
 * Written 2/15/2016
 */
public class Elevator_Independent : MonoBehaviour {

	public enum ElevatorState {ASCENDING, DESCENDING, STATIONARY_TOP, STATIONARY_BOTTOM};

	public ElevatorState state;
	public string activateKey = "space";

	public int movementTimeMax; //The length of movement = speed * time
	public int movementSpeed; //Movement_speed should always be positive

	private int timeSpentMoving = 0;
	private System.Collections.Generic.HashSet<CharacterMove> charactersOnElevator = new System.Collections.Generic.HashSet<CharacterMove>();

	
	void FixedUpdate () {
		//Check which state we are in.  If ascending/descending, increment time-in-motion and check if we should stop;
		//If stationary, check if we should begin moving.
		if (state == ElevatorState.ASCENDING || state == ElevatorState.DESCENDING) {
			timeSpentMoving++;

			if (timeSpentMoving == movementTimeMax) {
				GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
				timeSpentMoving = 0;
				if (state == ElevatorState.ASCENDING) {
					state = ElevatorState.STATIONARY_TOP;
				} else {
					state = ElevatorState.STATIONARY_BOTTOM;
				}
				foreach (CharacterMove move in charactersOnElevator) {
					move.autoWalk = true;
				}
			}
			foreach (CharacterMove move in charactersOnElevator) {
				move.GetComponent<Rigidbody2D> ().velocity = GetComponent<Rigidbody2D> ().velocity;
			}
		} else if (Input.GetKeyDown (activateKey)) {
			foreach (CharacterMove move in charactersOnElevator) {
				move.autoWalk = false;
			}
			if (state == ElevatorState.STATIONARY_BOTTOM) {
				state = ElevatorState.ASCENDING;
				GetComponent<Rigidbody2D> ().velocity = new Vector2(0, movementSpeed);
			} else {
				state = ElevatorState.DESCENDING;
				GetComponent<Rigidbody2D> ().velocity = new Vector2(0, -1*movementSpeed);
			}
		} 
	}
	 
	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Ally") {
			charactersOnElevator.Add (coll.GetComponent<CharacterMove> ());
		}
	}

	void OnTriggerExit2D(Collider2D coll) {
		if (coll.gameObject.tag == "Ally") {
			charactersOnElevator.Remove (coll.GetComponent<CharacterMove> ());
		}
	}


}
