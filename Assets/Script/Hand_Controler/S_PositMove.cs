using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class S_PositMove  {

    private Vector2 C_V2(Vector3 V_) { return new Vector2(V_.x, V_.z); }


    Transform _Main_T;
    GameObject _TAGET;
    GameObject _NAP;


    int STEP = 1;



    NavMeshPath _NAV_Path = new NavMeshPath();
    int        _NAV_Path_N = 0;


    Vector3 Start_Taget_Posit;

    public S_PositMove(Transform Main_Trans_, GameObject Taget_, GameObject Nap_) {

        _Main_T = Main_Trans_;
        _TAGET = Taget_;
        _NAP = Nap_;

        Start_Taget_Posit = _TAGET.transform.position;

        Distance = Vector2.Distance(C_V2(_TAGET.transform.position), C_V2(_Main_T.position));
        Delta_Angle = Mathf.Abs(Vector3.SignedAngle(_NAP.transform.position - _TAGET.transform.position, _Main_T.forward, Vector3.up));


        NavMeshHit HIT = new NavMeshHit();
        

        bool B =  NavMesh.Raycast(Main_Trans_.position , Taget_.transform.position , out HIT , NavMesh.AllAreas);
        if (B) {            
            NavMesh.CalculatePath(Main_Trans_.position, Taget_.transform.position, NavMesh.AllAreas, _NAV_Path);
            if (_NAV_Path.status == NavMeshPathStatus.PathComplete) STEP = 0; else _Break = true;

        }

        


    }





    float Distance;
    float Delta_Angle;

    public void MOVES () {
        if (_Break) return;
        if (STEP == 0) STEP_0();
        if (STEP == 1) STEP_1();

        if((Start_Taget_Posit - _TAGET.transform.position).magnitude > 0.01f) _Break = true;

    }

    bool _Break = false;
    public bool _Is_Break() { return _Break;   }
    

    private void STEP_0() {
          Vector3 T = _NAV_Path.corners[_NAV_Path_N];

        Vector3 VEC = new Vector3(T.x, _Main_T.position.y, T.z);
        _Main_T.position = Vector3.MoveTowards(_Main_T.position, VEC, 0.03f);


          float Dis = Vector2.Distance(C_V2(T), C_V2(_Main_T.position));
          if (Dis < 0.01f) _NAV_Path_N++;

          Delta_Angle = Mathf.Abs(Vector3.SignedAngle(_NAP.transform.position - _TAGET.transform.position, _Main_T.forward, Vector3.up));

          if (_NAV_Path_N >= _NAV_Path.corners.Length) STEP = 1;
    }



    private void STEP_1() {
        Distance = Vector2.Distance(C_V2(_TAGET.transform.position), C_V2(_Main_T.position));

        float SP = 0.035f;
        if (Distance < 0.2f) SP = 0.01f;

        Vector3 VEC = new Vector3(_TAGET.transform.position.x , _Main_T.position.y, _TAGET.transform.position.z);
        _Main_T.position = Vector3.MoveTowards(_Main_T.position , VEC, SP);
        
  

        if (Distance < 0.3f) {
            Quaternion N     = Quaternion.LookRotation  ( _NAP.transform.position - _TAGET.transform.position, Vector3.up);
            _Main_T.rotation = Quaternion.RotateTowards ( _Main_T.rotation, N, 10f);
        }

        Delta_Angle = Mathf.Abs(Vector3.SignedAngle(_NAP.transform.position - _TAGET.transform.position, _Main_T.forward, Vector3.up));
    }




    public void Final() {

        Quaternion N1 = Quaternion.LookRotation(_NAP.transform.position - _TAGET.transform.position, Vector3.up);
        _Main_T.rotation = Quaternion.RotateTowards(_Main_T.rotation, N1, 10f);

        Vector2 VT2 = C_V2(_TAGET.transform.position);
        _Main_T.position = new Vector3(VT2.x, _Main_T.position.y, VT2.y);

    }


    public bool _IS_Final() {
        return Distance > 0.001f || Delta_Angle > 0.05f;
    }

  


}
