using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIEmployeeClass : UIObjectClass {



	[SerializeField] Image m_iconImage; //아이콘
	[SerializeField] Text m_nameText; //이름
	[SerializeField] Text m_characterTypeText; //성격
	[SerializeField] Text m_rankText; //직급
	[SerializeField] Text m_genderText; //성별
	[SerializeField] Text m_contentsText; //설명
	[SerializeField] Text m_workmanshipText; //숙련도
	[SerializeField] Text m_healthText; //체력
	[SerializeField] Text m_quicknessText; //순발력
	[SerializeField] Text m_intelligenceText; //지능
	[SerializeField] Text m_charmText; //매력
	[SerializeField] Text m_experianceText; //경험
	[SerializeField] RectTransform m_experianceBar; //경험바
	[SerializeField] Text m_salaryText; //급여

	[SerializeField] GameObject m_employeeFeePanel; //고용비패널
	[SerializeField] Text m_employeeFeeText; //고용

	[SerializeField] GameObject m_graphBar; //그래프바

	//사용하지 않음
	[SerializeField] Text m_happyText; //행복
	[SerializeField] GameObject m_happyBar; //행복바

	[SerializeField] GameObject m_useBtnPanel; //일하는 직원
	[SerializeField] GameObject m_setBtnPanel; //사무실에 있는 직원
	[SerializeField] GameObject m_empFeeBtnPanel; //고용해야할 직원

	EmployeeClass m_employee;
	
	UIObjectClass m_parent;
	TYPE_PANEL m_panelType;
	int m_nowPos;
	
	/// <summary>
	/// 직원창 열기
	/// </summary>
	/// <param name="employee">직원.</param>
	/// <param name="panelType">패널 타입.</param>
	/// <param name="usePos">현재 위치.</param>
	public void setEmployeePanel(UIObjectClass parent, EmployeeClass employee, TYPE_PANEL panelType, int nowPos = 0){
		m_parent = parent;
		m_employee = employee; //시설
		m_nowPos = nowPos; //현재 위치
		//		Debug.Log ("시설 : " + m_facility.name);
//		Debug.Log ("위치 : " + m_nowPos);
		classPanelView (panelType);

	}

	/// <summary>
	/// 클래스 패널 보이기
	/// </summary>
	private void classPanelView(TYPE_PANEL panelType){
		//m_iconImage = 
		gameObject.SetActive (true);
		Debug.Log ("보기");
		
		if (m_employee != null) {

			if (m_employee.usePos != 0)
				panelType = TYPE_PANEL.NONE;

			StartCoroutine(panelUpdate());


		} 
		else {
			Debug.Log("직원 제외");
			panelType = TYPE_PANEL.SET;
			m_iconImage.sprite = EmployeePackageClass.GetInstance.getEmptyEmployeeSprite();
			m_nameText.text = "-";
			m_characterTypeText.text = "-"; //성격
			m_rankText.text = "-"; //직급
			m_genderText.text = "-"; //성별
			m_contentsText.text = "-"; //설명
			m_workmanshipText.text = "-"; //숙련도
			m_healthText.text = "-"; //체력
			m_quicknessText.text = "-"; //순발력
			m_intelligenceText.text = "-"; //지능
			m_charmText.text = "-"; //매력
			m_experianceText.text = "-"; //경험
			m_salaryText.text = "-"; //급여
			m_employeeFeeText.text = "-"; //고용
			m_contentsText.text = "-"; //설명
			m_experianceBar.localScale = Vector2.up;

		}
		
		panelView (panelType);
		
	}

	IEnumerator panelUpdate(){

		while (this.isActiveAndEnabled) {
			m_iconImage.sprite = m_employee.faceIcon;
			m_nameText.text = m_employee.name;
			m_characterTypeText.text = TranslatorClass.GetInstance.getCharacterWord(m_employee.characterType); //성격
			m_genderText.text = m_employee.gender.ToString (); //성별
			m_contentsText.text = m_employee.contents; //설명
			m_rankText.text = TranslatorClass.GetInstance.getRank(m_employee.rank);
			m_workmanshipText.text = m_employee.workmanship.ToString (); //숙련도
			m_healthText.text = m_employee.health.ToString (); //체력
			m_quicknessText.text = m_employee.quickness.ToString (); //순발력
			m_intelligenceText.text = m_employee.intelligence.ToString (); //지능
			m_charmText.text = m_employee.charm.ToString (); //매력
			m_salaryText.text = m_employee.salary.ToString(); //급여
			m_employeeFeeText.text = m_employee.employmentFee.ToString(); //고용비
			m_experianceText.text = string.Format("{0}/{1}", m_employee.experiance, m_employee.experianceMax); //경험
			m_experianceBar.localScale = new Vector2((float)m_employee.experiance / (float)m_employee.experianceMax, 1f);
			m_contentsText.text = m_employee.contents; //설명

			yield return null;
		}

	}

	private void panelView(TYPE_PANEL panelType){
		m_panelType = panelType;
		switch (m_panelType) {
		case TYPE_PANEL.NONE:
			m_useBtnPanel.SetActive(true);
			m_empFeeBtnPanel.SetActive(false);
			m_setBtnPanel.SetActive(false);
			m_graphBar.SetActive(true);
			m_employeeFeePanel.SetActive(true);
			break;
		case TYPE_PANEL.COST:
			m_useBtnPanel.SetActive(false);
			m_empFeeBtnPanel.SetActive(true);
			m_setBtnPanel.SetActive(false);
			m_graphBar.SetActive(false);
			m_employeeFeePanel.SetActive(true);
			break;
		case TYPE_PANEL.SET:
			m_useBtnPanel.SetActive(false);
			m_empFeeBtnPanel.SetActive(false);
			m_setBtnPanel.SetActive(true);
			m_graphBar.SetActive(true);
			m_employeeFeePanel.SetActive(false);
			break;
		}
	}

	/// <summary>
	/// 직원 패널 닫기 클릭
	/// </summary>
//	public void panelCloseClicked(){
//		panelClose ();
//	}

	/// <summary>
	/// 직원 변경 버튼
	/// </summary>
	public void changeEmployeeClicked(){
		//관리창 열기 - 변경창으로
		panelClose ();
		UIMadiatorClass.UIManagementPanelView (m_nowPos, this);
	}
	
	/// <summary>
	/// 직원 배치 버튼
	/// </summary>
	public void setEmployeeClicked(){
		//관리창 열기 - 변경창으로
		
		panelClose ();
		//메뉴에서 열었으면
		if (m_parent is UIProductionMenuClass) {
			UIMadiatorClass.UIManagementPanelView (m_nowPos, this);
			//관리창 열기
		} 
		//관리창 내에 시설요약창에서 열었으면
		else if(m_parent is UIManagementClass){
			//매니저 하이라이트 열기
			UIMadiatorClass.setAllHighLightOn(m_employee);
			m_parent.panelCloseEvents();
		}

	}
	
	/// <summary>
	/// 직원 해고 버튼
	/// </summary>
	public void fireEmployeeClicked(){
		//시설 팔기 
		
		UIProductionLineClass productLine = UIMadiatorClass.getUIProductionLine (m_nowPos);
		
		//if (productLine.employeeCase.employee != null) {
		//직원이 사용중입니다. 그래도 해고하시겠습니까? 해고시 추가 비용이 발생합니다.
		//} else {
		//해고하시겠습니까? 메시지
		
		if (AccountClass.GetInstance.fireEmployee (m_employee)) {
			productLine.initEmployeeCase(null);
			panelClose();
			//해고시 추가 비용이 듬 - 직원 경력(지낸월수) + 직원 숙련도(현재숙련도)
			Debug.Log("해고 완료");
		}
		UIMadiatorClass.UITextPanelView (m_employee.faceIcon, "해고 완료");

		
		
	}
	
	/// <summary>
	/// 직원 해제 버튼
	/// </summary>
	public void resetEmployeeClicked(){
		UIProductionLineClass productLine = UIMadiatorClass.getUIProductionLine (m_nowPos);
		productLine.initEmployeeCase (null);
		m_employee.usePos = 0;

		if (m_parent != null) {
			if (m_parent is UIManagementClass){
				((UIManagementClass)m_parent).resetEmployeeSummaryData (m_employee);
			}
		}
		UIMadiatorClass.UITextPanelView (m_employee.faceIcon, "직원 해제");

		panelClose ();
	}
	
	/// <summary>
	/// 직원 고용 버튼
	/// </summary>
	public void hireEmployeeClicked(){
		if (AccountClass.GetInstance.hireEmployee (m_employee)) {
			Debug.Log ("고용 완료");
			UIMadiatorClass.UITextPanelView (m_employee.faceIcon, "고용 완료");
//			UIMadiatorClass.UIMsgPanelView.initMsgPanel ("고용 완료", TYPE_MSG_ICON.INFOR);

//			UIMadiatorClass.UIMsgPanelView("고용 완료");
			//고용시 해당 하위 데이터 삭제
			//Destroy(m_parent.gameObject);
			((UIShopClass)m_parent).removeEmployeeSummary (m_employee);
			panelClose();
		} else {
			UIMadiatorClass.UIMsgPanelView.initMsgPanel ("자금이 부족합니다.", TYPE_MSG_ICON.ERROR);
		}
		
		
	}
}
