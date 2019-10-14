using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class S_UI_SCORE : MonoBehaviour
{


    [SerializeField] Text _Text;
    S_I_Event_Score _Main;



    // Start is called before the first frame update
    void Start(){

        _Main = S_A_GameRoom.GetInstance().ScoreEvent();


    }

    // Update is called once per frame
    void Update(){

        if (_Main != null) {
            _Text.text = "SCORE:"+_Main.GetScore().ToString();
        }

    }



}
