using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class UpdatingRealTimeYear : MonoBehaviour
{
    // Variables


    private void Start()
    {
        
    }


#if UNITY_EDITOR
    [CustomEditor(typeof(UpdatingRealTimeYear)), CanEditMultipleObjects]
    private class UpdatingRealTimeYearEditor : Editor
    {
        UpdatingRealTimeYear _target;

        private void OnEnable() => _target = (UpdatingRealTimeYear)target;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

        }
    }
#endif
}
