using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomPanelManager : MonoBehaviour
{
    //bottomPanel
    [SerializeField] GameObject _stepPlayerIcon;
    [SerializeField] GameObject _stepCompIcon;
    [SerializeField] GameObject _restart;

    public void ActivateRestart()
    {
        _stepCompIcon.SetActive(false);
        _stepPlayerIcon.SetActive(false);
        _restart.SetActive(true);
    }

    public void DeactivateRestart()
    {
        _stepCompIcon.SetActive(true);
        _stepPlayerIcon.SetActive(true);
        _restart.SetActive(false);
    }
}