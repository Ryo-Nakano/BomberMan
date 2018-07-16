﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class BlastElementScript : MonoBehaviour {



	void Start () {
		ColPlayer();//Playerに当たった時呼ばれる関数
		ColBlock();//Blockに当たった時に呼ばれる関数
	}

	//Playerに当たった時呼ばれる関数
    void ColPlayer()
    {
        this.OnTriggerEnterAsObservable()
            .TakeUntilDestroy(this)
            .Where(col => col.gameObject.tag == "Player")//当たった相手のtagが"Player"だった時だけプッシュ
		    .Subscribe(col => //購読(colに当たった相手の情報入ってる)
		    { 
			    Destroy(col.gameObject);//当たったGemをDestroy
            });
    }

    //Blockに当たった時に呼ばれる関数
	void ColBlock()
	{
		this.OnTriggerEnterAsObservable()
            .TakeUntilDestroy(this)
            .Where(col => col.gameObject.tag == "Block")//当たった相手のtagが"Player"だった時だけプッシュ
		    .Subscribe(_ =>//購読
            { 
			    Destroy(this.gameObject);//当たったGemをDestroy
            });
	}

}
