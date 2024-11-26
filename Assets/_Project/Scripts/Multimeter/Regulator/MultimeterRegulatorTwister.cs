using UnityEngine;

namespace Project
{
    public class MultimeterRegulatorTwister : MonoBehaviour
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private Vector3 _axis = new(0f, 1f, 0f);

        private MultimeterModeMap<float> _multimeterModeAngleMap;
        private Multimeter _multimeter;

        public void Construct(Multimeter multimeter, MultimeterModeMap<float> multimeterModeAngleMap)
        {
            _multimeter = multimeter;
            _multimeterModeAngleMap = multimeterModeAngleMap;

            _multimeter.CurrentModeChanged += OnCurrentModeChanged;
        }

        private void OnDestroy()
        {
            _multimeter.CurrentModeChanged -= OnCurrentModeChanged;
        }

        private void OnCurrentModeChanged(MultimeterMode mode)
        {
            Vector3 eulerAngles = _axis.normalized * GetAngle(mode);

            _transform.localEulerAngles = eulerAngles;
        }

        private float GetAngle(MultimeterMode mode)
        {
            return _multimeterModeAngleMap.Get(mode);
        }
    }
}
