using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Project
{
    public class MultimeterRegulator : MonoBehaviour
    {
        [SerializeField] private UnityEvent _activated;
        [SerializeField] private UnityEvent _deactivated;

        private GameInput _input;
        private Multimeter _multimeter;
        private MultimeterModeSelector _modeSelector;
        private bool _isActive;

        public void Construct(GameInput input, Multimeter multimeter, MultimeterMode[] availableModes)
        {
            _input = input;
            _multimeter = multimeter;
            _modeSelector = new(availableModes);

            _modeSelector.Selected += OnModeSelected;
        }

        public void Activate()
        {
            if (_isActive)
                throw new InvalidOperationException("Multimeter regulator already active.");

            _isActive = true;

            _input.Multimeter.NextMode.performed += OnNextModePerformed;
            _input.Multimeter.PreviousMode.performed += OnPreviousModePerformed;

            _activated?.Invoke();
        }

        public void Deactivate()
        {
            if (!_isActive)
                throw new InvalidOperationException("Multimeter regulator not active.");

            _isActive = false;

            _input.Multimeter.NextMode.performed -= OnNextModePerformed;
            _input.Multimeter.PreviousMode.performed -= OnPreviousModePerformed;

            _deactivated?.Invoke();
        }

        private void OnDestroy()
        {
            if (_isActive)
                Deactivate();

            _modeSelector.Selected -= OnModeSelected;
        }

        private void OnNextModePerformed(InputAction.CallbackContext context)
        {
            _modeSelector.SelectNextMode();
        }

        private void OnPreviousModePerformed(InputAction.CallbackContext context)
        {
            _modeSelector.SelectPreviousMode();
        }

        private void OnModeSelected(MultimeterMode mode)
        {
            _multimeter.SetMode(mode);
        }
    }
}
