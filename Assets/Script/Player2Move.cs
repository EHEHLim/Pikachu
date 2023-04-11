using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player2Move : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform tr;
    public float jumpPower = 15f;

    private float h;
    //private float JumpForce = 5f;
    public float MoveSpeed = 10f;


    SpriteRenderer sr;
    Animator anim;

    public bool isSmashing = false;
    public float SmashingTime = 0.5f;
    public bool canSmashing = true;
    public float SmashingCooldown = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame  
    void Update()
    {
        if (Input.GetButtonDown("JumpP2") && !anim.GetBool("IsJumping"))  //! animator.GetBool("isJumping") -  매개변수가 false인 경우
                                                                        //무한점프를 방지
        {
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse); //ForceMode2D.Impulse는  순간적으로 가해지는 충격의 힘
            anim.SetBool("IsJumping", true);
        }

        if (Input.GetButton("HorizontalP2"))//player1의 Horizontal 입력을 받았을때
        {
            sr.flipX = Input.GetAxisRaw("HorizontalP2") == -1;//좌우반전(FlipX)의 값을 바꿈
            anim.SetBool("IsWalking", true);
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }
          if (Input.GetKeyDown(KeyCode.Return) && canSmashing)      //엔터키 누르면 스파이크 발동 Kecode.Return ==Enter키
        {
            StartCoroutine(Smashing());
        }



    }
    private void FixedUpdate()
    {
        h = Input.GetAxisRaw("HorizontalP2");
        tr.position += new Vector3(h * MoveSpeed * Time.deltaTime, 0, 0);
        //Landing Platform
        if (rb.velocity.y < 0) //y축의 속도가 내려가는값일때 , 즉 - 값일때
        {
            Debug.DrawRay(rb.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rb.position, Vector3.down, 1, LayerMask.GetMask("Platform"));

            if (rayHit.collider != null) // null - (아무런값을 가지지않을때) // 만약 
            {
                if (rayHit.distance < 5f) //플레이어와 플렛폼의 거리가 0.5보다 작다
                    anim.SetBool("IsJumping", false);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ball"))
            anim.SetBool("Isball", true);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ball"))
            Invoke("EndAnim", 0.25f);
    }
    void EndAnim()
    {
        anim.SetBool("Isball", false);
    }
    IEnumerator Smashing()
    {
        isSmashing = true;
        canSmashing = false;
        anim.SetBool("IsAttack", isSmashing);
        yield return new WaitForSeconds(SmashingTime);
        isSmashing = false;
        anim.SetBool("IsAttack", isSmashing);

        yield return new WaitForSeconds(SmashingCooldown);
        canSmashing = true;
    }
}
