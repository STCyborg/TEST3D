using System;
using UnityEngine;
using UnityEngine.UI;

public class S_UI_TIMER : MonoBehaviour
{

    [SerializeField] Text _Text;
    [SerializeField] GameObject _Timer;
    S_I_TimerInfo _TimerInfo;

    [SerializeField] Color _Color_Def;
    [SerializeField] Color _Color_Fin;


    private void OnValidate() {

        if (_Timer != null) _TimerInfo = _Timer.GetComponent<S_I_TimerInfo>();
        if (_TimerInfo == null) _Timer = null;

    }


    // Update is called once per frame
    void Update()
    {
        

        if (_TimerInfo != null) {
            int T = _TimerInfo.GetGameSecond();
            TimeSpan TP = TimeSpan.FromSeconds(T);
            _Text.text = TP.ToString(@"m\:ss");           

            if (T > 15) _Text.color = _Color_Def; else _Text.color = _Color_Fin;
        }
      
     

    }



}
