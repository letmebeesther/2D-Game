using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchAction : MonoBehaviour
{
    public GameObject targetMoveBlock;
    public Sprite imgaeOn;
    public Sprite imgaeOff;
    public bool on = false; // ����ġ ����(true : ���� ���� false : ������ ���� ����)

    // Start is called before the first frame update
    void Start()
    {
        if (on)
        {
            GetComponent<SpriteRenderer>().sprite = imgaeOn;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = imgaeOff;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //���˽���
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (on)
            {
                on = false;
                GetComponent<SpriteRenderer>().sprite = imgaeOff;
                MovingBlock movBlock = targetMoveBlock.GetComponent<MovingBlock>();
                movBlock.Stop();
            }
            else
            {
                on = true;
                GetComponent<SpriteRenderer>().sprite = imgaeOn;
                MovingBlock movBlock = targetMoveBlock.GetComponent<MovingBlock>();
                movBlock.Move();
            }
        }
    }
}
