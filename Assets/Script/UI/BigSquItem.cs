using UnityEngine;
using System.Collections.Generic;

public class BigSquItem : MonoBehaviour
{
    private bool Create = false;

    private List<SmallSquItem> SmallSquItemList = new List<SmallSquItem>(9);

    /// <summary>
    /// idx = 0 ~ 8
    /// </summary>
    /// <param name="_item"></param>
    /// <param name="_idx"></param>
    public void InitBigSqu(SquItem[,] _item, int _idx)
    {
        int count = 0;
        int _row = _idx / 3 * 3;
        int _column = _idx % 3 * 3;
        for (int i = _row; i < _row + 3; ++i) 
        {
            for (int j = _column; j < _column + 3; ++j)
            {
                if (Create == false) CreateSmallSquItem(_item[i, j]);
                else RefreshSmallSquItem(_item[i, j], ref count);
            }
        }

        Create = true;
    }

    private void CreateSmallSquItem(SquItem _item)
    {
        var _createSmallSqu = Util.GetPrefab<SmallSquItem>("Prefabs/Item/SmallSquItem");
        _createSmallSqu.InitSamllSqu(_item);
        _createSmallSqu.transform.SetParent(transform, false);
        SmallSquItemList.Add(_createSmallSqu);
    }

    private void RefreshSmallSquItem(SquItem _item, ref int _count)
    {
        SmallSquItemList[_count].InitSamllSqu(_item);
        _count++;
    }
}
