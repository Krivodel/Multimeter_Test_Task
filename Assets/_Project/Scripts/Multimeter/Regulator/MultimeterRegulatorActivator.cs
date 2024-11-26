using UnityEngine;

namespace Project
{
    public class MultimeterRegulatorActivator : MonoBehaviour
    {
        [SerializeField] private MultimeterRegulator _multimeterRegulator;

        public void Construct(MultimeterRegulator multimeterRegulator)
        {
            _multimeterRegulator = multimeterRegulator;
        }

        private void OnMouseEnter()
        {
            _multimeterRegulator.Activate();
        }

        private void OnMouseExit()
        {
            _multimeterRegulator.Deactivate();
        }
    }
}
