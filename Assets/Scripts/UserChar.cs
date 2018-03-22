using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserChar
{
    public string name;
    public string id;
    public int lvl;
    public int power;

    public int Health;
    public int Agility;
    public int Strength;
    public int Intelligence;
    public float Potency;
    public float Tenacity;
    public float CriticalChance;
    public float HealthSteal;
    public float Armor;
    public float ArmorPenetration;
    public float EvadeChance;
    public float Speed;

    public float HealForce;
    public float CriticalDamage;
    public float PhysicalDamage;
    public float MagicalGamage;

    public bool isActive = true;

    protected int health;
    protected int agility;
    protected int strength;
    protected int intelligence;
    protected float potency;
    protected float tenacity;
    protected float criticalChance;
    protected float healthSteal;
    protected float armor;
    protected float armorPenetration;
    protected float evadeChance;
    protected float speed;

    protected int healForce;
    protected float criticalDamage;
    protected int physicalDamage;
    protected int magicalGamage;
    public float turnmeter = 0.0F;
}