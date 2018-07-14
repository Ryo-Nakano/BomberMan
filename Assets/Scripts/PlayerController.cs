using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class PlayerController : MonoBehaviour {

	[SerializeField] float speed;

	// Use this for initialization
	void Start () {
		PlayerMove1();

		this.OnTriggerEnterAsObservable()
		    .Where(col => col.gameObject.tag == "Gem")//当たった相手のtagが"Gem"だった時だけプッシュ
		    //.First()//これついてると1個Gemと接触したら他のGemの時反応しなくなっちゃう！→インスタンス化は4つされているはずだけど、ストリームは1つしかできてない？
		    .Subscribe(x => { 
			    Debug.Log("Gem!!!");
			    Destroy(x.gameObject);//当たったGemをDestroy
                //ポイント加算のコード書く
		});//購読

	}


    //Playerの移動実装①
	void PlayerMove1()
	{
		this.UpdateAsObservable()//Update中ずっと値流し込み
		    .TakeUntilDestroy(this)//Destroyされたら自動で登録解除(onCompleted通知も飛ぶ)
            .Where(_ => Input.GetKey("right"))//右押した時だけプッシュ
            .Subscribe(_ => BaseMove("right"));//購読
		//『this.』で書いてるから、ぶっちゃけこの場面では登録解除のコードはいらないかな！(インスタンス破棄と同時に登録先(自分自身)も消されるから)

        this.UpdateAsObservable()//Update中ずっと値流し込み
		    .TakeUntilDestroy(this)
            .Where(_ => Input.GetKey("left"))
            .Subscribe(_ => BaseMove("left"));

        this.UpdateAsObservable()//Update中ずっと値流し込み
		    .TakeUntilDestroy(this)
            .Where(_ => Input.GetKey("up"))
            .Subscribe(_ => BaseMove("up"));

        this.UpdateAsObservable()//Update中ずっと値流し込み
		    .TakeUntilDestroy(this)
            .Where(_ => Input.GetKey("down"))
            .Subscribe(_ => BaseMove("down"));

        //4本ストリーム作っちゃうのってどうなのかしら。
        //なんとかすおtリーム1本で、条件分岐できないものか...
	}

	//Playerの移動実装①
	void PlayerMove2()
	{
		/*
        this.UpdateAsObservable()
            .Where(_ => {
            string direction;
            if(Input.GetKey("right"))
            {
                direction = "right";
            }
            else if(Input.GetKey("left"))
            {
                direction = "left";
            }
            else if(Input.GetKey("up"))
            {
                direction = "up";
            }
        })

        Whereの中を条件分岐して、そのWhereから流し込まれる値に応じてSubscribe内で呼ぶ部分変えられたらいいなと思ったんだけど、ちょっと無理そう。
        */
	}

    //Playerの移動を司る関数
	void BaseMove(string direction)//引数のstring型の変数で場所を指定→その方向に動く
	{
		if(direction == "right")
		{
			this.transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
		}
		else if(direction == "left")
		{
			this.transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
		}
		else if(direction == "up")
		{
			this.transform.position += new Vector3(0, 0, speed * Time.deltaTime);
		}
		else if(direction == "down")
		{
			this.transform.position += new Vector3(0, 0, -speed * Time.deltaTime);
		}
	}
}
