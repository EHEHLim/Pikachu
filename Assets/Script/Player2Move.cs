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
        if (Input.GetButtonDown("JumpP2") && !anim.GetBool("IsJumping"))  //! animator.GetBool("isJumping") -  �Ű������� false�� ���
                                                                        //���������� ����
        {
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse); //ForceMode2D.Impulse��  ���������� �������� ����� ��
            anim.SetBool("IsJumping", true);
        }

        if (Input.GetButton("HorizontalP2"))//player1�� Horizontal �Է��� �޾�����
        {
            sr.flipX = Input.GetAxisRaw("HorizontalP2") == -1;//�¿����(FlipX)�� ���� �ٲ�
            anim.SetBool("IsWalking", true);
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }
          if (Input.GetKeyDown(KeyCode.Return) && canSmashing)      //����Ű ������ ������ũ �ߵ� Kecode.Return ==EnterŰ
        {
            StartCoroutine(Smashing());
        }



    }
    private void FixedUpdate()
    {
        h = Input.GetAxisRaw("HorizontalP2");
        tr.position += new Vector3(h * MoveSpeed * Time.deltaTime, 0, 0);
        //Landing Platform
        if (rb.velocity.y < 0) //y���� �ӵ��� �������°��϶� , �� - ���϶�
        {
            Debug.DrawRay(rb.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rb.position, Vector3.down, 1, LayerMask.GetMask("Platform"));

            if (rayHit.collider != null) // null - (�ƹ������� ������������) // ���� 
            {
                if (rayHit.distance < 5f) //�÷��̾�� �÷����� �Ÿ��� 0.5���� �۴�
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
