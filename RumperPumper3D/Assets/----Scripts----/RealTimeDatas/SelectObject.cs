using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using CMath;

namespace SelectObject
{
    public interface ISelectObject
    {
        public void Select(int index);
    }

    public class SelectObject : MonoBehaviour, ISelectObject
    {
        public UnityEvent<int> OnSelected;

        [SerializeField] private int _selectedIndex = 0;
        [SerializeField] private string _selectedIndexSaveKey = "SelectedIndex";
        [SerializeField] private Object[] _objects = new Object[3];

        [Serializable]
        private struct Object : IIncluded
        {
            [SerializeField] private UnityEvent<bool> _onSetState;
            [SerializeField] private UnityEvent _onSelect, _onDeSelect;
            [SerializeField] private UiSetColorOfSellect _uiSetColorOfSellect;

            public void Activate()
            {
                _uiSetColorOfSellect.Activate();
                _onSetState?.Invoke(true);
                _onSelect?.Invoke();
            }
            public void DeActivate()
            {
                _uiSetColorOfSellect.DeActivate();
                _onSetState?.Invoke(false);
                _onDeSelect?.Invoke();
            }
        }


        public void Select(int index)
        {
            if (index < 0 && index >= _objects.Length) throw new IndexOutOfRangeException();

            _objects[_selectedIndex].DeActivate();
            _selectedIndex = index;
            _objects[_selectedIndex].Activate();
            PlayerPrefs.SetInt(_selectedIndexSaveKey, _selectedIndex);

            OnSelected?.Invoke(index);
        }

        private void Awake()
        {
            foreach (var item in _objects) item.DeActivate();
            Select(PlayerPrefs.GetInt(_selectedIndexSaveKey));
        }
    }
}
