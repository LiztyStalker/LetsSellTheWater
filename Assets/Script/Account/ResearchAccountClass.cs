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
using System.Linq;


[Serializable]
public class ResearchAccountSerialClass : SerialClass{
	public int nowResearch;
//	public int maxResearch;
	public List<string> readyResearch = new List<string>(); //연구 대기
	public List<string> researched = new List<string>(); //연구 완료
//	public List<string> researching = new List<string>(); //연구 가능 - 연구 완료에서 가지고 와야 함
	public List<string> facilities = new List<string> (); //연구된 시설
	public List<ResearchAccountDataSerialClass> researchData = new List<ResearchAccountDataSerialClass> ();
	//연구 완료된 
}

[Serializable]
public class ResearchAccountDataSerialClass{
	public string key;
	public List<int> data = new List<int> ();
	public ResearchAccountDataSerialClass(string key, List<int> data){
		this.key = key;
		this.data = data;
	}
}

public class ResearchAccountClass
{

	const int c_employeeLength = 5;

	const int c_researchLength = 10;
	const int c_researchUpgradeLength = 9;

	const int c_maxResearchCount = 5; //최대 연구 개수
	int m_nowResearch = 0; //현재 연구량


//	delegate void RefreshResearchDelegate();
//	RefreshResearchDelegate refreshResearchEvents;
	//연구 완료시 새로고침 이벤트 실행
	//필요 이벤트
	//연구 매니저
	//연구 창
	//연구 큐 및 연구 패널

	

	List<ResearchClass> m_readyResearch = new List<ResearchClass>(); //대기 연구
	//연구 대기 라인. 첫번째 데이터는 현재 진행 연구

	//RS01 01 01
	Dictionary<string, List<int> > m_ResearchManager = new Dictionary<string, List<int> > ();

	//연구 완료된 데이터
	//m_researchManager랑 겹칠 수 있음
	List<string> m_researched = new List<string>();
	
	//연구해야할 데이터
	List<string> m_researching = new List<string>();





//	Dictionary<string, int> m_ExtendLv = new Dictionary<string, int>();
//	int[] m_extends = {0, 0, 0, 0};


	/// <summary>
	/// 환경 레벨
	/// RS0100 토지 확장 
	/// RS0101 물탱크 확장
	/// RS0102 대기줄 확장
	/// RS0103 창고 확장
	/// </summary>
	private List<int> extendResearch{ get { return m_ResearchManager["RS01"]; } }

	/// <summary>
	/// 지역 확장 값
	/// </summary>
	/// <returns>The extend area.</returns>
	public int getExtendArea(){	return extendResearch [0];}
	private int getExtendWaterBottle(){	return extendResearch [1];}
	private int getExtendStorage(){return extendResearch [2];}
	private int getExtendWaitingLine(){return extendResearch [3];}

	/// <summary>
	/// 물탱크 확장하기 [1 = 1^2 max 2^5]
	/// </summary>
	/// <returns>The extend water bottle.</returns>
	public int getExtendWaterBottle(int waterbottle){
		return waterbottle * getExtendWaterBottle () * getExtendWaterBottle ();
	}

	/// <summary>
	/// 창고 확장 [1 = 1 max 2^5]
	/// </summary>
	/// <returns>The extend storage.</returns>
	/// <param name="storage">Storage.</param>
	public int getExtendStorage(int storage){
		return storage * getExtendStorage () * getExtendStorage ();
	}

	/// <summary>
	/// 대기줄 확장 [1 = 1 max 2^5]
	/// </summary>
	/// <returns>The extend waiting line.</returns>
	/// <param name="waitingLine">Waiting line.</param>
	public int getExtendWaitingLine(int waitingLine){
		return waitingLine * getExtendWaitingLine () * getExtendWaitingLine ();
	}

	//시설
	//RS0200
	List<string> m_facilityLv = new List<string> ();
	public List<string> facilityLv{ get { return m_facilityLv; } }

	/// <summary>
	/// 인지도 레벨
	/// RS0300
	/// RS0301
	/// RS0302
	/// RS0303
	/// RS0304
	/// RS0305
	/// RS0306
	/// RS0307
	/// RS0308
	/// RS0309
	/// </summary>
//	Dictionary<string, int> m_ReputationLv = new Dictionary<string, int>();
	public List<int> reputationResearch{ get { return m_ResearchManager["RS03"]; } }






//	Dictionary<string, int> m_EmployeeLv = new Dictionary<string, int>();
	/// <summary>
	/// 구인 레벨
	/// RS0400 구인창 증가
	/// RS0401 구인비 감소
	/// RS0402 숙련자 집중 고용
	/// RS0403 전문가 집중 고용
	/// RS0404 숙련전문가 집중 고용
	/// RS0405 엘리트 집중 고용
	/// RS0406 
	/// RS0407
	/// RS0408
	/// RS0409
	/// </summary>
	public List<int> employeeResearch{ get { return m_ResearchManager["RS04"]; } }

	public int employeeLength(){
		return c_employeeLength + employeeResearch [0];
	}

	public int employeeSale(){
		return employeeResearch [1] * 10;
	}
	public int[] employeeRatio(){
		int[] ratio = new int[4];
		ratio [0] = employeeResearch [2] * 10;
		ratio [1] = employeeResearch [3] * 10;
		ratio [2] = employeeResearch [4] * 10;
		ratio [3] = employeeResearch [5] * 10;
		return ratio;
	}

	/// <summary>
	/// 교육 레벨
	/// RS0500 영구체력 증가
	/// RS0501 영구순발력 증가
	/// RS0502 영구지능 증가
	/// RS0503 영구매력 증가
	/// RS0504 
	/// RS0505 
	/// RS0506 
	/// RS0507 
	/// RS0508 
	/// RS0509 
	/// </summary>
	private List<int> educationResearch{ get { return m_ResearchManager["RS05"]; } }

	public int getHealthEducation(){
		return educationResearch [0] * educationResearch [0];
	}
	public int getQuicknessEducation(){
		return educationResearch [1] * educationResearch [1];
	}
	public int getIntelligenceEducation(){
		return educationResearch [2] * educationResearch [2];
	}
	public int getCharmEducation(){
		return educationResearch [3] * educationResearch [3];
	}

	/// <summary>
	/// 환경 레벨
	/// </summary>
	//RS0600 
	//RS0601 
	//RS0602 
	//RS0603 
	//RS0604 
	//RS0605 
//	Dictionary<string, int> m_NatureLv = new Dictionary<string, int>();

	/// <summary>
	/// 연구 레벨
	/// RS0700 연구량 감소
	/// RS0701 연구 생산량 증가
	/// RS0702 자동연구
	/// RS0703 연구 슬롯 추가
	/// RS0704 
	/// RS0705 
	/// </summary>
	private List<int> researchResearch{ get { return m_ResearchManager["RS07"]; } }

	/// <summary>
	/// 연구량 감소 지수
	/// </summary>
	/// <returns>The reduced research count.</returns>
	private int getReducedResearchCount(){
		return researchResearch [0];
	}

	/// <summary>
	/// 연구량 감소 [1 = 5% max 25%]
	/// </summary>
	/// <returns>The reduced research count.</returns>
	/// <param name="product">Product.</param>
	public int getReducedResearchCount(int product){
		return (int)((float)product * 0.05f * (float)getReducedResearchCount());
	}

	/// <summary>
	/// 연구 생산량 지수
	/// </summary>
	/// <returns>The research product count.</returns>
	private int getResearchProductCount(){
		return researchResearch [1];
	}

	/// <summary>
	/// 연구 생산량 증가 [1 = 5% max 25%]
	/// </summary>
	/// <returns>The research product count.</returns>
	public int getResearchProductCount(int product){
		return (int)((float)product * 0.05f * (float)getResearchProductCount());
	}

	/// <summary>
	/// 자동 연구
	/// </summary>
	/// <returns>The automatic research count.</returns>
	public int getAutomaticResearchCount(){
		return researchResearch [2];
	}
	/// <summary>
	/// 연구 슬롯 개수 가져오기
	/// </summary>
	/// <returns>The research slot count.</returns>
	public int getResearchSlotCount(){
		return c_maxResearchCount + researchResearch [3];
	}

	/// <summary>
	/// 파인콜라 추가 확률 가져오기
	/// </summary>
	/// <returns>The pine cola count.</returns>
	public int getPineColaCount(){
		return researchResearch [4];
	}

	/// <summary>
	/// 최대 연구 개수 가져오기
	/// </summary>
	/// <value>The max research count.</value>
	private int maxResearchCount{ get { return getResearchSlotCount (); } }


	/// <summary>
	/// 연구해야할 데이터 가져오기
	/// </summary>
	/// <returns>The researching.</returns>
	public List<string> getResearching(int index){
		string key = string.Format ("RS{0:d2}", index);

		Debug.Log ("key : " + key);
		Debug.Log ("m_researching : " + m_researching.Count);
//		foreach (string str in m_researching) {
//			Debug.Log ("m_researchingData : " + str);
//		}

//		IEnumerable<string> keys = m_researching.Where (research => research.Contains (key));//.Select (research => research);
//		Debug.Log ("keyCount : " + keys.Count ());
		List<string> filterResearching = m_researching.Where (research => research.Contains (key)).ToList<string> ();
		filterResearching.Sort ();
		return filterResearching;

	}

	public ResearchAccountClass (){

		//기초데이터로 초기화
		Debug.Log ("연구계정초기화");


//		refreshResearchEvents = new RefreshResearchDelegate ();

		for(int i = 1; i < 10; i++){
			string key = string.Format("RS{0:d2}0000", i);

			Debug.Log ("연구계정 : " + key);
			m_researched.Add(key);

			if(!addChildrenResearch(ResearchPackageClass.GetInstance.getResearch(key))){
				Debug.LogError("연구 데이터를 가져오지 못했습니다.");
			}

			if(i == 2){
				for(int j = 0; j < 4; j++){
					string fac = string.Format("RS{0:d2}{1}000", i, j);
					m_facilityLv.Add(fac);
					if(!m_researched.Contains(fac)){
						m_researched.Add(fac);
						addChildrenResearch(ResearchPackageClass.GetInstance.getResearch(fac));
					}


				}
				
			}
			//하위 자식 연구 등록하기
		}



		/// RS0400 구인창 증가
		/// RS0401 구인비 감소
		/// RS0402 숙련자 집중 고용
		/// RS0403 전문가 집중 고용
		/// RS0404 숙련전문가 집중 고용
		/// RS0405 엘리트 집중 고용
		/// RS0406 
		/// RS0407
		/// RS0408
		/// RS0409



		for (int i = 1; i < c_researchLength; i++) {
			string key = string.Format ("RS{0:d2}", i);
			m_ResearchManager.Add(key, new List<int>());
            for (int j = 0; j < 9; j++) {
				m_ResearchManager[key].Add(0);
			}
		}
	}


	/// <summary>
	/// 새로고침 델리게이트
	/// 연구 관리창
	/// 연구 창
	/// </summary>
	/// <param name="del">Del.</param>
//	public void setRefreshResearchDelegate(RefreshResearchDelegate del){
//		refreshResearchEvents += del;
//	}
//
//	/// <summary>
//	/// 새로고침 델리게이트 제외
//	/// 연구 관리창
//	/// 연구 창
//	/// </summary>
//	/// <param name="del">Del.</param>
//	public void removeRefreshResearchDelegate(RefreshResearchDelegate del){
//		refreshResearchEvents -= del;
//	}
	
		
		/// <summary>
	/// 저장하기
	/// </summary>
	/// <returns>The data.</returns>
	public ResearchAccountSerialClass saveData(){
		ResearchAccountSerialClass researchData = new ResearchAccountSerialClass ();
		researchData.nowResearch = m_nowResearch;
		researchData.researched = m_researched;
//		researchData.researching = m_researching;
//		researchData.maxResearch = 0;
		researchData.facilities = m_facilityLv;
		researchData.readyResearch.Clear ();

		researchData.readyResearch.Clear ();
		foreach (ResearchClass readyResearch in m_readyResearch) {
			researchData.readyResearch.Add(readyResearch.key);
		}

		researchData.researchData.Clear ();
		foreach (string key in m_ResearchManager.Keys) {
			researchData.researchData.Add(new ResearchAccountDataSerialClass(key, m_ResearchManager[key]));
		}

		return researchData;
//		IODataClass.GetInstance.saveData (researchData, "ResearchInfo");

	}

	/// <summary>
	/// 불러오기
	/// </summary>
	/// <returns><c>true</c>, if data was loaded, <c>false</c> otherwise.</returns>
	/// <param name="researchData">Research data.</param>
	public bool loadData(ResearchAccountSerialClass researchData){
		if (researchData != null) {
			m_nowResearch = researchData.nowResearch;
			m_researched = researchData.researched;
//			m_researching = researchData.researching;
			m_facilityLv = researchData.facilities;

			//연구 완료된 데이터의 자식 연구 가져오기
			m_researching.Clear();
			foreach(string researched in m_researched){
				ResearchClass research = ResearchPackageClass.GetInstance.getResearch(researched);
				addChildrenResearch(research);
			}

			//연구중인 데이터 가져오기
			m_readyResearch.Clear ();
			foreach (string readyResearch in researchData.readyResearch) {
				ResearchClass runResearch = ResearchPackageClass.GetInstance.getResearch(readyResearch);
				runResearch.setWaitResearch();
				m_readyResearch.Add(runResearch);

			}
		
			//연구 데이터 가져오기
			m_ResearchManager.Clear ();
			foreach (ResearchAccountDataSerialClass research in researchData.researchData) {
				m_ResearchManager.Add(research.key, research.data);
			}

			Debug.Log("로드 가능 : " + GetType());
			return true;
		}
		Debug.Log("로드 불가능 : " + GetType());
		return false;

	}

	/// <summary>
	/// 자식 연구 삽입하기
	/// </summary>
	/// <returns><c>true</c>, if children research was added, <c>false</c> otherwise.</returns>
	/// <param name="research">Research.</param>
	private bool addChildrenResearch(ResearchClass research){
		if(research != null){
			foreach(string childResearch in research.children){
				//연구가 이미 완료되어 있지 않으면
				if(!m_researched.Contains(childResearch)){
					//자식 연구 데이터가 중복되지 않으면 삽입
					if(!m_researching.Contains(childResearch)){
						//Debug.Log("addChildren : " + childResearch);
						m_researching.Add(childResearch);
					}
				}
				//Debug.Log("researchChildren : " + childResearch);
			}
			return true;
		}
		return false;
	}


	/// <summary>
	/// 현재 진행중인 연구
	/// </summary>
	/// <returns>The research.</returns>
	public ResearchClass nowResearch(){
		if (m_readyResearch.Count != 0) {
			m_readyResearch[0].setRunResearch();
			return m_readyResearch[0];
		}
		
		return null;
	}
	
	/// <summary>
	/// 대기중인 연구
	/// </summary>
	/// <returns>The research.</returns>
	public List<ResearchClass> readyResearch(){
		return m_readyResearch;
	}
	
	/// <summary>
	/// 연구 추가
	/// </summary>
	/// <returns><c>true</c>, 연구 삽입 완료 <c>false</c> 연구 삽입 실패.</returns>
	/// <param name="research">Research.</param>
	public bool addResearch(ResearchClass research){
		Debug.Log ("연구 큐에 삽입 : " + research.name);
//		m_researching.Remove (research.key);

		if (m_readyResearch.Count >= getResearchSlotCount ()) {
			Debug.Log("연구 슬롯 꽉참");
			return false;
		}

		research.setWaitResearch ();
		m_readyResearch.Add (research);
		nowResearch ();
		return true;
	}

	/// <summary>
	/// 연구 취소
	/// </summary>
	/// <param name="research">Research.</param>
	public bool cancelResearch(ResearchClass research){

		//맨 앞에 있는 연구 데이터이면
		if (nowResearch ().key.Equals(research.key))
			m_nowResearch = 0; //현재 연구 초기화

		if (m_readyResearch.Remove (research)) {
			//연구데이터 초기화
			research.setNoneResearch();
			nowResearch ();
//			saveData();
			return true;
		}
		return false;
	}


	/// <summary>
	/// 빠른연구 완료
	/// </summary>
	/// <returns><c>true</c>, if research was pinecolaed, <c>false</c> otherwise.</returns>
	/// <param name="research">Research.</param>
	public void fastResearch(ResearchClass research){
		//빠른 연구를 완료한 연구가 큐에 있으면 삭제하기
		successResearch (research);



		//현재 연구중이었으면 삭제
		if(m_readyResearch.Contains(research))
			m_readyResearch.Remove (research);

	}
	
	/// <summary>
	/// 연구 진행 중
	/// </summary>
	/// <returns><c>true</c>, if research was run, <c>false</c> otherwise.</returns>
	/// <param name="product">Product.</param>
	public bool runResearch(int product){
		ResearchClass now = nowResearch ();
		if (now != null) {
			if (m_nowResearch + product >= now.product) {
				successResearch(now); //현재 연구를 연구 완료 데이터에 삽입하기
				nextResearch(true); //현재 연구 없애고 다음 연구를 현재 연구로 바꾸기
				return true;
			}
			
			m_nowResearch += product;
		}
		return false;
		
	}
	
	/// <summary>
	/// 연구 비율
	/// </summary>
	/// <returns>The ratio.</returns>
	public float researchRatio(){
		ResearchClass now = nowResearch ();
		if (now != null) {
			return PrepClass.ratioCalculate((float) m_nowResearch, (float)now.product);
		}
		return 0f;
	}

	/// <summary>
	/// 연구 데이터 출력
	/// </summary>
	/// <returns>The data format.</returns>
	public string researchDataFormat(){
		ResearchClass now = nowResearch ();
		if (now != null) {
			return string.Format("{0}/{1}", m_nowResearch, now.product);
		}
		return "-/-";
	}

	/// <summary>
	/// 연구 완료. 현재 완료된 연구를 삽입
	/// </summary>
	/// <returns>The research.</returns>
	/// <param name="key">Key.</param>
	private string successResearch(ResearchClass research){

		//맨 앞에 연구 데이터가 있고 그것이 같은것이라면
		if (nowResearch() != null && nowResearch ().key.Equals (research.key)){
			m_nowResearch = 0; //현재 연구 초기화
		}

		//연구 완료된 데이터가 중복되지 않으면 삽입
		if(!m_researched.Contains(research.key))
			m_researched.Add (research.key);

		//해당하는 키에 맞게 데이터 삽입
		int index = int.Parse (research.key.Substring (2, 2));
		Debug.Log ("success index : " + index.ToString () + " - " + research.key);
		if (index != 2) {
			Debug.Log ("업그레이드 삽입");
			if(!addUpgrade(research.key)){
				Debug.LogError("연구 데이터에 삽입하지 못했습니다 : " + research.key);
			}
//			m_ResearchManager [research.key.Substring (0, 4)] [research.key.Substring (4, 2)] = int.Parse (research.key.Substring (6, 2));
		} else {
			Debug.Log ("시설 삽입");
			m_facilityLv.Add(research.key);
		}

		//연구 완료된 데이터의 하위 데이터 삽입
		addChildrenResearch (research);

		//연구 가능에서 삭제
		m_researching.Remove (research.key);

		//새로고침
		//refreshResearchEvents ();

		return research.key;
	}

	/// <summary>
	/// 연구 업그레이드
	/// </summary>
	/// <returns><c>true</c>, if upgrade was added, <c>false</c> otherwise.</returns>
	/// <param name="key">Key.</param>
	private bool addUpgrade(string key){
		string researchKey = key.Substring (0, 4);
		int researchIndex = int.Parse (key.Substring (4, 2));
		int researchUpgrade = int.Parse (key.Substring (6, 2));
		if (m_ResearchManager.ContainsKey (researchKey)) {
			m_ResearchManager [researchKey] [researchIndex] = researchUpgrade;
			return true;
		}
		return false;

	}

	/// <summary>
	/// 다음 연구 가져오기 [true : 현재 연구 삭제 false : 현재 연구 유지]
	/// </summary>
	/// <returns>The research.</returns>
	/// <param name="isRemove"></param>
	public ResearchClass nextResearch(bool isRemove = false){

		if (nowResearch () != null) {
			if(isRemove){ 
				m_readyResearch.Remove(nowResearch());
				return nowResearch ();
			}

			else if(m_readyResearch.Count > 1)
				return m_readyResearch [1];

		}



		return null;
	}

	/// <summary>
	/// 불러오기 - 연구 완료된 데이터 초기화 하기
	/// </summary>
	/// <returns><c>true</c>, if research was inited, <c>false</c> otherwise.</returns>
	/// <param name="keys">Keys.</param>
	public bool initResearch(string[] keys){
		
		if (m_researched.Count == 0) {
			
			try {
				foreach (string key in keys) {
					m_researched.Add (key);
				}
			} catch {
				Debug.LogError ("연구 자료를 삽입하지 못했습니다.");
			}
			
			return true;
		} else {
			Debug.Log("이미 연구 자료를 삽입했습니다.");
			return false;
		}
	}
	
	/// <summary>
	/// 연구 완료된 키 확인하기
	/// </summary>
	/// <returns><c>true</c>, if check was researched, <c>false</c> otherwise.</returns>
	/// <param name="key">Key.</param>
//	public bool researchedCheck(string key){
//		return m_researched.Contains (key);
//	}

	/// <summary>
	/// 연구중인 키 확인하기
	/// </summary>
	/// <returns><c>true</c>, if check was researching <c>false</c> otherwise.</returns>
	/// <param name="key">Key.</param>
//	public bool researchingCheck(string key){
//		return m_researching.Contains (key);
//	}

	/// <summary>
	/// 자동 연구 실행
	/// </summary>
	/// <returns><c>true</c>, if automatic research was run, <c>false</c> otherwise.</returns>
	public bool runAutomaticResearch(){
		if (nowResearch () != null) {
			m_nowResearch += getAutomaticResearchCount ();
			return true;
		}
		return false;
	}
	
	/// <summary>
	/// 연구 최대치 증가
	/// </summary>
	/// <returns><c>true</c>, if count max upgrade was researched, <c>false</c> otherwise.</returns>
//	public bool researchCountMaxUpgrade(){
//		m_maxResearchCount++;
//	}
}


