using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_UI_TASK : MonoBehaviour , S_I_Vis_Task {
    [SerializeField] Animator _Anim;

    [SerializeField] float OffsetY = 0;

    [SerializeField] GameObject _TaskSource;
    private S_I_TaskSource _TaskSource_M;

    [SerializeField] S_UI_Task_element _Task_E1;
    [SerializeField] S_UI_Task_element _Task_E2;
    [SerializeField] S_UI_Task_element _Task_E3;

    [SerializeField] GameObject _Container_Timer;
    [SerializeField] Image     _Sprite_Timer;


    int ANIM_ID_COULD;
    int ANIM_ID_Failed;


    private void OnValidate() {
        if (_TaskSource != null) _TaskSource_M = _TaskSource.GetComponent<S_I_TaskSource>();
        if (_TaskSource_M == null) _TaskSource = null;
    }

    


    private void Start() {

        ANIM_ID_COULD = Animator.StringToHash("Could_Task");
        ANIM_ID_Failed = Animator.StringToHash("Failed");

        _Container_Timer.SetActive(false);

    }

    private void Update() {
        
         if(_TaskSource_M != null) {





            S_Task ST = _TaskSource_M.GetTask();
            if(ST != null) {

                if (ST.TimeEnding()) {
                    SET_CONT_ACT(true);
                    _Sprite_Timer.fillAmount = ST.TimerEndingStatus();
                } else SET_CONT_ACT(false);


                _Anim.SetBool(ANIM_ID_Failed , ST.IsFailed());

                int CouldSub = ST.CouldSubTask();
                _Anim.SetInteger(ANIM_ID_COULD , CouldSub);
                
                if (CouldSub > 0) _Task_E1.SetTask(ST.GetSubTask(0));
                if (CouldSub > 1) _Task_E2.SetTask(ST.GetSubTask(1));
                if (CouldSub > 2) _Task_E3.SetTask(ST.GetSubTask(2));

                return;
            }

            _Anim.SetInteger(ANIM_ID_COULD, 0);
            SET_CONT_ACT(false);
            return;
        }



        _Anim.SetInteger(ANIM_ID_COULD, 0);
        SET_CONT_ACT(false);
    }


    bool _HASH_TIMER_ACT = false;
    private void SET_CONT_ACT (bool ACT_) {
        if(_HASH_TIMER_ACT != ACT_) {
            _HASH_TIMER_ACT = ACT_;
            _Container_Timer.SetActive(_HASH_TIMER_ACT);
        }
    }

    public void SetSourceTask(S_I_TaskSource Task_) {
        _TaskSource_M = Task_;
        if (_TaskSource_M != null) {
            Vector3 Posit_Task = Camera.main.WorldToScreenPoint(_TaskSource_M.GetPosit() + Vector3.up * OffsetY);
            transform.position = Posit_Task;
        }
    }



}
