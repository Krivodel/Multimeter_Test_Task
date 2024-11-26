using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class MultimeterRegulatorTwister : MonoBehaviour
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private Vector3 _axis = new(0f, 1f, 0f);

        private Dictionary<MultimeterMode, float> _multimeterModeAngleMap;
        private Multimeter _multimeter;

        public void Construct(Multimeter multimeter, Dictionary<MultimeterMode, float> multimeterModeAngleMap)
        {
            _multimeter = multimeter;
            _multimeterModeAngleMap = multimeterModeAngleMap;

            _multimeter.ModeChanged += OnModeChanged;
        }

        private void OnDestroy()
        {
            _multimeter.ModeChanged -= OnModeChanged;
        }

        private void OnModeChanged(MultimeterMode mode)
        {
            Vector3 eulerAngles = _axis.normalized * GetAngle(mode);

            _transform.localEulerAngles = eulerAngles;
        }

        private float GetAngle(MultimeterMode mode)
        {
            if (!_multimeterModeAngleMap.TryGetValue(mode, out var angle))
                throw new NotImplementedException($"Angle for mode '{mode}' not implemented.");

            return angle;
        }
    }
}
