using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public abstract class S_A_Hand_Controler : MonoBehaviour {

   public abstract void Object_Grab();
   public abstract void Object_Drop();

   public abstract bool Is_Ready();
   public abstract bool Is_Freeze();
   public abstract bool Is_Grabed();

   public abstract void EventGrab();
   public abstract void EventDrop();
   public abstract void EventLay();

   public abstract S_A_Chest GetCaptureChest();

}

