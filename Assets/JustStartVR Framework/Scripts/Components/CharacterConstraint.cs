using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JustStartVR {

	/// <summary>
	/// This CharacterConstraint will keep the size the Character Capsule along with the camera if not colliding with anything
	/// </summary>
	public class CharacterConstraint : MonoBehaviour {

		PlayerController JustStartVRController;
		CharacterController character;

		void Awake() {
			character = GetComponent<CharacterController>();
			JustStartVRController = transform.GetComponentInParent<PlayerController>();
		}

		private void Update() {
			CheckCharacterCollisionMove();
		}

		public virtual void CheckCharacterCollisionMove() {

			var initialCameraRigPosition = JustStartVRController.CameraRig.transform.position;
			var cameraPosition = JustStartVRController.CenterEyeAnchor.position;
			var delta = cameraPosition - transform.position;

			// Ignore Y position
			delta.y = 0;

			// Move Character Controller and Camera Rig to Camera's delta
			if (delta.magnitude > 0) {
				character.Move(delta);

				// Move Camera Rig back into position
				JustStartVRController.CameraRig.transform.position = initialCameraRigPosition;
			}
		}
	}
}