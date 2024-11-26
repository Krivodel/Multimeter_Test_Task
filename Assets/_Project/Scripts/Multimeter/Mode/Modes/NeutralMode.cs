namespace Project
{
    public class NeutralMode : MultimeterMode
    {
        protected override float OnCalculate(IDevice device)
        {
            return 0f;
        }
    }
}
