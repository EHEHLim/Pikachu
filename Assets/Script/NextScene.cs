using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextScene : MonoBehaviour
{
    public Text Ep;
    // Start is called before the first frame update
    void Start()
    {
        Ep.text = $"시작하려면 SPACE키를 눌러주세요.\r\n 게임 시작시 공이 바로 떨어집니다. \r\n도움말을 보시려면 Ctrl키를 누르시오";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("SampleScene");
        }else if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Ep.text = $"Player1  이동: A D 점프: W 스파이크: space \n Player2 이동: <- -> 점프: 위쪽 화살표 스파이크:Enter \n ";
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            Ep.text = $"시작하려면 SPACE키를 눌러주세요.\r\n 게임 시작시 공이 바로 떨어집니다. \r\n도움말을 보시려면 Ctrl키를 누르시오";
        }
    }
}
