using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMasterScript : MonoBehaviour {

    /// <summary>Cодержит все префабы  (получаем от SceneManager)</summary>
    public Dictionary<Enum, GameObject> GamePrefab;

    /// <summary>Карта уровеня (получаем от SceneManager)</summary>
    public Level LevelMap;

    /// <summary>Текущий уровень </summary>
    public int LevelCurrent;

    /// <summary>контейнер для обьектов уровня</summary>
    public GameObject Level;


    /// <summary>Создаем уровень</summary>
    void LevelObjInit()
    {
        // создаем контейнер
        Level = new GameObject("Level");
        Level.transform.parent = this.transform;
        Level.transform.localPosition = Vector3.zero;

        GameObject o;

        for (int y = 0; y < LevelMap.Map.GetLength(1); y++)
        {
            for (int x = 0; x < LevelMap.Map.GetLength(0); x++)
            {
                //Пропускаем пустое место
                if (LevelMap.Map[x, y] == PrefabEnum.ClearSpace) continue;
                //Debug.Log(LevelMap.Map[x, y]);
                o = Instantiate(GamePrefab[LevelMap.Map[x, y]]);
                o.transform.parent = Level.transform;
                o.transform.localPosition = new Vector3(x * 32f, y * -32f);
                
                o.name = x + "_" + y + " :" + o.name;

                //корекция для игрока
                if (LevelMap.Map[x, y] == PrefabEnum.Player)
                {
                    o.transform.parent = gameObject.transform;
                    o.transform.localPosition = new Vector3(x * 32f+16, y * -32f-16);
                    o.name = "Player";

                }
                    


            }

        }


    }

    // Use this for initialization
    void Start () {
        LevelObjInit();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
