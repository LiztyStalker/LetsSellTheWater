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
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIFacilitySummaryClass : UIObjectClass
{

//	enum TYPE_PANEL{NONE, COST, SET, UPGRADE}

	[SerializeField] Text m_nameText;
	[SerializeField] Image m_iconImage;
	[SerializeField] Text m_typeText;
	[SerializeField] Text m_productContentsText;
	[SerializeField] Text m_productText;
	[SerializeField] Text m_timeText;
	[SerializeField] Text m_upkeepText;
	[SerializeField] Text m_costText;
	[SerializeField] Text m_usePosText;
	[SerializeField] Text m_statusText;
	[SerializeField] Text m_btnText;


	[SerializeField] TYPE_PANEL m_typePanel;


	UIObjectClass m_parent;
	int m_nowPos = 0;

	FacilityClass m_facility;

	bool m_isActive = false;


//	public int nowPos{ get { return m_nowPos; } }
	public FacilityClass facility{ get { return m_facility; } }

	/// <summary>
	/// 시설 요약 초기화
	/// </summary>
	/// <param name="facility">Facility.</param>
	/// <param name="usePos">Use position.</param>
	public void setFacilityClass(UIObjectClass parent, FacilityClass facility, int nowPos = 0){

		gameObject.SetActive (true);

		m_parent = parent;
		m_facility = facility;
		m_nowPos = nowPos;
		m_isActive = true;
		if (m_nowPos == 0) {
			GetComponent<Button> ().enabled = true;
			GetComponent<Button> ().onClick.AddListener (new UnityAction (facilityClicked));

		}
		else
			GetComponent<Button> ().enabled = false;

		//StartCoroutine(facilityCoroutine());
		if(m_facility != null)
			setPanelView ();

	}

	public void setPanelView(){
		m_iconImage.sprite = m_facility.sprite;
		Vector2 sizeDelta = m_iconImage.GetComponent<RectTransform>().sizeDelta;
		Rect facRect = m_facility.sprite.rect;
		float ratio = sizeDelta.x / facRect.width;
		m_iconImage.GetComponent<RectTransform>().sizeDelta = new Vector2(facRect.width * ratio, facRect.height * ratio);

		
		m_nameText.text = m_facility.name;


		switch (m_facility.facilityType) {
		case TYPE_FACILITY.PRODUCT:
			m_typeText.text = "생산 시설";
			m_productContentsText.text = "용량";
			m_productText.text = string.Format("{0}", PrepClass.literCalculate(m_facility.product));
			break;
		case TYPE_FACILITY.MANUFACTURE:
			m_typeText.text = "제조 시설";
			m_productContentsText.text = "수량";
			m_productText.text = string.Format("{0}개", m_facility.product);

			break;
		case TYPE_FACILITY.BUSINESS:
			m_typeText.text = "판매 시설";
			m_productContentsText.text = "판매가";
			m_productText.text = string.Format("{0}", m_facility.product);

			break;
		case TYPE_FACILITY.RESEARCH:
			m_typeText.text = "연구 시설";
			m_productContentsText.text = "연구량";
			m_productText.text = string.Format("{0}", m_facility.product);

			break;
		}

		m_timeText.text = string.Format("{0:f2}s", m_facility.time);

		m_upkeepText.text = m_facility.upkeep.ToString();
		m_costText.text = m_facility.cost.ToString();
		m_usePosText.text = (m_facility.usePos == 0) ? "미배치" : string.Format("{0}-{1}", m_facility.usePos / 100, m_facility.usePos % 100);
		m_statusText.text = (m_facility.usePos == 0) ? "-" : "가동중";
		m_btnText.text = (m_nowPos == 0) ? "판매" : "배치";
	}


//	IEnumerator facilityCoroutine(){
//
//		while (m_isActive) {
//			Debug.Log ("activeSelf");
//			setPanelView();
//			yield return new WaitForSeconds(0.1f);
//		}
//
//		Debug.Log ("end");
//
//	}

	private void facilityClicked(){
		UIMadiatorClass.UIEffectSoundPlayer(TYPE_AUDIO.NONE);

		UIMadiatorClass.UIFacilityPanelView (m_parent, m_facility, m_typePanel, m_facility.usePos);


	}


	/// <summary>
	/// 보조버튼 클릭
	/// </summary>
	public void subBtnClicked(){
		//0이면 판매
		//1이상이면 배치 또는 변경 - 사용중인 시설이면 불가능
		//

		if (m_nowPos == 0) {
			//판매
			Debug.Log ("판매 : " + m_nowPos);
			if (m_facility.usePos != 0){
				effectSoundPlayer(TYPE_AUDIO.WARNING);
				UIMadiatorClass.UIMsgPanelView.initMsgPanel("사용중인 시설은 판매할 수 없습니다.", TYPE_MSG_ICON.ERROR);
			}
			else {
				sellFacility ();
//				UIMadiatorClass.UIMsgPanelView("판매하시겠습니까?");
			}
		} else {

			if(m_facility.usePos != 0){
				effectSoundPlayer(TYPE_AUDIO.WARNING);
				UIMadiatorClass.UIMsgPanelView.initMsgPanel ("사용중인 시설은 설치 및 변경할 수 없습니다.", TYPE_MSG_ICON.ERROR);
				return;
			}


			UIMadiatorClass.UIEffectSoundPlayer(TYPE_AUDIO.NONE);


			//생산라인 가져오기
			UIProductionLineClass productLine = UIMadiatorClass.getUIProductionLine(m_nowPos);


			//if(UIMadiatorClass.getProductPanel() == m_facility.facilityType){
			//삽입
				if(productLine.facilityCase.facility == null){
					m_facility.usePos = m_nowPos;
					productLine.initFacilityCase(m_facility);
					Debug.Log("삽입");
				}

				//변경
				else{

					// 삽입 m_facility
					// 위치 변경 productLine.facility

					//productLine.facilityCase.facility;

					productLine.facilityCase.facility.usePos = 0;

					m_facility.usePos = m_nowPos;
					productLine.initFacilityCase(m_facility);
					Debug.Log("변경");

					//모든 시설요약버튼 새로고침

					//현재 시설 반환
					//새 시설 삽입
				}
			//}
//			else{
//				UIMadiatorClass.UIMsgPanelView ("생산라인에 맞지 않습니다.");
//				return;
//			}
			



			setPanelView();

			Debug.Log ("장착 : " + m_nowPos);
			UIMadiatorClass.UIMsgPanelView.initMsgPanel("배치 완료", TYPE_MSG_ICON.INFOR);

			m_parent.gameObject.SetActive(false);
		}
	}


//	protected override void OnDisable(){
//		Debug.Log ("disable");
//	}

	/// <summary>
	/// 시설 판매하기
	/// </summary>
	private void sellFacility(){
//		UIProductionLineClass productLine = UIMadiatorClass.getUIProductionLine(m_nowPos);

//		Debug.Log ("productLine " + productLine);

		if (AccountClass.GetInstance.sellFacility (m_facility)) {
			//상위 리스트 오브젝트도 제거해야 함 - 상관없음
			effectSoundPlayer(TYPE_AUDIO.SELL);
			UIMadiatorClass.UIManagementPanelView(0, null);
//			Destroy(gameObject);

		}


	}

	public void removePanel(){
		m_isActive = false;
		Destroy (gameObject);
	}

}

