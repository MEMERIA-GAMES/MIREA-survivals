using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] int healAmount;

    void OnTriggerEnter2D(Collider2D collision){
        //Debug.Log("Let`s heal da hole!");
        Character c = collision.GetComponent<Character>();

        if (c != null){
            c.Heal(healAmount);
            Destroy(gameObject);
        }    
    }
}
