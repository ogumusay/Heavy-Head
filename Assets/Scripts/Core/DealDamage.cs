using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum DamageType
{
    Pierce,
    Fire,
    Crush
}

public class DealDamage : MonoBehaviour
{
    [SerializeField]
    public DamageType type;
}
