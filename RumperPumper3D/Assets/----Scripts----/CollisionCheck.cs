using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using CMath;

public class CollisionCheck : MonoBehaviour
{
    public UnityEvent<GameObject, Collision> OnCollision_;
    public List<OnCollisionObject> OnCollisionWithObjects = new();
    public List<OnCollisionObjectHasTag> OnCollisionWithTags = new();
    public List<OnCollisionObjectHasComponent> OnCollisionWithComponents = new();

    [SerializeField] private bool _collisionWithParticles = false, _collisionWithTriggers = false;

    #region OnCollisionObjectHasValue(classes)
    [Serializable]
    public abstract class OnCollisionObjectHasValue<T>
    {
        public UnityEvent<T, Collision> Event = new();

        public abstract void OnCollision(GameObject @object, Collision collision);
    }

    [Serializable]
    public class OnCollisionObject : OnCollisionObjectHasValue<GameObject>
    {
        [TagSelector, SerializeField] private GameObject _object;

        public override void OnCollision(GameObject @object, Collision collision) { if (@object == _object) Event?.Invoke(@object, collision); }
    }
    [Serializable]
    public class OnCollisionObjectHasTag : OnCollisionObjectHasValue<GameObject>
    {
        [TagSelector, SerializeField] private string _tag;

        public override void OnCollision(GameObject @object, Collision collision) { if (@object.tag == _tag) Event?.Invoke(@object, collision); }
    }
    [Serializable]
    public class OnCollisionObjectHasComponent : OnCollisionObjectHasValue<Component>
    {
        [SerializeField] private string _componentName = "CollisionCheck";
        private Type _componentType = null;

        public OnCollisionObjectHasComponent(in string componentName) => _componentName = componentName;
        public OnCollisionObjectHasComponent(Type componentType) => _componentType = componentType;

        public override void OnCollision(GameObject @object, Collision collision)
        {
            if (_componentType == null) _componentType = Type.GetType(_componentName, true);
            if (@object.TryGetComponent(_componentType, out Component component)) Event?.Invoke(component, collision);
        }
    }
    #endregion

    private void OnCollisionEnter(Collision collision) => OnCollision(collision.gameObject, collision);
    private void OnParticleCollision(GameObject other) { if (_collisionWithParticles) OnCollision(other); }
    private void OnTriggerEnter(Collider collision) { if (_collisionWithTriggers) OnCollision(collision.gameObject); }

    private void OnCollision(GameObject @object, Collision collision = null)
    {
        OnCollision_?.Invoke(@object, collision);

        foreach (var item in OnCollisionWithObjects) item.OnCollision(@object, collision);
        foreach (var item in OnCollisionWithTags) item.OnCollision(@object, collision);
        foreach (var item in OnCollisionWithComponents) item.OnCollision(@object, collision);
        //foreach (var item in _onCollisionWithTags) if (item.TagToCollision == @object.tag) item.Event?.Invoke(@object, collision);
    }
}
