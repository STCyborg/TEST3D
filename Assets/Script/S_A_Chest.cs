using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class S_A_Chest : MonoBehaviour
{
    [SerializeField] public int  _ID = 0;
    [SerializeField] public bool _Enable = true;
          
    public float Container_Size = 1;
    [SerializeField]public Sprite _Icon; 
    [SerializeField]public float _Mul_Move = 1;


    [SerializeField] Rigidbody _Body;


    abstract public S_A_GrabPosit GetPosit();

    abstract public void Event_Capture();
    abstract public void Event_Free();


    public void FreezeBody() {
        _Body.isKinematic = true;
    }

    public void UnFreezeBody() {
        _Body.isKinematic = false;
    }



    [HideInInspector] protected S_A_Container _Container = null;

    public bool Is_Container() { return _Container != null; }

    public void RegContainer(S_A_Container Con_) {
        _Container = Con_;
    }

    public void DegContainer() {
        _Container.DeleteChest(this);
        _Container = null;
    }





}
