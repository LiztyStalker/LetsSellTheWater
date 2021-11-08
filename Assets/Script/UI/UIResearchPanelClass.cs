using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;


public class UIResearchPanelClass : MonoBehaviour {

	[SerializeField] Button m_nowResearch;
	[SerializeField] Button m_nextResearch;
	[SerializeField] Text m_researchNameText;
	[SerializeField] RectTransform m_researchGraph;
	[SerializeField] Text m_researchStatusText;
	[SerializeField] Text m_researchDataText;


	// Use this for initialization
	void Awake () {
		m_nowResearch.onClick.AddListener (new UnityAction (nowResearchEvents));
		m_nextResearch.onClick.AddListener (new UnityAction (nextResearchEvents));
	}

	void OnEnable(){
		StartCoroutine (researchPanelCoroutine ());
	}

	IEnumerator researchPanelCoroutine(){
		while (gameObject.activeSelf) {
			uiDataView();
			yield return null;
		}
	}

	private void uiDataView(){
		
		ResearchClass nowResearch = AccountClass.GetInstance.researchAccount.nowResearch ();
		ResearchClass nextResearch = AccountClass.GetInstance.researchAccount.nextResearch ();

		m_nowResearch.image.sprite = (nowResearch != null) ? nowResearch.sprite : null;
		m_nextResearch.image.sprite = (nextResearch != null) ? nextResearch.sprite : null;

		m_researchNameText.text = (nowResearch != null) ? nowResearch.name : "-";
		m_researchGraph.transform.localScale = new Vector3 (AccountClass.GetInstance.researchAccount.researchRatio (), 1f);
		m_researchDataText.text = AccountClass.GetInstance.researchAccount.researchDataFormat ();
	}

	private void nowResearchEvents(){
		UIMadiatorClass.UIResearchPanelView (null, AccountClass.GetInstance.researchAccount.nowResearch ());
	}
	
	private void nextResearchEvents(){
		UIMadiatorClass.UIResearchPanelView (null, AccountClass.GetInstance.researchAccount.nextResearch ());
	}

}
