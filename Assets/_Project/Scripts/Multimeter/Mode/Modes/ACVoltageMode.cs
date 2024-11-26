namespace Project
{
    public class ACVoltageMode : MultimeterMode
    {
        protected override float OnCalculate(IDevice device)
        {
            return 0.01f;
        }
    }
}
