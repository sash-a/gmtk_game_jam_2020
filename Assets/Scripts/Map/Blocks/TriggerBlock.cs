using Game.Player;

public abstract class TriggerBlock : ElevatorBlock
{
    public static string TRIGGER_BLOCK = "trigger";

    public override string getTypeString()
    {
        return TRIGGER_BLOCK;
    }

    internal abstract void trigger(Game.Player.PlayerController pc);

    internal abstract void untrigger(PlayerController pc);
}
