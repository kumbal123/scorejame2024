using Godot;
using System;
using System.ComponentModel;

[GlobalClass]
public partial class UpgradeDataGarlic : UpgradeData
{
    public override string Title => "Garlic clone";
    public override Texture2D GetIcon => GD.Load<Texture2D>("res://asssets/icon.svg");

    public float TickInterval => 2.0f / Speed;

    public UpgradeDataGarlic()
    {    
        Attack = 10;
        Size = 1.0f;
        Speed = 1.0f;
    }

    protected override UpgradeNode GetUpgradeNode =>
        GD.Load<PackedScene>("res://objects/upgradeInstance/UpgradeGarlic.tscn").Instantiate<UpgradeNode>();

    protected override void LevelUpIncreaseParameters()
    {
        Size += 0.1f;
        Speed += 0.1f;
    }

    public override string LevelUpInfo()
    {
        if (Level == 0)
            return "Damages enemies in an area around you every few seconds.";
        else
            return "gets bigger and faster ig, idk";
    }

}
