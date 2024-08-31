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

    public UpgradeDataPlayer()
    {    
        Attack = 10;
        Size = 1.0f;
        Speed = 1.0f;
        MaxHp = 50;
    }

    protected override UpgradeNode GetUpgradeNode => 
        GD.Load<PackedScene>("res://objects/upgradeInstance/UpgradePlayer.tscn").Instantiate<UpgradeNode>();

    protected override void LevelUpIncreaseParameters()
    {
        Attack += 15;
        Speed += 25f;
        MaxHp += 10;
    }

    public override string LevelUpInfo()
    {
        if (Level == 0)
            return "Increases Player Stats - Hp, Movement Speed, Base Damage";
        else
            return "Get BEEFIER, STRONGER, FASTER";
    }

}
