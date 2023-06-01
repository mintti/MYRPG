namespace Infra.Util
{
    /// <summary>
    /// Source: https://stackoverflow.com/questions/2721939/how-to-iterate-through-two-ienumerables-simultaneously
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    internal sealed class ZipEntry<T1, T2>
    {
        public ZipEntry(int index, T1 value1, T2 value2)
        {
            Index = index;
            Value1 = value1;
            Value2 = value2;
        }

        public int Index { get; private set; }
        public T1 Value1 { get; private set; }
        public T2 Value2 { get; private set; }
    }
}