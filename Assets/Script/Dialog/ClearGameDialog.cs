using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearGameDialog : DialogBase
{
    [SerializeField] private List<Transform> StarList;

    private float RotY;

    public static ClearGameDialog DoModal()
    {
        ClearGameDialog Dialog = Util.GetPrefab<ClearGameDialog>("Prefabs/Dialog/ClearGameDialog");
        Dialog.Show();
        return Dialog;
    }

    // Update is called once per frame
    void Update()
    {
        RotY += Time.deltaTime * 80f;

        for (int _star = 0; _star < StarList.Count; ++_star)
        {
            StarList[_star].transform.rotation = Quaternion.Euler(0, RotY, 0);
        }
    }

    public void OnClickRegame()
    {
        GameManager.Instance.GameStart();
        Hide();
    }
}
