using UnityEngine;

namespace Project
{
    public class DCVoltageMode : MultimeterMode
    {
        protected override float OnCalculate(IDevice device)
        {
            return Mathf.Sqrt(device.Power * device.Resistance);
        }
    }
}
