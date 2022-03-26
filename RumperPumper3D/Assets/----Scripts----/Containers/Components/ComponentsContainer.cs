using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using CMath;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "Containers/Components", fileName = "NewComponentsContainer")]
public class ComponentsContainer : ScriptableObject
{
    [HideInInspector] public List<Component> Values { get; protected set; } = new List<Component>(1) { null };


    public T Get<T>(int index = 0) where T : Component { return (T)Values[index]; }
    public void Set(List<Component> values) => Values = CList.Equate<Component>(values);


    #region Editor
#if UNITY_EDITOR
    public virtual void OnValidateVaules()
    {
        if (Values != null && Values[0] != null)
        {
            var containerSet = Values[0].gameObject.GetComponent<ComponentsContainerSet>();
            if (!containerSet)
            {
                containerSet = Values[0].gameObject.AddComponent<ComponentsContainerSet>();
                containerSet.Container = this;

                CMath.CEditor.SetObjectDirty(containerSet.gameObject);
            }

            if (containerSet.Container == this) containerSet.Values = CList.Equate<Component>(Values);
        }
    }

    [CustomEditor(typeof(ComponentsContainer))]
    [CanEditMultipleObjects]
    public class ComponentsContainerEditor : Editor
    {
        private ComponentsContainer _container;
        private List<string> _containerValuesNames = new List<string>();


        private void OnEnable() => _container = (ComponentsContainer)target;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (_container.Values != null && _container.Values.Count > 0)
            {
                if (_containerValuesNames == null || _containerValuesNames.Count != _container.Values.Count)
                {
                    _containerValuesNames = new List<string>(_container.Values.Count);
                    for (int i = 0; i < _container.Values.Count; i++) _containerValuesNames.Add($"{_container.name} ({i})");
                }

                EditorGUILayout.LabelField("Values: ");

                for (int i = 0; i < _container.Values.Count; i++)
                {

                    EditorGUILayout.BeginVertical("Box", GUILayout.ExpandWidth(false));
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField($"ValueN{i}: ", GUILayout.Width(65));
                    _containerValuesNames[i] = EditorGUILayout.TextField(_containerValuesNames[i], GUILayout.Width(80));
                    _container.Values[i] = (Component)EditorGUILayout.ObjectField("", _container.Values[i], typeof(Component), true, 
                        GUILayout.MinWidth(65), GUILayout.MaxWidth(160), GUILayout.ExpandWidth(true));

                    if (i > 0 && GUILayout.Button("X", GUILayout.Height(20), GUILayout.Width(20)))
                    {
                        _container.Values.RemoveAt(i);
                        _containerValuesNames.RemoveAt(i);
                    }
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.EndVertical();
                }
            }
            if (GUILayout.Button("Add", GUILayout.Height(35), GUILayout.ExpandWidth(true)))
            {
                if (_container.Values == null) _container.Values = new List<Component>(1);
                _container.Values.Add(null);
                _containerValuesNames.Add($"default ({_containerValuesNames.Count})");


            }

            if (GUI.changed) _container.OnValidateVaules();
        }
    }
#endif
    #endregion
}
