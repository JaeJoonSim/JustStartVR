using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace JustStartVR
{
    public class Keypad : MonoBehaviour
    {
        public DoorHelper doorHelper;
        public UnityEvent Unlocked = new UnityEvent();

        public string code;
        public Text display;
        public string Entry = "";

        public int Index => Entry?.Length ?? 0;

        public int MaxLength => code?.Length ?? 0;

        private bool _unlocked;

        protected void Start()
        {
            var buttons = GetComponentsInChildren<Button>();
            var colliders = GetComponentsInChildren<Collider>();

            foreach (var keyCollider in colliders)
            {
                foreach (var ourCollider in GetComponents<Collider>())
                {
                    Physics.IgnoreCollision(ourCollider, keyCollider);
                }
            }

            for (int i = 0; i < buttons.Length; i++)
            {
                var button = buttons[i];
                //button.onButtonDown.AddListener(OnButtonDown);
                
                if (i >= 0 && i <= 9)
                {
                    button.Key = i.ToString()[0];
                }
                else if (i == 10)
                {
                    button.Key = '<';
                }
                else if (i == 11)
                {
                    button.Key = '+';
                }

                if (button.keyText)
                {
                    button.keyText.text = button.Key.ToString();
                }
            }

            Entry = "";
            if (display)
            {
                display.text = Entry.PadLeft(MaxLength, '*');
            }
        }

        public void OnButtonDown(Button button)
        {
            var keyPadButton = button;

            if (keyPadButton.Key == '<')
            {
                if (Entry.Length > 0)
                {
                    Entry = Entry.Substring(0, Entry.Length - 1);
                }
                else
                {
                    return;
                }
            }
            else if (keyPadButton.Key == '+')
            {
                if (code == Entry)
                {
                    doorHelper.DoorIsLocked = false;
                }
            }
            else if (Index >= 0 && Index < MaxLength)
            {
                Entry += keyPadButton.Key;
            }

            if (display)
            {
                display.text = Entry.PadLeft(MaxLength, '*');
            }
        }
    }
}