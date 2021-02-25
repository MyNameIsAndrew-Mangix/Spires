using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Spire.Stats
{
    [System.Serializable]
    public class AttributeBlock : ScriptableObject
    {
        public List<Attribute> attributes;
    }
}