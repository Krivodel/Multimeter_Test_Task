namespace Project
{
    public class ResistanceMode : MultimeterMode
    {
        private readonly DCVoltageMode _dcVoltageMode = new();

        public override float Calculate(IDevice device)
        {
            var dcVoltage = _dcVoltageMode.Calculate(device);

            return dcVoltage * dcVoltage / device.Power;
        }
    }
}
