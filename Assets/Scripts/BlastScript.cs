using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class BlastScript : MonoBehaviour {

	[SerializeField] float blastTimer;//Blastが生成されてから破壊されるまでの時間を規定

	void Start () {
		
		//一定時間後、Blast破壊
		Observable.Timer(TimeSpan.FromSeconds(blastTimer))
                  .TakeUntilDestroy(this)//自身がDestroyされるまで
                  .Subscribe(_ =>
                  {
                      Destroy(this.gameObject);
                  });
	}

}
