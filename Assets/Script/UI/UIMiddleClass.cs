using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIMiddleClass : MonoBehaviour {

	[SerializeField] Button[] m_midBtns;
	[SerializeField] GameObject m_feverPanel;

	AudioSource m_audioSource;

	// Use this for initialization
	void Awake () {
//		m_midBtns [0].gameObject.SetActive (false);
//		m_midBtns [1].gameObject.SetActive (false);
//		resetFeverPanel ();
		m_audioSource = GetComponent<AudioSource> ();
	}

	public void setMidBtns(){
		m_midBtns [0].gameObject.SetActive (true);
		m_midBtns [1].gameObject.SetActive (true);
	}

	public void setFeverPanel(){
		m_feverPanel.gameObject.SetActive (true);
		UIMadiatorClass.UIEffectSoundPlayer (m_audioSource, TYPE_AUDIO.FEVER);
	}

	public void resetFeverPanel(){
		m_feverPanel.gameObject.SetActive (false);
	}



}
