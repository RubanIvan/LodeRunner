using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/// <summary>Список всех префабов в игре</summary>
public enum PrefabEnum
{
    
    ClearSpace=0,

    Wall=10,
    FalseWall=11,

    Stone=20,
    Stairs=30,
    Chest=40,
    Rope=50,

    Player=100,
    Mummy=200,

    GameMaster=500,
}



public class GlobalManagerScript : MonoBehaviour
{
    /// <summary>Cодержит все префабы  GameScene</summary>
    public Dictionary<Enum, GameObject> GamePrefab = new Dictionary<Enum, GameObject>();

    /// <summary>карта всех уровней </summary>
    [HideInInspector]
    public Level[] LevelMap;

    /// <summary>Текущий уровень (номер уровня)</summary>
    public int LevelCurrent = 0;

    /// <summary>ссылка на GameMaster </summary>
    public GameObject GameMaster;

    /// <summary>Ссылка на pivot </summary>
    public GameObject PivotGameObject;

    #region Делаем обект неуничтожимым при перегрузке сцены

    private static GlobalManagerScript _instance;

    public static GlobalManagerScript Instance
    {
        get { return _instance; }
    }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
            _instance = this;
        }
        Awake2();
    }

    #endregion

    private void Awake2()
    {
        LoadPrefab();
        LevelLoad();

        PivotGameObject=GameObject.Find("PivotGameObject");

        AwakeGameScene();

    }


    /// <summary>Выполняется при переходе на сцену GameScene</summary>
    public void AwakeGameScene()
    {
        //создаем GameMastera для текущего уровня
        GameMaster = Instantiate(GamePrefab[PrefabEnum.GameMaster]);
        GameMaster.transform.parent = PivotGameObject.transform;
        GameMaster.transform.localPosition=Vector3.zero;
        GameMasterScript gms = GameMaster.GetComponent<GameMasterScript>();

        gms.LevelMap = LevelMap[LevelCurrent];
        gms.GamePrefab = GamePrefab;
        gms.LevelCurrent = LevelCurrent;
        GameMaster.name = "GameMaster_" + LevelCurrent;


    }


    private void LoadPrefab()
    {

        GamePrefab.Add(PrefabEnum.ClearSpace, Resources.Load("ClearSpace/ClearSpace") as GameObject);
        GamePrefab.Add(PrefabEnum.Wall, Resources.Load("Wall/Wall1") as GameObject);
        GamePrefab.Add(PrefabEnum.Chest, Resources.Load("Chest/Chest") as GameObject);
        GamePrefab.Add(PrefabEnum.Stairs, Resources.Load("Stairs/Stairs1") as GameObject);
        GamePrefab.Add(PrefabEnum.Rope, Resources.Load("Rope/Rope") as GameObject);
        GamePrefab.Add(PrefabEnum.Stone, Resources.Load("Stone/Stone") as GameObject);
        GamePrefab.Add(PrefabEnum.GameMaster, Resources.Load("GameMaster/GameMaster") as GameObject);
        GamePrefab.Add(PrefabEnum.Player, Resources.Load("1_Player/Player") as GameObject);
        GamePrefab.Add(PrefabEnum.FalseWall, Resources.Load("FalseWall/FalseWall1") as GameObject);


    }

    /// <summary>Сохраняет уровни</summary>
    public void LevelSave()
    {
        var stream = new FileStream("Levels.dat", FileMode.Create);
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(stream, LevelMap);
        stream.Close();
        Debug.Log("---LevelsWrite--\n");
    }

    public void LevelLoad()
    {
        var stream = new FileStream("Levels.dat", FileMode.Open);
        BinaryFormatter formatter = new BinaryFormatter();
        LevelMap = formatter.Deserialize(stream) as Level[];
        stream.Close();
    }


    /// <summary>не запускать стирает данные уровней</summary>
    private void LevelInit()
    {
        throw new Exception("Не запускать");
        LevelMap = new Level[150];
        for (int k = 0; k < 150; k++)
        {
            LevelMap[k] = (new Level() { Map = new PrefabEnum[Const.LevelDx,Const.LevelDy] });

            for (int y = 0; y < Const.LevelDy; y++)
            {
                for (int x = 0; x < Const.LevelDx; x++)
                {
                    LevelMap[k].Map[x, y] = PrefabEnum.ClearSpace;
                    if (y == Const.LevelDy - 1) LevelMap[k].Map[x, y] = PrefabEnum.Stone;
                }
            }
            LevelMap[k].Map[0, 1] = PrefabEnum.Stone;
            LevelMap[k].Map[Const.LevelDx - 1, 1] = PrefabEnum.Stone;
        }
    }
}
