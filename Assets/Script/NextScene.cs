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
        Ep.text = $"�����Ϸ��� SPACEŰ�� �����ּ���.\r\n ���� ���۽� ���� �ٷ� �������ϴ�. \r\n������ ���÷��� CtrlŰ�� �����ÿ�";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("SampleScene");
        }else if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Ep.text = $"Player1  �̵�: A D ����: W ������ũ: space \n Player2 �̵�: <- -> ����: ���� ȭ��ǥ ������ũ:Enter \n ";
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            Ep.text = $"�����Ϸ��� SPACEŰ�� �����ּ���.\r\n ���� ���۽� ���� �ٷ� �������ϴ�. \r\n������ ���÷��� CtrlŰ�� �����ÿ�";
        }
    }
}
