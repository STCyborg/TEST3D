using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_GameRoom :S_A_GameRoom {



    S_GameRule _Rule = new S_GameRule();

         
    [SerializeField] GameObject _Help_Controll;
    [SerializeField] GameObject _Help_PressSpace;
    [SerializeField] GameObject _Score;
    [SerializeField] GameObject _Timer;

    



    override public S_I_Event_Score ScoreEvent() { return _Rule; }
    override public S_I_Diff GetDiff() { return _Rule; }
       

    int TIMER = 10;
    bool _IS_GAME = false;
    bool _IS_FINISH = false;

    override public bool IS_GAME() { return _IS_GAME; }
    override public bool IS_FINISH() { return _IS_FINISH; }

    override public void StartGame() {        
        if (!_IS_IE_GAME) StartCoroutine(IE_GAME());
    }
       


    bool _IS_IE_GAME = false;
    private IEnumerator IE_GAME() {
        _IS_IE_GAME = true;

        yield return new WaitForSeconds(0.25f);

        _Help_Controll.SetActive(false);
        _Help_PressSpace.SetActive(false);
        _Score.SetActive(false);
        _Timer.SetActive(true);
        _IS_GAME = true;

        TIMER = 120;
        _Rule.ResetScore();

        yield return new WaitForSeconds(1f);

        while (!_IS_FINISH) {

            TIMER -= 1;
            if (TIMER <= 0) _IS_FINISH = true;

             yield return new WaitForSeconds(1f);
        }

        _IS_GAME = false;
        

        yield return new WaitForSeconds(1f);

        _Timer.SetActive(false);
        _Help_Controll.SetActive(true);
        _Help_PressSpace.SetActive(true);
        _Score.SetActive(true);

        _IS_FINISH  = false;
        _IS_IE_GAME = false;
    }

    override public int GetGameSecond() {
        return TIMER;
    }



}
