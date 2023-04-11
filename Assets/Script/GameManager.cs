using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement; //SceneManager ����� ���� �߰�
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public GameObject ball;
    public Text AText, BText, RoundT, EndT; 
    private GameObject Aobj, Bobj, Robj, Endobj; //AText�� ã�� ���� 
    public int player1Score, player2Score, Round; //Player 1�� ����
    private const int winningScore = 7; //���ӿ��� �̱� ����
    public bool GameSet = true;
    public void Start()
    {
        StartCoroutine(GameStart());
    }

    public void Update()
    {
        if (player1Score == winningScore || player2Score == winningScore)
        {
            EndGame();
        }
    }
    IEnumerator GameStart()
    {
        //UI �ּ�����
        Aobj = GameObject.Find("AText");
        Bobj = GameObject.Find("BText");
        Robj = GameObject.Find("Round");
        Endobj = GameObject.Find("winner");
        ball = GameObject.Find("ball");
        BText = Bobj.GetComponent<Text>();
        AText = Aobj.GetComponent<Text>();
        RoundT = Robj.GetComponent<Text>();
        EndT = Endobj.GetComponent<Text>();
        //�ܼ��� public Text AText �ϰ� �ν����� â���� �巡��&������� �� �� ã���� ���� - NULLREFERENCE ERROR
        //���� ���� �ʱ�ȭ
        player1Score = player2Score = 0;//���� �ʱ�ȭ
        ball.transform.position = new Vector3(-9f,7.5f,0);
        Round = 1;
        AText.text = $"player1 Score:{player1Score.ToString()}";
        BText.text = $"player2 Score:{player2Score.ToString()}";
        RoundT.text = $"Round {Round.ToString()}";
        GameSet = false;
        EndT.text = "";
        yield return null;
    }
    public void SetAText()//�÷��̾�1�� ����
    {
        player1Score++;
        AText.text = $"player1 Score:{player1Score.ToString()}";
        Round++;
        RoundT.text = $"Round: {Round.ToString()}";
        ball.transform.position = new Vector3(-9f, 7.5f, 0);
    }
    public void SetBText()//�÷��̾�2�� ����
    {
        player2Score++;
        BText.text = $"player2 Score:{player2Score.ToString()}";
        Round++;
        RoundT.text = $"Round: {Round.ToString()}";
        ball.transform.position = new Vector3(9f, 8.4f, 0);
    }
    void EndGame()
    {
        if (player1Score == winningScore)
        {
            EndT.text = "Player1�� �¸�! \n ���ο� ������ �Ͻ÷��� space�ٸ� �����ּ���";
        }
        else{
            EndT.text = "Player2 �� �¸�! \n ���ο� ������ �Ͻ÷��� space�ٸ� �����ּ���";
        }
        Time.timeScale = 0f;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(GameStart());
            GameSet = true;
            Time.timeScale = 1;
        }
    }
}
