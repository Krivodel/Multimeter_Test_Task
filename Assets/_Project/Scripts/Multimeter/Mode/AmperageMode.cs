using UnityEngine;

namespace Project
{
    public class AmperageMode : MultimeterMode
    {
        private readonly ResistanceMode _resistanceMode = new();

        public override float Calculate(IDevice device)
        {
            return Mathf.Sqrt(device.Power / _resistanceMode.Calculate(device));
        }
    }
}
