using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace JustStartVR
{
    public class Keypad : MonoBehaviour
    {
        public string code;


        public Text display;
        public string Entry = "";
        [HideInInspector]public int[] password;

        public int Index => Entry?.Length ?? 0;

        public int MaxLength => code?.Length ?? 0;


        protected void Start()
        {
            var buttons = GetComponentsInChildren<Button>();
            var colliders = GetComponentsInChildren<Collider>();

            code = "";
            password = new int[5];
            int value;
            for (int i = 0; i < 4; i++)
            {
                code += value = Random.Range(1, 9);
                password[i + 1] = value;
            }

            Debug.Log(code);

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
                if(i == 11)
                {
                    button.keyText.text = "Enter";
                }
            }

            Set_Entry();
        }

        public void Set_Entry()
        {
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
                    SceneManager.LoadScene("GameClearScene");
                }
                Set_Entry();
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