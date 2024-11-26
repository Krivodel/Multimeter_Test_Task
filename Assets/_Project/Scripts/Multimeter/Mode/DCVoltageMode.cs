using UnityEngine;

namespace Project
{
    public class DCVoltageMode : MultimeterMode
    {
        public override float Calculate(IDevice device)
        {
            return Mathf.Sqrt(device.Power * device.Resistance);
        }
    }
}
