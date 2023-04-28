using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс, управляющий временны отключением врагов 
/// </summary>
public class DisableAfterTime : MonoBehaviour
{
    /// <summary>
    /// Время дизейбла
    /// </summary>
    [SerializeField] float timeToDisable = 0.8f;
    float timer;

    /// <summary>
    /// Когда объект становится активным, обновляется таймер
    /// </summary>
    private void OnEnable(){
        timer = timeToDisable;
    }

    /// <summary>
    /// В конце каждого кадра идет отсчет таймер, и когда он дойдет до нуля, объект отключится.
    /// </summary>
    private void LateUpdate(){
        timer -= Time.deltaTime;
        if (timer < 0f){
            gameObject.SetActive(false);
        }
    }
}
