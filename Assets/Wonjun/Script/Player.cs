using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 4;
    [SerializeField] private float attackcool = 0.6f;
    Vector3 dir;


    private Rigidbody2D _rigid;
    public LayerMask a;
    public bool below;
    public bool att1 = true;
    public bool attDown = true;


    SpriteRenderer sp;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        _rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        below = Physics2D.Raycast(transform.position, Vector2.down, 0.5f, a);
        transform.position += dir * speed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space)&& below == true)
        {
            _rigid.velocity = new Vector2(_rigid.velocity.x, 5f);
            StartCoroutine(Jump());
        }
        attackcool += Time.deltaTime;


        float x = Input.GetAxisRaw("Horizontal");
        dir = new Vector3(x, 0, 0);
        if (x < 0)
        {
            //¿ÞÂÊ
            sp.flipX = true;
            anim.SetBool("run", true);
        }
        else if (x > 0)
        {
            //¿À¸¥ÂÊ
            sp.flipX = false;
            anim.SetBool("run", true);
        }
        else
        {
            anim.SetBool("run", false);
        }

        if (Input.GetMouseButtonDown(0) && below == true && attackcool >= 0.7f&& att1 == true)
        {
            anim.SetTrigger("Attack");
            attackcool = 0;
            attDown = false;
            StartCoroutine(attackpos());
        }
        if (Input.GetMouseButtonDown(0) && below == false && attDown == true)
        {
            anim.SetBool("Jattack", true);
            StartCoroutine(att());
            _rigid.gravityScale = 5;
        }
        else if(below == true)
        {
            _rigid.gravityScale = 1;
            anim.SetBool("Jattack", false);
        }
    }

    IEnumerator Jump()
    {
        anim.SetBool("Jump", true);
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("Jump", false);
    }
    IEnumerator attackpos()
    {
        speed = 0;
        yield return new WaitForSeconds(0.7f);
        speed = 5;
        attDown = true;
    }
    IEnumerator att()
    {
        att1 = false;
        yield return new WaitForSeconds(0.7f);
        att1 = true;
    }
}
