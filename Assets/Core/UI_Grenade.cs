using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UI_Grenade : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private int currentGrenadeCount=0;
    public void SetUpGrenadeUI(int _value )
    {
        currentGrenadeCount = _value;
        scoreText.text = currentGrenadeCount.ToString();
    }
    public void UpdateGrenadeUI(int _value)
    {
        scoreText.text = _value.ToString();

    }

}
