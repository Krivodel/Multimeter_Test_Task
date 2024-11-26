namespace Project
{
    public class NeutralMode : MultimeterMode
    {
        public override float Calculate(IDevice device)
        {
            return 0f;
        }
    }
}
