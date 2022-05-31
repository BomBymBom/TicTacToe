using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject _winLosePanel;
    [SerializeField] GameObject _winPanel;
    [SerializeField] GameObject _losePanel;

    //bottomPanel
    [SerializeField] Image _stepPlayerIcon;
    [SerializeField] Image _stepCompIcon;
    [SerializeField] GameObject _bottomPanel;

    [SerializeField] Animator _AddPointAnimation;

    [SerializeField] Text _winCompText;
    [SerializeField] Text _winPlayerText;
    [SerializeField] Text _drawText;

    int _winComp = 0;
    int _winPlayer = 0;
    int _draw = 0;

    public GameObject[] _cells;
    public bool _isPlayerXStep = true;

    private bool _winX;
    private bool _winO;
    private bool _beginGameX = true;


    public void ResetCells()
    {
        for (int i = 0; i < 9; i++)
        {
            _cells[i].GetComponent<CellController>().SetCellState(0);
        }
        GetComponent<BotController>()._stepsPassed = 0;

        _isPlayerXStep = _beginGameX = !_beginGameX;

        _bottomPanel.GetComponent<BottomPanelManager>().DeactivateRestart();
        StepController();
    }

    IEnumerator WaitAndResetCells(float time)
    {
        yield return new WaitForSeconds(time);
        ResetCells();
    }

    private IEnumerator WinAnimation(int player)
    {
        int[] cellsState =new int[9];
        for(int i=0; i<9; i++)
        {
            cellsState[i] = _cells[i].GetComponent<CellController>()._currentCellState;
        }


        if (cellsState[0] == player && cellsState[1] == player && cellsState[2] == player) 
        { _cells[0].GetComponent<CellController>().Win(); _cells[1].GetComponent<CellController>().Win(); _cells[2].GetComponent<CellController>().Win(); }

        else if (cellsState[3] == player && cellsState[4] == player && cellsState[5] == player)
        { _cells[3].GetComponent<CellController>().Win(); _cells[4].GetComponent<CellController>().Win(); _cells[5].GetComponent<CellController>().Win(); }

        else if (cellsState[6] == player && cellsState[7] == player && cellsState[8] == player)
        { _cells[6].GetComponent<CellController>().Win(); _cells[7].GetComponent<CellController>().Win(); _cells[8].GetComponent<CellController>().Win(); }

        else if (cellsState[0] == player && cellsState[4] == player && cellsState[8] == player)
        { _cells[0].GetComponent<CellController>().Win(); _cells[4].GetComponent<CellController>().Win(); _cells[8].GetComponent<CellController>().Win(); }

        else if (cellsState[2] == player && cellsState[4] == player && cellsState[6] == player)
        { _cells[2].GetComponent<CellController>().Win(); _cells[4].GetComponent<CellController>().Win(); _cells[6].GetComponent<CellController>().Win(); }

        else if (cellsState[0] == player && cellsState[3] == player && cellsState[6] == player)
        { _cells[0].GetComponent<CellController>().Win(); _cells[3].GetComponent<CellController>().Win(); _cells[6].GetComponent<CellController>().Win(); }

        else if (cellsState[1] == player && cellsState[4] == player && cellsState[7] == player)
        { _cells[1].GetComponent<CellController>().Win(); _cells[4].GetComponent<CellController>().Win(); _cells[7].GetComponent<CellController>().Win(); }

        else if (cellsState[2] == player && cellsState[5] == player && cellsState[8] == player)
        { _cells[2].GetComponent<CellController>().Win(); _cells[5].GetComponent<CellController>().Win(); _cells[8].GetComponent<CellController>().Win(); }

        _bottomPanel.GetComponent<BottomPanelManager>().ActivateRestart();
        yield return new WaitForSeconds(1f);

    }

    public void DeactivateWinLosePanel()
    {
        _winLosePanel.SetActive(false);

        _winPanel.SetActive(false);
        _losePanel.SetActive(false);
    }

    private bool CheckWin(int player)
    {
        int[] cellsState1 = new int[9];
        for (int i = 0; i < 9; i++)
        {
            cellsState1[i] = _cells[i].GetComponent<CellController>()._currentCellState;
        }
        if (
            (cellsState1[0] == player && cellsState1[1] == player && cellsState1[2] == player) ||
            (cellsState1[3] == player && cellsState1[4] == player && cellsState1[5] == player) ||
            (cellsState1[6] == player && cellsState1[7] == player && cellsState1[8] == player) ||
            (cellsState1[0] == player && cellsState1[4] == player && cellsState1[8] == player) ||
            (cellsState1[2] == player && cellsState1[4] == player && cellsState1[6] == player) ||
            (cellsState1[0] == player && cellsState1[3] == player && cellsState1[6] == player) ||
            (cellsState1[1] == player && cellsState1[4] == player && cellsState1[7] == player) ||
            (cellsState1[2] == player && cellsState1[5] == player && cellsState1[8] == player)
           )
        {
            return true;
        }
        else return false;
    }


    private void StepController()
    {
        if (CheckWin(2)) {
            _isPlayerXStep = false;
            _winComp += 1;
            _winCompText.text = _winComp.ToString();
            _AddPointAnimation.SetTrigger("CompWin");
            StartCoroutine(WinAnimation(2));

        }
        else if (CheckWin(1))
        {
            _isPlayerXStep = false;
            _winPlayer += 1;
            _winPlayerText.text = _winPlayer.ToString();
            _AddPointAnimation.SetTrigger("PlayerWin");
            StartCoroutine(WinAnimation(1));
        }
        else if (GetComponent<BotController>()._stepsPassed == 9)
        {
            _AddPointAnimation.SetTrigger("Draw");
            _draw += 1;
            _drawText.text = _draw.ToString();
            _bottomPanel.GetComponent<BottomPanelManager>().ActivateRestart();
        }
        else if (!_isPlayerXStep)
        {
            _stepPlayerIcon.color = new Color(0.5f, 0.5f, 0.5f);
            _stepCompIcon.color = new Color(1f, 1f, 1f);
            StartCoroutine(BotStep(0.7f));
        }
        else
        {
            _stepPlayerIcon.color = new Color(1f, 1f, 1f);
            _stepCompIcon.color= new Color(0.5f, 0.5f, 0.5f);
        }
    }


    IEnumerator  BotStep(float time)
    {
        int i = GetComponent<BotController>().BotAI();
        
        yield return new WaitForSeconds(time);

        if (_cells[i].GetComponent<CellController>()._currentCellState == 0)
            _cells[i].GetComponent<CellController>().SetCellState(2);
        _isPlayerXStep = true;

        StepController();
    }


    public void PlayerStep(GameObject cell)
    {

        if (_isPlayerXStep)
        {
            CellController clickedCell;
            clickedCell = cell.GetComponent<CellController>();

            if(clickedCell._currentCellState == 0 )
            {
                clickedCell.SetCellState(1);
                GetComponent<BotController>()._stepsPassed += 1;
                _isPlayerXStep = false;
                StepController();
            }
        }
    }

}
