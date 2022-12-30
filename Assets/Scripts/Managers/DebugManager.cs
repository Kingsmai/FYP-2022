using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CraftsmanHero
{
    public class DebugManager : Singleton<DebugManager>
    {
        public TextMeshProUGUI text;

        public static void Log(object msg) {
            Instance.text.text = msg.ToString();
        }
    }
}
