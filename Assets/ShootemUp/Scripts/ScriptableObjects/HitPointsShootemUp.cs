using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HitPoints", menuName = "HitPoints")]
public class HitPointsShootemUp : ScriptableObject
{
    public int hitPoints, maxHitPoints, initialHitPoints;
}

