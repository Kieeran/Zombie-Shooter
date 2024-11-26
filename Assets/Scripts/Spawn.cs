using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] private Transform TopLeft;
    [SerializeField] private Transform RightBottom;
    [SerializeField] private int spawnMeleeZombieAmount;
    [SerializeField] private int spawnRangeZombieAmount;

    [SerializeField] private Transform wayPoints;

    public Transform GetTopLeftTransform() { return TopLeft; }
    public Transform GetRightBottomTransform() { return RightBottom; }
    public int GetSpawnMeleeZombieAmount() { return spawnMeleeZombieAmount; }
    public int GetSpawnRangeZombieAmount() { return spawnRangeZombieAmount; }
    public Transform GetWayPoints() { return wayPoints; }
}