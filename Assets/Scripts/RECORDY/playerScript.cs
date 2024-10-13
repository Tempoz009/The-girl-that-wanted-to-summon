using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    #region playerFields
    [Header("PlayerBaseStats")]

    public static float playerHP = 5; // baseValue = 10;
    public static float movSpeed; // baseValue = 1.2;
    [Header("PlayerSpecialStats")]
    
    public static float charmCooldown;// baseValue = 10(seconds);
    public static float summonBonusDmg;
    public static int currentWave = 0;
    public static int EnemiesCount;

    public static int EnemyKilled = 0;
    #endregion
}