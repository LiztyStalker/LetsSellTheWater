using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class UITextClass : MonoBehaviour, IPointerClickHandler {


	public enum TEXT_ICON{INFOR, OK, WARNING, WATER, BOTTLE, CUSTOMER, RESEARCH};

	[SerializeField] Text m_textPanel;
	[SerializeField] Image m_imagePanel;

	[SerializeField] Sprite[] m_images;

	Coroutine coroutine_msg = null;



	public void setTextPanel(TEXT_ICON icon, string msg){
		m_imagePanel.sprite = m_images[(int)icon];
	}

	public void setTextPanel (string msg){

	}

	public void setTextPanel(Sprite image, string msg, TYPE_AUDIO typeAudio = TYPE_AUDIO.NONE){
//		UIMadiatorClass.UIEffectSoundPlayer(typeAudio);
		gameObject.SetActive (true);
		m_textPanel.text = msg;
		m_imagePanel.sprite = image;
		if(coroutine_msg != null){
			StopCoroutine(coroutine_msg);
		}
		coroutine_msg = StartCoroutine (msgCoroutine ());
	}



	IEnumerator msgCoroutine(){
		yield return new WaitForSeconds(5f);
		gameObject.SetActive (false);
		m_textPanel.text = "";
		m_imagePanel.sprite = null;
		coroutine_msg = null;
	}

	public void OnPointerClick(PointerEventData point){
		if (coroutine_msg != null){
			StopCoroutine(coroutine_msg);
			coroutine_msg = null;
		}

	}
}
