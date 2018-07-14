using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class ManagerScript : MonoBehaviour {

	[SerializeField] Text scoreText;

	public int score;//スコア計測用の変数

	// Use this for initialization
	void Start () {
		this.ObserveEveryValueChanged(_ => score)//変数scoreの値が変化する度にプッシュ
		    .Subscribe(s => {//sに受け取った値が入ってる
			Debug.Log("Score : " + s);
		});//
	}
}
