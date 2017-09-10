using UnityEngine;
using System.Collections;

public class PlayerCharacter : Character {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    //経験値処理
    public virtual void AddExp(int e,int pn){
        status[pn].exp += e;
        Debug.Log(e + "点の経験値を獲得した");
    }

    //レベルアップ処理
    public virtual void UpLevel(){
        Debug.Log("レベルアップ");
    }
}
