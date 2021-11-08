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
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Linq;

public enum TYPE_VIEW{FACILITY, EMPLOYEE}

public class UIManagementClass : UIObjectClass
{
	//페이지 최대 보여줄 수 있는 개수
	const int c_numberCount = 5;

	//아이템을 보여줄 수 있는 최대 개수
	const int c_viewCount = 10;


	[SerializeField] Text m_titleText;
	[SerializeField] Scrollbar m_scrollbar;
	[SerializeField] Toggle m_facilityToggleBtn;
	[SerializeField] Toggle m_employeeToggleBtn;
	[SerializeField] Toggle[] m_subBtnToggle;
//	[SerializeField] Toggle m_notUseToggle;
	[SerializeField] Button m_leftBtn;
	[SerializeField] Button m_rightBtn;
	[SerializeField] UINumberBtnClass m_numberToggleBtn;

	[SerializeField] RectTransform m_scrollPanel;
	[SerializeField] GameObject m_viewPanel;
	[SerializeField] GameObject m_numberPanel;
	[SerializeField] GameObject m_btnPanel;
	[SerializeField] GameObject m_subbtnPanel;
	[SerializeField] GameObject m_facilityBtnsPanel;
	[SerializeField] GameObject m_employeeBtnsPanel;

	[SerializeField] Toggle m_toggleBtn;
	[SerializeField] UIFacilitySummaryClass m_facilitySummary;
	[SerializeField] UIEmployeeSummaryClass m_employeeSummary;

	List<Toggle> toggleMainBtnList = new List<Toggle>();
	List<Toggle> toggleSubBtnList = new List<Toggle>();

	//배치 위치
	int m_usePos = 0;

	List<GameObject> m_tmpList = new List<GameObject>();
	List<UINumberBtnClass> m_numberBtnList = new List<UINumberBtnClass> ();

	//0 = 시설
	//1 = 직원
	TYPE_VIEW m_type = TYPE_VIEW.FACILITY;
	TYPE_FACILITY m_process = TYPE_FACILITY.NONE;


	//현재 페이지
	int m_page = 0;
	//최대 페이지
	int m_pageMax = 0;

	//미사용 여부
	bool m_isNotUse = false;


	void Awake(){
		mainBtnToggleCreate ();
		//		gameObject.GetComponentsInChildren<Toggle> ();
	}

	protected override void OnEnable ()
	{
		base.OnEnable ();

	}




	private void mainToggleSelection(){
		foreach (TYPE_VIEW view in Enum.GetValues(typeof(TYPE_VIEW))) {
			if(view == m_type) toggleMainBtnList [(int)view].isOn = true;
			else toggleMainBtnList [(int)view].isOn = false;
		}
	}


	private void subToggleSelection(){
		switch(m_type){
		case TYPE_VIEW.FACILITY:
			for(int i = 0; i < Enum.GetValues(typeof(TYPE_FACILITY)).Length; i++){
				if((TYPE_FACILITY)(i - 1) == m_process) toggleSubBtnList [i].isOn = true;
				else toggleSubBtnList [i].isOn = false;
			}
			break;
		case TYPE_VIEW.EMPLOYEE:
			toggleSubBtnList [0].isOn = true;
			break;
		}
	}

	private void subBtnToggleCreate(TYPE_VIEW viewType){
		
		panelSubToggleClear ();

		switch(viewType){
		case TYPE_VIEW.FACILITY:
		//서브 버튼 제작 



			//전체
			Toggle toggleBtnTotal = (Toggle)Instantiate(m_toggleBtn);
			//			toggleBtn.GetComponentInChildren<Text>().text = 
			//			toggleBtn.group = m_subbtnPanel.GetComponent<ToggleGroup>();
			toggleBtnTotal.GetComponent<UIToggleBtnClass>().setParent(
				this,
				m_subbtnPanel.GetComponent<ToggleGroup>(), 
				TranslatorClass.GetInstance.getFacilityWord(TYPE_FACILITY.NONE),
				(int)TYPE_FACILITY.NONE,
				subBtnClicked
				);
			
			toggleBtnTotal.transform.SetParent (m_subbtnPanel.transform);
			toggleSubBtnList.Add(toggleBtnTotal);
			
			foreach (TYPE_FACILITY type in Enum.GetValues(typeof(TYPE_FACILITY))) {

				if(type == TYPE_FACILITY.NONE)
					continue;

				Toggle toggleBtn = (Toggle)Instantiate(m_toggleBtn);
	//			toggleBtn.GetComponentInChildren<Text>().text = 
	//			toggleBtn.group = m_subbtnPanel.GetComponent<ToggleGroup>();
				toggleBtn.GetComponent<UIToggleBtnClass>().setParent(
																	this,
																	m_subbtnPanel.GetComponent<ToggleGroup>(), 
																	TranslatorClass.GetInstance.getFacilityWord(type),
																	(int)type,
																	subBtnClicked
																	);

				toggleBtn.transform.SetParent (m_subbtnPanel.transform);
				toggleSubBtnList.Add(toggleBtn);
			}

			break;
		case TYPE_VIEW.EMPLOYEE:
			Toggle toggleBtn2 = (Toggle)Instantiate(m_toggleBtn);
			//		toggleBtn1.GetComponentInChildren<Text>().text = "미사용";
			toggleBtn2.GetComponent<UIToggleBtnClass>().setParent(
				this,
				null, 
				"전체",
				(int)m_type,
				subBtnClicked
				);
			toggleBtn2.transform.SetParent (m_subbtnPanel.transform);
			toggleSubBtnList.Add(toggleBtn2);
			break;
		}



		if (m_btnPanel.activeSelf) {

			Toggle toggleBtn1 = (Toggle)Instantiate (m_toggleBtn);
//		toggleBtn1.GetComponentInChildren<Text>().text = "미사용";
			toggleBtn1.GetComponent<UIToggleBtnClass> ().setParent (
														this,
														null, 
														"미사용",
														(int)m_type,
														notUseBtnClicked
			);
			toggleBtn1.transform.SetParent (m_subbtnPanel.transform);
			toggleSubBtnList.Add (toggleBtn1);

			toggleBtn1.isOn = m_isNotUse;

		}
		
		toggleSubBtnList[(int)m_process + 1].isOn = true;

	}

	private void mainBtnToggleCreate(){
		//상위 버튼 제작
		foreach (TYPE_VIEW type in Enum.GetValues(typeof(TYPE_VIEW))) {
			Toggle toggleBtn = (Toggle)Instantiate(m_toggleBtn);
//			toggleBtn.GetComponentInChildren<Text>().text = TranslatorClass.GetInstance.getViewWord(type);
//			toggleBtn.group = m_btnPanel.GetComponent<ToggleGroup>();
			toggleBtn.GetComponent<UIToggleBtnClass>().setParent(
																this,
																m_btnPanel.GetComponent<ToggleGroup>(), 
																TranslatorClass.GetInstance.getViewWord(type),
																(int)type,
																managerBtnClicked
																);
			toggleBtn.transform.SetParent (m_btnPanel.transform);
			toggleMainBtnList.Add(toggleBtn);
		}
	}

	/// <summary>
	/// 관리패널 열기
	/// </summary>
	/// <param name="usePos">현재 배치 위치 - 0이면 관리창 1이상이면 현재 배치 위치</param>
	public void setManagementPanel(int nowPos = 0, UIObjectClass uiObject = null){
		//직원창
		//시설창

		m_usePos = nowPos;

		//레이아웃 맞추기
		m_viewPanel.GetComponent<LayoutElement>().minHeight = m_scrollPanel.rect.height;


		Debug.Log ("현재 위치 : " + nowPos);

		//panelClear ();
		gameObject.SetActive (true);


		if(uiObject == null) {
			Debug.Log ("null");
//			subBtnClicked (m_process);
//			facilityManagementView (nowPos);

			//m_type = TYPE_VIEW.FACILITY;
			//m_process = TYPE_FACILITY.NONE;
			//m_page = 0;
			//m_isNotUse = false;

			m_btnPanel.SetActive (true);
			//m_facilityBtnsPanel.SetActive (false);
			//m_employeeBtnsPanel.SetActive (false);
		}
		else if (uiObject is UIEmployeeClass) {
			Debug.Log ("employee");
			m_type = TYPE_VIEW.EMPLOYEE;
			m_isNotUse = true;
			m_page = 0;
			m_process = TYPE_FACILITY.NONE;
//			employeeManagementView (nowPos);
			m_btnPanel.SetActive (false);
			//m_facilityBtnsPanel.SetActive (false);
			//m_employeeBtnsPanel.SetActive (false);
		} else if (uiObject is UIFacilityClass) {
			Debug.Log ("facility");
			m_type = TYPE_VIEW.FACILITY;
			m_isNotUse = true;
			m_page = 0;
			m_process = TYPE_FACILITY.NONE;

//			m_fa ((UIFacilityClass)uiObject).facilityType;
//			facilityManagementView (nowPos);
			m_btnPanel.SetActive (false);
			//m_facilityBtnsPanel.SetActive (false);
			//m_employeeBtnsPanel.SetActive (false);
		} 

		subBtnToggleCreate (m_type);
		mainToggleSelection ();
		subToggleSelection ();

		
		//사용중인 시설도 보여주어야 함

		subBtnClicked (m_process);


	}

	/// <summary>
	/// 페이지 버튼 생성
	/// </summary>
	private void pageBtnCreate(){
		panelNumberClear ();
		Debug.Log ("pageMax : " + m_pageMax);
		int pageNum = (m_page / c_numberCount + 1) * c_numberCount;
		int pageStart = m_page / c_numberCount * c_numberCount;
		//전 페이지가 있으면 ... 삽입
//		if (pageNum != 0) {
//			UINumberBtnClass numberTgl = (UINumberBtnClass)Instantiate (m_numberToggleBtn);
//			numberTgl.setParent (this, m_numberPanel.GetComponent<ToggleGroup> (), "...");
//			numberTgl.transform.SetParent (m_numberPanel.transform);
//			m_numberBtnList.Add (numberTgl);
//		}

		//5장씩 넘기기
		for (int i = pageStart + 1; i <= pageNum; i++) {

			if(i > m_pageMax)
				break;

			UINumberBtnClass numberTgl = (UINumberBtnClass)Instantiate(m_numberToggleBtn);
			numberTgl.setParent(this, m_numberPanel.GetComponent<ToggleGroup>(), i);
			numberTgl.transform.SetParent(m_numberPanel.transform);
			m_numberBtnList.Add(numberTgl);

		}

//		if (pageNum < m_pageMax) {
//			UINumberBtnClass numberTgl = (UINumberBtnClass)Instantiate (m_numberToggleBtn);
//			numberTgl.setParent (this, m_numberPanel.GetComponent<ToggleGroup> (), "...");
//			numberTgl.transform.SetParent (m_numberPanel.transform);
//			m_numberBtnList.Add (numberTgl);
//		}
		//후 페이지가 있으면 ... 삽입
		if(m_numberBtnList.Count > 0)
			m_numberBtnList [m_page % c_numberCount].isOn = true;
	}


	private void panelClear(){
		foreach (GameObject tmpObj in m_tmpList) {
			Destroy(tmpObj);
		}

		m_tmpList.Clear ();

	}

	private void panelNumberClear(){
		foreach (UINumberBtnClass tmpObj in m_numberBtnList) {
			Destroy(tmpObj.gameObject);
		}
		
		m_numberBtnList.Clear ();
		
	}

	/// <summary>
	/// 시설 닫기
	/// </summary>
	public void panelCloseClicked(){

		panelClose ();
	}


	public void managerBtnClicked(int typeView){
		m_type = (TYPE_VIEW)typeView;
		m_process = TYPE_FACILITY.NONE;
		m_page = 0;

//		switch (m_type) {
//		case TYPE_VIEW.EMPLOYEE:
//			m_facilityBtnsPanel.SetActive (false);
//			m_employeeBtnsPanel.SetActive (true);
//			break;
//		case TYPE_VIEW.FACILITY:
//			m_facilityBtnsPanel.SetActive (true);
//			m_employeeBtnsPanel.SetActive (false);
//			break;
//		}
		subBtnToggleCreate (m_type);
		subBtnClicked (m_process);
	}

//	public void facilityManagementBtn(){
//		//		facilityManagementView (0);
//		m_type = 0;
//		m_subBtnPanel.SetActive (true);
//	}
//
//
//	public void employeeManagementBtn(){
//
////		employeeManagementView (0);
//		m_type = 1;
//		m_subBtnPanel.SetActive (true);
//	}


	private void employeeManagementView(int nowPos, TYPE_FACILITY type = 0){


		panelClear ();

		m_pageMax = AccountClass.GetInstance.employeePageMax (c_viewCount, type, m_isNotUse);
		
		if (m_page > m_pageMax - 1)
			m_page = m_pageMax - 1;
		
		List<EmployeeClass> employeeArray = AccountClass.GetInstance.getEmployee(type, c_viewCount, m_page, m_isNotUse);
		if (employeeArray != null) {
			Debug.Log("시설 개수 : " + employeeArray.Count);
			foreach (EmployeeClass employee in employeeArray) {
				UIEmployeeSummaryClass tmpEmployee = (UIEmployeeSummaryClass)Instantiate(m_employeeSummary);
				tmpEmployee.transform.SetParent(m_viewPanel.transform);
				tmpEmployee.setEmployeeClass(this, employee, nowPos);
				m_tmpList.Add(tmpEmployee.gameObject);
			}
		}

		//맨위로
		m_scrollbar.value = 1f;

		pageBtnCreate ();
	}

	private void facilityManagementView(int nowPos, TYPE_FACILITY type = 0){
		Debug.Log ("시설 관리 열기");
		panelClear ();

		m_pageMax = AccountClass.GetInstance.facilityPageMax (c_viewCount, type, m_isNotUse);

		if (m_page > m_pageMax - 1)
			m_page = m_pageMax - 1;

		List<FacilityClass> facilityArray = AccountClass.GetInstance.getFacility(type, c_viewCount, m_page, m_isNotUse);
		if (facilityArray != null) {
			Debug.Log("시설 개수 : " + facilityArray.Count);
			foreach (FacilityClass facility in facilityArray) {
				UIFacilitySummaryClass tmpFacility = (UIFacilitySummaryClass)Instantiate (m_facilitySummary);
				tmpFacility.transform.SetParent (m_viewPanel.transform);
				tmpFacility.setFacilityClass (this, facility, nowPos);
				m_tmpList.Add (tmpFacility.gameObject);
			}
		}

		//맨위로
		m_scrollbar.value = 1f;

		pageBtnCreate ();
	}

	public void resetFacilitySummaryData(FacilityClass facility){
		GameObject tmpData = m_tmpList.Where (tmp => tmp.GetComponent<UIFacilitySummaryClass> ().facility.Equals(facility)).Select (tmp => tmp).SingleOrDefault ();
		if (tmpData != null) {
			Debug.Log("set");
			tmpData.GetComponent<UIFacilitySummaryClass>().setPanelView();
		}
		Debug.Log("ok");
	}

	public void resetEmployeeSummaryData(EmployeeClass employee){
		GameObject tmpData = m_tmpList.Where (tmp => tmp.GetComponent<UIEmployeeSummaryClass> ().employee.Equals(employee)).Select (tmp => tmp).SingleOrDefault ();
		if (tmpData != null) {
			Debug.Log("set");
			tmpData.GetComponent<UIEmployeeSummaryClass>().setPanelView();
		}
		Debug.Log("ok");
	}

	/// <summary>
	/// 보조 버튼 클릭
	/// </summary>
	/// <param name="index">Index.</param>
	public void subBtnClicked(int type){
		m_page = 0;
		subBtnClicked ((TYPE_FACILITY)type);
	}

	private void subBtnClicked(TYPE_FACILITY type){
		m_process = type;

		switch (m_type) {
		case TYPE_VIEW.FACILITY:
			facilityManagementView(m_usePos, m_process);
			break;
		case TYPE_VIEW.EMPLOYEE:
			employeeManagementView(m_usePos, m_process);
			break;
		}
	}

	protected override void OnDisable (){
		panelClear ();
		panelNumberClear ();
		panelSubToggleClear ();
		base.OnDisable ();
	}

	public void leftBtnClicked(){
		m_page--;
		if (m_page < 0)
			m_page = 0;

		subBtnClicked (m_process); //페이지 초기화 필요
	}

	public void rightBtnClicked(){
		m_page++;
		if (m_page > m_pageMax - 1)
			m_page = m_pageMax - 1;

		subBtnClicked (m_process);
	}

	public void notUseBtnClicked(int page = 0){
		m_page = 0;
		m_isNotUse = !m_isNotUse;
		//m_notUseToggle.isOn = m_isNotUse;
		Debug.Log ("notUse : " + m_isNotUse);
		subBtnClicked (m_process);
	}

	private void pageBtnClicked(int page){
		m_page = page;
		subBtnClicked (m_process);
	}

	public void indexBtnClicked(int page){
		m_page = page;
		subBtnClicked (m_process);
	}

	private void panelSubToggleClear(){
		foreach (Toggle toggle in toggleSubBtnList) {
			Destroy(toggle.gameObject);
		}
		toggleSubBtnList.Clear ();
	}



}

