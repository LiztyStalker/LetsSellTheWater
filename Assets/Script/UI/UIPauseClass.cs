using UnityEngine;
using System.Collections;

public class UIPauseClass : MonoBehaviour {

	void OnEnable(){
		UIMadiatorClass.UIEffectSoundPlayer (TYPE_AUDIO.OK);
		Time.timeScale = 0f;
	}


	public void PauseChange(){
		gameObject.SetActive (false);
	}

	void OnDisable(){
		UIMadiatorClass.UIEffectSoundPlayer (TYPE_AUDIO.CANCEL);
		Time.timeScale = 1f;
	}
}
