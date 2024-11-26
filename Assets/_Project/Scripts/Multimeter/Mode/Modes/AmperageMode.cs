using UnityEngine;

namespace Project
{
    public class AmperageMode : MultimeterMode
    {
        private readonly ResistanceMode _resistanceMode = new();

        protected override float OnCalculate(IDevice device)
        {
            return Mathf.Sqrt(device.Power / _resistanceMode.Calculate(device));
        }
    }
}
