using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public bool isCountDown = true;
    public float gameTime = 0;
    public bool isTimeOver = false;
    public float displayTime = 0;

    float times = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (isCountDown)
        {
            //ī��Ʈ�ٿ�
            displayTime = gameTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimeOver == false)
        {
            times += Time.deltaTime;
            if (isCountDown)
            {
                //ī��Ʈ�ٿ�
                displayTime = gameTime - times;
                if (displayTime <= 0.0f)
                {
                    displayTime = 0.0f;
                    isTimeOver = true;
                }
            }
            else
            {
                //ī��Ʈ ��
                displayTime = times;
                if (displayTime >= gameTime)
                {
                    displayTime = gameTime;
                    isTimeOver = true;
                }
            }
            Debug.Log("TIMES : " + displayTime);
        }
    }
}