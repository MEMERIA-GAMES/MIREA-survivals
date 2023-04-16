using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    Transform targetDestination;
    GameObject targetGameobject;
    Character targetCharacter;
    [SerializeField] float speed;

    Rigidbody2D rgdbd2d;

    [SerializeField] int hp = 999;
    [SerializeField] int damage = 1;

    private void Awake(){
        rgdbd2d = GetComponent<Rigidbody2D>();
    }

    public void SetTarget(GameObject target){
        targetGameobject = target;
        targetDestination = target.transform;
    }

    private void FixedUpdate(){
        Vector3 direction = (targetDestination.position - transform.position).normalized;
        rgdbd2d.velocity = direction * speed;
    }

    private void OnCollisionStay2D(Collision2D collision){
        if(collision.gameObject == targetGameobject){
            Attack();
        }
    }

    private void Attack(){
        //.Debug.Log("Attacking the character");
        if(targetCharacter == null) 
            targetCharacter = targetGameobject.GetComponent<Character>();
        
        targetCharacter.TakeDamage(damage);
    }

    public void TakeDamage(int damage){
        hp -= damage;

        if (hp <= 0){
            Destroy(gameObject);
        }
    }
}
