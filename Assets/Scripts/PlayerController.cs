using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class PlayerController : MonoBehaviour {

	[SerializeField] float speed;
	[SerializeField] GameObject manager;
	[SerializeField] GameObject bomb;
	//ManagerScript ms;

	private void Awake()
	{
		//ms = manager.GetComponent<ManagerScript>();
	}

	void Start () {
		PlayerMove1();//Playerの移動実装①
		PutBomb();//Bombを置く関数
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
        //なんとかストリーム1本で、条件分岐できないものか...
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

    //spaceキー押して爆弾押す関数
	void PutBomb()
	{
		this.UpdateAsObservable()//Update中ずっと値流し込み
            .TakeUntilDestroy(this)
            .Where(_ => Input.GetKeyDown("space"))
		    .Subscribe(_ => 
		    { 
			    Instantiate(bomb, this.transform.position, Quaternion.identity);
		    });
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
