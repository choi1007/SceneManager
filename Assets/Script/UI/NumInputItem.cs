using UnityEngine;
using UnityEngine.UI;
using Event;

public class NumInputItem : MonoBehaviour
{
    [SerializeField] private Image NumberInputImage;
    [SerializeField] private Text NumberInpuText;

    private int Number;
    private bool Clear;

    private void OnEnable()
    {
        EventAggregator.Instance.Subscribe<EventUI.EventClickClear>(EventClickEvent);
        EventAggregator.Instance.Subscribe<EventUI.EventInputNumCheck>(EventNumberFull);
    }

    private void OnDisable()
    {
        EventAggregator.Instance.Unsubscribe<EventUI.EventClickClear>(EventClickEvent);
        EventAggregator.Instance.Unsubscribe<EventUI.EventInputNumCheck>(EventNumberFull);
    }

    private void EventClickEvent(EventUI.EventClickClear _event)
    {
        if (Clear) return;
        if (_event.Number == Number) return;
        NumberInputImage.color = Color.white;
    }

    private void EventNumberFull(EventUI.EventInputNumCheck _event)
    {
        if (Clear) return;
        if (_event.Number != Number) return;
        Clear = true;
        NumberInputImage.color = Color.black;
    }

    public void InitItem(SquItem[,] _item, int _idx)
    {
        Clear = false;
        Number = _idx;
        NumberInpuText.text = Number.ToString();
        NumberInputImage.color = Color.white;
    }

    public void OnClickInput()
    {
        if (Clear) return;

        var manager = GameManager.Instance;

        if (manager.ClickNum == Number)
        {
            manager.ClickNum = 0;
            NumberInputImage.color = Color.white;
            return;
        }

        manager.ClickNum = Number;
        NumberInputImage.color = Color.gray;
        EventAggregator.Instance.Publish<EventUI.EventClickClear>(new EventUI.EventClickClear() { Number = Number });
    }
}
