namespace Project
{
    public class ResistanceMode : MultimeterMode
    {
        private readonly DCVoltageMode _dcVoltageMode = new();

        protected override float OnCalculate(IDevice device)
        {
            var dcVoltage = _dcVoltageMode.Calculate(device);

            return dcVoltage * dcVoltage / device.Power;
        }
    }
}
