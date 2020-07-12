using Code.Player;

public abstract class TriggerBlock : Block
{
    public static string TRIGGER_BLOCK = "trigger";

    public override string getTypeString()
    {
        return TRIGGER_BLOCK;
    }

    internal abstract void trigger(Code.Player.PlayerController pc);

    internal abstract void untrigger(PlayerController pc);
}
