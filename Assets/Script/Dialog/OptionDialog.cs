using UnityEngine;
using UnityEngine.UI;

public class OptionDialog : DialogBase
{
    [SerializeField] private Slider LevelSlider;
    [SerializeField] private Text LevelText;

    public static OptionDialog DoModal()
    {
        OptionDialog Dialog = Util.GetPrefab<OptionDialog>("Prefabs/Dialog/OptionDialog");
        Dialog.Show();
        return Dialog;
    }

    protected override void OnShow()
    {
        LevelSlider.value = GameManager.Instance.SudokuLevel;
    }

    public void OnClickRestart()
    {
        GameManager.Instance.GameStart();
        Hide();
    }

    public void OnClickLevelSlider()
    {
        LevelText.text = $"LEVEL {LevelSlider.value.ToString()}";
        GameManager.Instance.SudokuLevel = (int)LevelSlider.value;
    }
}
