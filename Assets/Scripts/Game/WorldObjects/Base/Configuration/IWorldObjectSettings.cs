namespace Game.WorldObjects.Base.Configuration
{
    public interface IWorldObjectSettings
    {
        int Level { get; }
        int UpgradeCost { get; }
        int Units { get; }
        int Load { get; }
        float MoveTime { get; }
        float WorkTime { get; }
    }
}