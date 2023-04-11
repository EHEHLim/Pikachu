using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextScene : MonoBehaviour
{
    public Text Ep;
    public Image fade;
    float timer;//fade효과 감소
    float FadeT = 1f;//fade효과 지속시간
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
            StartCoroutine(FadeNext());
        }else if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Ep.text = $"Player1  이동: A D 점프: W 스파이크: space \n Player2 이동: <- -> 점프: 위쪽 화살표 스파이크:Enter \n ";
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            Ep.text = $"시작하려면 SPACE키를 눌러주세요.\r\n 게임 시작시 공이 바로 떨어집니다. \r\n도움말을 보시려면 Ctrl키를 누르시오";
        }
    }

    IEnumerator FadeNext()
    {
        fade.gameObject.SetActive(true);
        timer = 0f;
        Color alpha = fade.color;
        while (alpha.a < 1f)
        {
            timer += Time.deltaTime / FadeT;
            alpha.a = Mathf.Lerp(0,1,timer);//0부터 1까지 부드럽게 변화
            fade.color = alpha;//alpha값을 매 프레임 이미지에 넣어서 실행
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        fade.gameObject.SetActive(false);
        SceneManager.LoadScene("SampleScene");
        yield return null;
    }
}
