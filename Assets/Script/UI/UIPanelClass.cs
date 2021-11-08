using UnityEngine;
using System.Collections;

public class UIPanelClass : MonoBehaviour {

	[SerializeField] UIUpperClass m_upperUI;
	[SerializeField] UIMiddleClass m_midUI;
	[SerializeField] UILowerClass m_lowerUI;

	bool m_isMenu = false;


	public UIUpperClass upperUI{ get { return m_upperUI; } }
	public UIMiddleClass midUI{ get { return m_midUI; } }
	public UILowerClass lowerUI{ get { return m_lowerUI; } }
	public bool isMenu{ get { return m_isMenu; } }

	// Use this for initialization
	void Start () {
		//menuToggle ();
	}

	//사용하지 않음
	public void menuToggle(){
//		UIMadiatorClass.UIEffectSoundPlayer(TYPE_AUDIO.NONE);
		return;

		m_isMenu = !m_isMenu;


		//lower 패널 보이기, 보이지 않기
		if (m_isMenu) {
			m_lowerUI.GetComponent<RectTransform> ().offsetMax = new Vector2(m_lowerUI.GetComponent<RectTransform> ().offsetMax.x, -90f);
			m_lowerUI.GetComponent<RectTransform> ().offsetMin = new Vector2(m_lowerUI.GetComponent<RectTransform> ().offsetMin.x, -90f);
		}
		else{
			m_lowerUI.GetComponent<RectTransform> ().offsetMax = new Vector2(m_lowerUI.GetComponent<RectTransform> ().offsetMax.x, 0f);
			m_lowerUI.GetComponent<RectTransform> ().offsetMin = new Vector2(m_lowerUI.GetComponent<RectTransform> ().offsetMin.x, 0f);
		}
	}




	//상점
	//스테이터스
	//관리
	//설정
	//기타

	

}
