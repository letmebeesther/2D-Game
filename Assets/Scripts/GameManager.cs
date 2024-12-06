using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI�� ����� �� �ʿ�

public class GameManager : MonoBehaviour
{
    public GameObject mainImage;
    public Sprite gameOverSpr;
    public Sprite gameClearSpr;
    public GameObject panel;
    public GameObject restartButton;
    public GameObject nextButton;

    Image titleImage;

    // +++ �ð����� �߰� +++
    public GameObject timeBar; // �ð� ǥ�� �̹���
    public GameObject timeText; // �ð� �ؽ�Ʈ
    TimeController timeCnt; // TimeController

    // +++ �����߰� +++
    public GameObject scoreText; //���� �ؽ�Ʈ
    public static int totalScore; // ���� ����
    public int stageScore = 0; // �������� ����

    // +++ ���� ��� �߰� +++
    public AudioClip meGameOver; // ���� ����
    public AudioClip meGameClear; // ���� Ŭ����

    // +++ �÷��̾� ���� +++
    public GameObject inputUI; // ���� UI �г�

    // Start is called before the first frame update
    void Start()
    {
        //�̹��� �����
        Invoke("InactiveImage", 1.0f);

        //��ư(�г�)�� �����
        panel.SetActive(false);

        // +++�ð����� �߰�+++
        //TimeController ������
        timeCnt = GetComponent<TimeController>();
        if(timeCnt != null)
        {
            if(timeCnt.gameTime == 0.0f)
            {
                timeBar.SetActive(false); // �ð������� ������ ����
            }
        }
        // +++���� �߰�+++
        UpdateScore();
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController.gameState == "gameclear")
        {
            // ���� Ŭ����
            mainImage.SetActive(true); // �̹��� ǥ��
            panel.SetActive(true); // ��ư(�г�)�� ǥ��

            //RESTART ��ư ��ȿȭ
            Button bt = restartButton.GetComponent<Button>();
            bt.interactable = false;
            mainImage.GetComponent<Image>().sprite = gameClearSpr;
            PlayerController.gameState = "gameend";

            //+++�ð����� �߰�+++
            if (timeCnt != null)
            {
                timeCnt.isTimeOver = true;//�ð� ī��Ʈ ����

                //+++���� �߰�+++
                // ������ �Ҵ��Ͽ� �Ҽ����� ������
                int time = (int)timeCnt.displayTime;
                totalScore += time * 10; //���� �ð��� ������ ���Ѵ�
            }

            //+++�����߰�+++
            totalScore += stageScore;
            stageScore = 0;
            UpdateScore(); // ���� ����

            // +++ ���� ��� �߰� +++
            // ���� ���
            AudioSource soundPlayer = GetComponent<AudioSource>();
            if (soundPlayer != null)
            {
                // BGM ����
                soundPlayer.Stop();
                soundPlayer.PlayOneShot(meGameClear);
            }

            // +++ �÷��̾� ���� +++
            inputUI.SetActive(false); // ���� UI �����
        }

        else if (PlayerController.gameState == "gameover")
        {
            // ���� ����
            mainImage.SetActive(true); //�̹��� ǥ��
            panel.SetActive(true); // ��ư(�г�)�� ǥ��

            // +++ ���� ��� �߰� +++
            // ���� ���
            AudioSource soundPlayer = GetComponent<AudioSource>();
            if(soundPlayer != null)
            {
                // BGM ����
                soundPlayer.Stop();
                soundPlayer.PlayOneShot(meGameOver);
            }

            //NEXT ��ư ��Ȱ��
            Button bt = nextButton.GetComponent<Button>();
            bt.interactable = false;
            mainImage.GetComponent<Image>().sprite = gameOverSpr;
            PlayerController.gameState = "gameend";

            //+++�ð����� �߰�+++
            if(timeCnt != null)
            {
                timeCnt.isTimeOver = true;//�ð� ī��Ʈ ����
            }

            // +++ �÷��̾� ���� +++
            inputUI.SetActive(false); // ���� UI �����
        }
        else if (PlayerController.gameState == "playing")
        {
            //������
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            //PlayerController ��������
            PlayerController playerCnt = player.GetComponent<PlayerController>();
            //+++�ð����� �߰�+++
            //�ð� ����
            if(timeCnt != null)
            {
                if(timeCnt.gameTime > 0.0f)
                {
                    //������ �Ҵ��Ͽ� �Ҽ��� ���ϸ� ����
                    int time = (int)timeCnt.displayTime;
                    //�ð�����
                    timeText.GetComponent<Text>().text = time.ToString();
                    //Ÿ�ӿ���
                    if (time == 0)
                    {
                        playerCnt.GameOver(); // ���� ����
                    }
                }
            }

            //+++���� �߰�+++
            if(playerCnt.score != 0)
            {
                stageScore += playerCnt.score;
                playerCnt.score = 0;
                UpdateScore();
            }
        }
    }
    // �̹��� �����
    void InactiveImage()
    {
        mainImage.SetActive(false);
    }

    //+++���� �߰�+++
    void UpdateScore()
    {
        int score = stageScore + totalScore;
        scoreText.GetComponent<Text>().text = score.ToString();
    }

    // +++ �÷��̾� ���� +++
    // ����
    public void Jump()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerController playerCnt = player.GetComponent<PlayerController>();
        playerCnt.Jump();
    }
}
