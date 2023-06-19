using System.Collections.Generic;

public class SudokuManager
{
    public SudokuManager(int _level)
    {
        SudokuLevel = _level;
    }

    private int SudokuLevel;

    private SquItem[,] SquArray = new SquItem[9, 9];
    private List<SquItem> SquList = new List<SquItem>();
    private List<SquItem> SquBlankList = new List<SquItem>();

    private List<int> SquNumberList = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    private List<int> SquNumberCompareList = new List<int>();
    
    public SquItem[,] InitSuDoku()
    {
        ArrayInit();
        BackTracking();
        BlankInit();
        return SquArray;
    }

    public List<SquItem> BlankList() => SquBlankList;

    private void ArrayInit()
    {
        for (int _row = 0; _row < 9; ++_row)
        {
            for (int _column = 0; _column < 9; ++_column)
            {
                var _initSquItemData = new SquItem() { Row = _row, Column = _column, Value = 0, Blank = false };
                SquArray[_row, _column] = _initSquItemData;
                SquList.Add(_initSquItemData);
            }
        }
    }

    private bool BackTracking(int _idx = 0)
    {
        if (_idx >= SquList.Count) return true;
        SquItem _item = SquList[_idx];
        CompareListInit();
        for (int i = 0; i < 9; ++i)
        {
            var _value = CompareNumberRandomPick();
            SquNumberCompareList.Remove(_value);
            if (RowCheck(_item.Column, _value) == false) continue;
            if (ColumnCheck(_item.Row, _value) == false) continue;
            if (SmallSquCheck(_item.Row, _item.Column, _value) == false) continue;
            SquArray[_item.Row, _item.Column].Value = _value;
            if (BackTracking(_idx + 1)) return true;
        }
        SquArray[_item.Row, _item.Column].Value = 0;
        return false;
    }

    private void CompareListInit() // 비교값 초기화.
    {
        SquNumberCompareList.Clear();
        SquNumberCompareList.AddRange(SquNumberList);
    }

    /// <summary>
    /// 랜덤으로 값을 넣지 않고 1~9부터 랜덤하게 픽을 뽑아서 값을 넣는다.
    /// 그리고 그 모든 비교값을 다 비교하면 다시 초기화 해서 넣어준다.
    /// </summary>
    /// <returns></returns>
    private int CompareNumberRandomPick()
    {
        var _idx = Util.RandomValue(0, SquNumberCompareList.Count);
        if (_idx >= SquNumberCompareList.Count) CompareListInit(); // 배열 갯수 넘어가면 다시 초기화.
        return SquNumberCompareList[_idx];
    }

    private void BlankInit()
    {
        for (int _row = 0; _row < 9; ++_row)
        {
            for (int _column = 0; _column < 9; ++_column)
            {
                var _rand = Util.RandomValue(0, 10);
                if (_rand < SudokuLevel)
                {
                    SquArray[_row, _column].Blank = true;
                    SquBlankList.Add(SquArray[_row, _column]);
                }
            }
        }
    }

    /// <summary>
    /// 가로체크
    /// </summary>
    /// <param name="_column"></param>
    /// <param name="_num"></param>
    /// <returns></returns>
    private bool RowCheck(int _column, int _num)
    {
        for (int _row = 0; _row < 9; ++_row)
        {
            if (SquArray[_row, _column].Value == _num) return false;
        }
        return true;
    }

    /// <summary>
    /// 세로 체크.
    /// </summary>
    /// <param name="_row"></param>
    /// <param name="_num"></param>
    /// <returns></returns>
    private bool ColumnCheck(int _row, int _num)
    {
        for (int _column = 0; _column < 9; ++_column)
        {
            if (SquArray[_row, _column].Value == _num) return false;
        }
        return true;
    }

    /// <summary>
    /// 작은 네모 체크. 
    /// 스도쿠는 큰 네모와 더불어 각각의 작은 가로도 체크해야됨.
    /// </summary>
    /// <param name="_row"></param>
    /// <param name="_column"></param>
    /// <param name="_num"></param>
    /// <returns></returns>
    private bool SmallSquCheck(int _row, int _column, int _num)
    {
        var _smallSquRow = _row / 3 * 3;
        var _smallSquColumn = _column / 3 * 3;

        for (int i = _smallSquRow; i < _smallSquRow + 3; ++i)
        {
            for (int j = _smallSquColumn; j < _smallSquColumn + 3; j++)
            {
                if (SquArray[i, j].Value == _num) return false;
            }
        }
        return true;
    }
}
