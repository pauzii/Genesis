using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Minion : MonoBehaviour {

	public Vector3[] Path = new Vector3[0];

	private NavMeshAgent Navigation;
	private int PathIndex;

	void Awake() {
		Navigation = GetComponent<NavMeshAgent>();
	}

	void Start() {
		PathIndex = 0;
	}
	
	void Update() {
		// Choose the next destination point when the agent gets
		// close to the current one.
		if (!Navigation.pathPending && Navigation.remainingDistance < 0.5f)
			GotoNextPoint();
	}

	private void GotoNextPoint() {
		// Returns if no points have been set up
		if (Path.Length == 0)
			return;

		// Set the agent to go to the currently selected destination.
		Navigation.destination = Path[PathIndex];

		// Choose the next point in the array as the destination,
		// cycling to the start if necessary.
		PathIndex = (PathIndex + 1) % Path.Length;
	}
}
