using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 4;
    Vector3 dir;

    private Rigidbody2D _rigid;
    public LayerMask a;
    public bool below;


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

        if (Input.GetKeyDown(KeyCode.Space)&& below == true)
        {
            _rigid.velocity = new Vector2(_rigid.velocity.x, 5f);
            StartCoroutine(Jump());
        }


        float x = Input.GetAxisRaw("Horizontal");
        dir = new Vector3(x, 0, 0);
        transform.position += dir * speed * Time.deltaTime;
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
    }

    IEnumerator Jump()
    {
        anim.SetBool("Jump", true);
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("Jump", false);
    }
}
