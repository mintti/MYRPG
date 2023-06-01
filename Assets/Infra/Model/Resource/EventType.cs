namespace Infra.Model.Resource
{
    public enum EventType : int
    {
        None = 0,
        HealingLake,
        GetArtefact,
    }

    public enum BattleType : int
    {
        None = 0,
        Common,
        Elite,
        Boss
    }
}