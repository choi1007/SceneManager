using Event;
using UnityEngine;
using UnityEngine.UI;

public class SmallSquItem : MonoBehaviour
{
    [SerializeField] private Text NumText;
    [SerializeField] private Image BackImage;
    [SerializeField] private Button Button;

    private SquItem SquItem;

    public bool RightNum { get; private set; } // 맞는 수 체크.

    private int m_value;
    private bool m_blank;

    private void OnEnable()
    {
        EventAggregator.Instance.Subscribe<EventUI.EventHint>(Hint);
    }

    private void OnDisable()
    {
        EventAggregator.Instance.Unsubscribe<EventUI.EventHint>(Hint);
    }

    private void Hint(EventUI.EventHint _hint)
    {
        if (SquItem.Row != _hint.Row) return;
        if (SquItem.Column != _hint.Column) return;

        Button.enabled = false;
        NumText.text = m_value.ToString();
        NumText.color = Color.magenta; // 힌트로 맞춘건 색상 다르게.
        RightNum = true;
        SquItem.Blank = false;
        if (RightNum) GameManager.Instance.CheckSudokuRule(SquItem);
    }

    public void InitSamllSqu(SquItem _squData)
    {
        SquItem = _squData;
        m_value = SquItem.Value;
        m_blank = SquItem.Blank;
        string _value = m_value.ToString();
        if (m_blank) _value = string.Empty;
        RightNum = m_blank == false;
        Button.enabled = m_blank;
        NumText.text = _value;
        NumText.color = Color.black;
    }

    public void OnClickSqu()
    {
        var _clickNum = GameManager.Instance.ClickNum;
        if (_clickNum == 0) return;
        RightNum = m_value == _clickNum;
        SquItem.Blank = RightNum == false;
        NumText.text = _clickNum.ToString();
        NumText.color = RightNum ? Color.blue : Color.red;
        if (RightNum) GameManager.Instance.CheckSudokuRule(SquItem);
    }
}
