using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public double health;
    public float maxHealth;
    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;

    bool isLive = true;

    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriter;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (!isLive)
            return;
        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }

    void LateUpdate()
    {
        if (!isLive)
            return;
        spriter.flipX = target.position.x < rigid.position.x;
    }
        void OnEnable() // ���� Ȱ��ȭ, ������ ü�� �ʱ�ȭ��
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>(); // Ÿ�� ����, ���ӸŴ������� ����
        isLive = true;
        health = maxHealth;

        isLive = true;
        //coll.enabled = true; // ������Ʈ�� ��Ȱ��ȭ�� .enaled = false;
        rigid.simulated = true; // ������ٵ��� ������ ��Ȱ��ȭ�� .simulated = false;
        spriter.sortingOrder = 2;
        //anim.SetBool("Dead", false);
    }

    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }
}