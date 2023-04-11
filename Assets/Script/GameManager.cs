using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement; //SceneManager 사용을 위해 추가
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public GameObject ball;
    public Text AText, BText, RoundT, EndT; 
    private GameObject Aobj, Bobj, Robj, Endobj; //AText를 찾기 위함 
    public int player1Score, player2Score, Round; //Player 1의 점수
    private const int winningScore = 7; //게임에서 이길 점수
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
        //UI 주소지정
        Aobj = GameObject.Find("AText");
        Bobj = GameObject.Find("BText");
        Robj = GameObject.Find("Round");
        Endobj = GameObject.Find("winner");
        ball = GameObject.Find("ball");
        BText = Bobj.GetComponent<Text>();
        AText = Aobj.GetComponent<Text>();
        RoundT = Robj.GetComponent<Text>();
        EndT = Endobj.GetComponent<Text>();
        //단순히 public Text AText 하고 인스펙터 창에서 드래그&드랍으로 할 시 찾지를 못함 - NULLREFERENCE ERROR
        //게임 정보 초기화
        player1Score = player2Score = 0;//변수 초기화
        Round = 1;
        AText.text = $"player1 Score:{player1Score.ToString()}";
        BText.text = $"player2 Score:{player2Score.ToString()}";
        RoundT.text = $"Round {Round.ToString()}";
        GameSet = false;
        EndT.text = "";
        ball.transform.position = new Vector3(-9f, 7.5f, 0);
        yield return null;
    }
    public void SetAText()//플레이어1의 득점
    {
        player1Score++;
        AText.text = $"player1 Score:{player1Score.ToString()}";
        Round++;
        RoundT.text = $"Round: {Round.ToString()}";
        ball.transform.position = new Vector3(-9f, 7.5f, 0);
    }
    public void SetBText()//플레이어2의 득점
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
            EndT.text = "Player1의 승리! \n 새로운 게임을 하시려면 space바를 눌러주세요";
        }
        else{
            EndT.text = "Player2 의 승리! \n 새로운 게임을 하시려면 space바를 눌러주세요";
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
