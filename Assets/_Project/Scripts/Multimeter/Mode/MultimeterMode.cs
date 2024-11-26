namespace Project
{
    public abstract class MultimeterMode
    {
        public override bool Equals(object obj)
        {
            var mode = obj as MultimeterMode;

            return this == mode;
        }

        public override int GetHashCode()
        {
            return GetType().GetHashCode();
        }

        public abstract float Calculate(IDevice device);

        public static bool operator ==(MultimeterMode a, MultimeterMode b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.GetType() == b.GetType();
        }

        public static bool operator !=(MultimeterMode a, MultimeterMode b)
        {
            return !(a == b);
        }
    }
}
