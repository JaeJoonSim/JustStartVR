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

        //[SerializeField]
        Animator animator;
        [SerializeField]
        BoxCollider Needle;

        Player_HP Player_HP;

        //GameObject Needle;

        void Start()
        {
            animator = GetComponent<Animator>();
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Player");
            foreach(GameObject G in gameObjects)
            {
                Player_HP = G.GetComponent<Player_HP>();
                if (Player_HP != null)
                Player_HP = G.GetComponent<Player_HP>();
            }       
        }


        void Activate()
        {
            if (Activation)
            {
                Activation = false;
                animator.SetBool("Acte", false);
                Needle.enabled = false;
                return;
            }
            if (!Activation && !Use)
            {
                Activation = true;
                animator.SetBool("Acte", true);
                Needle.enabled = true;
                SoundManager.m_instance.PlaySound(transform.position, SoundManager.SoundType.HealTrigger);
                //Debug.Log("Ativete");
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if( Activation && other.tag == "Hand")
            {
                //Debug.Log("Use");
                Recovery();
            }
        }

        void Recovery()
        {
            animator.SetTrigger("Use");
            Activation = false;
            Use = true;
            Needle.enabled = false;
            Player_HP.change_HP(Amount_Of_Recovery);
            SoundManager.m_instance.PlaySound(transform.position, SoundManager.SoundType.Heal);
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
