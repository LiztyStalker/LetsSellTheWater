using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class UIProductionLineManagerClass : MonoBehaviour {

	const int c_maxArea = 10;
	const int c_maxProuductionLine = 9;
	const float c_createDirX = 15f;
	readonly float[] posY = {1.8f, 0.1f, -1.6f, -2.7f};

//	ProductionLineManagerClass m_productionLineManager;

	[SerializeField] UIProductionLineClass m_productionLineObject; //땅
	[SerializeField] UIProductionAreaClass m_productArea; //지역

	//지역
	//[SerializeField] GameObject[] m_productArea;
	//현재 패널
	//생산
	//가공
	//판매
	//	[SerializeField] GameObject m_productionPanel;
	//	[SerializeField] GameObject m_menufacturePanel;
	//	[SerializeField] GameObject m_servicePanel;


	//임시 풀
	Stack<UIProductionLineClass> m_productionLinePool = new Stack<UIProductionLineClass>(); //임시 풀
	
	//활성화 지역
	List<UIProductionAreaClass> m_productAreaList = new List<UIProductionAreaClass> ();

	//전체 사용중인 라인
	List<UIProductionLineClass> m_useProductionLine = new List<UIProductionLineClass>();

	//해당 공정라인 사용
	List<UIProductionLineClass> m_productLine = new List<UIProductionLineClass> ();
	List<UIProductionLineClass> m_menufactureLine = new List<UIProductionLineClass> ();
	List<UIProductionLineClass> m_businessLine = new List<UIProductionLineClass> ();
	List<UIProductionLineClass> m_researchLine = new List<UIProductionLineClass> ();

	//대기 공정라인
//	Queue<UIProductionLineClass> m_waitProductLine = new Queue<UIProductionLineClass>();
	List<UIProductionLineClass> m_waitMenufactureLine = new List<UIProductionLineClass>();
	List<UIProductionLineClass> m_waitBusinessLine = new List<UIProductionLineClass>();
//	Queue<UIProductionLineClass> m_waitResearchLine = new Queue<UIProductionLineClass>();

//	int[] m_productionIndex = {0, 0, 0};


//	public List<UIProductionLineClass> productLine{ get { return m_productLine; } }
//	public List<UIProductionLineClass> menufactureLine{ get { return m_menufactureLine; } }
//	public List<UIProductionLineClass> businessLine{ get { return m_businessLine; } }




	bool m_isEmptyWater = false;
	bool m_isEmptyBottle = false;
	bool m_isEmptyResearch = false;


	public void setWater(){
		m_isEmptyWater = true;
	}
	public void resetWater(){
		m_isEmptyWater = false;
	}
	public void setBottle(){
		m_isEmptyBottle = true;
	}
	public void resetBottle(){
		m_isEmptyBottle = false;
	}
	public void setResearch(){
		m_isEmptyResearch = true;
	}
	public void resetResearch(){
		m_isEmptyResearch = false;
	}


	//현재 보고있는 지역
	int m_productAreaIndex = 0;


	int[] m_usePos;// = {1, 101, 201, 301};

	private int getUsePos(){

		return m_usePos[m_productAreaList.Count - 1]++;
	}

	public TYPE_FACILITY getProductionArea(){
		return (TYPE_FACILITY)m_productAreaIndex;
	}

	// Use this for initialization
	void Start () {

		m_usePos = new int[c_maxArea];

		for (int i = 0 ; i < m_usePos.Length; i++) {
			m_usePos[i] = i * 100 + 1;
		}

		//풀 생성
		for (int i = 0; i < m_usePos.Length * c_maxProuductionLine; i++) {
			UIProductionLineClass productionLineObj = (UIProductionLineClass)Instantiate (m_productionLineObject);
//			productionLineObj.transform.parent = Vector3.up * 100f;
			productionLineObj.gameObject.transform.position = Vector3.up * 100f;
			productionLineObj.initProductionLine (this, null, 0);
			m_productionLinePool.Push (productionLineObj);
			productionLineObj.gameObject.SetActive(false);
		}

		//지역 생성
		createArea (AccountClass.GetInstance.extendCount + 1);


		//현재 인덱스 보기
//		if(AccountClass.GetInstance.viewIndex != 0)
//			changeProductArea (0, AccountClass.GetInstance.viewIndex);

		StartCoroutine (customerSet ());

	}
	
	/// <summary>
	/// 생산품 생산 (사용하지 않음)
	/// </summary>
//	public void runProducts(){
//		//int product = 0;
//
//		//해당하는 패널이 열려있을때 그 패널에 대한 이벤트 동작
//
//		//물 생산
//		//물병 생산
//		//물병 판매
//
//		Debug.Log ("사용 " + m_useProductionLine.Count);
//
////		product = m_useProductionLine.Where (list => list.facilityCase.facility.facilityType == (TYPE_FACILITY)m_productPos).Select (list => list).Sum (list => list.facilityCase.facility.product);
//
//		foreach (UIProductionLineClass useProduct in m_useProductionLine) {
//			Debug.Log("사용중인 생산라인 : " + useProduct.usePos);
//			//물 생산
//			//물병 생산
//			//물병 판매
//
//
//			useProduct.runProduct((TYPE_FACILITY)m_productAreaIndex);
//
//		}
//
////		AccountClass.GetInstance.addAssets (product);
//
//	}


	/// <summary>
	/// 지역 확장
	/// </summary>
	public bool extendAreaEvents(){
//		Debug.Log ("지역 추가");
		return createArea ();
	}

	/// <summary>
	/// 지역 생성
	/// </summary>
	/// <returns><c>true</c>, if area was created, <c>false</c> otherwise.</returns>
	/// <param name="cnt">Count.</param>
	private bool createArea(int cnt = 1){
		//지역 생성
		//땅 생성

		//현재 확장 완료된 개수가 연구 완료된 개수보다 높으면 확장 불가
		if (m_productAreaList.Count > AccountClass.GetInstance.researchAccount.getExtendArea ()) {
			Debug.Log("확장 불가");
			return false;
		}

		//
		while (cnt != 0) {
			//Debug.Log ("확장");

			//지역 확장시 실행
			//지역 생성 - 기본 틀만 생성
			//지역을 이동할 수 있는 배열 필요
			UIProductionAreaClass areaObject = (UIProductionAreaClass)Instantiate (m_productArea);
			areaObject.transform.SetParent (gameObject.transform);
			areaObject.setBackground (m_productAreaList.Count);
			areaObject.transform.localPosition = new Vector3 (15f * m_productAreaList.Count, 0f);
			m_productAreaList.Add (areaObject);
			AccountClass.GetInstance.setExtendCount(m_productAreaList.Count);
			//땅 생성 - 사물을 배치할 수 있는 땅 생성
			//땅을 관리하는 배열 필요
			//시설 종류 - 시설을 배치하면 시설 종류를 기준으로 땅 배열이 배치
			for (int i = 0; i < c_maxProuductionLine; i++) {
				addProductionLine (areaObject);
			}
			cnt--;
		}
//		Debug.Log ("확장 최대치 넘음 : " + m_productAreaList.Count);
		//확장 최대치를 넘었음
		return true;

	}




	/// <summary>
	/// 생산라인 가져오기
	/// </summary>
	/// <returns>생산라인</returns>
	/// <param name="nowPos">현재 위치</param>
	public UIProductionLineClass getProductionLine(int nowPos){
		Debug.Log ("nowPos : " + nowPos);
		return m_useProductionLine.Where (productLine => productLine.usePos == nowPos).Select (productLine => productLine).SingleOrDefault ();
	}

	/// <summary>
	/// 생산라인 반납
	/// </summary>
	/// <param name="productionLine">Production line.</param>
	public void returnProductionLine(UIProductionLineClass productionLine){
		productionLine.gameObject.SetActive (false);
		m_useProductionLine.Remove (productionLine);
		m_productionLinePool.Push (productionLine);
	}

	/// <summary>
	/// 생산라인 추가 - 땅 추가
	/// 지역 확장시 실행
	/// </summary>
	/// <returns>The production line.</returns>
	public void addProductionLine(UIProductionAreaClass area){

		//저장된 땅 꺼내기
		UIProductionLineClass productionLine = m_productionLinePool.Pop ();
		productionLine.gameObject.SetActive (true);
		m_useProductionLine.Add (productionLine);

		int usePos = getUsePos ();
		productionLine.gameObject.transform.SetParent (m_productAreaList [m_productAreaList.Count - 1].transform);

		float posX = -1.75f + ((float)((usePos % 100) -1) % 3) * 1.75f;
		float posY = 2.1f + (float)(((usePos % 100) -1) / 3) * -1.85f;
		productionLine.gameObject.transform.localPosition  = new Vector2 (posX, posY);

		//위치도 알아야 함
		productionLine.initProductionLine (this, area, usePos);

	}

	/// <summary>
	/// 생산라인 배치하기
	/// </summary>
	/// <returns><c>true</c>, if production line was set, <c>false</c> otherwise.</returns>
	/// <param name="productionLine">Production line.</param>
	public bool setProductionLine(UIProductionLineClass productionLine, bool isRemove = true){

		//비어있으면 - 삽입
		//삽입되어 있으면 - 비우기

		//해당 공정에 대한 빼기
		if (isRemove) {
			switch (productionLine.facilityCase.facility.facilityType) {
			case TYPE_FACILITY.PRODUCT:
				//더하기
				m_productLine.Remove (productionLine);
				break;
			case TYPE_FACILITY.MANUFACTURE:
				m_menufactureLine.Remove (productionLine);
				//대기 큐에 데이터가 있으면 강제 중지 후 삭제
				break;
			case TYPE_FACILITY.BUSINESS:
				m_businessLine.Remove (productionLine);
				//대기 큐에 데이터가 있으면 강제 중지 후 삭제
				break;
			case TYPE_FACILITY.RESEARCH:
				m_researchLine.Remove (productionLine);
				break;
			}
			removeWaitLine(productionLine);
			return false;
		} else {
			//해당 공정에 대한 더하기
			if(productionLine.facilityCase.facility != null){
				switch (productionLine.facilityCase.facility.facilityType) {
				case TYPE_FACILITY.PRODUCT:
					//더하기
					Debug.Log ("생산 추가");
					m_productLine.Add (productionLine);
					break;
				case TYPE_FACILITY.MANUFACTURE:
					Debug.Log ("가공 추가");
					m_menufactureLine.Add (productionLine);
					break;
				case TYPE_FACILITY.BUSINESS:
					Debug.Log ("판매 추가");
					m_businessLine.Add (productionLine);
					break;
				case TYPE_FACILITY.RESEARCH:
					Debug.Log ("연구 추가");
					m_researchLine.Add (productionLine);
					break;
				}
				//addWaitLine(productionLine);
			}

			return true;
		}
	
	}

	/// <summary>
	/// 대기 큐에 삽입하기 (사용하지 않음)
	/// </summary>
	/// <param name="productionLine">Production line.</param>
	public void addWaitLine(UIProductionLineClass productionLine){

		//시설과 직원이 있으면
		if (productionLine.facilityCase.facility != null && productionLine.employeeCase.employee != null) {
			switch (productionLine.facilityCase.facility.facilityType) {
			case TYPE_FACILITY.MANUFACTURE:
				//가공 큐에서 대기
				if(!m_waitMenufactureLine.Contains(productionLine))
					m_waitMenufactureLine.Add (productionLine);
				break;
			case TYPE_FACILITY.BUSINESS:
				//판매 큐에서 대기
				if(!m_waitBusinessLine.Contains(productionLine))
					m_waitBusinessLine.Add (productionLine);
				break;
			}
		}
	}

	/// <summary>
	/// 대기 큐에서 나오기 (사용하지 않음)
	/// </summary>
	/// <param name="productionLine">Production line.</param>
	public void removeWaitLine(UIProductionLineClass productionLine){
		
		//시설과 직원이 있으면
		if (productionLine.facilityCase.facility != null && productionLine.employeeCase.employee != null) {
			switch (productionLine.facilityCase.facility.facilityType) {
			case TYPE_FACILITY.MANUFACTURE:
				//가공 큐에서 대기
				if(m_waitMenufactureLine.Contains(productionLine))
					m_waitMenufactureLine.Remove (productionLine);
				break;
			case TYPE_FACILITY.BUSINESS:
				//판매 큐에서 대기
				if(m_waitBusinessLine.Contains(productionLine))
					m_waitBusinessLine.Remove (productionLine);
				break;
			}
		}
	}

	/// <summary>
	/// 직원 모든 오브젝트 하이라이트 켜기
	/// </summary>
	public void setAllHighLightOn(EmployeeClass employee){
		foreach (UIProductionLineClass productionLine in m_useProductionLine) {
			if(productionLine.employeeCase.employee == null)
				productionLine.highlightOn();
		}
	}

	/// <summary>
	/// 시설 모든 오브젝트 하이라이트 켜기
	/// </summary>
	public void setAllHighLightOn(FacilityClass facility){
		foreach (UIProductionLineClass productionLine in m_useProductionLine) {
			if(productionLine.facilityCase.facility == null)
				productionLine.highlightOn();
		}
	}

	/// <summary>
	/// 모든 오브젝트 하이라이트 끄기
	/// </summary>
	public void setAllHighLightOff(){
		foreach (UIProductionLineClass productionLine in m_useProductionLine) {
			productionLine.highlightOff();
		}
	}

	public void leftProductArea(){

		UIMadiatorClass.UIEffectSoundPlayer (TYPE_AUDIO.PAGE);

		int prevProductPos = m_productAreaIndex;

		if (m_productAreaIndex - 1 < 0)
			m_productAreaIndex = m_productAreaList.Count;

		--m_productAreaIndex;

		changeProductArea (prevProductPos, m_productAreaIndex);

//		Vector3 prevVector = m_productAreaList[prevProductPos].transform.position;
//		m_productAreaList[prevProductPos].transform.position = m_productAreaList[m_productAreaIndex].transform.position;
//		m_productAreaList [m_productAreaIndex].transform.position = prevVector;
		//panelCheck ();

	}
	public void rightProductArea(){

		UIMadiatorClass.UIEffectSoundPlayer (TYPE_AUDIO.PAGE);

		int prevProductPos = m_productAreaIndex;

		if (m_productAreaIndex + 1 > m_productAreaList.Count - 1)
			m_productAreaIndex = -1;
		
		++m_productAreaIndex;

		changeProductArea (prevProductPos, m_productAreaIndex);
//		Vector3 prevVector = m_productAreaList[prevProductPos].transform.position;
//		m_productAreaList[prevProductPos].transform.position = m_productAreaList[m_productAreaIndex].transform.position;
//		m_productAreaList [m_productAreaIndex].transform.position = prevVector;
		//panelCheck ();
	}

	private void changeProductArea(int pastIndex, int nowIndex){
		Vector3 prevVector = m_productAreaList[pastIndex].transform.position;
		m_productAreaList[pastIndex].transform.position = m_productAreaList[nowIndex].transform.position;
		m_productAreaList [nowIndex].transform.position = prevVector;

		//기억
	//	AccountClass.GetInstance.viewIndex = nowIndex;

	}
	

	IEnumerator customerSet(){

		//yield return new WaitForSeconds (2f);

		CustomerClass customer = null;

		while (gameObject.activeSelf) {

//			Debug.Log("대기 직원 : " + m_waitBusinessLine.Count);
			//판매 큐에 있는 첫 데이터를 불러와 손님 삽입 후 대기 취소 
//			if(m_waitBusinessLine.Count > 0){
//				UIProductionLineClass prodLine = m_waitBusinessLine.First();
//				if(prodLine.facilityCase.setCustomer(AccountClass.GetInstance.getCustomer())){
////					customer = null;
////					prodLine.employeeCase.readyProduct ();
//					//businessWakeUp();
//					removeWaitLine(prodLine);
//				}
//			}





			foreach(UIProductionLineClass prodLine in m_businessLine){

				if(customer == null){
					int index = AccountClass.GetInstance.reputationAccount.getReputateIndex();
					if(index >= 0){
						customer = AccountClass.GetInstance.getCustomer(index);
					}
				}

				if(prodLine.facilityCase.facility != null){
					//Debug.Log("시설 있음");
					if(!prodLine.facilityCase.isCustomer()){
						//Debug.Log("손님 없음");
						if(prodLine.facilityCase.setCustomer(customer))
							customer = null;
					}
				}
			}
			//}
			//catch{
			//}

//			Debug.Log("set");

			yield return new WaitForSeconds(0.1f);
		}
	}

	public void waterWakeUp(){
		wakeUp (m_productLine);
//		foreach (UIProductionLineClass product in m_productLine) {
//			product.employeeCase.readyProduct();
//		}
	}

	public void businessWakeUp (){
		//if (m_isEmptyBottle) {
//			resetBottle ();
//			Debug.Log ("판매 깨우기");
			wakeUp(m_businessLine);
//			foreach (UIProductionLineClass product in m_businessLine) {
//				product.employeeCase.readyProduct ();
//			}
		//}
	}

	public void menufactureWakeUp (){
//		if (m_isEmptyWater) {
//			resetWater ();
			wakeUp(m_menufactureLine);
//			foreach (UIProductionLineClass product in m_menufactureLine) {
//				product.employeeCase.readyProduct ();
//			}
//		}
	}

	public void researchWakeUp (){
		//if (m_isEmptyResearch) {
		//	resetResearch ();
		wakeUp (m_researchLine);
//			foreach (UIProductionLineClass product in m_researchLine) {
//				product.employeeCase.readyProduct ();
//			}
		//}
	}

	private void wakeUp(List<UIProductionLineClass> productLine){
		foreach (UIProductionLineClass product in productLine) {
			if(!product.employeeCase.isRunning()){
				product.employeeCase.readyProduct ();
				productLine.Remove(product);
				productLine.Add(product);
				break;
			}
		}
	}

	/// <summary>
	/// 현재 가동중인 라인 개수 가져오기
	/// </summary>
	/// <returns>The running line count.</returns>
	public int getRunningLineCount(){
		int count = 0;
		foreach (UIProductionLineClass prod in m_useProductionLine) {
			if(prod.employeeCase.isRunning()){
				count++;
			}
		}
		return count;
	}


}
