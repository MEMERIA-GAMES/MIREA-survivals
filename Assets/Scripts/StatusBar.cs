using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusBar : MonoBehaviour
{
    [SerializeField] Transform bar;

    public void SetState(int currentHP, int maxHP){
        float state = (float)currentHP; 
        state /= maxHP;
        //Debug.Log(state);
        if(state < 0f) {state = 0f;}
        bar.transform.localScale = new Vector3(state, 1f, 1f);
    }
}
