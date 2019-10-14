using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Task_Ligth : MonoBehaviour
{

    [SerializeField] Light _Ligth_red;
    [SerializeField] Light _Ligth_Blue;

    [SerializeField] GameObject _TaskSource;
    private S_I_TaskSource _TaskSource_M;

    private void OnValidate() {
        if (_TaskSource != null) _TaskSource_M = _TaskSource.GetComponent<S_I_TaskSource>();
        if (_TaskSource_M == null) _TaskSource = null;
    }

    private void Update() {

        if (_TaskSource_M != null) {

            S_Task ST = _TaskSource_M.GetTask();
            if(ST != null) {

                if (ST.TimeEnding()) {
                    _Ligth_red.enabled = true;
                    _Ligth_Blue.enabled = false;
                    return;
                }

                _Ligth_Blue.enabled = true;
                _Ligth_red.enabled = false;
                return;
            }

            _Ligth_Blue.enabled = false;
            _Ligth_red.enabled = false;

        }

        _Ligth_Blue.enabled = false;
        _Ligth_red.enabled = false;

    }


 


}
