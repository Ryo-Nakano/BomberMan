using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class GemScript : MonoBehaviour {

	ManagerScript ms;

	private void Awake()
	{
		ms = GameObject.Find("Manager").GetComponent<ManagerScript>();
	}

	void Start () {
		ColPlayer();//Playerに当たった時呼ばれる関数

		this.UpdateAsObservable()
			.TakeUntilDestroy(this)
		    .Subscribe(
			    _ => {},//OnNext 
			    () => { Debug.Log("Called OnCompleted!!"); 
		    });//OnCompleted(GemがDestroyされた瞬間に処理したい場合はここに書けば良い！)
	}
	
	//Playerに当たった時呼ばれる関数
    void ColPlayer()
    {
        this.OnTriggerEnterAsObservable()
		    .TakeUntilDestroy(this)
            .Where(col => col.gameObject.tag == "Player")//当たった相手のtagが"Player"だった時だけプッシュ
            .Subscribe(_ => { //購読
			    Destroy(this.gameObject);//当たったGemをDestroy
                ms.score++;//score加算
            });
    }
}
