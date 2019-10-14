using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_GameRule : S_I_Event_Score , S_I_Diff{


    int _Score = 0;

    float _Diff = 1;



    public void Event_Task_Finish(bool Failed_ , int Sc_) {
        if (!Failed_) {
            _Score += Sc_;
            if(_Diff < 1.9f) _Diff += Sc_ * 0.001f;           
        }
    }

    public float GetDiffMul() {
        return _Diff;
    }

    public int GetScore() {
        return _Score;
    }

    public void ResetScore() {
        _Score = 0;
    }
}
