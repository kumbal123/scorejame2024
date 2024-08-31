using Godot;
using System;

[GlobalClass]
public partial class UpgradeDataGarlic : UpgradeData
{
    public override string Title => "Garlic clone";
    public override Texture2D GetIcon => GD.Load<Texture2D>("res://asssets/icon.svg");

    protected override UpgradeNode GetUpgradeNode =>
        GD.Load<PackedScene>("res://objects/upgradeInstance/UpgradeGarlic.tscn").Instantiate<UpgradeNode>();

    protected override void LevelUpIncreaseParameters()
    {
        throw new NotImplementedException();
    }

    public override string LevelUpInfo()
    {
        if (Level == 0)
            return "Damages enemies in an area around you every few seconds.";
        else
            return "I dunno what this level up does.";
    }

}
