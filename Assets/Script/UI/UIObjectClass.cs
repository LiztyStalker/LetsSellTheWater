//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.34209
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class UIObjectClass : MonoBehaviour
{

	[SerializeField] Button m_exitBtn;
	[SerializeField] UISoundEffectClass m_effectSound = null;

	protected virtual void Start(){
		Debug.Log ("start");
		if(m_exitBtn != null)
			//m_exitBtn.onClick.AddListener (new UnityAction (panelClose));
			m_exitBtn.onClick.AddListener(delegate { panelClose(false); });
	}

	protected virtual void OnEnable(){
		if(m_exitBtn != null){
			Debug.Log("스택 인 : " + GetType());
			UIMadiatorClass.UIStackPanel (this);
		}
	}

	/// <summary>
	/// 패널 닫기
	/// </summary>
	protected virtual void panelClose(bool isSound = false){
		if (!isSound) {
			if (m_effectSound == null)
				effectSoundPlayer (TYPE_AUDIO.CANCEL);
			else
				m_effectSound.audioPlay (TYPE_AUDIO.CANCEL);
		}
		gameObject.SetActive(false);
	}

	protected void effectSoundPlayer(TYPE_AUDIO audioEffectType){
		if (m_effectSound == null)
			UIMadiatorClass.UIEffectSoundPlayer (audioEffectType);
		else
			m_effectSound.audioPlay (audioEffectType);
	}

	/// <summary>
	/// 패널 닫기
	/// </summary>
	public void panelCloseEvents(){
		panelClose();
	}

	protected virtual void OnDisable(){
		if (m_exitBtn != null) {
			Debug.Log("스택 아웃 : " + GetType());
			UIMadiatorClass.UIStackPanel (this, true);
		}
	}

}

