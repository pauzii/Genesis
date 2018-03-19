using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class FreeCamera : MonoBehaviour {

	[Range(0f, 1f)] public float Damping = 0.95f;

	private float Velocity = 5f;
	private float AngularVelocity = 5f;
	private float ZoomVelocity = 10;
	private float Sensitivity = 1f;
	private Vector2 MousePosition;
	private Vector2 LastMousePosition;
	private Vector3 DeltaRotation;
	private Quaternion ZeroRotation;

	void Start() {
		Vector3 euler = transform.rotation.eulerAngles;
		transform.rotation = Quaternion.Euler(0f, euler.y, 0f);
		ZeroRotation = transform.rotation;
		MousePosition = GetNormalizedMousePosition();
		LastMousePosition = GetNormalizedMousePosition();
	}

	void LateUpdate() {
		MousePosition = GetNormalizedMousePosition();

		if(EventSystem.current != null) {
			if(!Input.GetKey(KeyCode.Mouse0)) {
				EventSystem.current.SetSelectedGameObject(null);
			}
			if(EventSystem.current.currentSelectedGameObject == null) {
				UpdateFreeCamera();
			}
		} else {
			UpdateFreeCamera();
		}

		LastMousePosition = MousePosition;
	}

	private void UpdateFreeCamera() {
		//Translation
		Vector3 direction = Vector3.zero;
		if(Input.GetKey(KeyCode.A)) {
			direction.x -= 1f;
		}
		if(Input.GetKey(KeyCode.D)) {
			direction.x += 1f;
		}
		if(Input.GetKey(KeyCode.W)) {
			direction.z += 1f;
		}
		if(Input.GetKey(KeyCode.S)) {
			direction.z -= 1f;
		}
		transform.position += Velocity*Sensitivity*Time.deltaTime*(transform.rotation*direction);

		//Zoom
		if(Input.mouseScrollDelta.y != 0) {
			transform.position += ZoomVelocity*Sensitivity*Time.deltaTime*Input.mouseScrollDelta.y*transform.forward;
		}

		//Rotation
		MousePosition = GetNormalizedMousePosition();
		if(Input.GetMouseButton(0)) {
			DeltaRotation += 1000f*AngularVelocity*Sensitivity*Time.deltaTime*new Vector3(GetNormalizedDeltaMousePosition().x, GetNormalizedDeltaMousePosition().y, 0f);
			transform.rotation = ZeroRotation * Quaternion.Euler(-DeltaRotation.y, DeltaRotation.x, 0f);
		}
	}

	private Vector2 GetNormalizedMousePosition() {
		Vector2 ViewPortPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
		return new Vector2(ViewPortPosition.x, ViewPortPosition.y);
	}

	private Vector2 GetNormalizedDeltaMousePosition() {
		return MousePosition - LastMousePosition;
	}

}