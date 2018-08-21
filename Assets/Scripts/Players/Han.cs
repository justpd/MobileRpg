using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Han : UserChar
{
    private void Awake()
    {
        Name = "Han";
        FullName = "Han Solo";
        isThirdSkill = true;
        hasTaunt = true;
        Health = 35000;
        Speed = 410;
        PhysicalDamage = 3000;
        Potency = 12.0F;
        Tenacity = 15.0F;
        CriticalDamage = 160.0F;
        CriticalChance = 40.0F;
        HealthSteal = 0.0F;
        Armor = 20.0F;
        EvadeChance = 5.0F;

        SetPlayer(Name);
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        UpdatePlayer(sprite, SpriteCurrent, SpriteTarget, SpriteIcon);
    }

    private void OnMouseDown()
    {
        OnClickPlayer();
    }

    public override void Skill_1()
    {
        GameSession.target.GetDamage(GameSession.current);
    }
    public override void Skill_2()
    {
        criticalDamageBuff = 2;
        criticalDamageBuffActive = true;
        criticalDamageBuffAmount = 40.0F;
        NewBuff("criticalDamageBuff");
        Log("criticalDamage: +" + criticalDamageBuffAmount + "%", new Color32(0, 255, 0, 255));

        criticalChanceBuff = 2;
        criticalChanceBuffActive = true;
        criticalChanceBuffAmount = 40.0F;
        NewBuff("criticalChanceBuff");
        Log("CriticalChance: +" + criticalChanceBuffAmount + "%", new Color32(0, 255, 0, 255));

        skill_2CD = 3;
    }
    public override void Skill_3()
    {
        taunt = 2;
        Log("Taunt", new Color32(255, 100, 0, 255));
        if (SideLeft)
            GameSession.tauntLeft += 1;
        else
            GameSession.tauntRight += 1;
        skill_3CD = 5;
    }
}
