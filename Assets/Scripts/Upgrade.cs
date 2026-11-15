using UnityEngine;



//stats container for when upgrading abilities

[System.Serializable] public class Stats
{
    public int extraMoves = 0;
    public float scoreMultiplyer=1f;
    public float comboBonus = 0f;
    public float shakePower = 1f;
    public float flipPower = 1f;
    public float tiltPower = 1f;
    public float windPower = 1f;    //not yet implemented
}

public enum UpgradeType { extraMove, scoreMulitplyer, comboBonus, shakePower, flipPower, tiltPower, windPower}



[CreateAssetMenu(menuName="Balliards/Upgrades")]
public class Upgrade : ScriptableObject
{
    public string title;
    [TextArea] public string description;
    public UpgradeType upgradeType;
    public float amount = 0.2f;     //upgrade increase amount- even for all upgrades

    public void applyUpgrade(Stats s)
    {
        switch (upgradeType)
        {
            case UpgradeType.extraMove: s.extraMoves += 1; break;
            case UpgradeType.scoreMulitplyer: s.scoreMultiplyer += amount; break;
            case UpgradeType.comboBonus: s.comboBonus += amount; break;
            case UpgradeType.shakePower: s.shakePower += amount; break;
            case UpgradeType.flipPower: s.flipPower += amount; break;
            case UpgradeType.tiltPower: s.tiltPower += amount;break;
            case UpgradeType.windPower: s.windPower += amount; break;
        }
    }
}
