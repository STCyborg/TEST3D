using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Task_Conveer : MonoBehaviour , S_I_TaskSource , S_Task_Event
    {

    S_Task _Task;

    [SerializeField] MonoBehaviour _ControllContain;
    S_I_CointainerControll         _ControllContainer;


    [SerializeField] GameObject _VIS_Task;

    S_A_GameRoom _Room;

    private void OnValidate() {
        if (_ControllContain   != null) _ControllContainer = _ControllContain.GetComponent<S_I_CointainerControll>();
        if (_ControllContainer == null) _ControllContain = null;  

    }


    private void Start() {

        _Room = S_A_GameRoom.GetInstance();

        if (_VIS_Task) {
            GameObject TaskVis = Instantiate(_VIS_Task , _Room._Canvas_Container.transform);
            S_I_Vis_Task VT = TaskVis.GetComponent<S_I_Vis_Task>();
            VT.SetSourceTask(this);
        }


    }


    // Update is called once per frame
    void Update()
    {
        if (!_IS_IE_TASK  && _Room.IS_GAME()) StartCoroutine(IE_TASK());
    }



    public S_Task GetTask() {
        return _Task;
    }



    bool _IS_IE_TASK = false;


    IEnumerator IE_TASK() {
        _IS_IE_TASK = true;

        yield return new WaitForSeconds(Random.Range(1, 5));
        
        _Task = S_Task.GetRandomTask();

        if (_ControllContainer != null) _ControllContainer.StartWork();

         yield return new WaitForSeconds(1);

        while (_Task.CouldSubTask() > 0 && !_Task.IsFailed() && !_Room.IS_FINISH()) {
    
            _Task.TimerAdd(-0.5f);
            if (_Task.GetTimer() <= 0) _Task.Failed();      


            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(0.5f);

        if (_ControllContainer != null) _ControllContainer.PreEndWork();

        if(!_Room.IS_FINISH()) _Room.ScoreEvent().Event_Task_Finish( _Task.IsFailed() , _Task.GetReward() );

         _Task = null;
        yield return new WaitForSeconds(2f);

        if (_ControllContainer != null) _ControllContainer.EndWork();    

        yield return new WaitForSeconds(Random.Range(1,10));


        _IS_IE_TASK = false;
    }

    public Vector3 GetPosit() {
        return transform.position;
    }



    public void E_Chest(S_A_Chest Ch_) {

        if (_Task != null) {

            S_Task_Element SE = _Task.GetSubTask(_Task.CouldSubTask() - 1);
            if (Ch_._ID == SE._Chest._ID) {
                SE._Could--;
            } else {
                _Task.Failed();
            }

            if (SE._Could <= 0) _Task.DeleteTask(_Task.CouldSubTask() - 1);


        }

    }





}
