using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PilotCard : ScriptableObject
{
    [Header("Settings")]
    public Sprite cardArt;
    public bool isUnique;
    public int cost;

    [Header("Common addons")]
    public int elitePilotTalents;
    public int torpedos;
    public int missiles;
    public int bombs;
    public int modifications = 1;

    [Header("Uncommon addons")]
    public int titles;
    public int astromechs;
    public int cannons;
    public int turret;
    public int crew;
    public int systems;
    public int techs;

    [Header("Rare addons")]
    public int illicits;
    public int cargo;
    public int hardpoints;
    public int teams;
    public int salvagedAstromechs;
}