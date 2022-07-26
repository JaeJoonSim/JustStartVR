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
        public GameObject xrRig;

        public void OnGrab(bool Grab)
        {
            _Grab = Grab;
            if(_Grab)
            StartCoroutine(Shake());
        }
        void OnOffFlash()
        {
            if (batter < 0) return;
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
                if(batter_UI != null)
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
            Vector3 pos = transform.position;
            while (_Grab)
            {
                yield return new WaitForSeconds(0.1f);
                float Distance = Vector3.Distance(pos, transform.position);
                //Debug.Log("Distance" + Distance);
                if (Distance< 0.1f)
                {
                    float Difference = Mathf.Abs(Distance - Old_Distance);
                    Old_Distance = Distance;
                    Debug.Log("Difference" + Difference);
                    if (Difference > 0.025f)
                    {
                        if (batter < 1)
                        {
                            batter_UI.fillAmount = batter;
                            batter += 0.01f;
                        }
                     
                       //Debug.Log("Shake");

                    }
                }
                else
                {
                    pos = transform.position;
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
