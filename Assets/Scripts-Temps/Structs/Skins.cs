using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CraftsmanHero {
    [System.Serializable]
    public struct Skins {
        public int skinId;
        public string skinName;
        public RuntimeAnimatorController controller;
    }
}
