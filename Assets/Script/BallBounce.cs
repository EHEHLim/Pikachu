using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BallBounce : MonoBehaviour
{
    public float forceMagnitude = 10f;
    private int direction = 1;//공 시작 방향 0409임정빈,접촉시 공의 이동방향을 각도에따라 달라지도록 수정*/


    public Rigidbody2D rb;
    Animator anim;
    public GameManager GM;

   
    void Start()
    {
        //transform.position = new Vector3(-9f, 7.5f, 0);//기본 공 시작 위치
        anim = GetComponent<Animator>();
        GM = gameObject.AddComponent<GameManager>();

    }
    private void FixedUpdate()
    {

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌한 객체가 플레이어인 경우
        if (collision.gameObject.CompareTag("Player"))   
        {
            float ballPositionX = transform.position.x;       //0409임정빈,접촉시 공의 이동방향을 각도에따라 달라지도록 수정
            float playerPositionX = collision.transform.position.x;

            // 공과 충돌한 플레이어 사이의 각도를 구한다.
            float angle = (ballPositionX - playerPositionX) * 5f;

            // 각도에 따라 방향을 계산한다.
            float directionX = Mathf.Sin(angle * Mathf.Deg2Rad);
            float directionY = Mathf.Cos(angle * Mathf.Deg2Rad);

            // 방향을 더한 힘으로 공을 밀어낸다.
            Vector2 directionVector = new Vector2(directionX, directionY).normalized;
            rb.AddForce(forceMagnitude * directionVector, ForceMode2D.Impulse);
            /* if (transform.position.x < 0.0f)//네트를 기준으로 왼쪽에서 닿았을 경우(플레이어1)   //0409기존의 공 방향
             {
                 direction = 1;
                 rb.AddForce(forceMagnitude * new Vector2(2f * direction, 1f), ForceMode2D.Impulse);

             }
             else if (transform.position.x > 0.0f)//네트를 기준으로 오른쪽에서 닿았을 경우(플레이어2)
             {
                 direction = -1;
                 rb.AddForce(forceMagnitude * new Vector2(2f * direction, 1f), ForceMode2D.Impulse);
             }*/

            if (collision.gameObject.name=="Player1")              
            {
                if (collision.gameObject.GetComponent<PlayerMove>().isSmashing)   //스파이크
                {
                    rb.AddForce(rb.velocity.normalized * rb.velocity.magnitude * 1.7f, ForceMode2D.Impulse);   //공의 force magnitude를 2배 증가시킴
                    direction = 1;
                    rb.AddForce(forceMagnitude * new Vector2(4f * direction, 1f), ForceMode2D.Impulse);      //이 코드를 안넣으면 공이 너무 위로 쏫아오름

                }
                /*rb.AddForce(rb.velocity.normalized * rb.velocity.magnitude * 2f, ForceMode2D.Impulse);        //공의 force magnitude를 2배 증가시킴
                direction = 1;
                rb.AddForce(forceMagnitude * new Vector2(4f * direction, 1f), ForceMode2D.Impulse);  */     //이 코드를 안넣으면 공이 너무 위로 쏫아오름

            }
            if (collision.gameObject.name=="Player2") //플레이어2의 스파이크 기능
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
         if (collision.gameObject.CompareTag("Ascore"))//플레이어1 득점 
         {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            GM.SetAText();
            StartCoroutine(ReStartCoroutine2());
         }
        if (collision.gameObject.CompareTag("Bscore"))//플레이어2의 득점
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            GM.SetBText();
            StartCoroutine(ReStartCoroutine());
        }
        IEnumerator ReStartCoroutine()                                 //플레이어 1의 득점
        {
            yield return new WaitForSeconds(1f);                       //1초 딜레이
            //transform.position = new Vector3(9f, 8.4f, 0);             //공의 위치를 득점 한 사람 앞으로 위치변경
            rb.constraints = RigidbodyConstraints2D.None;              //활성화 해둔 x축,y축 고정을 비활성화
            rb.velocity = Vector2.down;                                //공을 떨굼으로 게임 시작
        }
        IEnumerator ReStartCoroutine2()                                //플레이어2의 득점
        {   
            yield return new WaitForSeconds(1f);                       //1초 딜레이
           // transform.position = new Vector3(-9f, 7.5f, 0);            //공의 위치를 득점 플레이어 앞으로 변경
            rb.constraints = RigidbodyConstraints2D.None;              //x,y축 고정 해제
            rb.velocity = Vector2.down;                                //공을 떨굼으로 게임 시작
        }
       
    }
    
    
}

