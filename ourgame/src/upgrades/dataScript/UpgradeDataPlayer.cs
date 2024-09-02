using Godot;
using System;
using System.ComponentModel;

[GlobalClass]
public partial class UpgradeDataPlayer : UpgradeData
{
    public override string Title => "Player Stats";
    public override Texture2D GetIcon => GD.Load<Texture2D>("res://asssets/icon.svg");

    public int MaxHp;
    private PlayerCharacter Player;

    /// <summary>
    /// These number represent how much the stats increase with each upgrade.
    /// It's added on top of the previous ones!
    /// </summary>
    public UpgradeDataPlayer()
    {    
        Attack = 10;
        Size = 1.0f;
        Speed = 10.0f;
        MaxHp = 50;
        Cost = 500;
    }

    protected override UpgradeNode GetUpgradeNode => 
        GD.Load<PackedScene>("res://objects/upgradeInstance/UpgradePlayer.tscn").Instantiate<UpgradeNode>();

    /// <summary>
    /// The stat increase itself gets bigger with each level up. 
    /// </summary>
    protected override void LevelUpIncreaseParameters()
    {
        Attack += 1;
        Speed += 5.0f;
        MaxHp += 10;
        Cost += (int)Cost/2;
    }

    public override string LevelUpInfo()
    {
        if (Level == 0)
            return "Increases Player Stats - Hp, Movement Speed, Base Damage";
        else
            return "Get BEEFIER, STRONGER, FASTER";
    }

}
