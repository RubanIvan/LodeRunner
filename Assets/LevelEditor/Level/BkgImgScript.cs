using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BkgImgScript : MonoBehaviour
{

    public Sprite[] LevelBkg = new Sprite[75];

    private SpriteRenderer Sprite;

    public void SetBkgImg(int level)
    {
        Sprite.sprite = LevelBkg[level];
    }


    void Awake()
    {
        Sprite = gameObject.GetComponent<SpriteRenderer>();

        string directory = Path.Combine(Directory.GetCurrentDirectory(), "Assets\\LevelEditor\\Level\\");
        string fileName;
        Texture2D texture;


        for (int i = 0; i < Const.MaxLevels; i++)
        {
            texture = new Texture2D(1120, 898, TextureFormat.ARGB32, false);
            fileName = Path.Combine(directory, i.ToString("D2") + ".png");

            if (File.Exists(fileName) == false) continue;

            texture.LoadImage(File.ReadAllBytes(fileName));
            LevelBkg[i] = UnityEngine.Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.0f, 1.0f), 1f);
            LevelBkg[i].name = i.ToString("D2");


        }




    }


}
