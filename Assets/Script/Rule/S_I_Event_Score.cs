using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface S_I_Event_Score {

    void Event_Task_Finish  (bool Failed_ , int Sc_);
    int GetScore();
    void ResetScore();
}
