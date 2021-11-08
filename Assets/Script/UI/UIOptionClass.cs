using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIOptionClass : UIObjectClass {


	[SerializeField] Toggle m_BGMToggle;
	[SerializeField] Toggle m_effectToggle;


	// Use this for initialization
	protected override void Start () {
		base.Start ();
		m_BGMToggle.isOn = (PlayerPrefs.GetInt("isBackgroundSound", 1) == 1) ? false : true;
		m_effectToggle.isOn = (PlayerPrefs.GetInt("isEffectSound", 1) == 1) ? false : true;
	}


	public void BGMToggleChange(){
		PlayerPrefs.SetInt ("isBackgroundSound", ((m_BGMToggle.isOn) ? 0 : 1));
		UIMadiatorClass.UIEffectSoundPlayer (TYPE_AUDIO.OK);
		UIMadiatorClass.BGMMute ();
	}

	public void EffectToggleChange(){
		PlayerPrefs.SetInt ("isEffectSound", ((m_effectToggle.isOn) ? 0 : 1));
		UIMadiatorClass.UIEffectSoundPlayer (TYPE_AUDIO.OK);
	}

}
