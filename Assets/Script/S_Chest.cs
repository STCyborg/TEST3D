using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Chest : S_A_Chest
{
    [SerializeField] S_A_GrabPosit _Posit;




    public override S_A_GrabPosit GetPosit() {
        if (_Container != null) return _Container.GetPosit();
        return _Posit;
    }



    public bool TIMER_DESTROY = false;


    public override void Event_Capture() {
        if (_Container != null) DegContainer();
        TIMER_DESTROY = false;

        if (_IE_RUN) StopCoroutine(IE_DESTROY());
    }

    public override void Event_Free() {
        TIMER_DESTROY = true;
        if (!_IE_RUN) StartCoroutine(IE_DESTROY());
    }


    bool _IE_RUN = false;
    IEnumerator IE_DESTROY() {
        _IE_RUN = true;


        yield return new WaitForSeconds(1f);


        S_A_GameRoom.GetInstance().DestroyChest(this);
        Destroy(gameObject);

        _IE_RUN = false;
    }





}
