using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_UI_Task_element : MonoBehaviour
{
    public GameObject _Main;
    public Image _Icon;
    public Text _Could;


    public void SetTask(S_Task_Element STE_) {
        _Icon.sprite = STE_._Chest._Icon;
        _Could.text = STE_._Could.ToString();
    }


    private void OnDisable() {
        _Main.gameObject.SetActive(false);
        _Icon.gameObject.SetActive(false);
        _Could.gameObject.SetActive(false);

    }







}
