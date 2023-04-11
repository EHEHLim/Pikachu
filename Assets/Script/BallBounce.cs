using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BallBounce : MonoBehaviour
{
    public float forceMagnitude = 10f;
    private int direction = 1;//�� ���� ���� 0409������,���˽� ���� �̵������� ���������� �޶������� ����*/


    public Rigidbody2D rb;
    Animator anim;
    public GameManager GM;

   
    void Start()
    {
        //transform.position = new Vector3(-9f, 7.5f, 0);//�⺻ �� ���� ��ġ
        anim = GetComponent<Animator>();
        GM = gameObject.AddComponent<GameManager>();

    }
    private void FixedUpdate()
    {

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        // �浹�� ��ü�� �÷��̾��� ���
        if (collision.gameObject.CompareTag("Player"))   
        {
            float ballPositionX = transform.position.x;       //0409������,���˽� ���� �̵������� ���������� �޶������� ����
            float playerPositionX = collision.transform.position.x;

            // ���� �浹�� �÷��̾� ������ ������ ���Ѵ�.
            float angle = (ballPositionX - playerPositionX) * 5f;

            // ������ ���� ������ ����Ѵ�.
            float directionX = Mathf.Sin(angle * Mathf.Deg2Rad);
            float directionY = Mathf.Cos(angle * Mathf.Deg2Rad);

            // ������ ���� ������ ���� �о��.
            Vector2 directionVector = new Vector2(directionX, directionY).normalized;
            rb.AddForce(forceMagnitude * directionVector, ForceMode2D.Impulse);
            /* if (transform.position.x < 0.0f)//��Ʈ�� �������� ���ʿ��� ����� ���(�÷��̾�1)   //0409������ �� ����
             {
                 direction = 1;
                 rb.AddForce(forceMagnitude * new Vector2(2f * direction, 1f), ForceMode2D.Impulse);

             }
             else if (transform.position.x > 0.0f)//��Ʈ�� �������� �����ʿ��� ����� ���(�÷��̾�2)
             {
                 direction = -1;
                 rb.AddForce(forceMagnitude * new Vector2(2f * direction, 1f), ForceMode2D.Impulse);
             }*/

            if (collision.gameObject.name=="Player1")              
            {
                if (collision.gameObject.GetComponent<PlayerMove>().isSmashing)   //������ũ
                {
                    rb.AddForce(rb.velocity.normalized * rb.velocity.magnitude * 1.7f, ForceMode2D.Impulse);   //���� force magnitude�� 2�� ������Ŵ
                    direction = 1;
                    rb.AddForce(forceMagnitude * new Vector2(4f * direction, 1f), ForceMode2D.Impulse);      //�� �ڵ带 �ȳ����� ���� �ʹ� ���� �K�ƿ���

                }
                /*rb.AddForce(rb.velocity.normalized * rb.velocity.magnitude * 2f, ForceMode2D.Impulse);        //���� force magnitude�� 2�� ������Ŵ
                direction = 1;
                rb.AddForce(forceMagnitude * new Vector2(4f * direction, 1f), ForceMode2D.Impulse);  */     //�� �ڵ带 �ȳ����� ���� �ʹ� ���� �K�ƿ���

            }
            if (collision.gameObject.name=="Player2") //�÷��̾�2�� ������ũ ���
            {
                if (collision.gameObject.GetComponent <Player2Move>().isSmashing)
                {
                    rb.AddForce(rb.velocity.normalized * rb.velocity.magnitude * 1.7f, ForceMode2D.Impulse);
                    direction = -1;
                    rb.AddForce(forceMagnitude * new Vector2(4f * direction, 1f), ForceMode2D.Impulse);
                }
                else
                {
                    forceMagnitude = 10f;
                }
            }
            
        }
         if (collision.gameObject.CompareTag("Ascore"))//�÷��̾�1 ���� 
         {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            GM.SetAText();
            StartCoroutine(ReStartCoroutine2());
         }
        if (collision.gameObject.CompareTag("Bscore"))//�÷��̾�2�� ����
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            GM.SetBText();
            StartCoroutine(ReStartCoroutine());
        }
        IEnumerator ReStartCoroutine()                                 //�÷��̾� 1�� ����
        {
            yield return new WaitForSeconds(1f);                       //1�� ������
            //transform.position = new Vector3(9f, 8.4f, 0);             //���� ��ġ�� ���� �� ��� ������ ��ġ����
            rb.constraints = RigidbodyConstraints2D.None;              //Ȱ��ȭ �ص� x��,y�� ������ ��Ȱ��ȭ
            rb.velocity = Vector2.down;                                //���� �������� ���� ����
        }
        IEnumerator ReStartCoroutine2()                                //�÷��̾�2�� ����
        {   
            yield return new WaitForSeconds(1f);                       //1�� ������
           // transform.position = new Vector3(-9f, 7.5f, 0);            //���� ��ġ�� ���� �÷��̾� ������ ����
            rb.constraints = RigidbodyConstraints2D.None;              //x,y�� ���� ����
            rb.velocity = Vector2.down;                                //���� �������� ���� ����
        }
       
    }
    
    
}

