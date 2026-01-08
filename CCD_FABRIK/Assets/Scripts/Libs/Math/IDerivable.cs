namespace Math
{
    public interface IDerivable<T> where T : Transformation
    {
        public T Derivative();
    }
}