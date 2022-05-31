using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellController : MonoBehaviour
{

    public int _currentCellState = 0;

    [SerializeField] Animator _winAnimator;

    private bool _isPlayerX = true;
    private GameObject _O;
    private GameObject _X;



    private void Start()
    {
        _O = transform.Find("O").gameObject;
        _X = transform.Find("X").gameObject;
    }
    public void SetCellState(int type)
    {
        switch (type)
        {
            case 0:
                _currentCellState = 0;
                _O.SetActive(false);
                _X.SetActive(false);
                break;
            case 1:
                _currentCellState = 1;
                _O.SetActive(false);
                _X.SetActive(true);
                break;
            case 2:
                _currentCellState = 2;
                _O.SetActive(true);
                _X.SetActive(false);
                break;
        }
    }
    public void PlayerSelectCell()
    {
        GetComponentInParent<GameController>().PlayerStep(gameObject);
    }

    public void Win()
    {
        if(_currentCellState == 1)
        {
            _winAnimator.SetTrigger("WinX");
            _currentCellState = 0;
            Debug.Log("WINX");
        }
        else if(_currentCellState == 2)
        {
            _winAnimator.SetTrigger("WinO");
            _currentCellState = 0;
            Debug.Log("WINO");
        }
    }

}
