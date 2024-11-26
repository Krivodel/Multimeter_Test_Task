using UnityEngine;

namespace Project
{
    public class ElectricalDevice : MonoBehaviour, IDevice
    {
        public float Resistance => _resistance;
        public float Power => _power;

        [SerializeField] private float _resistance = 1000f;
        [SerializeField] private float _power = 400f;
    }
}
