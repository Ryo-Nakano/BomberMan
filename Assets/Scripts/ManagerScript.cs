using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class ManagerScript : MonoBehaviour {

	[SerializeField] Text scoreText;
	[SerializeField] GameObject block;
	[SerializeField] GameObject blocks;//生成されたBlockの親になる為のGameObject

	public int score;//スコア計測用の変数

	// Use this for initialization
	void Start () {
		BlockGenerator();

		this.ObserveEveryValueChanged(_ => score)//変数scoreの値が変化する度にプッシュ
		    .Subscribe(s => 
		    {//sに受け取った値が入ってる
			    Debug.Log("Score : " + s);
		    });//
	}

    //Stage上のBlockを生成する関数
	void BlockGenerator()
	{
		for (float z = 7.5f; z > -10; z -= 2.5f)
		{
			for (float x = 7.5f; x > -10; x -= 2.5f)
			{
				var instantiatePos = new Vector3(x, 0.5f, z);
				GameObject _obj =  Instantiate(block, instantiatePos, Quaternion.identity) as GameObject;
				_obj.transform.parent = blocks.transform;//Blockの親をBlocksに指定
			}
		}
	}
}
