using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObject : MonoBehaviour, IDamagable
{
    public void TakeDamage(int damage){
        //Debug.Log("Ouch");
        Destroy(gameObject);
    }
}
