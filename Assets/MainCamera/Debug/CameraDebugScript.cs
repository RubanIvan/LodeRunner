using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CameraDebugScript : MonoBehaviour {

    public GameObject Line_16_9;
    public GameObject Line_16_10;
    public GameObject Line_3_2;
    public GameObject Line_4_3;
    public GameObject Line_5_4;


    public Camera curCamera;

    public void Awake()
    {
        curCamera=Camera.main;
    }

    void Start()
    {
        string s = "Camera resalution " + curCamera.pixelWidth + "x" + curCamera.pixelHeight;
        s += "   Aspect ratio= " + AspectConvert(curCamera.aspect) + "  (" + curCamera.aspect + ")";
        s += "\n";
        Debug.Log(s);

        GameObject o;
        float y = curCamera.orthographicSize;

        //o = Instantiate(Line_16_9, new Vector3(16f / 9f * y, y), Quaternion.identity);
        //o.name = "16х9";
        //   o.transform.parent = Camera.main.transform;
        //   o = Instantiate(Line_16_9, new Vector3(16f / 9f * y*-1, y), Quaternion.identity);
        //   o.name = "16х9(Clone)";
        //   o.transform.parent = Camera.main.transform;

        //   o = Instantiate(Line_16_10, new Vector3(16f / 10f * y, y), Quaternion.identity);
        //   o.name = "16х10";
        //   o.transform.parent = Camera.main.transform;
        //   o = Instantiate(Line_16_10, new Vector3(16f / 10f * y * -1, y), Quaternion.identity);
        //   o.name = "16х10(Clone)";
        //   o.transform.parent = Camera.main.transform;

        //   o = Instantiate(Line_3_2, new Vector3(3f / 2f * y, y), Quaternion.identity);
        //   o.name = "3х2";
        //   o.transform.parent = Camera.main.transform;
        //   o = Instantiate(Line_3_2, new Vector3(3f / 2f * y * -1, y), Quaternion.identity);
        //   o.name = "3х2(Clone)";
        //   o.transform.parent = Camera.main.transform;

        //   o = Instantiate(Line_4_3, new Vector3(4f / 3f * y, y), Quaternion.identity);
        //   o.name = "4х3";
        //   o.transform.parent = Camera.main.transform;
        //   o = Instantiate(Line_4_3, new Vector3(4f / 3f * y * -1, y), Quaternion.identity);
        //   o.name = "4х3(Clone)";
        //   o.transform.parent = Camera.main.transform;

        o = Instantiate(Line_5_4, new Vector3(5f / 4f * y, y), Quaternion.identity);
        o.name = "5х4";
        o.transform.parent = Camera.main.transform;
        o = Instantiate(Line_5_4, new Vector3(5f / 4f * y * -1, y), Quaternion.identity);
        o.name = "5х4(Clone)";
        o.transform.parent = Camera.main.transform;


    }



    // Update is called once per frame
    void Update()
    {

    }

    private string AspectConvert(float aspect)
    {
        if (aspect == 1.5f) return "3:2";
        if (aspect >= 1.333332f && aspect <= 1.333334f) return "4:3";
        if (aspect == 1.25f) return "5:4";
        if (aspect >= 1.777777f && aspect <= 1.777779f) return "16:9";
        if (aspect == 1.6f) return "16:10";
        return "Free aspect";
    }

}
