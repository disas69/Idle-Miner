namespace Game.WorldObjects.Base.Configuration
{
    public interface IWorldObjectConfiguration<out T> where T : IWorldObjectSettings
    {
        string Id { get;}
        int ManagerCost { get; }
        T GetSettings(int level);
    }
}