using UnityEngine;

[CreateAssetMenu]
public class Title : AddonCard
{
    [Header("Removes slot")]
    [SerializeField] private bool losesCannon;
    [SerializeField] private bool losesMissile;
    [SerializeField] private bool losesCrew;
}
