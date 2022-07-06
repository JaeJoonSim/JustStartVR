using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JustStartVR
{


    /// <summary>
    /// This is an example of how to spawn ammo depending on the weapon that is equipped in the opposite hand
    /// </summary>
    public class AmmoDispenser : MonoBehaviour
    {

        /// <summary>
        /// Used to determine if holding a weapon
        /// </summary>
        public Grabber LeftGrabber;

        /// <summary>
        /// Used to determine if holding a weapon
        /// </summary>
        public Grabber RightGrabber;

        /// <summary>
        /// Disable this if weapon not equipped
        /// </summary>
        public GameObject AmmoDispenserObject;

        /// <summary>
        /// Instantiate this if M1911 equipped
        /// </summary>
        public GameObject M1911Clip;

        /// <summary>
        /// Instantiate this if Glock equipped
        /// </summary>
        public GameObject GlockClip;

        /// <summary>
        /// Instantiate this if Glock equipped
        /// </summary>
        public GameObject AK74MClip;

        /// <summary>
        /// Amount of M1911 Clips currently available
        /// </summary>
        public int CurrentM1911Clips = 5;

        public int CurrentAK74MClips = 5;

        public int CurrentGlockClips = 30;

        // Update is called once per frame
        void Update()
        {
            bool weaponEquipped = false;

            if (grabberHasWeapon(LeftGrabber) || grabberHasWeapon(RightGrabber))
            {
                weaponEquipped = true;
            }

            // Only show if we have something equipped
            if (AmmoDispenserObject.activeSelf != weaponEquipped)
            {
                AmmoDispenserObject.SetActive(weaponEquipped);
            }
        }

        bool grabberHasWeapon(Grabber g)
        {

            if (g == null || g.HeldGrabbable == null)
            {
                return false;
            }

            // Holding Glock, M1911, or AK74MClip
            string grabName = g.HeldGrabbable.transform.name;
            if (grabName.Contains("Glock") || grabName.Contains("M1911") || grabName.Contains("AK74M"))
            {
                return true;
            }

            return false;
        }

        public GameObject GetAmmo()
        {

            bool leftGrabberValid = LeftGrabber != null && LeftGrabber.HeldGrabbable != null;
            bool rightGrabberValid = RightGrabber != null && RightGrabber.HeldGrabbable != null;

            // Glock
            if (leftGrabberValid && LeftGrabber.HeldGrabbable.transform.name.Contains("Glock") && CurrentGlockClips > 0)
            {
                CurrentGlockClips--;
                return GlockClip;
            }
            else if (rightGrabberValid && RightGrabber.HeldGrabbable.transform.name.Contains("Glock") && CurrentGlockClips > 0)
            {
                CurrentGlockClips--;
                return GlockClip;
            }

            // AK74MClip
            if (leftGrabberValid && LeftGrabber.HeldGrabbable.transform.name.Contains("AK74M") && CurrentAK74MClips > 0)
            {
                CurrentAK74MClips--;
                return AK74MClip;
            }
            else if (rightGrabberValid && RightGrabber.HeldGrabbable.transform.name.Contains("AK74M") && CurrentAK74MClips > 0)
            {
                CurrentAK74MClips--;
                return AK74MClip;
            }

            // M1911
            if (leftGrabberValid && LeftGrabber.HeldGrabbable.transform.name.Contains("M1911") && CurrentM1911Clips > 0)
            {
                CurrentM1911Clips--;
                return M1911Clip;
            }
            else if (rightGrabberValid && RightGrabber.HeldGrabbable.transform.name.Contains("M1911") && CurrentM1911Clips > 0)
            {
                CurrentM1911Clips--;
                return M1911Clip;
            }

            // Default to nothing
            return null;
        }

        public void GrabAmmo(Grabber grabber)
        {

            GameObject ammoClip = GetAmmo();
            if (ammoClip != null)
            {
                GameObject ammo = Instantiate(ammoClip, grabber.transform.position, grabber.transform.rotation) as GameObject;
                Grabbable g = ammo.GetComponent<Grabbable>();

                // Disable rings for performance
                GrabbableRingHelper grh = ammo.GetComponentInChildren<GrabbableRingHelper>();
                if (grh)
                {
                    Destroy(grh);
                    RingHelper r = ammo.GetComponentInChildren<RingHelper>();
                    Destroy(r.gameObject);
                }

                // Offset to hand
                ammo.transform.parent = grabber.transform;
                ammo.transform.localPosition = -g.GrabPositionOffset;
                ammo.transform.parent = null;

                grabber.GrabGrabbable(g);
            }
        }

        public virtual void AddAmmo(string AmmoName)
        {
            if (AmmoName.Contains("Glock"))
            {
                CurrentGlockClips++;
            }
            else if (AmmoName.Contains("AK74M"))
            {
                CurrentAK74MClips--;
            }
            else if (AmmoName.Contains("M1911"))
            {
                CurrentM1911Clips++;
            }
        }
    }
}

