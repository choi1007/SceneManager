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

    private void CompareListInit() // �񱳰� �ʱ�ȭ.
    {
        SquNumberCompareList.Clear();
        SquNumberCompareList.AddRange(SquNumberList);
    }

    /// <summary>
    /// �������� ���� ���� �ʰ� 1~9���� �����ϰ� ���� �̾Ƽ� ���� �ִ´�.
    /// �׸��� �� ��� �񱳰��� �� ���ϸ� �ٽ� �ʱ�ȭ �ؼ� �־��ش�.
    /// </summary>
    /// <returns></returns>
    private int CompareNumberRandomPick()
    {
        var _idx = Util.RandomValue(0, SquNumberCompareList.Count);
        if (_idx >= SquNumberCompareList.Count) CompareListInit(); // �迭 ���� �Ѿ�� �ٽ� �ʱ�ȭ.
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
    /// ����üũ
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
    /// ���� üũ.
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
    /// ���� �׸� üũ. 
    /// ������� ū �׸�� ���Ҿ� ������ ���� ���ε� üũ�ؾߵ�.
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
