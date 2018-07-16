using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

public class BombScript : MonoBehaviour {

	SphereCollider sc;

	[SerializeField] int bombTimer;//Bombが爆発するまでの時間

	void Awake()
	{
		sc = GetComponent<SphereCollider>();
	}

	void Start () 
	{
		ControllIsTrigger();//isTriggerの制御する関数

        //一定時間後、bomb爆発
		Observable.Timer(TimeSpan.FromSeconds(bombTimer))
				  .TakeUntilDestroy(this)//自身がDestroyされるまで
				  .Subscribe(_ =>
				  {
					  Debug.Log("Bomb!!!!!!!!!!!!");
                      Destroy(this.gameObject);
				  });
	}


    //isTriggerの制御する関数
	void ControllIsTrigger()
	{
		sc.isTrigger = true;//最初はあたり判定無効

		this.OnTriggerExitAsObservable()//離れた瞬間に値流し込み
            .TakeUntilDestroy(this)//自身がDestroyされるまで
            .Where(col => col.gameObject.tag == "Player")//当たった相手のtagが"Player"だった時だけプッシュ
            .Subscribe(_ => { //購読
                sc.isTrigger = false;//あたり判定有効に
            });
	}
	

}
