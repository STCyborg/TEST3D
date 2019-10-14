using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Task
{

    List<S_Task_Element> _ListTask = new List<S_Task_Element>();

    float Timer;
    bool _Failed = false;
    int   Reward = 0;


    static public S_Task GetRandomTask() {

        S_Task Tas = new S_Task();

        
        





        S_I_Diff DIFF_ = S_A_GameRoom.GetInstance().GetDiff();
        float MUL_DIF = DIFF_.GetDiffMul();
        float MUL_DIF_M = 2 - MUL_DIF;


        int Could_Task     = ConvertDiff(Random.Range(0, 100) , 100 , MUL_DIF , 3);
        int Max_Chest_Task = ConvertDiff(Random.Range(0, 100) , 100 , MUL_DIF * 1.1f , 4) ;



        List<GameObject> Temp = new List<GameObject>();
        Temp.AddRange(S_A_GameRoom.GetInstance().GetAllTemplate());


        for (int T_ = 0; T_ < Could_Task; T_++) {

            S_Task_Element ST = new S_Task_Element();

            GameObject TT = Temp[Random.Range(0,Temp.Count)];
            Temp.Remove(TT);

            ST._Could = Random.Range(1, Max_Chest_Task);
            ST._Chest = TT.GetComponent<S_A_Chest>();

            Tas.Reward += (int)(ST._Could * 10f * MUL_DIF);
            Tas.Reward =  (int)(Tas.Reward * 1.1f);
            Tas.Timer += ST._Could * 25f * MUL_DIF_M;
            

            Tas._ListTask.Add(ST);

        }

       

        return Tas;
    }


    public float GetTimer() { return Timer; }
    public void TimerAdd(float T_) { Timer += T_; }

    public bool TimeEnding() { return Timer < 10; }
    public float TimerEndingStatus() { return Timer / 10f; }

    public void Failed() { _Failed = true; }
    public bool IsFailed() { return _Failed; }


    public S_Task_Element GetSubTask (int I_) {
        return _ListTask[I_];
    }

    public int CouldSubTask() { return _ListTask.Count; }


    public void DeleteTask(int I_) { _ListTask.RemoveAt(I_); }

    public int GetReward() { return Reward; }


   static private int ConvertDiff (float I_ , float Max_I_ , float Diff_ , float Max_Res_) {

        int L = 1;

        float Ost = Max_I_;
        float Car = 0;
        float NN = Ost * (0.5f * (2 - Diff_));

        Ost -= NN;
        Car += NN;

        while (Car < I_ && L < Max_Res_) {

            NN = Ost * (0.5f * (2 - Diff_));
            Ost -= NN;
            Car += NN;
            L++;

        }

        return L;
    }




}
