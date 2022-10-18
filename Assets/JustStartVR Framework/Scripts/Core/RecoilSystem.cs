using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JustStartVR
{
    public class RecoilSystem : MonoBehaviour
    {
        // Recoil Setting
        [Header("Recoil Forces")]
        [Tooltip("Recoil applied about the X axis.")]
        public float UpForce = 1f;

        [Tooltip("Recoil applied at the BackRecoil transform position in the Z direction.")]
        public float BackwardsForce = 1f;

        [Header("TwoHandTorque")] public float TwoHandUpForce;
        public float TwoHandBackwardsForce;
        public bool UseTwoHandRecoilForce;

        public bool ImpulseForce;

        [Header("Side To Side Recoil")]
        public bool RandomSideToSideRecoil = false;

        public float SideToSideMin;
        public float SideToSideMax;



        public float TwoHandSideToSideMin;
        public float TwoHandSideToSideMax;


        [Header("Limits")]
        public bool LimitRecoilForce = true;

        [Tooltip("Maximum constant force applied to the up recoil.")]
        public float MaxUpForce = 10f;

        public bool UseTwoHandMaxUpforce;
        public bool UseTwoHandMaxSideForce;

        [Tooltip("Maximum constant force applied to the back recoil")]
        public float MaxBackForce = 10f;

        [Tooltip("Maximum constant force applied to the up recoil when two handed.")]
        public float TwoHandMaxUpForce = 200f;

        //is constant side force even a useful or wanted item?

        [Tooltip("Maximum constant torque applied for side to side recoil")]
        public float MaxSideForce = 0f;

        [Tooltip("Maximum constant force applied to the side recoil")]
        public float TwoHandMaxSideForce = 0f;



        [Header("Recovery")]
        public float RecoveryDelay = .2f;
        public float TwoHandedRecoveryDelay = .1f;

        public float RecoveryTime = .1f;
        public float TwoHandedRecoveryTime = .05f;

        // Recoil Setting

        protected Grabbable grab;

        public UpRecoilType UpRecoilType;

        public Transform UpRecoil;
        public Transform BackRecoil;

        public Vector3 CurrentForce;

        [Tooltip("If the gun is rotated set to true to reverse the x torque direction")]
        public bool TorqueAxisReversed;

        private float _timeSinceLastRecoil;
        private float _recoveryTimer;
        private bool _recoil;


        public Rigidbody Rigidbody => UpRecoilType == JustStartVR.UpRecoilType.TorqueHand ? HandRigidBody : GunRigidBody;

        public Rigidbody HandRigidBody { get; set; }

        public Rigidbody GunRigidBody { get; set; }

        public bool TwoHanded { get; set; }

        public float SideToSide
        {
            get
            {
                if (!RandomSideToSideRecoil)
                {
                    return 0f;
                }

                if (TwoHanded)
                    return Random.Range(TwoHandSideToSideMin, TwoHandSideToSideMax);

                return Random.Range(SideToSideMin, SideToSideMax);
            }
        }

        private void Awake()
        {
            grab = GetComponent<Grabbable>();
        }

        private void Start()
        {
            GunRigidBody = gameObject.GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            _timeSinceLastRecoil += Time.fixedDeltaTime;

            if (!Rigidbody)
                return;

            ApplyRecoil();
            CheckRecovery();
            ApplyConstantForce();


            _recoil = false;
        }

        private void ApplyRecoil()
        {
            if (_recoil)
            {
                grab.RequestSpringTime(0.3f);
                var upForce = UpForce;
                var backForce = BackwardsForce;

                if (TwoHanded && UseTwoHandRecoilForce)
                {
                    upForce = TwoHandUpForce;
                    backForce = TwoHandBackwardsForce;
                }

                if (ImpulseForce)
                {
                    ApplyImpulseRecoil(upForce, backForce);
                }
                else
                {
                    CurrentForce.y += upForce;
                    CurrentForce.z += backForce;
                }

                if (RandomSideToSideRecoil)
                {
                    //Rigidbody.AddForceAtPosition(UpRecoil.right * SideToSide, UpRecoil.position, ForceMode.Impulse);
                    Rigidbody.AddTorque(transform.up * SideToSide, ForceMode.Impulse);
                    CurrentForce.x += SideToSide / Time.fixedDeltaTime;
                }
            }

            if (LimitRecoilForce)
            {
                var maxForce = MaxUpForce;
                if (UseTwoHandMaxUpforce && TwoHanded)
                {
                    maxForce = TwoHandMaxUpForce;
                }

                var maxSideForce = MaxSideForce;
                if (UseTwoHandMaxSideForce && TwoHanded)
                {
                    maxSideForce = TwoHandMaxSideForce;
                }

                CurrentForce.x = Mathf.Clamp(CurrentForce.x, -maxSideForce, maxSideForce);
                CurrentForce.y = Mathf.Clamp(CurrentForce.y, 0, maxForce);
                CurrentForce.z = Mathf.Clamp(CurrentForce.z, 0, MaxBackForce);
            }
        }

        private void CheckRecovery()
        {
            var delay = TwoHanded ? TwoHandedRecoveryDelay : RecoveryDelay;

            if (_timeSinceLastRecoil > delay)
            {
                var recoveryTime = TwoHanded ? TwoHandedRecoveryTime : RecoveryTime;
                _recoveryTimer += Time.fixedDeltaTime;
                var percentRecovered = Mathf.Clamp(_recoveryTimer / recoveryTime, 0, 1);

                CurrentForce *= 1 - percentRecovered;
            }
        }

        private void ApplyConstantForce()
        {
            if (UpRecoilType == UpRecoilType.UpRecoil)
            {
                if (UpRecoil)
                {
                    Rigidbody.AddForceAtPosition(UpRecoil.up * CurrentForce.y, UpRecoil.position, ForceMode.Force);
                }
            }
            else
            {
                Rigidbody.AddTorque(transform.right * (CurrentForce.y * (TorqueAxisReversed ? -1 : 1)), ForceMode.Force);
            }

            Rigidbody.AddTorque(transform.up * CurrentForce.x, ForceMode.Force);

            if (BackRecoil)
            {
                Rigidbody.AddForceAtPosition(BackRecoil.forward * CurrentForce.z, BackRecoil.position, ForceMode.Force);
            }
        }

        private void ApplyImpulseRecoil(float upForce, float backForce)
        {
            if (UpRecoilType == JustStartVR.UpRecoilType.UpRecoil)
            {
                if (UpRecoil)
                {
                    Rigidbody.AddForceAtPosition(UpRecoil.up * upForce, UpRecoil.position, ForceMode.Impulse);
                }
            }
            else
            {
                Rigidbody.AddTorque(transform.right * (upForce * (TorqueAxisReversed ? -1 : 1)), ForceMode.Impulse);
            }


            if (BackRecoil)
            {
                Rigidbody.AddForceAtPosition(BackRecoil.forward * backForce, BackRecoil.position, ForceMode.Impulse);
            }

            CurrentForce.y += upForce / Time.fixedDeltaTime;
            CurrentForce.z += backForce / Time.fixedDeltaTime;
        }

        public void Recoil()
        {
            _recoil = true;
            _timeSinceLastRecoil = 0f;
            _recoveryTimer = 0f;
        }
    }

    public enum UpRecoilType
    {
        UpRecoil,
        TorqueHand,
        TorqueGun
    }
}