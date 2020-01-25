using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinPanel : MonoBehaviour
{

    public Text ResultText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitInfos(float usedTime)
    {
        ResultText.text = "用时：" + String.Format("{0:F}", usedTime) + "秒";
    }
}
