using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Const
{
    /// <summary>Количество уровней в игре</summary>
    public const int MaxLevels = 75;

    /// <summary>Размер уровня в блоках в длинну</summary>
    public const int LevelDx = 35;
    /// <summary>Размер уровня в блоках в ширену</summary>
    public const int LevelDy = 28;

    public static Vector3 BlockPivotPoint=new Vector3(-560,450+32);

    
    /// <summary>Скорость падения</summary>
    public const float Gravity = 90f;

    /// <summary>Скорость бега</summary>
    public const float PlayerRunSpeed = 150f;

    /// <summary>Скорость бега</summary>
    public const float PlayerRopeSpeed = 100f;


    /// <summary>Маска для непроходимых обьектов</summary>
    public const int WallMask=1<<8;
    public const int StairsMask = 1 << 9;
    public const int RopeMask = 1 << 10;



}
