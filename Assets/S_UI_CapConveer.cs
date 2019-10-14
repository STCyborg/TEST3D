using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_UI_CapConveer : MonoBehaviour
{

    [SerializeField] GameObject _TAGET_OBJ;
    [SerializeField] S_A_Hand_Controler _Handcontll;

    S_A_GameRoom _Room;

    private void Start() {

        _Room = S_A_GameRoom.GetInstance();

    }



    float _Time = 0;

    void Update(){
        _Time += Time.deltaTime;
        if (_Time < 0.1f) return;
        _Time = 0;

        if (_Handcontll && _Handcontll.Is_Grabed() && _Handcontll.Is_Ready()) {
           S_Conveer CV =  _Room.GetNearCoveers(transform.position , transform.forward);
           if(CV != null) {
                _TAGET_OBJ.transform.position = CV._Point_Posit.transform.position;
                SET_ACT_TAGET(true);
                return;
            }
        }

        SET_ACT_TAGET(false);
    }


         


    bool _Hash_Taget = false;
    private void SET_ACT_TAGET(bool ACT_) {
        if(ACT_ != _Hash_Taget) {
            _Hash_Taget = ACT_;
            _TAGET_OBJ.SetActive(_Hash_Taget);
        }
    }


}
