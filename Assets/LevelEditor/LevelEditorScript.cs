using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEditorScript : MonoBehaviour
{

    #region Отображение номера редактируемого уровня
    public Text CurrentLevelText;
    private int _levelCurent;

    public int LevelCurrent
    {
        get { return _levelCurent; }
        set
        {
            _levelCurent = value;
            CurrentLevelText.text = "Level: " + value.ToString("D2");
        }
    }
    #endregion

    private GlobalManagerScript GlobalManager;


    /// <summary>Курсор (квадрат) </summary>
    public GameObject CursorsPrefab;
    public GameObject Cursors;
    public GameObject CursorsCurrentBlock;
    public PrefabEnum CursorsCurrentBlockType;
    public Texture2D CursorPoint;

    /// <summary>Позиция мыши в экранных координатах </summary>
    public Vector3 mousePosition;
    /// <summary>Позиция мыши в блочных координатах </summary>
    public int MouseX;
    public int MouseY;


    public GameObject BkgroundPrefab;
    public GameObject BkgroundImg;
    public bool BkgShow;

    void Awake()
    {
       

        BkgroundImg = Instantiate(BkgroundPrefab);
        BkgroundImg.transform.parent= gameObject.transform;
        BkgroundImg.transform.localPosition=new Vector3(0,-32);
        BkgroundImg.name = "BkgroundImg";
        

    }


    void Start()
    {
        GameObject.Destroy(Cursors);
        Cursors = Instantiate(CursorsPrefab);
        Cursors.name = "Cusor";
        Cursors.transform.parent = gameObject.transform;

        
        GlobalManager = GameObject.Find("GlobalManager").GetComponent<GlobalManagerScript>();
        LevelCurrent = GlobalManager.LevelCurrent;
        CursorsCurrentBlock = Instantiate(GlobalManager.GamePrefab[PrefabEnum.ClearSpace]);
        CursorsCurrentBlock.transform.parent = Cursors.transform;
        CursorsCurrentBlockType = PrefabEnum.ClearSpace;

        BkgroundImg.GetComponent<BkgImgScript>().SetBkgImg(LevelCurrent);
        

        

        

    }

    // Update is called once per frame
    void Update()
    {

        #region расчет координат установка курсора

        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10));
        mousePosition = mousePosition - Const.BlockPivotPoint;

        MouseX = (int)mousePosition.x / 32;
        MouseY = (int)mousePosition.y / -32;
       

        if (MouseX >= 0 && MouseX <= Const.LevelDx-1 && MouseY >= 0 && MouseY <= Const.LevelDy-1)
        {
            Cursor.SetCursor(CursorPoint, Vector2.zero, CursorMode.Auto);
            Cursors.SetActive(true);
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            Cursors.SetActive(false);
        }
        Cursors.transform.localPosition = new Vector3(MouseX * 32f, MouseY * -32f);

        #endregion

        //размещение блока под курсором
        if (Input.GetMouseButton(0) && Cursors.activeSelf)
        {
            GlobalManager.LevelMap[LevelCurrent].Map[MouseX, MouseY] = CursorsCurrentBlockType;
            GameObject o = Instantiate(GlobalManager.GamePrefab[CursorsCurrentBlockType], Cursors.transform.position, Quaternion.identity);
            o.transform.parent = GlobalManager.GameMaster.GetComponent<GameMasterScript>().Level.transform;
        }

        BkgroundImg.SetActive(BkgShow);

    }

    public void LevelChange()
    {
        GameObject.Destroy(GlobalManager.GameMaster);
        GlobalManager.AwakeGameScene();
        GlobalManager.LevelSave();
        this.Start();
    }

    /// <summary> смена типа блока</summary>
    public void SetBlock(PrefabEnum blockType)
    {
        CursorsCurrentBlockType = blockType;
        GameObject.Destroy(CursorsCurrentBlock);
        CursorsCurrentBlock = Instantiate(GlobalManager.GamePrefab[blockType]);
        CursorsCurrentBlock.transform.localPosition = new Vector3(0, 0);
        CursorsCurrentBlock.transform.position = Cursors.transform.position;
        CursorsCurrentBlock.transform.parent = Cursors.transform;
    }

    public void ButtonPrev()
    {
        if (GlobalManager.LevelCurrent > 0)
        {
            GlobalManager.LevelCurrent--;
        }
        else
        {
            GlobalManager.LevelCurrent = Const.MaxLevels-1;
        }

        LevelChange();
    }

    public void ButtonNext()
    {
        if (GlobalManager.LevelCurrent < Const.MaxLevels-1)
        {
            GlobalManager.LevelCurrent++;
        }
        else
        {
            GlobalManager.LevelCurrent = 0;
        }

        LevelChange();
    }

    public void ButtonSave()
    {
        GlobalManager.LevelSave();
    }

    public void ButtonClearSpace()
    {
        SetBlock(PrefabEnum.ClearSpace);
    }

    public void ButtonWall()
    {
        SetBlock(PrefabEnum.Wall);
    }

    public void ButtonSetPlayerStartPosition()
    {
        SetBlock(PrefabEnum.Player);
    }

    public void ButtonStone()
    {
        SetBlock(PrefabEnum.Stone);
    }

    public void ButtonStairs()
    {
        SetBlock(PrefabEnum.Stairs);
    }

    public void ButtonChest()
    {
        SetBlock(PrefabEnum.Chest);
    }

    public void ButtonRope()
    {
        SetBlock(PrefabEnum.Rope);
    }

    public void ButtonFalseWall()
    {
        SetBlock(PrefabEnum.FalseWall);
    }

    public void ButtonBkg()
    {
        BkgShow = BkgShow ^ true;
    }
}
