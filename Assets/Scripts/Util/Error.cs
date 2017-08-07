using UnityEngine;
using System.Collections;

namespace Util {
    public class Error : MonoBehaviour {

        public static void ShowError(string message)
        {
            Debug.LogError(message);
            if(UnityEditor.EditorApplication.isPlaying)
                UnityEditor.EditorApplication.isPlaying = false;
        }
    }
}
