using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Conveer : S_A_Container , S_I_CointainerControll {

    [SerializeField] Animator _Animator;


    [SerializeField] public bool _Enable = true;


    [SerializeField] public GameObject _Point_IN;
    [SerializeField] public GameObject _Point_OUT;
    [SerializeField] public GameObject _Point_Posit;

    [SerializeField] S_A_GrabPosit _Posit;


    [SerializeField] public bool _Enter = true;

    [SerializeField] public float _Speed = 1;

    [SerializeField] MeshRenderer _Mesh;
  
   override  public S_A_GrabPosit GetPosit() {
        return _Posit;
    }


    static private Vector2 C_V2(Vector3 V_) { return new Vector2(V_.x, V_.z); }
    static private Vector3 C_V3(Vector2 V_) { return new Vector3(V_.x, 0 , V_.y); }

    S_A_Chest _Chest;

    Chest_Conv _Last_Chest = null;
    List<Chest_Conv> _List_Chest = new List<Chest_Conv>();




    private void Start() {
        if (!_Enter) {
            _Mesh.material.SetFloat("_ScrollYSpeed", _Speed * KOF_SP_CON);
            StartWork();
        }

    }
         


    class Chest_Conv {

        Chest_Conv Parens;
        Chest_Conv Child;
        public S_A_Chest Obj;

        public Chest_Conv(Chest_Conv Parens_ , S_A_Chest Obj_) {
            Parens = Parens_;
            if (Parens != null) Parens.Child = this;
            Obj = Obj_;
        }

        public void Move(Vector2 Point_OUT_ , float Speed_) {          

            float Dis2 = Vector2.Distance(C_V2(Obj.transform.position), Point_OUT_);
            Obj._Enable = Dis2 < 0.05f;        

            if (Parens == null) {                              
                Vector2 VEC = Point_OUT_ - C_V2(Obj.transform.position);
                if(VEC.magnitude > 0.025f)Obj.transform.Translate(C_V3(VEC.normalized) * Speed_ * 0.02f, Space.World);                
                return;
            }

            float Dis = Vector2.Distance(C_V2(Obj.transform.position) , C_V2(Parens.Obj.transform.position) );
            if (Dis > (Parens.Obj.Container_Size + Obj.Container_Size) * 0.5f) {
                Vector2 VEC = Point_OUT_ - C_V2(Obj.transform.position);
                Obj.transform.Translate(C_V3(VEC.normalized) * Speed_ * 0.02f, Space.World);
            }

            Parens.Move(Point_OUT_, Speed_);
        }

       public void ClearChild() {if(Child != null) Child.Parens = null; }

       public bool _Is_MoveEnd(Vector2 Point_OUT_) {
            return 0.1f > Vector2.Distance(C_V2(Obj.transform.position), Point_OUT_);
       }

    }
    

    public void CreateChest() {
        GameObject G = Instantiate(S_A_GameRoom.GetInstance().GetRandomTemplate() , _Point_IN.transform.position , _Point_IN.transform.rotation);
        _Chest = G.GetComponent<S_Chest>();
        _Chest.FreezeBody();
        _Chest.RegContainer(this);
        S_A_GameRoom.GetInstance().AddChest(_Chest);

        Chest_Conv Buf = new Chest_Conv(_Last_Chest , _Chest);
        _List_Chest.Add(Buf);
        _Last_Chest = Buf;

    }

    override public void AddChest(S_A_Chest Chest_) {
        _Chest = Chest_;
        _Chest._Enable = false;
        Chest_Conv Buf = new Chest_Conv(_Last_Chest, _Chest);
        _List_Chest.Add(Buf);
        _Last_Chest = Buf;
        if (_Task_Event_M != null) _Task_Event_M.E_Chest(Chest_);
    }

    override public void DeleteChest(S_A_Chest Ch_) {
        Chest_Conv CC = _List_Chest.Find(A => A.Obj == Ch_);
        if (CC == null) return;
        CC.ClearChild();
        _List_Chest.Remove(CC);
    }


    private void Update() {
        if (!_Enter) {

            if (_Last_Chest != null) _Last_Chest.Move(C_V2(_Point_OUT.transform.position), _Speed);
            if (_List_Chest.Count < 4)  CreateChest();
            
        } else {

            if (_Last_Chest != null) _Last_Chest.Move(C_V2(_Point_IN.transform.position), _Speed);
            Chest_Conv BUF = _List_Chest.Find(A => A._Is_MoveEnd(C_V2(_Point_IN.transform.position)));

            if(BUF != null) {
                
                DeleteChest(BUF.Obj);
                S_A_GameRoom.GetInstance().DestroyChest(BUF.Obj);
                

                if (_List_Chest.Count == 0) _Last_Chest = null;

            }            

        }

        if (_Last_Chest != null) {
            if (_Enter) _Mesh.material.SetFloat("_ScrollYSpeed", -_Speed * KOF_SP_CON);
            else _Mesh.material.SetFloat("_ScrollYSpeed", _Speed * KOF_SP_CON);
        } else {
            _Mesh.material.SetFloat("_ScrollYSpeed", 0);
        }

    }


    float KOF_SP_CON = 28f;

    public void StartWork() {   
        _Enable = true;
        _Animator.SetTrigger("Open");
    }


    public void PreEndWork() {
        _Enable = false;
        
    }


    public void EndWork() {
       _Animator.SetTrigger("Close");
    }


}
