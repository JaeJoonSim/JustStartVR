using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JustStartVR
{
    public class Syringe : GrabbableEvents
    {
        public int Amount_Of_Recovery = 10;

        bool Activation = false;
        bool Use = false;

        [SerializeField]
        Animator animator;
        [SerializeField]
        GameObject Needle;

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void Activate()
        {
            if (!Activation && !Use)
            {
                Activation = true;
                animator.SetBool("Use", true);
                Needle.SetActive(true);
                Debug.Log("Ativete");
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if( Activation && other.tag == "Player")
            {
                Debug.Log("Use");
                Recovery();
            }
        }

        void Recovery()
        {
            Activation = false;
            Use = true;
            animator.SetBool("Use", false);
        }

        public override void OnButton1Down()
        {
            Activate();
        }
        public override void OnButton2Down()
        {
            Activate();
        }
    }
}
