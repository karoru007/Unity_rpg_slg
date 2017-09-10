using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class BattleCtrl : MonoBehaviour {

    Slider _atbSlider,_PcHpSlider,_EnemyHpSlider;
    PlayerCharacter _pc;
    EnemyCharacter _ec;
    Text _Name, _Level, _Hp, _Mp, _Str, _Def, _Agl, _Exp, _NextExp;

    float _atb = 0;
    float _pcAutoAtb = 0;
    float _ecAutoAtb = 0;
    int en = 0;

    void Start()
    {
        _pc = GetComponent<PlayerCharacter>(); //プレイヤーキャラクタースクリプト参照
        _ec = GetComponent<EnemyCharacter>(); //エネミーキャラクタースクリプト参照
        _atbSlider = GameObject.Find("Atb").GetComponent<Slider>(); //ATBスライダー取得
        _PcHpSlider = GameObject.Find("Player_Hp").GetComponent<Slider>();//プレイヤーHPスライダー取得
        _EnemyHpSlider = GameObject.Find("Enemy_Hp").GetComponent<Slider>();//エネミーHPスライダー取得

        //スライダーにHPを設定
        _PcHpSlider.maxValue = _pc.status[0].maxHp;
        _PcHpSlider.value = _pc.status[0].hp;
        _EnemyHpSlider.maxValue = _ec.status[en].maxHp;
        _EnemyHpSlider.value = _ec.status[en].hp;


        _Name = GameObject.Find("Name").GetComponent<Text>();
        _Level = GameObject.Find("Level").GetComponent<Text>();
        _Hp = GameObject.Find("Hp").GetComponent<Text>();
        _Mp = GameObject.Find("Mp").GetComponent<Text>();
        _Str = GameObject.Find("Str").GetComponent<Text>();
        _Def = GameObject.Find("Def").GetComponent<Text>();
        _Agl = GameObject.Find("Agl").GetComponent<Text>();
        _Exp = GameObject.Find("Exp").GetComponent<Text>();
        _NextExp = GameObject.Find("NextExp").GetComponent<Text>();

        //テキストにステータス値をセット
        _Name.text = _pc.status[0].name;

        _Level.text = _pc.status[0].level.ToString();
        _Hp.text = _pc.status[0].hp.ToString();
        _Mp.text = _pc.status[0].mp.ToString();
        _Str.text = _pc.status[0].str.ToString();
        _Def.text = _pc.status[0].def.ToString();
        _Agl.text = _pc.status[0].agl.ToString();
        _Exp.text = _pc.status[0].exp.ToString();
        _NextExp.text = _pc.status[0].nextExp.ToString() + "exp";

    }
    
    void Update()
    {
        UpAtb();
        Attack();
    }

    void Attack()
    {
        int dmg = 0;
        //プレイヤーオート攻撃処理
        if(_pcAutoAtb > 1)
        {

            //ダメージ計算式
            //（攻撃力÷２×レベル）ー（防御力÷４×レベル）
            dmg = ((_pc.status[0].str / 2) * _pc.status[0].level) - ((_ec.status[en].def / 4) * _ec.status[en].level);

            //ダメージ乱数付加
            if(dmg < 100)
            {
                dmg += UnityEngine.Random.Range(0, 10);
            }
            else
            {
                dmg += UnityEngine.Random.Range(0, 30);
            }
            _ec.AddDamage(dmg, en);
            _EnemyHpSlider.value -= dmg;
            _pcAutoAtb = 0;
        }
        //エネミーオート攻撃処理
        if (_ecAutoAtb > 1)
        {

            //ダメージ計算式
            //（攻撃力÷２×レベル）ー（防御力÷４×レベル）
            dmg = ((_ec.status[en].str / 2) * _ec.status[en].level) - ((_pc.status[0].def / 4) * _pc.status[0].level);

            //ダメージ乱数付加
            if (dmg < 100)
            {
                dmg += UnityEngine.Random.Range(0, 10);
            }
            else
            {
                dmg += UnityEngine.Random.Range(0, 30);
            }
            _pc.AddDamage(dmg, 0);
            _PcHpSlider.value -= dmg;
            _ecAutoAtb = 0;
            _Hp.text = _pc.status[0].hp.ToString();
        }
    }

    //ATB管理
    void UpAtb()
    {
        // ATB上昇
        _atb = _atb + (0.000050f * _pc.status[0].agl);
        if (_atb > 1)
        {
            _atb = 1;
        }
        // ATBゲージに値を設定
        _atbSlider.value = _atb;

        //オート攻撃の間隔設定
        _pcAutoAtb = _pcAutoAtb + (0.0001f * _pc.status[0].agl);
        _ecAutoAtb = _ecAutoAtb + (0.0001f * _ec.status[en].agl);

    }
}
