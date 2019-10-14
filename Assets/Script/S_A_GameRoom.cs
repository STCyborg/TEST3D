using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class S_A_GameRoom : MonoBehaviour, S_I_TimerInfo {

    static S_A_GameRoom _Ins;
       

    [SerializeField] public GameObject _Canvas_Container;

    [SerializeField] List<S_A_Chest> _Chests;
    [SerializeField] List<S_Conveer> _Conveers;

    [SerializeField] GameObject[] _Template_Chests;


    public static S_A_GameRoom GetInstance() {
        return _Ins;
    }

    private void Awake() {
        _Ins = this;
    }
          


    public S_ResultNearPosit GetNearChest(Vector3 Posit_) {


        int CompareDisSUm(S_A_Chest P1_, S_A_Chest P2_) {
            float DIS1 = Vector3.Distance(P1_.transform.position, Posit_);
            float DIS2 = Vector3.Distance(P2_.transform.position, Posit_);
            if (DIS1 > DIS2) return 1;
            if (DIS1 < DIS2) return -1;
            return 0;
        }

        _Chests.Sort(CompareDisSUm);


        foreach (S_A_Chest SD_ in _Chests) {
            if (5 > Vector3.Distance(Posit_, SD_.transform.position) && SD_._Enable) {

                GameObject[] RES = SD_.GetPosit().GetNearPosit(Posit_);
                if (RES != null) {
                    S_ResultNearPosit RES2 = new S_ResultNearPosit();
                    RES2._Chest = SD_;
                    RES2._TAGET = RES[0];
                    RES2._NAP = RES[1];
                    return RES2;
                }

            }
        }


        // return  _Chests.Find(A => { return 5 > Vector3.Distance(Posit_, A.transform.position) && A._Enable; });
        return null;

    }

    public S_Conveer GetNearCoveers(Vector3 Posit_, Vector3 NAP_) {

        int CompareDisSUm(S_Conveer P1_, S_Conveer P2_) {
            float DIS1 = Vector3.Distance(P1_._Point_Posit.transform.position, Posit_);
            float DIS2 = Vector3.Distance(P2_._Point_Posit.transform.position, Posit_);
            if (DIS1 > DIS2) return 1;
            if (DIS1 < DIS2) return -1;
            return 0;
        }

        _Conveers.Sort(CompareDisSUm);

        return _Conveers.Find(A => {
            Vector3 Nap2 = A._Point_Posit.transform.position - Posit_;
            float Angle = Mathf.Abs(Vector3.SignedAngle(Nap2, NAP_, Vector3.up));
            return 2f > Vector3.Distance(Posit_, A._Point_Posit.transform.position) && A._Enable && A._Enter && Angle < 120f;
        });
    }

    public S_Conveer GetConveerOutReady() {
        return _Conveers.Find(A => !A._Enter && A._Enable);
    }


    public void AddChest(S_A_Chest Chest_) {
        _Chests.Add(Chest_);
    }

    public void DestroyChest(S_A_Chest Chest_) {
        _Chests.Remove(Chest_);
        Destroy(Chest_.gameObject);
    }


    private GameObject Random_ChestLast = null;
    public GameObject GetRandomTemplate() {
        GameObject RanCh;
        do {
            RanCh = _Template_Chests[Random.Range(0, _Template_Chests.Length)];
        } while (RanCh == Random_ChestLast);

        Random_ChestLast = RanCh;
        return RanCh;
    }

    public GameObject[] GetAllTemplate() {
        return _Template_Chests;
    }

    public GameObject GetCanvasContainer() { return _Canvas_Container; }


    public abstract S_I_Event_Score ScoreEvent();
    public abstract S_I_Diff GetDiff();


    public abstract bool IS_GAME();
    public abstract bool IS_FINISH();

    public abstract void StartGame();
    public abstract int GetGameSecond();










}
