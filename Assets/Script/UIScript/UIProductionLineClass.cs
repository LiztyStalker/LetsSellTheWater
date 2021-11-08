using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIProductionLineClass : MonoBehaviour {

	UIProductionLineManagerClass m_parent;
	UIProductionAreaClass m_area;

//	[SerializeField] UIProductionMenuClass m_menuPanel;
	[SerializeField] UIEmployeeCaseClass m_employeeObject;
	[SerializeField] UIFacilityCaseClass m_facilityObject;
	[SerializeField] GameObject m_highLight;
	int m_usePos; //사용 위치

	AudioSource m_audioSource;

	//000 생산
	//100 가공
	//200 판매
	//300 운영
	//400 연구
	//500 운송

//	[SerializeField] UIEmployeeClass m_employeePanel;
//	[SerializeField] UIFacilityClass m_facilityPanel;
//	[SerializeField] Button m_productionBtn; 


//	TYPE_FACILITY m_facilityType;

	public UIEmployeeCaseClass employeeCase{ get { return m_employeeObject; } }
	public UIFacilityCaseClass facilityCase{ get { return m_facilityObject; } }
	public UIProductionAreaClass area{ get { return m_area; } }
	public int usePos{ get { return m_usePos; } }
	public bool isHighLight{get {return m_highLight.activeSelf;}}
	public AudioSource audioSource{ get { return m_audioSource; } }


	public void setWater(){
		m_parent.setWater();
	}
	public void resetWater(){
		m_parent.resetWater();
	}
	public void setBottle(){
		m_parent.setBottle();
	}
	public void resetBottle(){
		m_parent.resetBottle();
	}
	public void setResearch(){
		m_parent.setResearch();
	}
	public void resetResearch(){
		m_parent.resetResearch();
	}

	/// <summary>
	/// 잠에서 깨우기
	/// </summary>
	public void wakeUp(TYPE_FACILITY typeFacility){
		//m_facilityObject.runProduct (typeFacility, isSuccess, isPineCola);

		switch (typeFacility) {
		case TYPE_FACILITY.PRODUCT:
			m_parent.menufactureWakeUp();
			break;
		case TYPE_FACILITY.MANUFACTURE:
//			Debug.Log("WakeUp");
			m_parent.waterWakeUp();
			m_parent.businessWakeUp ();
			break;
		case TYPE_FACILITY.BUSINESS:
			m_parent.menufactureWakeUp();
			break;
//		case TYPE_FACILITY.RESEARCH:
//			Debug.Log("WakeUp");
//			m_parent.researchWakeUp ();
//			break;

		}
	}

	/// <summary>
	/// 1회 행동 - 직원
	/// </summary>
//	public void runProduct(){
//		m_facilityObject.runProduct ();
//	}

	/// <summary>	
	/// 생산라인 초기화
	/// </summary>
	/// <param name="productionLineManager">Production line manager.</param>
	public void initProductionLine(UIProductionLineManagerClass parent, UIProductionAreaClass area, int usePos){
		highlightOff ();

		m_audioSource = GetComponent<AudioSource>();

		m_parent = parent;
		m_area = area;
		m_usePos = usePos;

		//시설 데이터 삽입
		//null이면 시설 데이터 없음
		initEmployeeCase (AccountClass.GetInstance.getWorkEmployee (m_usePos));
		initFacilityCase (AccountClass.GetInstance.getUseFacility (m_usePos));
		if (m_area != null) {m_facilityObject.setCostSetPos(m_area);}

	}

	/// <summary>
	/// 시설케이스 초기화
	/// </summary>
	/// <param name="facility">Facility.</param>
	public void initFacilityCase (FacilityClass facility){

		m_facilityObject.setParent (this);


		//삽입
			//빈 공간에 시설 삽입 후 시설 종류에 따라 리스트 삽입	
		//변경
			//시설 공간에 시설 빼고 시설 종류 초기화
			//빈 공간에 시설 삽입 후 시설 종류에 따라 리스트 삽입
		//삭제
			//시설 공간에 시설 빼고 시설 종류 초기화

		//시설이 있으면 시설 빼기
		if (m_facilityObject.facility != null)
			m_parent.setProductionLine (this);




		//현재 시설
		m_facilityObject.initFacilityCase (facility);
		//삽입하고 현재 시설을 리스트에 넣는다.

		//시설이 추가되지 않았으면 시설 넣기
		if (facility != null)
			m_parent.setProductionLine (this, false);
	}

	/// <summary>
	/// 직원 케이스 초기화
	/// </summary>
	/// <param name="employee">Employee.</param>
	public void initEmployeeCase(EmployeeClass employee){
		m_employeeObject.setParent (this);
		m_employeeObject.initEmployeeCase(employee);
		//addWaitLine ();
	}

	/// <summary>
	/// 대기 큐에 들어가기 (사용하지 않음)
	/// </summary>
	public void addWaitLine(){
//		Debug.Log ("대기 중 : " + m_usePos);
		m_parent.addWaitLine (this);
	}

	/// <summary>
	/// 대기 큐에서 나오기 (사용하지 않음)
	/// </summary>
	public void removeWaitLine(){
//		Debug.Log ("작업 중 : " + m_usePos);
		m_parent.removeWaitLine (this);
	}

	/// <summary>
	/// 하이라이트 켜기
	/// </summary>
	public void highlightOn(){
		m_highLight.SetActive (true);
	}

	/// <summary>
	/// 하이라이트 끄기
	/// </summary>
	public void highlightOff(){
		m_highLight.SetActive (false);
	}

//	public void employeePanelView(){
//		m_employeePanel.setEmployeePanel (m_employeeObject.employee);
//	}
//
//	public void facilityPanelView(){
//		m_facilityPanel.setFacilityPanel (m_facilityObject.facility);
//	}

	/// <summary>
	/// 생산라인 반납
	/// </summary>
	public void returnProductLine(){
		m_parent.returnProductionLine (this);
	}

	/// <summary>
	/// 생산라인 선택
	/// </summary>
	public void productionLineClicked(){
//		if (!m_menuPanel.gameObject.activeSelf) {
//			m_menuPanel.gameObject.SetActive(true);
//			m_menuPanel.setMenuPanel(this);
//		}
	}





}
