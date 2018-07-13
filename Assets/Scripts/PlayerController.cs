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
	
	// Update is called once per frame
	void Update () {
		
	}

    //Playerの移動実装①
	void PlayerMove1()
	{
		this.UpdateAsObservable()//Update中ずっと値流し込み
            .Where(_ => Input.GetKey("right"))
            .Subscribe(_ => BaseMove("right"));

        this.UpdateAsObservable()//Update中ずっと値流し込み
            .Where(_ => Input.GetKey("left"))
            .Subscribe(_ => BaseMove("left"));

        this.UpdateAsObservable()//Update中ずっと値流し込み
            .Where(_ => Input.GetKey("up"))
            .Subscribe(_ => BaseMove("up"));

        this.UpdateAsObservable()//Update中ずっと値流し込み
            .Where(_ => Input.GetKey("down"))
            .Subscribe(_ => BaseMove("down"));

        //4本ストリーム作っちゃうのってどうなのかしら。
        //なんとかすおtリーム1本で、条件分岐できないものか...
	}

	//Playerの移動実装①
	void PlayerMove2()
	{
		
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
