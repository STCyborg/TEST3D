using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class S_A_Container :MonoBehaviour {

    [SerializeField]protected MonoBehaviour _TaskEvent;
    public S_Task_Event _Task_Event_M;

    abstract public S_A_GrabPosit GetPosit();

    abstract public void AddChest(S_A_Chest Chest_);
    abstract public void DeleteChest(S_A_Chest Ch_);




    private void OnValidate() {
        if(_TaskEvent != null) _Task_Event_M = _TaskEvent.GetComponent<S_Task_Event>();
        if (_Task_Event_M == null) _TaskEvent = null;
    }


}
