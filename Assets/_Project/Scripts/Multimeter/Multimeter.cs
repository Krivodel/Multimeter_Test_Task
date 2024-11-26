using System;
using UnityEngine;

namespace Project
{
    public class Multimeter : MonoBehaviour
    {
        public MultimeterMode CurrentMode => _currentMode;

        public event Action Ticked;
        public event Action<MultimeterMode> CurrentModeChanged;

        private MultimeterModePool _modePool;
        private MultimeterMode _currentMode;
        private IDevice _device;

        public void Construct(MultimeterModePool modePool)
        {
            _modePool = modePool;
        }

        public void SetCurrentMode(MultimeterMode mode)
        {
            if (mode == null)
                throw new ArgumentNullException(nameof(mode));

            if (_currentMode == mode)
                throw new InvalidOperationException($"Mode '{mode}' already selected.");

            _currentMode = mode;

            CurrentModeChanged?.Invoke(_currentMode);

            OnTicked();
        }

        public void SetCurrentMode<TMode>() where TMode : MultimeterMode, new()
        {
            SetCurrentMode(_modePool.Get<TMode>());
        }

        public void ConnectDevice(IDevice device)
        {
            if (device == null)
                throw new ArgumentNullException(nameof(device));

            if (HasDevice())
                throw new InvalidOperationException("Cannot connect 2+ devices.");

            _device = device;

            OnTicked();
        }

        public void DisconnectDevice()
        {
            if (!HasDevice())
                throw new InvalidOperationException("Device not connected.");

            _device = null;

            OnTicked();
        }

        public bool HasDevice()
        {
            return _device != null;
        }

        public bool HasCurrentMode()
        {
            return _currentMode != null;
        }

        public bool CanTick()
        {
            return HasDevice() && HasCurrentMode();
        }

        public float GetResult(MultimeterMode mode)
        {
            if (mode == null)
                throw new ArgumentNullException(nameof(mode));

            return mode.Calculate(_device);
        }

        public float GetResult()
        {
            return GetResult(_currentMode);
        }

        public float GetResult<TMode>() where TMode : MultimeterMode, new()
        {
            return GetResult(_modePool.Get<TMode>());
        }

        private void OnTicked()
        {
            if (CanTick())
                Ticked?.Invoke();
        }
    }
}
