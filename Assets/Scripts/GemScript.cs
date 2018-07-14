using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class GemScript : MonoBehaviour {

	void Start () {
		this.UpdateAsObservable()
			.TakeUntilDestroy(this)
		    .Subscribe(
			    _ => {},//OnNext 
		        () => { Debug.Log("Called OnCompleted!!"); });//OnCompleted
	}
	

}
