using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init : MonoBehaviour
{
    [SerializeField] private GameObject StartMenu;
    [SerializeField] private List<GameObject> SdqTextList;

    private void Awake()
    {
        GameManager.Instance.OnWake();
    }

    private void Start()
    {
        for(int _squText = 0; _squText < SdqTextList.Count; ++_squText)
        {
            var rand = Util.RandomValue(0, 3);
            SdqTextList[_squText].SetActive(rand > 0);
        }
    }

    public void OnClickGameStart()
    {
        Destroy(StartMenu);
        GameManager.Instance.GameStart();
    }
}
