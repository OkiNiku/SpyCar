using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpyCarUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI stateText;
 
    public void UpdateToCurrentState(IState state)
    {
        stateText.text = state.GetType().Name;
    }
}
