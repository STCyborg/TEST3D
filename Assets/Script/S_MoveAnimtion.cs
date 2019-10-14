using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_MoveAnimtion : MonoBehaviour
{

    [SerializeField] Animator _Animator;
    [SerializeField][Range(0.1f, 20)] float _Mul_Move = 1;


    [SerializeField] string _STR_ID_ANIM_MOVE = "MoveSpeed";
    int    _ID_ANIM_MOVE;


    Vector3 _OLD_POSIT;
    float _Speed_Old = 0;
    float _Speed = 0;

    // Start is called before the first frame update
    void Start(){
        _ID_ANIM_MOVE =  Animator.StringToHash(_STR_ID_ANIM_MOVE);
        _OLD_POSIT = transform.position;
    }

    // Update is called once per frame
    void Update(){

        Vector3 POSIT = transform.position;

        float SpeedTaget = ( POSIT - _OLD_POSIT ).magnitude * _Mul_Move;

        _Speed = Mathf.SmoothDamp(_Speed , SpeedTaget , ref _Speed_Old , 0.1f);

        _Animator.SetFloat(_ID_ANIM_MOVE , _Speed);


        _OLD_POSIT = POSIT;
    }






}
