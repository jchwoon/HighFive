using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public Transform player;
    public Transform spawnZone;     // 몬스터의 스폰존
    public float chaseRadius = 5f;  // 몬스터가 플레이어를 추적하는 범위(몬스터-플레이어)
    public float returnRadius = 8f; // 몬스터가 스폰존으로 돌아가는 범위(몬스터-스폰존)
    public float moveSpeed = 1f;
    

    public bool isChasing = false;
    public bool isStopped;
    private Animator anim;


    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }


    void Update()
    {
        MoveProcess();
        
    }


    void MoveProcess()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        float distanceToSpawnZone = Vector2.Distance(transform.position, spawnZone.position);

        if (distanceToPlayer < chaseRadius) // 플레이어랑 거리가 좁을때
        {
            if (isChasing && distanceToSpawnZone > returnRadius)
            {
                //isStopped = false; isChasing = false;
                ReturnToSpawnZone();
            }
            //isStopped = false; isChasing = true;
            ChasePlayer();
        }
        else if (distanceToSpawnZone > 0.1)
        {
            //isStopped = false; isChasing = false;
            ReturnToSpawnZone();
        }
        else
        {
            //isStopped = true;
        }
    }


    void ChasePlayer()
    {
        // 플레이어를 향해 이동하는 로직을 구현
        Vector2 direction = (player.position - transform.position).normalized;
        transform.Translate(direction * Time.deltaTime * moveSpeed);
        float angle = Mathf.Atan2(player.position.y - transform.position.y, player.position.x - transform.position.x) * Mathf.Rad2Deg;
        SetAnimationDirection(angle);
    }


    void ReturnToSpawnZone()
    {
        // 스폰존으로 돌아가는 로직을 구현
        Vector2 direction = (spawnZone.position - transform.position).normalized;
        transform.Translate(direction * Time.deltaTime * moveSpeed);
        float angle = Mathf.Atan2(spawnZone.position.y - transform.position.y, spawnZone.position.x - transform.position.x) * Mathf.Rad2Deg;
        SetAnimationDirection(angle);
    }

    // 이동 방향에 따라 애니메이션 설정
    void SetAnimationDirection(float angle)
    {
        if (angle > 45 && angle <= 135)
        {
            // 위로 이동하는 애니메이션
            anim.SetBool("isDown", false);
            anim.SetBool("isLeft", false);
            anim.SetBool("isRight", false);
            anim.SetBool("isUp", true);
        }
        else if (angle > 135 && angle <= 225)
        {
            // 왼쪽으로 이동하는 애니메이션
            anim.SetBool("isUp", false);
            anim.SetBool("isDown", false);
            anim.SetBool("isRight", false);
            anim.SetBool("isLeft", true);
        }
        else if (angle > 225 && angle <= 315)
        {
            // 아래로 이동하는 애니메이션
            anim.SetBool("isUp", false);
            anim.SetBool("isLeft", false);
            anim.SetBool("isRight", false);
            anim.SetBool("isDown", true);
        }
        else
        {
            // 오른쪽으로 이동하는 애니메이션
            anim.SetBool("isUp", false);
            anim.SetBool("isDown", false);
            anim.SetBool("isLeft", false);
            anim.SetBool("isRight", true);
        }
    }
}
