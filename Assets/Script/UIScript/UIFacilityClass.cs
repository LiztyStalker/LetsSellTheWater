using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum TYPE_PANEL{NONE, COST, SET, UPGRADE}

public class UIFacilityClass : UIObjectClass {

	const float c_maxWidth = 180f;
	const float c_maxHeight = 110f;

	FacilityClass m_facility;
	[SerializeField] Image m_iconImage;
	[SerializeField] Text m_titleText;
	[SerializeField] Text m_nameText;
	[SerializeField] Text m_typeText;
	[SerializeField] Text m_contentsText; //
	[SerializeField] Text m_productContentsText;
	[SerializeField] Text m_productText;
	[SerializeField] Text m_contText; //
	[SerializeField] Text m_timeText;
	[SerializeField] Text m_upkeepText; //유지비
	[SerializeField] Text m_costText; //구입비
//	[SerializeField] Text m_durabilityText;

	[SerializeField] GameObject m_contPanel; //오염도 패널
	[SerializeField] GameObject m_costPanel; //구입 패널

	[SerializeField] UIFacilitySummaryClass m_facilUpgradeData;
	[SerializeField] GameObject m_upgradePanel; //업글 패널
	[SerializeField] GameObject m_useBtnPanel; //사용중인 시설
	[SerializeField] GameObject m_setBtnPanel; //인벤토리에 있는 시설
	[SerializeField] GameObject m_costBtnPanel; //구입해야할 시설

	UIObjectClass m_parent;
	TYPE_PANEL m_panelType;
	int m_nowPos;

//	public TYPE_FACILITY facilityType{ get { return m_facility.facilityType; } }

	/// <summary>
	/// 시설창 열기
	/// </summary>
	/// <param name="facility">시설.</param>
	/// <param name="panelType">패널 타입.</param>
	/// <param name="usePos">현재 위치.</param>
	public void setFacilityPanel(UIObjectClass parent, FacilityClass facility, TYPE_PANEL panelType, int nowPos = 0){
		m_parent = parent;
		m_facility = facility; //시설
		m_nowPos = nowPos; //현재 위치
//		Debug.Log ("시설 : " + m_facility.name);
		Debug.Log ("위치 : " + m_nowPos);
		classPanelView (panelType);
	}

	/// <summary>
	/// 클래스 패널 보이기
	/// </summary>
	private void classPanelView(TYPE_PANEL panelType){
		//m_iconImage = 
		gameObject.SetActive (true);
		Debug.Log ("보기");

		m_contPanel.SetActive(false);

		if (m_facility != null) {

			if (m_facility.usePos != 0)
				panelType = TYPE_PANEL.NONE;

			Debug.Log("시설 설정");
			m_iconImage.sprite = m_facility.sprite;


			Rect facRect = m_facility.sprite.rect;
			m_iconImage.GetComponent<RectTransform>().sizeDelta = new Vector2(facRect.width, facRect.height);
			m_nameText.text = m_facility.name;

			switch(m_facility.facilityType){
			case TYPE_FACILITY.PRODUCT:
				m_typeText.text = "생산 시설";//m_facility.facilityType;
				m_productContentsText.text = "용량";
				m_contPanel.SetActive(true);
				m_contText.text = string.Format("{0}", m_facility.contamination);
				m_productText.text = string.Format("{0}", PrepClass.literCalculate(m_facility.product));
				break;
			case TYPE_FACILITY.MANUFACTURE:
				m_typeText.text = "제조 시설"; //m_facility.facilityType;
				m_productContentsText.text = "수량";
				m_productText.text = string.Format("{0}개", m_facility.product);
				break;
			case TYPE_FACILITY.BUSINESS:
				m_typeText.text = "판매 시설";//m_facility.facilityType;
				m_productContentsText.text = "판매가";
				m_productText.text = string.Format("{0}", m_facility.product);
				break;
			case TYPE_FACILITY.RESEARCH:
				m_typeText.text = "연구 시설";// m_facility.facilityType;
				m_productContentsText.text = "연구량";
				m_productText.text = string.Format("{0}", m_facility.product);
				break;
			}


			m_timeText.text = string.Format("{0:f2}s", m_facility.time);
			m_upkeepText.text = string.Format("{0}", m_facility.upkeep);
			m_costText.text = string.Format("{0}", m_facility.cost);

			m_contentsText.text = m_facility.contents;
		} 
		else {
			Debug.Log("시설 제외");
			panelType = TYPE_PANEL.SET;
			m_typeText.text = "-";
			m_iconImage.sprite = FacilityPackageClass.GetInstance.getEmptyFacilitySprite();
			m_nameText.text = "-";
			m_productText.text = "-";
			m_timeText.text = "-";
			m_upkeepText.text = "-";
			m_costText.text = "-";
			m_contentsText.text = "-";
		}

		panelView (panelType);

	}

	private void panelView(TYPE_PANEL panelType){
		m_panelType = panelType;
		switch (m_panelType) {
		case TYPE_PANEL.NONE:
			m_useBtnPanel.SetActive(true);
			m_costBtnPanel.SetActive(false);
			m_setBtnPanel.SetActive(false);
			m_costPanel.SetActive(true);
			break;
		case TYPE_PANEL.COST:
			m_useBtnPanel.SetActive(false);
			m_costBtnPanel.SetActive(true);
			m_setBtnPanel.SetActive(false);
			m_costPanel.SetActive(true);
			break;
		case TYPE_PANEL.SET:
			m_useBtnPanel.SetActive(false);
			m_costBtnPanel.SetActive(false);
			m_setBtnPanel.SetActive(true);
			m_costPanel.SetActive(false);
			break;
		case TYPE_PANEL.UPGRADE:
			break;
		}
	}

	/// <summary>
	/// 시설 패널 닫기 클릭
	/// </summary>
//	public void panelCloseClicked(){
//		panelClose ();
//	}

	/// <summary>
	/// 시설 변경 버튼
	/// </summary>
	public void changeFacilityClicked(){
		//관리창 열기 - 변경창으로
		panelClose ();
		UIMadiatorClass.UIManagementPanelView (m_nowPos, this);
	}

	/// <summary>
	/// 시설 배치 버튼
	/// </summary>
	public void setFacilityClicked(){
		//관리창 열기 - 변경창으로

		panelClose ();

		//생산 메뉴에서 열었으면
		if (m_parent is UIProductionMenuClass) {
			UIMadiatorClass.UIManagementPanelView (m_nowPos, this);
			//관리창 열기
		} 
		//관리창 내에 시설요약창에서 열었으면
		else if(m_parent is UIManagementClass){
			//매니저 하이라이트 열기
			UIMadiatorClass.setAllHighLightOn(m_facility);
			m_parent.panelCloseEvents();
		}


	}

	/// <summary>
	/// 시설 팔기 버튼
	/// </summary>
	public void sellFacilityClicked(){
		//시설 팔기 

		UIProductionLineClass productLine = UIMadiatorClass.getUIProductionLine (m_nowPos);

//		if (productLine.employeeCase.employee != null) {
		//직원이 사용중입니다. 그래도 판매하시겠습니까?
		//} else {
		//판매하시겠습니까? 메시지
		if (m_facility.usePos == 0) {
			if (AccountClass.GetInstance.sellFacility (m_facility)) {
				productLine.initFacilityCase (null);
				panelClose ();
				Debug.Log ("판매 완료");
			}
		} else {
			UIMadiatorClass.UIMsgPanelView.initMsgPanel("사용중인 시설은 판매할 수 없습니다.", TYPE_MSG_ICON.ERROR);
		}


		//}

		//직원이 있으면

		UIMadiatorClass.UITextPanelView (m_facility.sprite, "판매 완료");

	}

	/// <summary>
	/// 시설 해제 버튼
	/// </summary>
	public void resetFacilityClicked(){

		UIProductionLineClass productLine = UIMadiatorClass.getUIProductionLine (m_nowPos);
		productLine.initFacilityCase (null);
		m_facility.usePos = 0;

//		UIMadiatorClass.

		//관리 패널에서 열였을 경우
		if (m_parent != null) {
			if (m_parent is UIManagementClass){
				((UIManagementClass)m_parent).resetFacilitySummaryData (m_facility);
			}
		}

		UIMadiatorClass.UITextPanelView (m_facility.sprite, "해제 완료");

		panelClose ();
	}

	/// <summary>
	/// 시설 구입 버튼
	/// </summary>
	public void costFacilityClicked(){
//		int cost = AccountClass.GetInstance.addAssets (-m_facility.cost, TYPE_FINANCE.FACILITY);
		if (AccountClass.GetInstance.addFacility (m_facility)) {
			Debug.Log ("구입 완료");
//			UIMadiatorClass.UIMsgPanelView("구입 완료");
			UIMadiatorClass.UITextPanelView (m_facility.sprite, "구입 완료");
			panelClose ();
		} else {
			UIMadiatorClass.UIMsgPanelView.initMsgPanel ("자금이 부족합니다.", TYPE_MSG_ICON.ERROR);
		}
	}

	public void upgradeFacilityClicked(){
		//업그레이드 데이터
		//
	}


}
