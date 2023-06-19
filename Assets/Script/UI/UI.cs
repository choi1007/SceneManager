using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private Transform Main;
    [SerializeField] private Transform Dialog;

    public static Transform MainPanel { get { return UIObject.Main; } }
    //public static Transform DialogPanel { get { return UIObject.Dialog; } }

    public static UI UIObject;

    private void Awake()
    {
        UIObject = this;
        DialogManager.Instance.OnWake(Dialog);
    }

}
