// cSpell:ignore Begaviour

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserChar : MonoBehaviour
{

    public SpriteRenderer sprite;

    public List<Buff> buffsList = new List<Buff>();

    public int BuffsAmount = 0;

    public string Name;
    public string FullName;
    public bool SideLeft;
    public int GamePosition;

    public bool isActive = true;
    public int Health;
    public int HealForce;
    protected int PhysicalDamage;
    protected int Speed;
    protected float Potency;
    protected float Tenacity;
    protected float CriticalDamage;
    protected float CriticalChance;
    protected float HealthSteal;
    protected float Armor;
    protected float EvadeChance;

    public int health;
    public int physicalDamage;
    public int speed;
    public int healForce;
    public float potency;
    public float tenacity;
    public float criticalDamage;
    public float criticalChance;
    public float healthSteal;
    public float armor;
    public float evadeChance;
    public float Turnmeter = 0.0F;
    protected SpriteRenderer icon;

    public Sprite SpriteIcon;
    protected Sprite SpriteCurrent;
    protected Sprite SpriteTarget;
    protected Sprite SpriteTaunt;
    protected Sprite SpriteTauntTarget;
    protected Sprite SpriteTauntCurrent;

    public Sprite Skill_1_Sprite;
    public Sprite Skill_2_Sprite;
    public Sprite Skill_3_Sprite;
    public Sprite Special_Sprite;

    public Buff healthBuffObject;
    public Buff physicalDamageBuffObject;
    public Buff speedBuffObject;
    public Buff potencyBuffObject;
    public Buff tenacityBuffObject;
    public Buff criticalDamageBuffObject;
    public Buff criticalChanceBuffObject;
    public Buff healthStealBuffObject;
    public Buff armorBuffObject;
    public Buff evadeChanceBuffObject;

    public Buff healthDebuffObject;
    public Buff physicalDamageDebuffObject;
    public Buff speedDebuffObject;
    public Buff potencyDebuffObject;
    public Buff tenacityDebuffObject;
    public Buff criticalDamageDebuffObject;
    public Buff criticalChanceDebuffObject;
    public Buff healthStealDebuffObject;
    public Buff armorDebuffObject;
    public Buff evadeChanceDebuffObject;
    public Buff noHealDebuffObject;
    public Buff abilityBlockDebuffObject;

    //Debuffs && Amount
    public int healthDebuff;
    public int physicalDamageDebuff;
    public int speedDebuff;
    public int potencyDebuff;
    public int tenacityDebuff;
    public int criticalDamageDebuff;
    public int criticalChanceDebuff;
    public int healthStealDebuff;
    public int armorDebuff;
    public int evadeChanceDebuff;
    public int noHealDebuff;
    public int abilityBlockDebuff;

    public float healthDebuffAmount = 0;
    public float physicalDamageDebuffAmount = 0;
    public float speedDebuffAmount = 0;
    public float potencyDebuffAmount = 0;
    public float tenacityDebuffAmount = 0;
    public float criticalDamageDebuffAmount = 0;
    public float criticalChanceDebuffAmount = 0;
    public float healthStealDebuffAmount = 0;
    public float armorDebuffAmount = 0;
    public float evadeChanceDebuffAmount = 0;

    //Buffs && Amount
    public int healthBuff;
    public int physicalDamageBuff;
    public int speedBuff;
    public int potencyBuff;
    public int tenacityBuff;
    public int criticalDamageBuff;
    public int criticalChanceBuff;
    public int healthStealBuff;
    public int armorBuff;
    public int evadeChanceBuff;

    public int taunt;

    public float healthBuffAmount = 0;
    public float physicalDamageBuffAmount = 0;
    public float speedBuffAmount = 0;
    public float potencyBuffAmount = 0;
    public float tenacityBuffAmount = 0;
    public float criticalDamageBuffAmount = 0;
    public float criticalChanceBuffAmount = 0;
    public float healthStealBuffAmount = 0;
    public float armorBuffAmount = 0;
    public float evadeChanceBuffAmount = 0;

    public bool healthBuffActive;
    public bool physicalDamageBuffActive;
    public bool speedBuffActive;
    public bool potencyBuffActive;
    public bool tenacityBuffActive;
    public bool criticalDamageBuffActive;
    public bool criticalChanceBuffActive;
    public bool healthStealBuffActive;
    public bool armorBuffActive;
    public bool evadeChanceBuffActive;
    public bool tauntActive;
    public bool healthDebuffActive;
    public bool physicalDamageDebuffActive;
    public bool speedDebuffActive;
    public bool potencyDebuffActive;
    public bool tenacityDebuffActive;
    public bool criticalDamageDebuffActive;
    public bool criticalChanceDebuffActive;
    public bool healthStealDebuffActive;
    public bool armorDebuffActive;
    public bool evadeChanceDebuffActive;

    public bool noHealDebuffActive;
    public bool abilityBlockDebuffActive;

    public bool hasTaunt = false;
    public bool isThirdSkill = false;

    public int skill_2CD;
    public int skill_3CD;

    public bool HittedCriticalDamage;
    public bool Evaded;


    private void Awake()
    {
        health = Health;
        speed = Speed;
        physicalDamage = PhysicalDamage;
        potency = Potency;
        tenacity = Tenacity;
        criticalDamage = CriticalDamage;
        criticalChance = CriticalChance;
        healthSteal = HealthSteal;
        armor = Armor;
        evadeChance = EvadeChance;
    }

    protected virtual void SetPlayer(string Name)
    {
        health = Health;
        healForce = HealForce;
        speed = Speed;
        physicalDamage = PhysicalDamage;
        potency = Potency;
        tenacity = Tenacity;
        criticalDamage = CriticalDamage;
        criticalChance = CriticalChance;
        healthSteal = HealthSteal;
        armor = Armor;
        evadeChance = EvadeChance;

        SpriteIcon = Resources.Load<Sprite>("Sprites/" + Name + "/" + Name);
        SpriteCurrent = Resources.Load<Sprite>("Sprites/" + Name + "/" + Name + "Current");
        SpriteTarget = Resources.Load<Sprite>("Sprites/" + Name + "/" + Name + "Target");
        Skill_1_Sprite = Resources.Load<Sprite>("Sprites/" + Name + "/" + Name + "Skill_1");
        Skill_2_Sprite = Resources.Load<Sprite>("Sprites/" + Name + "/" + Name + "Skill_2");
        Skill_3_Sprite = Resources.Load<Sprite>("Sprites/" + Name + "/" + Name + "Skill_3");
        Special_Sprite = Resources.Load<Sprite>("Sprites/" + Name + "/" + Name + "Special");
        if (hasTaunt)
        {
            SpriteTaunt = Resources.Load<Sprite>("Sprites/" + Name + "/" + Name + "Taunt");
            SpriteTauntTarget = Resources.Load<Sprite>("Sprites/" + Name + "/" + Name + "TauntTarget");
            SpriteTauntCurrent = Resources.Load<Sprite>("Sprites/" + Name + "/" + Name + "TauntCurrent");
        }
    }

    protected virtual void UpdatePlayer(SpriteRenderer sprite, Sprite SpriteCurrent, Sprite SpriteTarget, Sprite SpriteIcon)
    {
        if (GameSession.current == this && tauntActive && hasTaunt)
        {
            sprite.sprite = SpriteTauntCurrent;
        }
        else if (GameSession.current == this)
        {
            sprite.sprite = SpriteCurrent;
        }
        else if (GameSession.target == this && tauntActive && hasTaunt)
        {
            sprite.sprite = SpriteTauntTarget;
        }
        else if (GameSession.target == this)
        {
            sprite.sprite = SpriteTarget;
        }
        else if (tauntActive && hasTaunt)
        {
            sprite.sprite = SpriteTaunt;
        }
        else
        {
            sprite.sprite = SpriteIcon;
        }
        if (!isActive)
        {
            sprite.sprite = null;
            Turnmeter = 0;
            taunt = 0;
            if (tauntActive)
            {
                tauntActive = false;
                if (SideLeft)
                    GameSession.tauntLeft -= 1;
                else
                    GameSession.tauntRight -= 1;
            }
            try
            {
                foreach (Buff buff in buffsList)
                {
                    Destroy(buff.gameObject);
                    buffsList.Remove(buff);
                }
            }
            catch
            {

            }
        }
    }

    public virtual void UpdateStats()
    {
        if (health > Health) health = Health;
        if (health <= 0)
        {
            health = 0;
            Turnmeter = 0;
            if (GameSession.lastTarget_1 == this)
            {
                GameSession.lastTarget_1 = null;
            }
            else if (GameSession.lastTarget_2 == this)
            {
                GameSession.lastTarget_2 = null;
            }
            if (isActive)
            {
                Log("Died", new Color(0, 0, 255, 255));
            }
            isActive = false;
        }

        if (potency > 100) potency = 100;
        if (potency < 0) potency = 0;

        if (tenacity > 100) tenacity = 100;
        if (tenacity < 0) tenacity = 0;

        if (armor > 100) armor = 100;
        if (armor < 0) armor = 0;

        if (evadeChance > 100) evadeChance = 100;
        if (evadeChance < 0) evadeChance = 0;

        if (criticalChance > 100) criticalChance = 100;
        if (criticalChance < 0) criticalChance = 0;

        if (tenacity > 100) tenacity = 100;
        if (tenacity < 0) tenacity = 0;

        if (physicalDamage < 0) physicalDamage = 0;
        if (healthSteal < 0) healthSteal = 0;

    }

    protected void OnClickPlayer()
    {
        if (GameSession.current && GameSession.current != this && GameSession.target != this && GameSession.current.SideLeft != SideLeft && isActive && !SideLeft && (GameSession.tauntRight <= 0 | taunt > 0))
        {
            GameSession.target = this;
        }
        else if (GameSession.current && GameSession.current != this && GameSession.target != this && GameSession.current.SideLeft != SideLeft && isActive && SideLeft && (GameSession.tauntLeft <= 0 | taunt > 0))
        {
            GameSession.target = this;
        }
    }

    public void ChangeTarget()
    {
        switch (GameSession.target.GamePosition)
        {
            case 1:
                if (GameSession.positions[1].isActive && (!GameSession.target.tauntActive | GameSession.positions[1].tauntActive))
                    GameSession.target = GameSession.positions[1];
                else if (GameSession.positions[2].isActive && (!GameSession.target.tauntActive | GameSession.positions[2].tauntActive))
                    GameSession.target = GameSession.positions[2];
                break;

            case 2:
                if (GameSession.positions[2].isActive && (!GameSession.target.tauntActive | GameSession.positions[2].tauntActive))
                    GameSession.target = GameSession.positions[2];
                else if (GameSession.positions[0].isActive && (!GameSession.target.tauntActive | GameSession.positions[0].tauntActive))
                    GameSession.target = GameSession.positions[0];
                break;

            case 3:
                if (GameSession.positions[0].isActive && (!GameSession.target.tauntActive | GameSession.positions[0].tauntActive))
                    GameSession.target = GameSession.positions[0];
                else if (GameSession.positions[1].isActive && (!GameSession.target.tauntActive | GameSession.positions[1].tauntActive))
                    GameSession.target = GameSession.positions[1];
                break;

            case 4:
                if (GameSession.positions[4].isActive && (!GameSession.target.tauntActive | GameSession.positions[4].tauntActive))
                    GameSession.target = GameSession.positions[4];
                else if (GameSession.positions[5].isActive && (!GameSession.target.tauntActive | GameSession.positions[5].tauntActive))
                    GameSession.target = GameSession.positions[5];
                break;

            case 5:
                if (GameSession.positions[5].isActive && (!GameSession.target.tauntActive | GameSession.positions[5].tauntActive))
                    GameSession.target = GameSession.positions[5];
                else if (GameSession.positions[3].isActive && (!GameSession.target.tauntActive | GameSession.positions[3].tauntActive))
                    GameSession.target = GameSession.positions[3];
                break;

            case 6:
                if (GameSession.positions[3].isActive && (!GameSession.target.tauntActive | GameSession.positions[3].tauntActive))
                    GameSession.target = GameSession.positions[3];
                else if (GameSession.positions[4].isActive && (!GameSession.target.tauntActive | GameSession.positions[4].tauntActive))
                    GameSession.target = GameSession.positions[4];
                break;

        }

    }

    public virtual void Skill_1()
    {

    }

    public virtual void Skill_2()
    {

    }

    public virtual void Skill_3()
    {

    }

    public virtual void ResetCD()
    {
        skill_2CD -= 1;
        skill_3CD -= 1;

        if (skill_3CD < 0) skill_3CD = 0;
        if (skill_2CD < 0) skill_2CD = 0;
    }

    public void UpdateBuffIcons()
    {
        try
        {
            foreach (Buff buff in buffsList)
            {
                if (buff.duration <= 0)
                {
                    Destroy(buff.gameObject);
                    GameSession.current.buffsList.Remove(buff);
                }
            }
        }
        catch
        {

        }
    }

    public void UpdateBuffIcons(UserChar player)
    {
        try
        {
            foreach (Buff buff in player.buffsList)
            {
                if (buff.duration <= 0 | player.health <= 0)
                {
                    Destroy(buff.gameObject);
                    player.buffsList.Remove(buff);
                }
            }
        }
        catch
        {

        }
    }

    public void StartCheckBuffs()
    {
    }

    public void CheckBuffs()
    {
        if (healthDebuff > 0)
        {
            health = (int)(health - healthDebuffAmount);
            Log("--" + healthDebuffAmount, new Color32(255, 0, 0, 255));
            healthDebuffActive = true;
            healthDebuff -= 1;
        }
        if (healthDebuff <= 0 && healthDebuffActive)
        {
            healthDebuffActive = false;
        }

        if (speedDebuff > 0)
        {
            speed = (int)(speed - speedDebuffAmount);
            speedDebuffActive = true;
            speedDebuff -= 1;
        }
        else if (speedDebuff <= 0 && speedDebuffActive)
        {
            speedDebuffActive = false;
            speed = Speed;
            speedDebuffAmount = 0;
        }

        if (physicalDamageDebuff > 0)
        {
            physicalDamage = (int)(physicalDamage - physicalDamageDebuffAmount);
            physicalDamageDebuffActive = true;
            physicalDamageDebuff -= 1;
        }
        else if (physicalDamageDebuff <= 0 && physicalDamageDebuffActive)
        {
            physicalDamageDebuffActive = false;
            physicalDamage = PhysicalDamage;
            physicalDamageDebuffAmount = 0;
        }

        if (noHealDebuff > 0)
        {
            noHealDebuff -= 1;
            noHealDebuffActive = true;
        }
        if (noHealDebuff <= 0 && noHealDebuffActive)
        {
            noHealDebuffActive = false;
        }

        if (abilityBlockDebuff > 0)
        {
            abilityBlockDebuff -= 1;
            abilityBlockDebuffActive = true;
        }
        if (abilityBlockDebuff <= 0 && abilityBlockDebuffActive)
        {
            abilityBlockDebuffActive = false;
        }

        if (criticalDamageBuff > 0)
        {
            criticalDamage = (int)(criticalDamage + criticalDamageBuffAmount);
            criticalDamageBuffActive = true;
            criticalDamageBuff -= 1;
        }
        else if (criticalDamageBuff <= 0 && criticalDamageBuffActive)
        {
            criticalDamageBuffActive = false;
            criticalDamage = CriticalDamage;
            criticalDamageBuffAmount = 0;
        }

        if (criticalChanceBuff > 0)
        {
            criticalChance = (int)(criticalChance + criticalChanceBuffAmount);
            criticalChanceBuffActive = true;
            criticalChanceBuff -= 1;
        }
        else if (criticalChanceBuff <= 0 && criticalChanceBuffActive)
        {
            criticalChanceBuffActive = false;
            criticalChance = CriticalChance;
            criticalChanceBuffAmount = 0;
        }

        // float stats Debuffs

        if (potencyDebuff > 0)
        {
            potency = (int)(potency - potencyDebuffAmount);
            potencyDebuffActive = true;
            potencyDebuff -= 1;
        }
        else if (potencyDebuff <= 0 && potencyDebuffActive)
        {
            potencyDebuffActive = false;
            potency = Potency;
            potencyDebuffAmount = 0;
        }

        if (tenacityDebuff > 0)
        {
            tenacity = (int)(tenacity - tenacityDebuffAmount);
            tenacityDebuffActive = true;
            tenacityDebuff -= 1;
        }
        else if (tenacityDebuff <= 0 && tenacityDebuffActive)
        {
            tenacityDebuffActive = false;
            tenacity = Tenacity;
            tenacityDebuffAmount = 0;
        }

        if (criticalDamageDebuff > 0)
        {
            criticalDamage = (int)(criticalDamage - criticalDamageDebuffAmount);
            criticalDamageDebuffActive = true;
            criticalDamageDebuff -= 1;
        }
        else if (criticalDamageDebuff <= 0 && criticalDamageDebuffActive)
        {
            criticalDamageDebuffActive = false;
            criticalDamage = CriticalDamage;
            criticalDamageDebuffAmount = 0;
        }

        if (criticalChanceDebuff > 0)
        {
            criticalChance = (int)(criticalChance - criticalChanceDebuffAmount);
            criticalChanceDebuffActive = true;
            criticalChanceDebuff -= 1;
        }
        else if (criticalChanceDebuff <= 0 && criticalChanceDebuffActive)
        {
            criticalChanceDebuffActive = false;
            criticalChance = CriticalChance;
            criticalChanceDebuffAmount = 0;
        }

        if (healthStealDebuff > 0)
        {
            healthSteal = (int)(healthSteal - healthStealDebuffAmount);
            healthStealDebuffActive = true;
            healthStealDebuff -= 1;
        }
        else if (healthStealDebuff <= 0 && healthStealDebuffActive)
        {
            healthStealDebuffActive = false;
            healthSteal = HealthSteal;
            healthStealDebuffAmount = 0;
        }

        if (armorDebuff > 0)
        {
            armor = (int)(armor - armorDebuffAmount);
            armorDebuffActive = true;
            armorDebuff -= 1;
        }
        else if (armorDebuff <= 0 && armorDebuffActive)
        {
            armorDebuffActive = false;
            armor = Armor;
            armorDebuffAmount = 0;
        }

        if (evadeChanceDebuff > 0)
        {
            evadeChance = (int)(evadeChance - evadeChanceDebuffAmount);
            evadeChanceDebuffActive = true;
            evadeChanceDebuff -= 1;
        }
        else if (evadeChanceDebuff <= 0 && evadeChanceDebuffActive)
        {
            evadeChanceDebuffActive = false;
            evadeChance = EvadeChance;
            evadeChanceDebuffAmount = 0;
        }

        // Buffs

        if (taunt <= 0 && tauntActive)
        {
            tauntActive = false;
            if (SideLeft)
                GameSession.tauntLeft -= 1;
            else
                GameSession.tauntRight -= 1;
        }
        else if (taunt > 0)
        {
            taunt -= 1;
            tauntActive = true;
        }

        if (healthBuff > 0)
        {
            health = (int)(health + healthBuffAmount);
            Log("++" + healthBuffAmount, new Color32(0, 255, 0, 255));
            healthBuffActive = true;
            healthBuff -= 1;
        }
        if (healthBuff <= 0 && healthBuffActive)
        {
            healthBuffActive = false;
        }

        if (speedBuff > 0)
        {
            speed = (int)(speed + speedBuffAmount);
            speedBuffActive = true;
            speedBuff -= 1;
        }
        else if (physicalDamageBuff <= 0 && speedBuffActive)
        {
            speedBuffActive = false;
            speed = Speed;
            speedBuffAmount = 0;
        }

        if (physicalDamageBuff > 0)
        {
            physicalDamage = (int)(physicalDamage + physicalDamageBuffAmount);
            physicalDamageBuffActive = true;
            physicalDamageBuff -= 1;
        }
        else if (physicalDamageBuff <= 0 && physicalDamageBuffActive)
        {
            physicalDamageBuffActive = false;
            physicalDamage = PhysicalDamage;
            physicalDamageBuffAmount = 0;
        }

        // float buffs

        if (potencyBuff > 0)
        {
            potency = (int)(potency + potencyBuffAmount);
            potencyBuffActive = true;
            potencyBuff -= 1;
        }
        else if (potencyBuff <= 0 && potencyBuffActive)
        {
            potencyBuffActive = false;
            potency = Potency;
            potencyBuffAmount = 0;
        }

        if (tenacityBuff > 0)
        {
            tenacity = (int)(tenacity + tenacityBuffAmount);
            tenacityBuffActive = true;
            tenacityBuff -= 1;
        }
        else if (tenacityBuff <= 0 && tenacityBuffActive)
        {
            tenacityBuffActive = false;
            tenacity = Tenacity;
            tenacityBuffAmount = 0;
        }


        if (healthStealBuff > 0)
        {
            healthSteal = (int)(healthSteal + healthStealBuffAmount);
            healthStealBuffActive = true;
            healthStealBuff -= 1;
        }
        else if (healthStealBuff <= 0 && healthStealBuffActive)
        {
            healthStealBuffActive = false;
            healthSteal = HealthSteal;
            healthStealBuffAmount = 0;
        }

        if (armorBuff > 0)
        {
            armor = (int)(armor + armorBuffAmount);
            armorBuffActive = true;
            armorBuff -= 1;
        }
        else if (armorBuff <= 0 && armorBuffActive)
        {
            armorBuffActive = false;
            armor = Armor;
            armorBuffAmount = 0;
        }

        if (evadeChanceBuff > 0)
        {
            evadeChance = (int)(evadeChance + evadeChanceBuffAmount);
            evadeChanceBuffActive = true;
            evadeChanceBuff -= 1;
        }
        else if (evadeChanceBuff <= 0 && evadeChanceBuffActive)
        {
            evadeChanceBuffActive = false;
            evadeChance = EvadeChance;
            evadeChanceBuffAmount = 0;
        }

        if (health <= 0)
        {
            healthDebuff = 0;
            physicalDamageDebuff = 0;
            speedDebuff = 0;
            potencyDebuff = 0;
            tenacityDebuff = 0;
            criticalDamageDebuff = 0;
            criticalChanceDebuff = 0;
            healthStealDebuff = 0;
            armorDebuff = 0;
            evadeChanceDebuff = 0;
            noHealDebuff = 0;
            abilityBlockDebuff = 0;
            healthBuff = 0;
            physicalDamageBuff = 0;
            speedBuff = 0;
            potencyBuff = 0;
            tenacityBuff = 0;
            criticalDamageBuff = 0;
            criticalChanceBuff = 0;
            healthStealBuff = 0;
            armorBuff = 0;
            evadeChanceBuff = 0;

            healthDebuffActive = false;
            physicalDamageDebuffActive = false;
            speedDebuffActive = false;
            potencyDebuffActive = false;
            tenacityDebuffActive = false;
            criticalDamageDebuffActive = false;
            criticalChanceDebuffActive = false;
            healthStealDebuffActive = false;
            armorDebuffActive = false;
            evadeChanceDebuffActive = false;
            noHealDebuffActive = false;
            abilityBlockDebuffActive = false;
            healthBuffActive = false;
            physicalDamageBuffActive = false;
            speedBuffActive = false;
            potencyBuffActive = false;
            tenacityBuffActive = false;
            criticalDamageBuffActive = false;
            criticalChanceBuffActive = false;
            healthStealBuffActive = false;
            armorBuffActive = false;
            evadeChanceBuffActive = false;
        }

        CheckBuffObjects();
    }

    void CheckBuffObjects()
    {
        if (healthBuffObject)
        {
            healthBuffObject.duration = healthBuff;

            if (healthBuff <= 0 && !healthBuffActive)
            {
                Destroy(healthBuffObject.gameObject);
                buffsList.Remove(healthBuffObject);
                healthBuffAmount = 0;
            }
        }
        if (physicalDamageBuffObject)
        {
            physicalDamageBuffObject.duration = physicalDamageBuff;
            if (physicalDamageBuff <= 0 && physicalDamageBuffAmount == 0)
            {
                Destroy(physicalDamageBuffObject.gameObject);
                buffsList.Remove(physicalDamageBuffObject);
            }
        }
        if (speedBuffObject)
        {
            speedBuffObject.duration = speedBuff;
            if (speedBuff <= 0 && speedBuffAmount == 0)
            {
                Destroy(speedBuffObject.gameObject);
                buffsList.Remove(speedBuffObject);
            }
        }
        if (potencyBuffObject)
        {
            potencyBuffObject.duration = potencyBuff;
            if (potencyBuff <= 0 && potencyBuffAmount == 0)
            {
                Destroy(potencyBuffObject.gameObject);
                buffsList.Remove(potencyBuffObject);
            }
        }
        if (tenacityBuffObject)
        {
            tenacityBuffObject.duration = tenacityBuff;
            if (tenacityBuff <= 0 && tenacityBuffAmount == 0)
            {
                Destroy(tenacityBuffObject.gameObject);
                buffsList.Remove(tenacityBuffObject);
            }
        }
        if (criticalDamageBuffObject)
        {
            criticalDamageBuffObject.duration = criticalDamageBuff;
            if (criticalDamageBuff <= 0 && criticalDamageBuffAmount == 0)
            {
                Destroy(criticalDamageBuffObject.gameObject);
                buffsList.Remove(criticalDamageBuffObject);
            }
        }
        if (criticalChanceBuffObject)
        {
            criticalChanceBuffObject.duration = criticalChanceBuff;
            if (criticalChanceBuff <= 0 && !criticalChanceBuffActive)
            {
                Destroy(criticalChanceBuffObject.gameObject);
                buffsList.Remove(criticalChanceBuffObject);
            }
        }
        if (healthStealBuffObject)
        {
            healthStealBuffObject.duration = healthStealBuff;
            if (healthStealBuff <= 0 && healthStealBuffAmount == 0)
            {
                Destroy(healthStealBuffObject.gameObject);
                buffsList.Remove(healthStealBuffObject);
            }
        }
        if (armorBuffObject)
        {
            armorBuffObject.duration = armorBuff;
            if (armorBuff <= 0 && armorBuffAmount == 0)
            {
                Destroy(armorBuffObject.gameObject);
                buffsList.Remove(armorBuffObject);
            }
        }
        if (evadeChanceBuffObject)
        {
            evadeChanceBuffObject.duration = evadeChanceBuff;
            if (evadeChanceBuff <= 0 && evadeChanceBuffAmount == 0)
            {
                Destroy(evadeChanceBuffObject.gameObject);
                buffsList.Remove(evadeChanceBuffObject);
            }
        }

        //Debuffs
        if (healthDebuffObject)
        {
            healthDebuffObject.duration = healthDebuff;

            if (healthDebuff <= 0 && !healthDebuffActive)
            {
                Destroy(healthDebuffObject.gameObject);
                buffsList.Remove(healthDebuffObject);
                healthDebuffAmount = 0;
            }
        }
        if (physicalDamageDebuffObject)
        {
            physicalDamageDebuffObject.duration = physicalDamageDebuff;
            if (physicalDamageDebuff <= 0 && physicalDamageDebuffAmount == 0)
            {
                Destroy(physicalDamageDebuffObject.gameObject);
                buffsList.Remove(physicalDamageDebuffObject);
            }
        }
        if (speedDebuffObject)
        {
            speedDebuffObject.duration = speedDebuff;
            if (speedDebuff <= 0 && speedDebuffAmount == 0)
            {
                Destroy(speedDebuffObject.gameObject);
                buffsList.Remove(speedDebuffObject);
            }
        }
        if (potencyDebuffObject)
        {
            potencyDebuffObject.duration = potencyDebuff;
            if (potencyDebuff <= 0 && potencyDebuffAmount == 0)
            {
                Destroy(potencyDebuffObject.gameObject);
                buffsList.Remove(potencyDebuffObject);
            }
        }
        if (tenacityDebuffObject)
        {
            tenacityDebuffObject.duration = tenacityDebuff;
            if (tenacityDebuff <= 0 && tenacityDebuffAmount == 0)
            {
                Destroy(tenacityDebuffObject.gameObject);
                buffsList.Remove(tenacityDebuffObject);
            }
        }
        if (criticalDamageDebuffObject)
        {
            criticalDamageDebuffObject.duration = criticalDamageDebuff;
            if (criticalDamageDebuff <= 0 && criticalDamageDebuffAmount == 0)
            {
                Destroy(criticalDamageDebuffObject.gameObject);
                buffsList.Remove(criticalDamageDebuffObject);
            }
        }
        if (criticalChanceDebuffObject)
        {
            criticalChanceDebuffObject.duration = criticalChanceDebuff;
            if (criticalChanceDebuff <= 0 && criticalChanceDebuffAmount == 0)
            {
                Destroy(criticalChanceDebuffObject.gameObject);
                buffsList.Remove(criticalChanceDebuffObject);
            }
        }
        if (healthStealDebuffObject)
        {
            healthStealDebuffObject.duration = healthStealDebuff;
            if (healthStealDebuff <= 0 && healthStealDebuffAmount == 0)
            {
                Destroy(healthStealDebuffObject.gameObject);
                buffsList.Remove(healthStealDebuffObject);
            }
        }
        if (armorDebuffObject)
        {
            armorDebuffObject.duration = armorDebuff;
            if (armorDebuff <= 0 && armorDebuffAmount == 0)
            {
                Destroy(armorDebuffObject.gameObject);
                buffsList.Remove(armorDebuffObject);
            }
        }
        if (evadeChanceDebuffObject)
        {
            evadeChanceDebuffObject.duration = evadeChanceDebuff;
            if (evadeChanceDebuff <= 0 && evadeChanceDebuffAmount == 0)
            {
                Destroy(evadeChanceDebuffObject.gameObject);
                buffsList.Remove(evadeChanceDebuffObject);
            }
        }
        if (noHealDebuffObject)
        {
            noHealDebuffObject.duration = noHealDebuff;
            if (noHealDebuff <= 0)
            {
                Destroy(noHealDebuffObject.gameObject);
                buffsList.Remove(noHealDebuffObject);
            }
        }
        if (abilityBlockDebuffObject)
        {
            abilityBlockDebuffObject.duration = abilityBlockDebuff;
            if (abilityBlockDebuff <= 0)
            {
                Destroy(abilityBlockDebuffObject.gameObject);
                buffsList.Remove(abilityBlockDebuffObject);
            }
        }

    }

    public virtual void ResetTurnBools()
    {
        HittedCriticalDamage = false;
        Evaded = false;
    }

    public virtual void Log(string text, Color32 color)
    {
        GameSession.gameController.Log(Name + "(" + (SideLeft ? 1 : 2) + ")" + ": " + text, color);
    }
    public virtual void Log(string text, Color32 color, UserChar enemy)
    {
        GameSession.gameController.Log(enemy.Name + "(" + (enemy.SideLeft ? 1 : 2) + ")" + ": " + text, color);
    }

    public void NewBuff(string buffType)
    {
        Vector3 position;
        Transform transformBuff;
        if (GameSession.positions[0] == this)
        {
            position = GameSession.gameController.Buffs_1.transform.position;
            transformBuff = GameSession.gameController.Buffs_1.transform;
        }
        else if (GameSession.positions[1] == this)
        {
            position = GameSession.gameController.Buffs_2.transform.position;
            transformBuff = GameSession.gameController.Buffs_2.transform;
        }
        else if (GameSession.positions[2] == this)
        {
            position = GameSession.gameController.Buffs_3.transform.position;
            transformBuff = GameSession.gameController.Buffs_3.transform;
        }
        else if (GameSession.positions[3] == this)
        {
            position = GameSession.gameController.Buffs_4.transform.position;
            transformBuff = GameSession.gameController.Buffs_4.transform;
        }
        else if (GameSession.positions[4] == this)
        {
            position = GameSession.gameController.Buffs_5.transform.position;
            transformBuff = GameSession.gameController.Buffs_5.transform;
        }
        else
        {
            position = GameSession.gameController.Buffs_6.transform.position;
            transformBuff = GameSession.gameController.Buffs_6.transform;
        }

        position.x = position.x - 0.23F * (SideLeft ? 1 : -1);

        foreach (Buff buff in buffsList)
        {
            buff.transform.position = new Vector3(buff.transform.position.x - 0.47F * (SideLeft ? 1 : -1), buff.transform.position.y, buff.transform.position.z);
        }
        Buff newBuff = Instantiate(GameSession.gameController.buff, position, new Quaternion(0, 0, 0, 1), transformBuff) as Buff;

        switch (buffType)
        {
            case "healthBuff":
                healthBuffObject = newBuff;
                newBuff.duration = healthBuff;
                newBuff.value = healthBuffAmount;
                newBuff.type = buffType;
                newBuff.gameObject.GetComponentInChildren<SpriteRenderer>().sprite = GameSession.gameController.healthBuffSprite;
                break;

            case "physicalDamageBuff":
                physicalDamageBuffObject = newBuff;
                newBuff.duration = physicalDamageBuff;
                newBuff.value = physicalDamageBuffAmount;
                newBuff.type = buffType;
                newBuff.GetComponentInChildren<SpriteRenderer>().sprite = GameSession.gameController.physicalDamageBuffSprite;
                break;

            case "speedBuff":
                speedBuffObject = newBuff;
                newBuff.duration = speedBuff;
                newBuff.value = speedBuffAmount;
                newBuff.type = buffType;
                newBuff.GetComponentInChildren<SpriteRenderer>().sprite = GameSession.gameController.speedBuffSprite;
                break;

            case "potencyBuff":
                potencyBuffObject = newBuff;
                newBuff.duration = potencyBuff;
                newBuff.value = potencyBuffAmount;
                newBuff.type = buffType;
                newBuff.GetComponentInChildren<SpriteRenderer>().sprite = GameSession.gameController.potencyBuffSprite;
                break;

            case "tenacityBuff":
                tenacityBuffObject = newBuff;
                newBuff.duration = tenacityBuff;
                newBuff.value = tenacityBuffAmount;
                newBuff.type = buffType;
                newBuff.GetComponentInChildren<SpriteRenderer>().sprite = GameSession.gameController.tenacityBuffSprite;
                break;

            case "criticalDamageBuff":
                criticalDamageBuffObject = newBuff;
                newBuff.duration = criticalDamageBuff;
                newBuff.value = criticalDamageBuffAmount;
                newBuff.type = buffType;
                newBuff.GetComponentInChildren<SpriteRenderer>().sprite = GameSession.gameController.criticalDamageBuffSprite;
                break;

            case "criticalChanceBuff":
                criticalChanceBuffObject = newBuff;
                newBuff.duration = criticalChanceBuff;
                newBuff.value = criticalChanceBuffAmount;
                newBuff.type = buffType;
                newBuff.GetComponentInChildren<SpriteRenderer>().sprite = GameSession.gameController.criticalChanceBuffSprite;
                break;

            case "healthStealBuff":
                healthStealBuffObject = newBuff;
                newBuff.duration = healthStealBuff;
                newBuff.value = healthStealBuffAmount;
                newBuff.type = buffType;
                newBuff.GetComponentInChildren<SpriteRenderer>().sprite = GameSession.gameController.healthStealBuffSprite;
                break;

            case "armorBuff":
                armorBuffObject = newBuff;
                newBuff.duration = armorBuff;
                newBuff.value = armorBuffAmount;
                newBuff.type = buffType;
                newBuff.GetComponentInChildren<SpriteRenderer>().sprite = GameSession.gameController.armorBuffSprite;
                break;

            case "evadeChanceBuff":
                evadeChanceBuffObject = newBuff;
                newBuff.duration = evadeChanceBuff;
                newBuff.value = evadeChanceBuffAmount;
                newBuff.type = buffType;
                newBuff.GetComponentInChildren<SpriteRenderer>().sprite = GameSession.gameController.evadeChanceBuffSprite;
                break;

            case "healthDebuff":
                healthDebuffObject = newBuff;
                newBuff.duration = healthDebuff;
                newBuff.value = healthDebuffAmount;
                newBuff.type = buffType;
                newBuff.gameObject.GetComponentInChildren<SpriteRenderer>().sprite = GameSession.gameController.healthDebuffSprite;
                break;

            case "physicalDamageDebuff":
                physicalDamageDebuffObject = newBuff;
                newBuff.duration = physicalDamageDebuff;
                newBuff.value = physicalDamageDebuffAmount;
                newBuff.type = buffType;
                newBuff.GetComponentInChildren<SpriteRenderer>().sprite = GameSession.gameController.physicalDamageDebuffSprite;
                break;

            case "speedDebuff":
                speedDebuffObject = newBuff;
                newBuff.duration = speedDebuff;
                newBuff.value = speedDebuffAmount;
                newBuff.type = buffType;
                newBuff.GetComponentInChildren<SpriteRenderer>().sprite = GameSession.gameController.speedDebuffSprite;
                break;

            case "potencyDebuff":
                potencyDebuffObject = newBuff;
                newBuff.duration = potencyDebuff;
                newBuff.value = potencyDebuffAmount;
                newBuff.type = buffType;
                newBuff.GetComponentInChildren<SpriteRenderer>().sprite = GameSession.gameController.potencyDebuffSprite;
                break;

            case "tenacityDebuff":
                tenacityDebuffObject = newBuff;
                newBuff.duration = tenacityDebuff;
                newBuff.value = tenacityDebuffAmount;
                newBuff.type = buffType;
                newBuff.GetComponentInChildren<SpriteRenderer>().sprite = GameSession.gameController.tenacityDebuffSprite;
                break;

            case "criticalDamageDebuff":
                criticalDamageDebuffObject = newBuff;
                newBuff.duration = criticalDamageDebuff;
                newBuff.value = criticalDamageDebuffAmount;
                newBuff.type = buffType;
                newBuff.GetComponentInChildren<SpriteRenderer>().sprite = GameSession.gameController.criticalDamageDebuffSprite;
                break;

            case "criticalChanceDebuff":
                criticalChanceDebuffObject = newBuff;
                newBuff.duration = criticalChanceDebuff;
                newBuff.value = criticalChanceDebuffAmount;
                newBuff.type = buffType;
                newBuff.GetComponentInChildren<SpriteRenderer>().sprite = GameSession.gameController.criticalChanceDebuffSprite;
                break;

            case "healthStealDebuff":
                healthStealDebuffObject = newBuff;
                newBuff.duration = healthStealDebuff;
                newBuff.value = healthStealDebuffAmount;
                newBuff.type = buffType;
                newBuff.GetComponentInChildren<SpriteRenderer>().sprite = GameSession.gameController.healthStealDebuffSprite;
                break;

            case "armorDebuff":
                armorDebuffObject = newBuff;
                newBuff.duration = armorDebuff;
                newBuff.value = armorDebuffAmount;
                newBuff.type = buffType;
                newBuff.GetComponentInChildren<SpriteRenderer>().sprite = GameSession.gameController.armorDebuffSprite;
                break;

            case "evadeChanceDebuff":
                evadeChanceDebuffObject = newBuff;
                newBuff.duration = evadeChanceDebuff;
                newBuff.value = evadeChanceDebuffAmount;
                newBuff.type = buffType;
                newBuff.GetComponentInChildren<SpriteRenderer>().sprite = GameSession.gameController.evadeChanceDebuffSprite;
                break;

            case "noHealDebuff":
                noHealDebuffObject = newBuff;
                newBuff.duration = noHealDebuff;
                newBuff.value = 0;
                newBuff.type = buffType;
                newBuff.GetComponentInChildren<SpriteRenderer>().sprite = GameSession.gameController.noHealDebuffSprite;
                break;

            case "abilityBlockDebuff":
                abilityBlockDebuffObject = newBuff;
                newBuff.duration = abilityBlockDebuff;
                newBuff.value = 0;
                newBuff.type = buffType;
                newBuff.GetComponentInChildren<SpriteRenderer>().sprite = GameSession.gameController.abilityBlockDebuffSprite;
                break;
        }

        buffsList.Add(newBuff);
        BuffsAmount++;
    }

    public virtual void GetDamage(UserChar enemy)
    {
        if (Random.Range(1.0F, 100.0F) > evadeChance)
        {
            int damage = (int)(0.9F * enemy.physicalDamage + (0.002F * enemy.physicalDamage * Random.Range(0.0F, 100.0F)));
            if (Random.Range(1.0F, 100.0F) < enemy.criticalChance)
            {
                damage = (int)(damage * enemy.criticalDamage / 100);
                enemy.HittedCriticalDamage = true;
            }
            damage = (int)(damage * (100 - armor) / 100);
            health -= damage;
            if (enemy.HittedCriticalDamage)
                Log("-" + damage, new Color32(255, 255, 0, 255));
            else
                Log("-" + damage, new Color32(255, 0, 0, 255));

            int healthStealed = (int)(damage * enemy.healthSteal / 100);

            enemy.health += healthStealed;
            if (healthStealed > 0)
                Log("+" + healthStealed, new Color32(0, 255, 0, 255), enemy);
        }
        else
        {
            Log("Evaded", new Color32(255, 255, 255, 255));
        }
    }

    public virtual void Heal(UserChar friend)
    {
        int heal = (int)(0.9F * healForce + (0.002F * healForce * Random.Range(0.0F, 100.0F)));
        friend.health += heal;
        Log("+" + heal, new Color32(0, 255, 0, 255), friend);
    }
}
