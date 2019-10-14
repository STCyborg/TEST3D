using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Keyboard : MonoBehaviour
{
    [SerializeField] float Mul_Move = 1;
    [SerializeField] S_A_Hand_Controler _HandControl;
    [SerializeField] Rigidbody _Body;

    S_A_GameRoom _Room;



    Vector3 _OLD_Posit;


    private void Start() {
        _Room = S_A_GameRoom.GetInstance();
        _OLD_Posit = transform.position;
    }


    // Update is called once per frame
    void Update()
    {



       if(Input.GetKey(KeyCode.Space) && _HandControl.Is_Ready() && _Room.IS_GAME()) {
            if (!_HandControl.Is_Grabed()) _HandControl.Object_Grab();
            if (_HandControl.Is_Grabed()) _HandControl.Object_Drop();
        }

        if (Input.GetKey(KeyCode.Space) &&  !_Room.IS_GAME()) {
            _Room.StartGame();
        }

        if (!_Room.IS_GAME() && _HandControl.Is_Grabed()) _HandControl.Object_Drop();


        Move();

    }



    private void Move() {


        if (_HandControl.Is_Freeze()) return;

        Vector3 Posit = transform.position;
        Vector3 Posit_Vel = (Posit - _OLD_Posit).normalized;
        Posit_Vel.y = 0;

        if (Posit_Vel.magnitude > 0.01f) {
            Quaternion N = Quaternion.LookRotation(Posit_Vel, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, N, 8f);
        }

        _OLD_Posit = Posit;


        if (!_HandControl.Is_Ready()) return;
        if (!_Room.IS_GAME()) return;

        float H = Input.GetAxis("Horizontal");
        float V = Input.GetAxis("Vertical");

        if (Mathf.Abs(H) < 0.1f) H = 0;
        if (Mathf.Abs(V) < 0.1f) V = 0;

        float FIN_MUL = Mul_Move;

        S_A_Chest CH = _HandControl.GetCaptureChest();
        if (CH != null) FIN_MUL *= CH._Mul_Move;

        _Body.velocity = Vector3.Lerp(_Body.velocity, (Vector3.forward * V + Vector3.left * -H) * FIN_MUL, 0.5f);



    }


}
