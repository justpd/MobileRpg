using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Luke : UserChar
{
    private void Awake()
    {
        Name = "Luke";
        FullName = "Rebel Luke";
        isThirdSkill = false;
        Health = 28000;
        Speed = 170;
        PhysicalDamage = 3000;
        Potency = 22.0F;
        Tenacity = 14.0F;
        CriticalDamage = 150.0F;
        CriticalChance = 25.0F;
        HealthSteal = 30.0F;
        Armor = 5.0F;
        EvadeChance = 10.0F;
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
        foreach (UserChar player in GameSession.positions)
        {
            if (player.SideLeft != SideLeft | player == this)
            {
                float random = Random.Range(1.0F, 100.0F);
                if (random > player.tenacity - GameSession.current.potency && player.health > 0)
                {
                    if (!player.healthDebuffObject)
                    {
                        player.healthDebuff = 2;
                        player.healthDebuffAmount = 1200;
                        player.healthDebuffActive = true;
                        player.NewBuff("healthDebuff");
                        Log("HealthDebuff", new Color32(255, 0, 0, 255), player);
                    }
                    else
                    {
                        player.healthDebuff += 2;
                        player.healthDebuffAmount += 1200;
                        Log("HealthDebuff", new Color32(255, 0, 0, 255), player);
                    }
                }
                else if (player.health > 0)
                    Log("Resisted", new Color32(255, 255, 255, 255), player);
            }
        }

        skill_2CD = 4;
    }
}
