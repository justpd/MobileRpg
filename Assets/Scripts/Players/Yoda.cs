using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Yoda : UserChar
{
    private void Awake()
    {
        Name = "Yoda";
        FullName = "Master Yoda";
        isThirdSkill = true;
        Health = 20000;
        Speed = 210;
        PhysicalDamage = 2000;
        Potency = 32.0F;
        Tenacity = 34.0F;
        CriticalDamage = 150.0F;
        CriticalChance = 25.0F;
        HealthSteal = 0.0F;
        Armor = 0.0F;
        EvadeChance = 25.0F;
        HealForce = 8000;
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
        int random = Random.Range(1, 4);
        Debug.Log(random);
        int i = 0;
        foreach (UserChar player in GameSession.positions)
        {
            if (player.SideLeft == SideLeft)
            {
                i++;
                if (i == random)
                {
                    if (!player.healthBuffObject)
                    {
                        player.healthBuff = 2;
                        player.healthBuffActive = true;
                        player.healthBuffAmount = 400.0F;
                        player.NewBuff("healthBuff");
                        player.Log("HealthBuff", new Color32(0, 255, 0, 255));
                    }
                    else
                    {
                        player.healthBuff += 1;
                        player.healthBuffAmount += 400.0F;
                        player.Log("HealthBuff", new Color32(0, 255, 0, 255));
                    }
                }
            }
        }
    }
    public override void Skill_2()
    {
        physicalDamage = (int) (physicalDamage * 1.2F);
        GameSession.target.GetDamage(GameSession.current);
        physicalDamage = PhysicalDamage;
        if (Random.Range(1.0F, 100.0F) > GameSession.target.tenacity - GameSession.current.potency && GameSession.target.health > 0)
        {
            if (!GameSession.target.abilityBlockDebuffObject)
            {
                GameSession.target.abilityBlockDebuff = 1;
                GameSession.target.abilityBlockDebuffActive = true;
                GameSession.target.NewBuff("abilityBlockDebuff");
                Log("AbilityBlock", new Color32(255, 0, 0, 255), GameSession.target);
            }
            else
            {
                GameSession.target.abilityBlockDebuff += 1;
                Log("AbilityBlock", new Color32(255, 0, 0, 255), GameSession.target);
            }
        }
        else if (GameSession.target.health > 0)
            Log("Resisted", new Color32(255, 255, 255, 255), GameSession.target);
        skill_2CD = 3;
    }

}
