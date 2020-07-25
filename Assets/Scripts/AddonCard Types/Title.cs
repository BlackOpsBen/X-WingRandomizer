using UnityEngine;

[CreateAssetMenu]
public class Title : AddonCard
{
    [Header("Removes slot")]
    [SerializeField] public bool losesCannon;
    [SerializeField] public bool losesMissile;
    [SerializeField] public bool losesCrew;
    [SerializeField] public bool losesAllOrdnance;

    [Header("Misc")]
    [SerializeField] public bool noBrainer;
}
