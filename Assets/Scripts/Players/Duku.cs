using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : UserChar
{
    private void Awake()
    {
        Name = "Duku";
        FullName = "Count Duku";
        isThirdSkill = false;
        Health = 31000;
        Speed = 150;
        PhysicalDamage = 2000;
        Potency = 10.0F;
        Tenacity = 20.0F;
        CriticalDamage = 150.0F;
        CriticalChance = 5.0F;
        HealthSteal = 0.0F;
        Armor = 0.0F;
        EvadeChance = 25.0F;
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
        GameSession.target.tenacity -= 1;
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
    }
    public override void Skill_2()
    {
        if (Random.Range(1.0F, 100.0F) > GameSession.target.tenacity - GameSession.current.potency && GameSession.target.health > 0)
        {
            if (!GameSession.target.abilityBlockDebuffObject)
            {
                GameSession.target.tenacityDebuff = 2;
                GameSession.target.tenacityDebuffActive = true;
                GameSession.target.tenacityDebuffAmount = 10.0F;
                GameSession.target.NewBuff("tenacityDebuff");
                Log("tenacityDebuff", new Color32(255, 0, 0, 255), GameSession.target);
            }
            else
            {
                GameSession.target.tenacityDebuff += 1;
                Log("tenacityDebuff", new Color32(255, 0, 0, 255), GameSession.target);
            }
        }
        else if (GameSession.target.health > 0)
            Log("Resisted", new Color32(255, 255, 255, 255), GameSession.target);

        skill_2CD = 4;
    }
}
