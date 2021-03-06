//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.36373
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using UnityEngine;

public enum TYPE_AUDIO {
	NONE, 
	INFOR, 
	OK, 
	WARNING, 
	CANCEL, 
	COST, 
	SELL, 
	SUCCESS, 
	ENERGY, 
	ALARM, 
	PINECOLA, 
	EXTEND, 
	RSUCCESS, 
	SLEEP,
	PAGE,
	FEVER
}

public class UISoundEffectClass : MonoBehaviour
{
	AudioClip[] m_audios = new AudioClip[Enum.GetValues(typeof(TYPE_AUDIO)).Length];
	AudioSource m_audioSource;


	void Start(){

		//m_audios = Resources.LoadAll<AudioClip> ("Sound/Effect");



		m_audioSource = GetComponent<AudioSource> ();
		m_audioSource.loop = false;
		m_audioSource.playOnAwake = false;
		m_audioSource.mute = (PlayerPrefs.GetInt ("isEffectSound", 0) > 0) ? true : false;
		//playerprefeb으로 가져와야 함

		DontDestroyOnLoad (this);
	}

	/// <summary>
	/// 효과음 설정하기
	/// </summary>
	/// <returns><c>true</c>, if effect sound was set, <c>false</c> otherwise.</returns>
	public bool setEffectSound(AssetBundle bundle){
		for(int i = 0; i < Enum.GetValues(typeof(TYPE_AUDIO)).Length; i++){
			try{
				string type = string.Format("{0}_{1}", i, (TYPE_AUDIO)i);
				Debug.Log("Effect Type : " + type);
				m_audios[i] = bundle.LoadAsset<AudioClip>(type);
			}
			catch(UnityException e){
				Debug.LogError("효과음 사운드 불러오기 오류 : " + e.Message);
			}
		}

		return true;
	}



	public void audioPlay(TYPE_AUDIO audioType){
		Debug.Log ("사운드 : " + audioType);
		m_audioSource.clip = m_audios [(int)audioType];
		m_audioSource.mute = (PlayerPrefs.GetInt ("isEffectSound", 0) > 0) ? true : false;
		m_audioSource.Play ();
	}

	public void audioPlay(AudioSource audioSource, TYPE_AUDIO audioType){
		Debug.Log ("사운드 대리 : " + audioType);
		audioSource.clip = m_audios [(int)audioType];
		audioSource.mute = (PlayerPrefs.GetInt ("isEffectSound", 0) > 0) ? true : false;
		audioSource.Play ();
	}




}


