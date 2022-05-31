using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BotController : MonoBehaviour
{
    private GameObject[] _cellCopy;
    public int _stepsPassed = 0;

    private void Start()
    {
        _cellCopy = GetComponent<GameController>()._cells;
    }
    public int BotAI()
    {
        int[] boardValues = new int[9];
        long[] evaluatedValues = new long[9];

        for (int i = 0; i < 9; i++)
        {
            boardValues[i] = _cellCopy[i].GetComponent<CellController>()._currentCellState;
        }

        for(int i = 0; i<9; i++)
        {
            evaluatedValues[i] = -10000;
            if(boardValues[i] == 0)
            {
                evaluatedValues[i] = EvaluateMove(boardValues, 9 - _stepsPassed, 2, i);

                if (CheckIfNextStepIsWin(boardValues, i))
                    evaluatedValues[i] = 10000;
                else if (CheckIfNextStepIsLose(boardValues, i))
                    evaluatedValues[i] = 5000;
            }

        }

        long bestMoveValue = evaluatedValues.Max();
        int bestMoveIndex = evaluatedValues.ToList().IndexOf(bestMoveValue);

        _stepsPassed++;
        return bestMoveIndex;
    }

    private long EvaluateMove(int[] cells, int deph, int player, int move)
    {
        cells[move] = player;

        if(
            (cells[0] == 1 && cells[1] == 1 && cells[2] == 1)||
            (cells[3] == 1 && cells[4] == 1 && cells[5] == 1)||
            (cells[6] == 1 && cells[7] == 1 && cells[8] == 1)||
            (cells[0] == 1 && cells[4] == 1 && cells[8] == 1)||
            (cells[2] == 1 && cells[4] == 1 && cells[6] == 1)||
            (cells[0] == 1 && cells[3] == 1 && cells[6] == 1)||
            (cells[1] == 1 && cells[4] == 1 && cells[7] == 1)||
            (cells[2] == 1 && cells[5] == 1 && cells[8] == 1)
           )
        {
            cells[move] = 0;
            return -5;
        }

        if (
            (cells[0] == 2 && cells[1] == 2 && cells[2] == 2) ||
            (cells[3] == 2 && cells[4] == 2 && cells[5] == 2) ||
            (cells[6] == 2 && cells[7] == 2 && cells[8] == 2) ||
            (cells[0] == 2 && cells[4] == 2 && cells[8] == 2) ||
            (cells[2] == 2 && cells[4] == 2 && cells[6] == 2) ||
            (cells[0] == 2 && cells[3] == 2 && cells[6] == 2) ||
            (cells[1] == 2 && cells[4] == 2 && cells[7] == 2) ||
            (cells[2] == 2 && cells[5] == 2 && cells[8] == 2)
           )
        {
            cells[move] = 0;
            return 1 ;
        }

        if (deph == 0)
        {
            cells[move] = 0;
            return 0;
        }

        long sum = 0;
        for(int i = 0; i<9; i++)
        {
            if (cells[i] == 0)
            {
                if (player == 2)
                {
                    sum += EvaluateMove(cells, deph - 1, 1, i);
                }

                if (player == 1)
                {
                    sum += EvaluateMove(cells, deph - 1, 2, i);
                }
            }   
        }
        cells[move] = 0;
        return sum;
    }

    private bool CheckIfNextStepIsWin(int[] cells, int i)
    {
        cells[i] = 2;
        if (
            (cells[0] == 2 && cells[1] == 2 && cells[2] == 2) ||
            (cells[3] == 2 && cells[4] == 2 && cells[5] == 2) ||
            (cells[6] == 2 && cells[7] == 2 && cells[8] == 2) ||
            (cells[0] == 2 && cells[4] == 2 && cells[8] == 2) ||
            (cells[2] == 2 && cells[4] == 2 && cells[6] == 2) ||
            (cells[0] == 2 && cells[3] == 2 && cells[6] == 2) ||
            (cells[1] == 2 && cells[4] == 2 && cells[7] == 2) ||
            (cells[2] == 2 && cells[5] == 2 && cells[8] == 2)
           )
        {
            cells[i] = 0;
            return true;
        }
        else
        {
            cells[i] = 0;
            return false;
        }
    }
    private bool CheckIfNextStepIsLose(int[] cells, int i)
    {
        cells[i] = 1;
        if (
            (cells[0] == 1 && cells[1] == 1 && cells[2] == 1) ||
            (cells[3] == 1 && cells[4] == 1 && cells[5] == 1) ||
            (cells[6] == 1 && cells[7] == 1 && cells[8] == 1) ||
            (cells[0] == 1 && cells[4] == 1 && cells[8] == 1) ||
            (cells[2] == 1 && cells[4] == 1 && cells[6] == 1) ||
            (cells[0] == 1 && cells[3] == 1 && cells[6] == 1) ||
            (cells[1] == 1 && cells[4] == 1 && cells[7] == 1) ||
            (cells[2] == 1 && cells[5] == 1 && cells[8] == 1)
           )
        {
            cells[i] = 0;
            return true;
        }
        else
        {
            cells[i] = 0;
            return false;
        }
    }

}
