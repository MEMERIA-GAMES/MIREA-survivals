using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whip : MonoBehaviour
{
    [SerializeField] float timeToAttack = 4f;
    float timer = 0f;

    [SerializeField] GameObject leftWhipObject;
    [SerializeField] GameObject rightWhipObject;
    
    PlayerMove playerMove;
    [SerializeField] Vector2 whipAttackSize = new Vector2(4f, 2f);
    [SerializeField] int whipDamage = 1;

    private void Awake(){
        playerMove = GetComponentInParent<PlayerMove>();
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0f){
            Attack();
        }
    }

    private void Attack(){
        //Debug.Log("Whip Attack");
        timer = timeToAttack;

        if(playerMove.lastHorizontalVector > 0){
            rightWhipObject.SetActive(true);
            Collider2D[] colliders = Physics2D.OverlapBoxAll(rightWhipObject.transform.position, whipAttackSize, 0f);
            ApplyDamage(colliders);
        }else{
            leftWhipObject.SetActive(true);
            Collider2D[] colliders = Physics2D.OverlapBoxAll(leftWhipObject.transform.position, whipAttackSize, 0f);
            ApplyDamage(colliders);
        }
    }

    private void ApplyDamage(Collider2D[] colliders){
        for(int i = 0; i<colliders.Length; ++i){
            //Debug.Log(colliders[i].gameObject.name);
            IDamagable e = colliders[i].GetComponent<IDamagable>();
            if (e != null){
                e.TakeDamage(whipDamage);
            }
        }
    }
}
