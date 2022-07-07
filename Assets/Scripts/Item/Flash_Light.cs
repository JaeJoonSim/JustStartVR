using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace JustStartVR
{

    public class Flash_Light : GrabbableEvents
    {
        [SerializeField]
        GameObject Light;
        [SerializeField]
        Image batter_UI;

        public float batter = 1;
        public bool _Grab = false;
        bool On_Off = false, Shake_bool = false;


        public void OnGrab(bool Grab)
        {
            _Grab = Grab;
            StartCoroutine(Shake());
        }

        void OnOffFlash()
        {
            if (On_Off)
            {
                On_Off = false;
                Light.SetActive(false);
            }
            else
            {
                On_Off = true;
                Light.SetActive(true);
                StartCoroutine(Time());
            }
        }

        IEnumerator Time()
        {
            while (On_Off)
            {
                batter -= 0.001f;
                //Debug.Log(batter);
                batter_UI.fillAmount = batter;
                if (batter < 0) batter_End();
                yield return new WaitForSeconds(0.1f);
            }
        }

        void batter_End()
        {
            On_Off = false;
            Light.SetActive(false);
        }


        IEnumerator Shake()
        {

            float Old_Distance = 0;
            while (_Grab)
            {
                Vector3 pos = transform.position;
                yield return new WaitForSeconds(0.1f);
                float Distance = Vector3.Distance(pos, transform.position);

                float Difference = Mathf.Abs(Distance - Old_Distance);
                Old_Distance = Distance;
                Debug.Log("Distance" + Distance);
                if (Difference > 0.02f && Difference < 0.3f)
                {
                    batter += 0.1f;
                    Debug.Log("Shake");
                }
            }
            yield return null;
        }
        public override void OnButton1Down()
        {
            OnOffFlash();
        }
        public override void OnButton2Down()
        {
            OnOffFlash();
        }
    }
}
