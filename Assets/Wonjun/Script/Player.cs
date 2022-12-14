using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 4;
    [SerializeField] private float attackcool = 0.6f;
    [SerializeField] private float slidecool = 1f;
    public float Hp;
    public float Hpmax = 10f;
    Vector3 dir;

    public Transform player;
    public Slider hpbar;
    public Slider skillbar;

    private Rigidbody2D _rigid;
    public LayerMask a;
    public LayerMask b;
    public bool below;
    public bool Jpad;
    public bool att1 = true;
    public bool attDown = true;


    SpriteRenderer sp;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        Hp = Hpmax;
        sp = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        _rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        below = Physics2D.Raycast(transform.position, Vector2.down, 0.5f, a);
        Jpad = Physics2D.Raycast(transform.position, Vector2.down, 0.5f, b);
        transform.position += dir * speed * Time.deltaTime;

        transform.position = player.position + new Vector3(0, 0, 0);
        hpbar.value = Hp / Hpmax;
        skillbar.value = slidecool / 1f;

        if (Input.GetKeyDown(KeyCode.Space)&& below == true && attDown == true)
        {
            _rigid.velocity = new Vector2(_rigid.velocity.x, 5f);
            StartCoroutine(Jump());
        }
        attackcool += Time.deltaTime;
        slidecool += Time.deltaTime;


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

        if (Input.GetMouseButtonDown(0) && below == true && attackcool >= 0.8f&& att1 == true)
        {
            attDown = false;
            anim.SetTrigger("Attack");
            attackcool = 0;
            StartCoroutine(Attackpos());
        }
        if (Input.GetMouseButtonDown(0) && below == false && attDown == true) //|| Input.GetMouseButtonDown(0) && attDown == true ||Jpad == false//)
        {
            anim.SetBool("Jattack", true);
            StartCoroutine(Att());
            _rigid.gravityScale = 5;
        }
        else if(below == true )//|| Jpad == true)
        {
            _rigid.gravityScale = 1;
            anim.SetBool("Jattack", false);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && slidecool >= 1f)
        {
            
            StartCoroutine(Dodge());
        }
    }

    public void jpad()
    {
        _rigid.velocity = new Vector2(_rigid.velocity.x, 10f);
        Jump();
    }
    

    IEnumerator Jump()
    {
        anim.SetBool("Jump", true);
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("Jump", false);
    }
    IEnumerator Attackpos()
    {
        speed = 0;
        yield return new WaitForSeconds(0.7f);
        speed = 5;
        attDown = true;
    }
    IEnumerator Att()
    {
        att1 = false;
        yield return new WaitForSeconds(0.7f);
        att1 = true;
    }
    IEnumerator Dodge()
    {
        anim.SetTrigger("Dodge");
        slidecool = 0;
        speed = 8;
        yield return new WaitForSeconds(0.5f);
        speed = 5;
    }
}
