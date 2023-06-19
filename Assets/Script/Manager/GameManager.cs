using Event;
using UnityEngine;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager>
{
    public int ClickNum;

    public int SudokuLevel = 1; // 1부터 시작해야됨. 0이면 모두 클리어로 시작.

    private MainUI m_mainUI;
    private SquItem[,] SquArray = new SquItem[9, 9];
    private List<SquItem> SquBlankList = new List<SquItem>(9);

    public void OnWake() { }

    public void GameStart() => InitData();

    /// <summary>
    /// 데이터 초기화.
    /// </summary>
    private void InitData()
    {
        ClickNum = 0;
        SudokuManager sudokuManager = new SudokuManager(SudokuLevel);
        SquArray = sudokuManager.InitSuDoku();
        SquBlankList = sudokuManager.BlankList();

        if (m_mainUI != null)
        {
            m_mainUI.InitUI(SquArray);
            NumberInputCheck();
            return;
        }

        m_mainUI = Util.GetPrefab<MainUI>("Prefabs/UI/MainUI");
        m_mainUI.transform.SetParent(UI.MainPanel, false);
        m_mainUI.InitUI(SquArray);
        NumberInputCheck();
    }

    public void CheckSudokuRule(SquItem _item)
    {
        var _squItem = SquBlankList.Find(x => x.Row == _item.Row && x.Column == _item.Column);
        if (_squItem != null) _squItem.Blank = false;
        NumberInputCheck();

        // 모두 다 채움,
        if (SquBlankList.Find(x => x.Blank == true) == null) ClearGameDialog.DoModal();
    }

    private void NumberInputCheck()
    {
        for(int _num = 1; _num <= 9; ++_num)
        {
            var _numItem = SquBlankList.FindAll(x => x.Blank && x.Value == _num);
            if (_numItem.Count == 0) EventAggregator.Instance.Publish<EventUI.EventInputNumCheck>(new EventUI.EventInputNumCheck() { Number = _num });
        }
    }

    public void BlankHint()
    {
        var _squBlankList = SquBlankList.FindAll(x => x.Blank);
        var _randIdx = Util.RandomValue(0, _squBlankList.Count);
        var _squItem = _squBlankList[_randIdx];
        EventAggregator.Instance.Publish<EventUI.EventHint>(new EventUI.EventHint() { Row = _squItem.Row, Column = _squItem.Column });
    }
}
