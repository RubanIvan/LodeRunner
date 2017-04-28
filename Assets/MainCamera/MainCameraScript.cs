using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraScript : MonoBehaviour
{
    //размеры текущего экрана
    public float ScreenX;
    public float ScreenY;

    // Use this for initialization
    void Start () {
        

       
    }
	
	// Update is called once per frame
	void Update () {

        Vector3 v = Camera.main.ScreenToWorldPoint(new Vector3(-1, -1));
        ScreenX = Mathf.Abs(float.Parse(v.x.ToString("F2")));
        ScreenY = Mathf.Abs(float.Parse(v.y.ToString("F2")));
    }
}
