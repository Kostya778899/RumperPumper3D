using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ComponentsContainerSet : MonoBehaviour
{
    public ComponentsContainer Container;
    public List<Component> Values;

    [SerializeField] private bool _setOnAwake = true;


    private void Awake() { if (_setOnAwake) TrySet(); }
    public void TrySet() { if (Container) Container.Set(Values); }
}
