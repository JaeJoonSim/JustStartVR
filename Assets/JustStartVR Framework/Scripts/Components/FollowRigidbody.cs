using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JustStartVR {
    public class FollowRigidbody : MonoBehaviour {

        public Transform FollowTransform;
        Rigidbody rigid;

        void Start() {
            rigid = GetComponent<Rigidbody>();
        }

        void FixedUpdate() {
            rigid.MovePosition(FollowTransform.transform.position);
        }
    }
}

