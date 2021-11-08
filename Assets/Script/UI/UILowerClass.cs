using UnityEngine;
using System.Collections;

public class UILowerClass : MonoBehaviour {

	[SerializeField] UIProductClass	m_productPanel;
	[SerializeField] UITextClass m_textPanel;

	void Awake(){
		m_textPanel.gameObject.SetActive (false);
	}

	public void textPanelView(Sprite image, string msg){
		m_textPanel.setTextPanel (image, msg);
	}

//	public void productPanelView(){
//		m_productPanel.productPanelView ();
//	}
//
//	public void researchPanelView(){
//		m_productPanel.researchPanelView ();
//	}
}
