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
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIMadiatorClass : MonoBehaviour
{
	[SerializeField] GameObject m_loadingPanel;
	[SerializeField] UIPanelClass m_uiPanel;
	[SerializeField] UIStatusClass m_statusPanel;
	[SerializeField] UIProductionMenuClass m_ProductionLineMenuPanel;
	[SerializeField] UIFacilityClass m_facilityPanel;
	[SerializeField] UIEmployeeClass m_emplayeePanel;
	[SerializeField] UIResearchClass m_researchPanel;
	[SerializeField] UIResearchManagementClass m_researchManagementPanel;
	[SerializeField] UIMsgClass m_msgPanel;
	[SerializeField] UIProductionLineManagerClass m_productionLineManager;
	[SerializeField] UIShopClass m_shopPanel;
	[SerializeField] UIFinanceClass m_financePanel;
	[SerializeField] UIManagementClass m_managementPanel;
	[SerializeField] UIReputationClass m_reputationPanel;
	[SerializeField] UIOptionClass m_optionPanel;
	[SerializeField] UIPauseClass m_pausePanel;
	[SerializeField] UIAdbClass m_adbPanel;

	UISoundEffectClass m_soundEffectPanel;
	UIBGMSoundClass m_BGMPanel;
	//	[SerializeField] UIProductClass m_productPanel;

	static UIProductionLineManagerClass m_productionLineManagerView;
	static UIFacilityClass m_facilityPanelView;
	static UIEmployeeClass m_emplayeePanelView;
	static UIResearchClass m_researchPanelView;
	//static UIFacilityShopClass m_facilityShopPanelView;
	static UIMsgClass m_msgPanelView;
	static UIManagementClass m_managementPanelView;
	static UIReputationClass m_reputationPanelView;
	static UIStatusClass m_statusPanelView;
	static UIPanelClass m_uiPanelView;
	static UIFinanceClass m_financePanelView;
	static UIResearchManagementClass m_researchManagementPanelView;
	static UISoundEffectClass m_soundEffectPlayer;
	static UIBGMSoundClass m_BGMPlayer;
	static UIAdbClass m_adbPanelView;

	static FacilityClass m_tmpFacility = null;
	static EmployeeClass m_tmpEmployee = null;
	static GameObject m_loadingPanelView;

//	static AudioSource m_audioSource;
//	static AudioClip[] m_backgroundClip;

	Coroutine coroutine_GameLoop = null;

	static Stack<UIObjectClass> m_panelStack = new Stack<UIObjectClass> ();

//	bool m_isPause = false;

	/// <summary>
	/// 메시지 패널 가져오기
	/// </summary>
	/// <param name="msg">Message.</param>
	public static UIMsgClass UIMsgPanelView{ get { return m_msgPanelView; } }

	void Awake(){

//		m_backgroundClip = Resources.LoadAll<AudioClip> ("Sound/BGM");
		


//		m_audioSource = GetComponent<AudioSource> ();
		//m_audioSource.clip = m_backgroundClip [1];
//		BGMMute ();


		//화면이 꺼지지 않게 하기
		Screen.sleepTimeout = SleepTimeout.NeverSleep;

		m_uiPanelView = m_uiPanel;
		m_productionLineManagerView = m_productionLineManager;
		m_facilityPanelView = m_facilityPanel;
		m_emplayeePanelView = m_emplayeePanel;
		m_msgPanelView = m_msgPanel;
		m_managementPanelView = m_managementPanel;
		m_reputationPanelView = m_reputationPanel;
		m_statusPanelView = m_statusPanel;
		m_researchManagementPanelView = m_researchManagementPanel;
		m_researchPanelView = m_researchPanel;
		m_financePanelView = m_financePanel;
		m_adbPanelView = m_adbPanel;

		m_soundEffectPanel = GameObject.Find ("Game@SoundEffect").GetComponent<UISoundEffectClass>();
		m_soundEffectPlayer = m_soundEffectPanel;


		m_BGMPanel = GameObject.Find ("Game@BGM").GetComponent<UIBGMSoundClass>();
		m_BGMPlayer = m_BGMPanel;

		m_BGMPlayer.BGMPlay (TYPE_BGM.PLAY);

		//확장이 1 이상이면
		//화살표 보임
		if(AccountClass.GetInstance.extendCount > 0)
			m_uiPanelView.midUI.setMidBtns ();

		//현재 피버타임이면
		//피버 이미지 보임
		if(AccountClass.GetInstance.isFever)
			m_uiPanelView.midUI.setFeverPanel ();
		else
			m_uiPanelView.midUI.resetFeverPanel ();


		coroutine_GameLoop = StartCoroutine (gameLoop ());

	}

	void OnDisable(){
		if(coroutine_GameLoop != null)
			StopCoroutine (coroutine_GameLoop);
	}


	IEnumerator gameLoop(){
		float looptime = 1f;
		while (true) {

//			int index = AccountClass.GetInstance.reputationAccount.getReputateIndex();
//
//			Debug.Log("index : " + index);
//
//			if(index >= 0){
////				AccountClass.GetInstance.addCustomer(CustomerPackageClass.GetInstance.getRandomCustomer(index));
//				AccountClass.GetInstance.addCustomer();
//			}

			if(AccountClass.GetInstance.getCustomerPercent() == 1f)
				UIMadiatorClass.UITextPanelView(null, "대기줄이 가득 찼습니다. 판매시설을 늘리거나 대기줄을 확장해야 합니다.");
			else
				AccountClass.GetInstance.addCustomer();
			//생산 쿨타임
			//전체 평균
			//0~100
			//초반에는 1%로 시작
			//
			//우선순위


			//AccountClass.GetInstance.researchAccount.runResearch(AccountClass.GetInstance.researchAccount.getAutomaticResearchCount());
			looptime = AccountClass.GetInstance.reputationAccount.getReputateTime();
			Debug.Log("Looptime : " + looptime);
			//평균 인지도에 따른 루프 횟수 증가
			yield return new WaitForSeconds(looptime);
		}
	}

	// Update is called once per frame
	void Update () {
		//if (Input.touchCount > 0) {



		//메뉴가 열려 있으면 메뉴 우선
		//메뉴 버튼을 눌렀으면 메뉴버튼 실행 후 닫기
		//메뉴 버튼울 누르지 않았으면
			//다른 생산라인을 선택했으면 메뉴 다시 열기
			//아무것도 선택하지 않았으면 메뉴 닫기
//		if (m_ProductionLineMenuPanel.isActiveAndEnabled) {
//
//		}

		//




		if (Input.GetKeyDown (KeyCode.Escape)) {

			if(m_pausePanel.gameObject.activeSelf){
				m_pausePanel.PauseChange();
			}
			else if(m_tmpEmployee != null || m_tmpFacility != null){
				m_tmpEmployee = null;
				m_tmpFacility = null;
				setAllHighLightOff();
			}
			else{
				if(m_panelStack.Count > 0){
					m_panelStack.Peek().panelCloseEvents();
				}
				else{
					m_msgPanelView.initMsgPanel("게임을 종료하시겠습니까?", applicationQuit, TYPE_MSG_SIGN.OKCANCEL, TYPE_MSG_ICON.WARNING);
				}
			}

		}


		if (m_tmpFacility != null) {
			if (Input.GetMouseButtonDown (0)) {
				setFacility();
			}
		}

		if (m_tmpEmployee != null) {
			if (Input.GetMouseButtonDown (0)) {
				setEmployee();
			}
		}

		//하위 메뉴가 열려 있으면
		//if (!m_uiPanel.isMenu) {

			//패널 열기
			//현재 열린 패널 중심으로 움직이기
			//


			//열려있는 창이 없으면
				//패널 중심으로 움직임
		if (Input.GetMouseButtonDown (0)){



			if(EventSystem.current.currentSelectedGameObject == null){
				if(m_panelStack.Count == 0 || m_panelStack.Peek() is UIProductionMenuClass){
					productionMenuView();
				}
			}
			else{
				if(EventSystem.current.currentSelectedGameObject.tag == "UIPopUpMenuTag"){
					return;
				}
				else if(m_panelStack.Count != 0 && m_panelStack.Peek() is UIProductionMenuClass){
					m_panelStack.Peek().panelCloseEvents();
				}

			}
//			Debug.Log("current : " + EventSystem.current.currentSelectedGameObject.name);

			//				if(m_panelStack.Count == 0){
//					m_ProductionLineMenuPanel.menuPanelClose();
					//if(EventSystem.current.currentSelectedGameObject == null){
						//메뉴를 생성하거나
					//}
//				}
		}
		//}
		//하위 메뉴가 닫혀 있으면
		//터치 중심으로 움직임

		//1회 터치
//		else if (Input.GetMouseButtonDown (0)) {
			//			m_facilityObject.actionMethod();

			//하위메뉴가 닫혀 있으면 -> 생산라인 1회 행동하기
			//if (m_uiPanel.isMenu) {
				//m_productionLineManager.runProducts ();
			//	return;
			//}


			//메뉴가 열려있을 때
			//겉을 누를 경우
			//버튼을 누를 경우



//			productionMenuView ();

//			if(!m_ProductionLineMenuPanel.gameObject.activeSelf){
//				//생산라인 메뉴창 띄우기
//			}
//			else{
//				//메뉴 패널이 열려 있으면 -> 메뉴 패널 닫기
//				m_ProductionLineMenuPanel.menuPanelClose();
//			}



//		}
		
	}



	/// <summary>
	/// 생산라인 메뉴창 띄우기
	/// </summary>
	private bool productionMenuView(){
		//메뉴창 띄우기

		if (m_ProductionLineMenuPanel.gameObject.activeSelf)
			m_ProductionLineMenuPanel.menuPanelClose ();


		RaycastHit2D hit = mouseRaycast ();

		if (hit.collider != null) {

		 


			//Debug.Log ( hit.transform.tag);
	

			switch (hit.collider.tag) {
			case "ProductionLineTag":
				m_ProductionLineMenuPanel.setMenuPanel (hit.collider.gameObject.GetComponent<UIProductionLineClass> ());
				m_ProductionLineMenuPanel.transform.position = Input.mousePosition;

				break;
//			case "UITag":
//
//				break;
			}
			return true;


		} else {
			Debug.Log ("충돌 없음");
			return false;
			//m_ProductionLineMenuPanel.menuPanelClose();
		}


	}

	/// <summary>
	/// 시설 삽입하기
	/// </summary>
	private bool setFacility(){
		RaycastHit2D hit = mouseRaycast ();
		if (hit.collider != null) {
			
			
			
			
			//Debug.Log ( hit.transform.tag);
			
			
			switch (hit.collider.tag) {
			case "ProductionLineTag":

				//if(m_tmpFacility.facilityType == UIMadiatorClass.getProductPanel()){
					if(hit.collider.GetComponent<UIProductionLineClass>().facilityCase.facility == null){

						hit.collider.GetComponent<UIProductionLineClass>().initFacilityCase(m_tmpFacility);
						m_tmpFacility.usePos = hit.collider.GetComponent<UIProductionLineClass>().usePos;
						m_tmpFacility = null;
						setAllHighLightOff();
						Debug.Log("설치 완료" + hit.collider.GetComponent<UIProductionLineClass>().facilityCase.facility.usePos);
					}
				//}
//				else{
//					UIMadiatorClass.UIMsgPanelView ("생산라인에 맞지 않습니다.");
//					return false;
//				}
				break;
				//			case "UITag":
				//
				//				break;
			}
			return true;
			
			
		} else {
			Debug.Log ("충돌 없음");
			return false;
			//m_ProductionLineMenuPanel.menuPanelClose();
		}
	}



	/// <summary>
	/// 직원 배치하기
	/// </summary>
	private bool setEmployee(){
		RaycastHit2D hit = mouseRaycast ();
		if (hit.collider != null) {
			
			
			
			
			//Debug.Log ( hit.transform.tag);
			
			
			switch (hit.collider.tag) {
			case "ProductionLineTag":
				
//				if(m_tmpEmployee. == UIMadiatorClass.getProductPanel()){
					if(hit.collider.GetComponent<UIProductionLineClass>().employeeCase.employee == null){
						
						hit.collider.GetComponent<UIProductionLineClass>().initEmployeeCase(m_tmpEmployee);
						m_tmpEmployee.usePos = hit.collider.GetComponent<UIProductionLineClass>().usePos;
						m_tmpEmployee = null;
						setAllHighLightOff();
						Debug.Log("배치 완료" + hit.collider.GetComponent<UIProductionLineClass>().employeeCase.employee.usePos);
					}
//				}
//				else{
//					UIMadiatorClass.UIMsgPanelView ("생산라인에 맞지 않습니다.");
//					return false;
//				}
				break;
				//			case "UITag":
				//
				//				break;
			}
			return true;
			
			
		} else {
			Debug.Log ("충돌 없음");
			return false;
			//m_ProductionLineMenuPanel.menuPanelClose();
		}
	}

	private RaycastHit2D mouseRaycast(){
		return Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero, 0f);
	}
	
	/// <summary>
	/// 시설 보기
	/// </summary>
	/// <param name="facilityCase">Facility case.</param>
	public static void UIFacilityPanelView(UIObjectClass parent, FacilityClass facility, TYPE_PANEL typePanel, int nowPos = 0){
		UIEffectSoundPlayer(TYPE_AUDIO.NONE);
		m_facilityPanelView.setFacilityPanel (parent, facility, typePanel, nowPos);
	}

	/// <summary>
	/// 직원 보기
	/// </summary>
	/// <param name="employeeCase">Employee Case.</param>
	public static void UIEmployeePanelView(UIObjectClass parent, EmployeeClass employee, TYPE_PANEL typePanel, int nowPos = 0){
		UIEffectSoundPlayer(TYPE_AUDIO.NONE);
		m_emplayeePanelView.setEmployeePanel (parent, employee, typePanel, nowPos);
	}

	/// <summary>
	/// 구인, 시설, 상점 패널 열기
	/// 0 구인
	/// 1 시설 
	/// 2 상점
	/// </summary>
	public void UIShopPanelView(int shopType){
		UIEffectSoundPlayer(TYPE_AUDIO.NONE);
		m_shopPanel.setShopView ((TYPE_SHOP)shopType);
	}

	/// <summary>
	/// 관리 패널 열기
	/// </summary>
	public void UIManagementPanelViewClicked(){
		UIEffectSoundPlayer(TYPE_AUDIO.NONE);
		UIManagementPanelView(0);
	}

	/// <summary>
	/// 관리 패널 특정하게 열기
	/// </summary>
	public static void UIManagementPanelView(int usePos, UIObjectClass uiObject = null){
		m_managementPanelView.setManagementPanel (usePos, uiObject);
	}

	/// <summary>
	/// 생산라인 가져오기
	/// </summary>
	/// <returns>The user interface production line.</returns>
	/// <param name="nowPos">Now position.</param>
	public static UIProductionLineClass getUIProductionLine(int nowPos){
		UIEffectSoundPlayer(TYPE_AUDIO.NONE);
		return m_productionLineManagerView.getProductionLine(nowPos);
	}

	/// <summary>
	/// 생산시설 모든 하이라이트 켜기
	/// </summary>
	public static void setAllHighLightOn(FacilityClass facility){
		UIEffectSoundPlayer(TYPE_AUDIO.NONE);
		m_tmpFacility = facility;
		m_productionLineManagerView.setAllHighLightOn (facility);
	}

	/// <summary>
	/// 직원 모든 하이라이트 켜기
	/// </summary>
	public static void setAllHighLightOn(EmployeeClass employee){
		UIEffectSoundPlayer(TYPE_AUDIO.NONE);
		m_tmpEmployee = employee;
		m_productionLineManagerView.setAllHighLightOn (employee);
	}

	/// <summary>
	/// 모든 하이라이트 끄기
	/// </summary>
	public static void setAllHighLightOff(){
		UIEffectSoundPlayer(TYPE_AUDIO.NONE);
		m_productionLineManagerView.setAllHighLightOff ();
	}

	/// <summary>
	/// 왼쪽 버튼 누르기
	/// </summary>
	public void leftBtnClicked(){
		UIEffectSoundPlayer(TYPE_AUDIO.NONE);
		m_productionLineManagerView.leftProductArea ();
	}

	/// <summary>
	/// 오른쪽 버튼 누르기
	/// </summary>
	public void rightBtnClicked(){
		m_productionLineManagerView.rightProductArea ();
	}

	/// <summary>
	/// 생산패널 가져오기
	/// </summary>
	/// <returns>The product panel.</returns>
	public static TYPE_FACILITY getProductPanel(){
		UIEffectSoundPlayer(TYPE_AUDIO.NONE);
		return m_productionLineManagerView.getProductionArea ();
	}

	/// <summary>
	/// 인지도 패널 열기
	/// </summary>
	public static void setReputationPanelView(){
		UIEffectSoundPlayer(TYPE_AUDIO.NONE);
		m_reputationPanelView.gameObject.SetActive (true);
	}

	/// <summary>
	/// 상태 패널 열기
	/// </summary>
	public void UIStatusPanelView(){
		UIEffectSoundPlayer(TYPE_AUDIO.NONE);
		m_statusPanelView.gameObject.SetActive (true);
	}

	/// <summary>
	/// 연구 관리 패널 열기
	/// </summary>
	public void UIResearchManagementPanelView(int index = 1){
		UIEffectSoundPlayer(TYPE_AUDIO.NONE);
		m_researchManagementPanel.setManagementPanel (null, index);
	}

	/// <summary>
	/// 연구 관리 패널 열기 static
	/// </summary>
	public static void UIResearchManagementPanel(int index = 1, bool isView = true){
		UIEffectSoundPlayer(TYPE_AUDIO.NONE);
		if (isView) {
			m_researchManagementPanelView.setManagementPanel (null, index);
		}
		else {
			if (m_researchManagementPanelView.isActiveAndEnabled){
				//해당 시설이 맞으면 새로고침
				if(m_researchManagementPanelView.isPanelIndex(index)){
					m_researchManagementPanelView.setManagementPanel (null, index);
				}
			}
		}
	}

	/// <summary>
	/// 가동중인 라인
	/// </summary>
	/// <returns>The run line count.</returns>
	public static int getRunningLineCount(){
		return m_productionLineManagerView.getRunningLineCount ();
	}

	/// <summary>
	/// 생산라인 하단 패널 보기
	/// </summary>
//	public static void UILowerProductPanelView(){
//		m_uiPanelView.lowerUI.productPanelView ();
//	}
//
//	/// <summary>
//	/// 연구라인 하단 패널 보기
//	/// </summary>
//	public static void UILowerResearchPanelView(){
//		m_uiPanelView.lowerUI.researchPanelView();
//	}

	/// <summary>
	/// 지역 확장하기
	/// </summary>
	/// <returns><c>true</c>, if area events was extended, <c>false</c> otherwise.</returns>
	public static bool extendAreaEvents (){
		return m_productionLineManagerView.extendAreaEvents ();
	}

	/// <summary>
	/// 연구패널 보기
	/// </summary>
	/// <param name="parent">Parent.</param>
	/// <param name="research">Research.</param>
	public static void UIResearchPanelView(UIObjectClass parent, ResearchClass research){
		m_researchPanelView.setResearchPanel(parent, research);
	}


	public static void ResearchWakeUp(){
		m_productionLineManagerView.researchWakeUp ();
	}

	public static void UITextPanelView(Sprite image, string msg){
		m_uiPanelView.lowerUI.textPanelView (image, msg);
	}

	public void UIFinancePanelView(){
		UIEffectSoundPlayer(TYPE_AUDIO.NONE);
		m_financePanel.initFinancePanel ();
	}

	/// <summary>
	/// 중간 버튼 활성화
	/// </summary>
	public static void setMidBtns(){
		m_uiPanelView.midUI.setMidBtns ();
	}

	/// <summary>
	/// 피버 활성화
	/// </summary>
	public static void setFeverTime(){
//		m_audioSource.Stop ();
		//m_audioSource.clip = m_backgroundClip [2];
		m_BGMPlayer.BGMStop ();
		m_BGMPlayer.BGMPlay (TYPE_BGM.FEVER);
		m_uiPanelView.midUI.setFeverPanel ();
	}

	/// <summary>
	/// 피버 비활성화
	/// </summary>
	public static void resetFeverTime(){
//		m_audioSource.Stop ();
		//m_audioSource.clip = m_backgroundClip [1];
		m_BGMPlayer.BGMStop ();
		m_BGMPlayer.BGMPlay (TYPE_BGM.PLAY);
		m_uiPanelView.midUI.resetFeverPanel ();
	}


	/// <summary>
	/// 다른 어플리케이션 사용
	/// </summary>
	/// <param name="isPause">If set to <c>true</c> is pause.</param>
	void OnApplicationFocus(bool isPause){
		gamePause (isPause);
	}

	/// <summary>
	/// 어플리케이션 일시정지
	/// </summary>
	/// <param name="isPause">If set to <c>true</c> is pause.</param>
	void OnApplicationPause(bool isPause){
		//일시정지 활성화
		gamePause (isPause);

	}

	/// <summary>
	/// 어플리케이션 종료 완료
	/// </summary>
	void OnApplicationQuit(){
		gamePause (true);
	}

	/// <summary>
	/// 어플리케이션 종료
	/// </summary>
	void applicationQuit(){
		//모든 리소스 해제
		Application.Quit ();
	}

	/// <summary>
	/// 패널 스택 삽입
	/// </summary>
	/// <param name="uiPanel">User interface panel.</param>
	public static void UIStackPanel(UIObjectClass uiPanel, bool isPop = false){
		//다른게 있으면 삽입 같은게 있으면 빼기
		if(!isPop)
			m_panelStack.Push (uiPanel);
		else
			m_panelStack.Pop ();

		Debug.Log ("스택 카운트 : " + m_panelStack.Count);
	}

	private void gamePause(bool isPause){
#if UNITY_EDITOR_WIN 
		//PC용
		if (!isPause) 
#else
		//기타용
		if(isPause)
#endif
		{
			//저장 및 일시정지
			if(AccountClass.GetInstance.saveData()){
				Debug.Log("저장 완료");
			}
			else{
				Debug.Log("저장 실패");
			}
			m_pausePanel.gameObject.SetActive(true);
		} 
		//일시정지 취소
		else {
			Debug.Log("게임 재개");
			//일시정지 풀기
		}
	}


	public void UIOptionPanelView(){
		m_optionPanel.gameObject.SetActive (true);
	}

	public static void BGMMute(){
		m_BGMPlayer.BGMMute ();
	}



//	public static void UIBackgroundSoundPlayer(){
//		BGMMute ();
//	}

	/// <summary>
	/// 효과음 재생
	/// </summary>
	/// <param name="typeAudio">Type audio.</param>
	public static void UIEffectSoundPlayer(TYPE_AUDIO typeAudio = TYPE_AUDIO.NONE){
		try{
			m_soundEffectPlayer.audioPlay (typeAudio);
		}
		catch{
//			m_soundEffectPanel = GameObject.Find ("Game@SoundEffect").GetComponent<UISoundEffectClass>();
//			m_soundEffectPlayer = m_soundEffectPanel;
		}
	}

	/// <summary>
	/// 효과음 대리 재생
	/// </summary>
	/// <param name="audioSource">Audio source.</param>
	/// <param name="typeAudio">Type audio.</param>
	public static void UIEffectSoundPlayer(AudioSource audioSource, TYPE_AUDIO typeAudio){
		try{
			m_soundEffectPlayer.audioPlay (audioSource, typeAudio);
		}
		catch{
		}
	}
}



