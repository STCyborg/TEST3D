using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Hand_Controler : S_A_Hand_Controler
{

    [SerializeField] Animator _Animator;
    [SerializeField] Rigidbody _Body;
    [SerializeField] GameObject _Cont_Chest;
    

    S_Conveer _TAGET_CONV;
    GameObject _TAGET_CONV_POSIT;
    GameObject _TAGET_CONV_NAP;

    S_A_Chest _TAGET_Chest;
    GameObject  _TAGET_POSIT;
    GameObject  _TAGET_NAP;

    public override bool Is_Ready() {
        return !_IE_Grabling;
    }

    public override bool Is_Grabed() {
        return _Is_Grab;
    }

    public override bool Is_Freeze() {
        return _Is_Freeze;
    }

    public override void Object_Grab() {
        if (_IE_Grabling) return;
        S_ResultNearPosit Posit = S_A_GameRoom.GetInstance().GetNearChest(transform.position);             
        if (Posit == null) return;

        _TAGET_Chest = Posit._Chest;
        _TAGET_POSIT = Posit._TAGET;
        _TAGET_NAP   = Posit._NAP;

        StartCoroutine(IE_GRABLING());
    }

    public override void Object_Drop() {
        if (_IE_Grabling) return;
                
        _TAGET_CONV = S_A_GameRoom.GetInstance().GetNearCoveers(transform.position , transform.forward);

        if (_TAGET_CONV) {
            GameObject[] POSITS = _TAGET_CONV.GetPosit().GetNearPosit(transform.position);
            _TAGET_CONV_POSIT = POSITS[0];
            _TAGET_CONV_NAP = POSITS[1];
            StartCoroutine(IE_LAY());
            return;
        }

        StartCoroutine(IE_DROP());
    }


    bool _Is_Freeze = false;
    bool _Is_Grab = false;
    bool _Is_Capture = false;

    bool _IE_Grabling = false;


    private Vector2 C_V2(Vector3 V_) { return new Vector2(V_.x , V_.z); }


    IEnumerator IE_GRABLING() {
        _IE_Grabling = true;

        RigidbodyConstraints RC = _Body.constraints;
        _Body.constraints = RigidbodyConstraints.FreezeAll;


        yield return new WaitForSeconds(0.1f);

        S_PositMove MP = new S_PositMove(transform , _TAGET_POSIT , _TAGET_NAP);
        while (MP._IS_Final()) {
            MP.MOVES();

            if (MP._Is_Break()) {
                _IE_Grabling = false;
                _Body.constraints = RC;
                yield break;
            }

            yield return new WaitForSeconds(0.01f);
        }
        
        _Is_Freeze = true;
        yield return new WaitForSeconds(0.1f); 



        MP.Final();
               

        if (_TAGET_Chest.Is_Container())
            _Animator.SetTrigger("ActionPickObject");
        else
            _Animator.SetTrigger("ActionPickFloorObject");
                    

        while (!_Is_Capture) {
            yield return new WaitForSeconds(0.1f);
        }
        
        _TAGET_Chest.Event_Capture();        

       _Animator.SetInteger("MoveMode", 1);

        yield return new WaitForSeconds(0.5f);


        _Body.constraints = RC;

        _Is_Grab = true;
        _Is_Freeze = false;
        _IE_Grabling = false;
    }


    IEnumerator IE_DROP() {
        _IE_Grabling = true;

        yield return new WaitForSeconds(0.1f);
        _Animator.SetInteger("ActionMode", 1);
        _Animator.SetTrigger("ActionDropObject");

        yield return new WaitForSeconds(0.1f);

        _Animator.SetInteger("ActionMode", 0);
        _Animator.SetInteger("MoveMode", 0);


        _TAGET_Chest.Event_Free();
        _TAGET_Chest._Enable = false;

        yield return new WaitForSeconds(1f);
        

        _Is_Grab = false;
        _IE_Grabling = false;
    }

    IEnumerator IE_LAY() {
        _IE_Grabling = true;

        RigidbodyConstraints RC = _Body.constraints;
        _Body.constraints = RigidbodyConstraints.FreezeAll;

        yield return new WaitForSeconds(0.1f);

        S_PositMove MP = new S_PositMove(transform, _TAGET_CONV_POSIT, _TAGET_CONV_NAP);
        while (MP._IS_Final()) {
            MP.MOVES();

            if (MP._Is_Break()) {
                _IE_Grabling = false;
                _Body.constraints = RC;
                yield break;
            }

            yield return new WaitForSeconds(0.01f);
        }

        _Is_Freeze = true;

        yield return new WaitForSeconds(0.1f);

        MP.Final();


        _Animator.SetInteger("ActionMode", 1);
        _Animator.SetTrigger("ActionReplaceObject");


        yield return new WaitForSeconds(0.1f);


        _Animator.SetInteger("ActionMode", 0);
        _Animator.SetInteger("MoveMode", 0);


        yield return new WaitForSeconds(1f);

        _TAGET_CONV.AddChest(_TAGET_Chest);        

        Clear();

        _Body.constraints = RC;

        _Is_Grab = false;
        _Is_Freeze = false;
        _IE_Grabling = false;
    }










    public override void EventGrab() {
        _TAGET_Chest.gameObject.transform.SetParent(_Cont_Chest.transform);
        _TAGET_Chest.FreezeBody();
        _Is_Capture = true;
    }

    public override void EventDrop() {
        _TAGET_Chest.gameObject.transform.SetParent(null);
        _TAGET_Chest.UnFreezeBody();
        _Is_Capture = false;
        Clear();
    }


    public override void EventLay() {
        _TAGET_Chest.gameObject.transform.SetParent(null);        
        _Is_Capture = false;
        
    }


    private void Clear() {
        _TAGET_Chest = null;
        _TAGET_POSIT = null;
    }

    public override S_A_Chest GetCaptureChest() {
        return _TAGET_Chest;
    }


}
