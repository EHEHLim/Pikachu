using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextScene : MonoBehaviour
{
    public Text Ep;
    public Image fade;
    float timer;//fadeȿ�� ����
    float FadeT = 1f;//fadeȿ�� ���ӽð�
    // Start is called before the first frame update
    void Start()
    {
        Ep.text = $"�����Ϸ��� SPACEŰ�� �����ּ���.\r\n ���� ���۽� ���� �ٷ� �������ϴ�. \r\n������ ���÷��� CtrlŰ�� �����ÿ�";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(FadeNext());
        }else if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Ep.text = $"Player1  �̵�: A D ����: W ������ũ: space \n Player2 �̵�: <- -> ����: ���� ȭ��ǥ ������ũ:Enter \n ";
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            Ep.text = $"�����Ϸ��� SPACEŰ�� �����ּ���.\r\n ���� ���۽� ���� �ٷ� �������ϴ�. \r\n������ ���÷��� CtrlŰ�� �����ÿ�";
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
            alpha.a = Mathf.Lerp(0,1,timer);//0���� 1���� �ε巴�� ��ȭ
            fade.color = alpha;//alpha���� �� ������ �̹����� �־ ����
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        fade.gameObject.SetActive(false);
        SceneManager.LoadScene("SampleScene");
        yield return null;
    }
}
