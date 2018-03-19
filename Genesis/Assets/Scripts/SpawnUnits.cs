using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class SpawnUnits : MonoBehaviour {

	public GameObject Prefab;
	public float Frequency = 30f;
	public int Amount = 1;
	public float Radius = 1f;
	public Vector3[] Path = new Vector3[0];

	void Awake() {

	}
	
	void Start() {
		StartCoroutine(Spawn());
	}

	void Update() {
		
	}

	private IEnumerator Spawn() {
		while(true) {
			for(int i=0; i<Amount; i++) {
				float step = (float)i / (float)Amount * 2f * Mathf.PI;
				Vector3 position = transform.position + Radius * new Vector3(Mathf.Sin(step), 0f, Mathf.Cos(step));
				GameObject instance = Instantiate(Prefab, position, Quaternion.Euler(0f, 360f * Random.value, 0f));
				instance.GetComponent<Minion>().Path = Path; //TODO: THIS IS SHIT
			}
			yield return new WaitForSeconds(Frequency);
		}
	}

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere(transform.position, Radius);
		Gizmos.color = Color.magenta;
		for(int i=1; i<Path.Length; i++) {
			Gizmos.DrawLine(Path[i-1], Path[i]);
		}
		for(int i=0; i<Path.Length; i++) {
			Gizmos.DrawSphere(Path[i], 0.1f);
		}
	}
}
