namespace Project
{
    public class ACVoltageMode : MultimeterMode
    {
        public override float Calculate(IDevice device)
        {
            return 0.01f;
        }
    }
}
