using System;
using UnityEngine;

namespace Project
{
    [RequireComponent(typeof(Renderer))]
    public class MaterialHighlight : MonoBehaviour
    {
        [SerializeField] private Color _highlightColor = Color.white;

        private Material _material;
        private bool _isSelected;
        private Color _previousColor;

        public void Select()
        {
            if (_isSelected)
                throw new InvalidOperationException("Material already selected.");

            _isSelected = true;
            _previousColor = _material.color;
            _material.color = _highlightColor;
        }

        public void Unselect()
        {
            if (!_isSelected)
                throw new InvalidOperationException("Material not selected.");

            _isSelected = false;

            _material.color = _previousColor;
        }

        private void Awake()
        {
            _material = GetComponent<Renderer>().material;
        }
    }
}
