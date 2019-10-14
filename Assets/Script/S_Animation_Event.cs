using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Animation_Event : MonoBehaviour
{
    [SerializeField] S_A_Hand_Controler _HandControll;



     public void PlaySoundStep() {

     }

    public void GrapChest() {
        _HandControll.EventGrab();
    }

    public void DropChest() {
        _HandControll.EventDrop();
    }

    public void LayChest() {
        _HandControll.EventLay();
    }


}
