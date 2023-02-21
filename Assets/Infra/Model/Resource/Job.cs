namespace Infra.Model.Resource
{
    public class Job
    {
        public int Index { get; }
        public string Name { get; }


        public Job(int index, string name)
        {
            Index = index;
            Name = name;
        }
    }
}