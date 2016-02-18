using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * By Matthew Ielusic
 * Written 2/16/2016
 * 
 * Base class for elevators.  An Elevator is a platform that, when activated, will move up or down at
 * a specified speed for a specified time until halting; the next time it is activated it will return to its
 * initial position.  A moving elevator will halt the motion of anything tagged "ally" on it; this is to prevent
 * weird physics things when it stops and starts moving.
 * 
 * If you make any changes to this file, copy them to Elevator_Activatable.  Due to conflichts with other systems, Elevator_Activatable
 * EXTENDS A DIFFERENT CLASS, and to make it work I had to copy-paste all code here into that file.
 */
public class Elevator : MonoBehaviour {

	public enum ElevatorState {ASCENDING, DESCENDING, STATIONARY_TOP, STATIONARY_BOTTOM};

	/*
	 * The initial value of this should only be STATIONARY_TOP and STATIONARY_BUTTOM.  Anything else will cause weird logic to happen. 
	 * If the initial state is STATIONARY_BOTTOM, then the elevator will move upwards when first activated.
	 * If the initial state is
	 */
	public ElevatorState state;

	/* How long the elevator should move for. */
	public float movementTimeMax;

	/* 
	 * How fast the elevator moves.  Only a positive number should be provided -- if you want an elevator that moves up or down by default,
	 * tinker with the state variable.
	 */
	public float movementSpeed; //Movement_speed should always be positive

	protected int timeSpentMoving = 0;

	//TODO: Impelement this
	//public bool haltAllyMovementWhileMoving = true;

	/*If we want to  disable movement of characters while they are on a moving elevator, we neet a set to put them in.*/
	protected HashSet<CharacterMove> charactersOnElevator = new HashSet<CharacterMove>();

	public virtual void ActivateElevator () { //Not strictly necessary to make virtual at this time, but hey -- it might come up.
		if (state == ElevatorState.STATIONARY_BOTTOM) {
			state = ElevatorState.ASCENDING;
			GetComponent<Rigidbody2D> ().velocity = new Vector2(0, movementSpeed);
			foreach (CharacterMove move in charactersOnElevator) {
				move.GetComponent<Rigidbody2D> ().velocity = GetComponent<Rigidbody2D> ().velocity; 
				//Hack to prevent phyics weirdness if elevator suddenly stops, or if elevator moves downward faster than fall speed
			}
		} else if (state == ElevatorState.STATIONARY_TOP) {
			state = ElevatorState.DESCENDING;
			GetComponent<Rigidbody2D> ().velocity = new Vector2(0, -1*movementSpeed);
			foreach (CharacterMove move in charactersOnElevator) {
				move.GetComponent<Rigidbody2D> ().velocity = GetComponent<Rigidbody2D> ().velocity; 
				//Hack to prevent phyics weirdness if elevator suddenly stops, or if elevator moves downward faster than fall speed
			}
		}
	}

	/*
	 * Will increment variables that keep track of how far elevator has moved, and change state to stationary (if necessary).
	 * Should only be called from Fixed Update.
	 */
	public virtual void IncrementElevatorMoveTime () {  //Not strictly necessary to make virtual at this time, but hey -- it might come up.
		//If ascending/descending, increment time-in-motion and check if we should stop;
		if (state == ElevatorState.ASCENDING || state == ElevatorState.DESCENDING) {
			timeSpentMoving++;
			//Characters on a moving elevator shouldn't walk -- disable their movement through the stored CharacterMove.
			foreach (CharacterMove move in charactersOnElevator) {
				move.autoWalk = false;
			}
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
				//Hack to prevent phyics weirdness if elevator suddenly stops, or if elevator moves downward faster than fall speed
			}
		} 
	}
	
	void FixedUpdate () {
		IncrementElevatorMoveTime ();
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
