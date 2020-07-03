using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PilotCard : ScriptableObject
{
    public string ship;

    public Sprite cardArt;

    public Title[] title;
    public Astromech[] astromech;
    public Torpedo[] torpedo;
    public Missile[] missile;
    public AddonCard[] elitePilotTalent;
    public AddonCard[] modification;

    public bool isUnique;

    public int cost;
}
