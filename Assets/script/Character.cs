using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

    //キャラクターステータス配列
    [System.Serializable]
    public struct Status{
        public string name;
        public int level;
        public int hp;
        public int maxHp;
        public int mp;
        public int maxMp;
        public int str;
        public int def;
        public int agl;
        public int exp;
        public int nextExp;
    }

    public Status[] status;

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update () {
        
	}

    //ダメージ処理　
    public virtual void AddDamage (int dmg,int cn) { //cn =  キャラクターナンバー
        status[cn].hp -= dmg;
        Debug.Log(dmg + "点のダメージを受けた");
        Debug.Log("残りHP：" + status[cn].hp);
    }

    //死亡処理
    public virtual void Die () {
        Debug.Log("死んだ");
    } 
}
