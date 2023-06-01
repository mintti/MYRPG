namespace Infra.Model.Data
{
    public abstract class Artefact
    {
        public int Index { get; }

        protected Artefact(int index)
        {
            Index = index;
        }
    }
}