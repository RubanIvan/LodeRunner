using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Const  {

    /// <summary>Размер уровня в блоках в длинну</summary>
    public const int LevelDx = 35;
    /// <summary>Размер уровня в блоках в ширену</summary>
    public const int LevelDy = 28;

    public static Vector3 BlockPivotPoint=new Vector3(-560,450+32);

    //Скорость падения
    public const float Gravity = 70f;

    public const float PlayerSpeed = 90f;

}
