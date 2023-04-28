using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс статус-бара
/// </summary>
public class StatusBar : MonoBehaviour
{
    /// <summary>
    /// Объект статус-бара
    /// </summary>
    [SerializeField] Transform bar;

    /// <summary>
    /// Изменить значение статус-бара. При этом изменяет привязанный игровой объект
    /// </summary>
    /// <param name="currentHP">Текущее ХП</param>
    /// <param name="maxHP">Максимальное ХП</param>
    public void SetState(int currentHP, int maxHP){
        float state = (float)currentHP; 
        // Находим долю от ХП
        state /= maxHP;
        //Debug.Log(state);
        if(state < 0f) {state = 0f;}
        // Заполняем ХП-бар на долю текущего ХП
        bar.transform.localScale = new Vector3(state, 1f, 1f);
    }
}
