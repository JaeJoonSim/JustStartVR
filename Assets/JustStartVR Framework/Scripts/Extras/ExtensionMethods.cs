﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JustStartVR {
    public static class ExtensionMethods {

        /// <summary>
        /// Returns true if given key is held down. Calls  "InputBridge.Instance.GetControllerBindingValue(binding)"
        /// </summary>
        /// <param name="binding"></param>
        public static bool GetDown(this ControllerBinding binding) {
            return InputBridge.Instance.GetControllerBindingValue(binding);
        }
    }
}

