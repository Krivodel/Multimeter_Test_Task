using System;
using UnityEngine;

namespace Project
{
    public class Multimeter : MonoBehaviour
    {
        public MultimeterMode Mode => _mode;

        public event Action<MultimeterMode> ModeChanged;
        public event Action<float> ResultChanged;

        private MultimeterMode _mode;
        private IDevice _device;

        public void SetMode(MultimeterMode mode)
        {
            if (mode == null)
                throw new ArgumentNullException(nameof(mode));

            if (_mode == mode)
                throw new InvalidOperationException($"Mode '{mode}' already selected.");

            _mode = mode;

            ModeChanged?.Invoke(_mode);
        }

        public void SetMode<TMode>() where TMode : MultimeterMode, new()
        {
            SetMode(new TMode());
        }

        public void ConnectDevice(IDevice device)
        {
            if (device == null)
                throw new ArgumentNullException(nameof(device));

            if (HasDevice())
                throw new InvalidOperationException("Cannot connect 2+ devices.");

            _device = device;
        }

        public void DisconnectDevice()
        {
            if (!HasDevice())
                throw new InvalidOperationException("Device not connected.");

            _device = null;
        }

        public bool HasDevice()
        {
            return _device != null;
        }

        private void Update()
        {
            float result = _mode.Calculate(_device);

            ResultChanged?.Invoke(result);
        }
    }
}
