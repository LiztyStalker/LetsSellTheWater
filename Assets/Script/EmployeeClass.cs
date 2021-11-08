using UnityEngine;
using System.Collections;
using System;

public enum TYPE_CHARACTER{
	NONE, //평범
	WARMBLOODED, //열혈
	TIMID, //소심
	HEALTY, //건강
	BRAINY, //총명
	CHARISMA, //카리스마
	FAST, //신속
	SINCERITY, //성실
	CHEERFUL, //명랑
	ANGER //분노
}

public enum TYPE_RANK{
	INTERN,
	MEMBER,
	ASSOCIATE,
	ASSOCIATE_MANAGER,
	MANAGER,
	TEAM_MANAGER,
	DEPARTMENT_MANAGER,
	DIRECTOR,
	MANAGING_DIRECTOR,
	SENIOR_MANAGING_DIRECTOR
}

[Serializable]
public class EmployeeSerialClass{
	public string key; //키
	public int health; //체력
	public int quickness; //순발력
	public int intelligence; //지능
	public int charm; //매력
	public int workmanship; //숙련도
	public int rank; //직급
	public int experiance; //경험치
	public int experianceMax; //경험치 최대

	public int usePos; //작업 위치
	
	public int nowHealth; //현재 체력
	public int maxHealth; //최대 체력
	
	public bool isExhaust; //탈진상태 true 탈진
	public bool isImmersion; //몰입상태 true 몰입

	public EmployeeSerialClass(EmployeeClass employee){
		key = employee.key;
		health = employee.healthDefault;
		quickness = employee.quicknessDefault;
		intelligence = employee.intelligenceDefault;
		charm = employee.charmDefault;
		workmanship = employee.workmanship;
		experiance = employee.experiance;
		experianceMax = employee.experianceMax;
		usePos = employee.usePos;
		nowHealth = employee.nowHealth;
//		maxHealth = employee.maxHealth;
	}
}

public class EmployeeClass {


	readonly int[] c_rankList = {5, 25, 50, 80, 120, 170, 230, 300, 400};
	readonly int[] c_rankAbility = {0, 15, 30, 45, 60, 80, 100, 130, 160, 200};

//	const int c_abilityValue = 10;

	//1당 각각의 능력치 배율 (사용하지 않음)
	const int c_healthRate = 1;
	const int c_quicknessRate = 1;
	const int c_intelligenceRate = 1;
	const int c_charmRate = 1;

	const int c_pinecolaRate = 5;
	const int c_pinecolaAdd = 5;
	const int c_pinecolaMax = 10000;

	//추가 급여 비율
	const int c_salaryRatio = 5;

//	Sprite m_faceIcon; //얼굴 이미지
	Sprite[] m_icons; //아이콘들
	string m_key; //키
//	string m_imagekey; //이미지 키
	string m_name; //이름
	bool m_gender; //성별
	TYPE_CHARACTER m_characterType; //성격
	TYPE_RANK m_rank; //직급
	int m_health; //체력
	int m_quickness; //순발력
	int m_intelligence; //지능
	int m_charm; //매력
	int m_salary; //월급
	int m_workmanship; //숙련도
	int m_experiance; //경험치
	int m_experianceMax; //경험치 최대
//	int m_abilityPoint;
	int m_employmentFee; //고용비
	string m_contents; //설명

	int m_usePos;

	const int c_maxHealth = 100;

	int m_nowHealth = 100; //현재 체력
	int m_maxHealth = 100; //최대 체력

	const int c_successPoint = 10;
	const float c_experianceRate = 0.05f;

	bool m_isExhaust = false; //탈진상태 true 탈진
	bool m_isImmersion = false; //몰입상태 true 몰입
	
	public Sprite faceIcon{ get { return m_icons[0]; } }
	public Sprite[] icons{ get { return m_icons; } }
//	public Sprite bodyIcon{ get { return m_bodyIcon; } }
	public string key{ get { return m_key; } }
//	public string imagekey{ get { return m_imagekey; } }
	public string name{ get { return m_name; } }// set{m_name=value;}}
	public bool gender{get{return m_gender;}}// set{m_gender=value;}}
	public TYPE_CHARACTER characterType{ get { return m_characterType; } }// set{m_characterType=value;}}
	public TYPE_RANK rank{ get { return m_rank; } }

	public int health{get{return getHealthValue();}}// set{m_health=value;}}
	public int healthDefault{ get { return m_health; } }
	public int intelligence{get{return getIntelligenceValue();}}// set{m_intelligence=value;}}
	public int intelligenceDefault{ get { return m_intelligence; } }
	public int quickness{get{return getQuicknessValue();}}// set{m_quickness=value;}}
	public int quicknessDefault{ get { return m_quickness; } }
	public int charm{get{return getCharmValue();}}// set{m_charm=value;}}
	public int charmDefault{ get { return m_charm; } }

	public int salary{get{return m_salary + (workmanship / c_salaryRatio);}}// set{m_salary=value;}}
	public int workmanship{get{return m_workmanship;}}// set{m_workmanship=value;}}
	public int experiance{get{return m_experiance;}}// set{m_experiance=value;}}
	public int experianceMax{get{return m_experianceMax;}}// set{m_experianceMax=value;}}
//	public int abilityPoint{get{return m_abilityPoint;} set{m_abilityPoint=value;}}
	public int employmentFee{get{return m_employmentFee - (int)((float)m_employmentFee * 0.01f * (float)AccountClass.GetInstance.researchAccount.employeeSale());}}// set{m_employmentFee=value;}}
	public string contents{ get { return m_contents; } }

	public int usePos{ get { return m_usePos; } set { m_usePos = value; } } 

	public int nowHealth{ get { return m_nowHealth; } }
	public int maxHealth{ get { return m_maxHealth + health * 5; } }

	public bool isExhaust { get { return m_isExhaust; } }
	public bool isImmersion{ get { return m_isImmersion; } }

	private int pineCola{ get { return c_pinecolaRate + AccountClass.GetInstance.researchAccount.getPineColaCount() * c_pinecolaAdd; } } //

	private int getGrade(){
		return m_workmanship / 25;
	}

	private int getHealthValue(){
		int nowData = m_health + AccountClass.GetInstance.researchAccount.getHealthEducation();// + c_rankAbility[(int)m_rank];// * c_healthRate;

		switch (m_characterType) {
		case TYPE_CHARACTER.NONE:
			//능력치 5% 상승
			return nowData + (int)((float)nowData * (float)getGrade() * 0.05f);
		case TYPE_CHARACTER.HEALTY:
			//능력치 10%상승
			return nowData + (int)((float)nowData * (float)getGrade() * 0.1f);
		}
		return nowData;

	}

	private int getIntelligenceValue(){
		int nowData = m_intelligence + AccountClass.GetInstance.researchAccount.getIntelligenceEducation ();// + c_rankAbility[(int)m_rank];// * c_intelligenceRate;
		
		switch (m_characterType) {
		case TYPE_CHARACTER.NONE:
			//능력치 5% 상승
			return nowData + (int)((float)nowData * (float)getGrade() * 0.05f);
		case TYPE_CHARACTER.BRAINY:
			//능력치 10%상승
			return nowData + (int)((float)nowData * (float)getGrade() * 0.1f);
		}
		return nowData;

	}

	private int getQuicknessValue(){
		int nowData = m_quickness + AccountClass.GetInstance.researchAccount.getQuicknessEducation();// + c_rankAbility[(int)m_rank];// * c_quicknessRate;
		
		switch (m_characterType) {
		case TYPE_CHARACTER.NONE:
			//능력치 5% 상승
			return nowData + (int)((float)nowData * (float)getGrade() * 0.05f);
		case TYPE_CHARACTER.FAST:
			//능력치 10%상승
			return nowData + (int)((float)nowData * (float)getGrade() * 0.1f);
		}
		return nowData;

	}

	private int getCharmValue(){
		int nowData = m_charm + AccountClass.GetInstance.researchAccount.getCharmEducation();// + c_rankAbility[(int)m_rank];// * c_charmRate;
		
		switch (m_characterType) {
		case TYPE_CHARACTER.NONE:
			//능력치 5% 상승
			return nowData + (int)((float)nowData * (float)getGrade() * 0.05f);
		case TYPE_CHARACTER.CHARISMA:
			//능력치 10%상승
			return nowData + (int)((float)nowData * (float)getGrade() * 0.1f);
		}
		return nowData;
	}

	public void setEmployeeSerial(EmployeeSerialClass employeeSerialData){
		m_charm = employeeSerialData.charm;
		m_experiance = employeeSerialData.experiance;
		m_experianceMax = employeeSerialData.experianceMax;
		m_health = employeeSerialData.health;
		m_intelligence = employeeSerialData.intelligence;
		m_isExhaust = employeeSerialData.isExhaust;
		m_isImmersion = employeeSerialData.isImmersion;
//		m_maxHealth = employeeSerialData.maxHealth;
		m_rank = (TYPE_RANK)employeeSerialData.rank;
		m_nowHealth = employeeSerialData.nowHealth;
		m_quickness = employeeSerialData.quickness;
		m_usePos = employeeSerialData.usePos;
		m_workmanship = employeeSerialData.workmanship;
	}

	public EmployeeClass(Sprite[] icons, 
	                     string key,
	                     string name, 
	                     bool gender, 
	                     TYPE_CHARACTER characterType,
	                     TYPE_RANK rank,
	                     int health, 
	                     int quickness,
	                     int intelligence,
	                     int charm,
	                     int salary,
	                     int workmanship,
	                     int experiance,
	                     int experianceMax,
	                     int employmentFee,
	                     string contents
	                     ){
		initEmployeeData (icons, key, name, gender, characterType, rank, health, quickness, intelligence, charm, salary, workmanship, experiance, experianceMax, employmentFee, contents);
	}

	public EmployeeClass(EmployeeClass employee){
		initEmployeeData (employee.icons, employee.key, employee.name, employee.gender, employee.characterType, employee.rank, employee.health, employee.intelligence, employee.quickness, employee.charm, employee.salary, employee.workmanship, employee.experiance, employee.experianceMax, employee.employmentFee, employee.contents);
	}

	private void initEmployeeData(Sprite[] icons, 
	                              string key,
	                              string name, 
	                              bool gender, 
	                              TYPE_CHARACTER characterType,
	                              TYPE_RANK rank,
	                              int health, 
	                              int quickness,
	                              int intelligence,
	                              int charm,
	                              int salary,
	                              int workmanship,
	                              int experiance,
	                              int experianceMax,
	                              int employmentFee,
	                              string contents
	                              ){
		m_icons = (Sprite[])icons.Clone();
		m_key = key;
		m_name = name;
		m_gender = gender;
		m_characterType = characterType;
		m_rank = rank;
		m_health = health;
		m_quickness = quickness;
		m_intelligence = intelligence;
		m_charm = charm;
		m_salary = salary;
		m_workmanship = workmanship;
		m_experiance = experiance;
		m_experianceMax = experianceMax;
		m_employmentFee = employmentFee;
		m_contents = contents;
		m_usePos = 0;



//		Debug.Log (string.Format("{0}{1}{2}{3}{4}", m_health,
//		                         m_quickness,
//		                         m_intelligence,
//		                         m_charm,
//		                         m_employmentFee));

	}

	/// <summary>
	/// 체력 보정
	/// </summary>
//	private void healthCalculate(){
//		m_maxHealth = c_maxHealth + (health * 5);
//		m_nowHealth = (int)((float)m_maxHealth * healthRatio());
//	}


	/// <summary>
	/// 숙련도 업그레이드
	/// </summary>
	/// <returns><c>true</c>, if upgrade was workmanshiped, <c>false</c> otherwise.</returns>
	/// <param name="experiance">Experiance.</param>
	public bool workmanshipUpgrade(int experiance = 1){
//	                               experiance){

//		int experiance = 0;
//		if (facility != null) {
//			switch (facility.facilityType) {
//			case TYPE_FACILITY.PRODUCT:
//				//1초당 1
//				break;
//			case TYPE_FACILITY.MANUFACTURE:
//				break;
//			case TYPE_FACILITY.BUSINESS:
//				break;
//			case TYPE_FACILITY.RESEARCH:
//				break;
//			}
//		} else {
//			return false;
//		}

		bool isUpgrade = false;
		m_experiance += experiance;
		while(m_experiance >= m_experianceMax){
			m_experiance -= m_experianceMax;
			experianceCal();
			m_workmanship++;
			isUpgrade = true;
		}
		return isUpgrade;
	}

	private void experianceCal(){
		m_experianceMax += (int)((float)m_experianceMax * c_experianceRate);
		characterAbilityUpgrade();
		//healthCalculate ();
	}

	private void characterAbilityUpgrade(){
		int index = UnityEngine.Random.Range (0, 4);
		switch (index) {
		case 0:
			m_health++; //체력
			break;
		case 1:
			m_quickness++; //순발력
			break;
		case 2:
			m_intelligence++; //지능
			break;
		case 3:
			m_charm++; //매력
			break;
		}
	}


	/// <summary>
	/// 몰입 상태 활성화 - 체력 풀 충전
	/// </summary>
	public void setImmersion(){
		m_isImmersion = true;
		m_nowHealth = m_maxHealth;
	}

	/// <summary>
	/// 몰입상태 비활성화
	/// </summary>
	public void resetImmersion(){
		m_isImmersion = false;
	}

	/// <summary>
	/// 체력 사용하기
	/// </summary>
	/// <returns>The health.</returns>
	/// <param name="count">Count.</param>
	public int useHealth(int count = 1){

		//몰입상태이면 체력이 달지 않음
		if (!m_isImmersion){

			//1보다 낮으면 1로 보정
			if (count < 1)
				count = 1;

			if (!isHealth (count)) {
				//탈진
				Debug.Log ("탈진");
				m_isExhaust = true;
				return m_nowHealth - count;
			}
			m_nowHealth -= count;
	//		Debug.Log("체력 : " + m_nowHealth);
		}
		return m_nowHealth;
	}

	/// <summary>
	/// 체력 충전하기
	/// </summary>
	/// <returns>The health.</returns>
	/// <param name="count">Count.</param>
	public int chargeHealth(int count = 1){

		//1보다 낮으면 1로 보정
		if (count < 1)
			count = 1;

		if (m_nowHealth + count > maxHealth) {
			//회복
			m_isExhaust = false;
			m_nowHealth = maxHealth;
		} else {
			m_nowHealth += count;
		}
		return m_nowHealth;
	}

	/// <summary>
	/// 체력 사용 가능 여부 true 사용 가능
	/// </summary>
	/// <returns><c>true</c>, if health was ised, <c>false</c> otherwise.</returns>
	/// <param name="count">Count.</param>
	public bool isHealth(int count = 1){
//		Debug.Log (" - : " + m_nowHealth);
		if (m_nowHealth >= 0) {
			m_isExhaust = false;
			return true;
		} else {
			m_isExhaust = true;
			return false;
		}

	}


	/// <summary>
	/// 체력 비율
	/// </summary>
	/// <returns>The ratio.</returns>
	public float healthRatio(){
		return PrepClass.ratioCalculate ((float)m_nowHealth, (float)maxHealth);
	}

	/// <summary>
	/// 체력 충전 여부 
	/// </summary>
	/// <returns><c>true</c>, if health charge was ised, <c>false</c> otherwise.</returns>
	public bool isHealthCharge(){
		switch (m_characterType) {
		case TYPE_CHARACTER.WARMBLOODED:
			int check = m_workmanship / 25 * 10;

			if(check > 50)
				check = 50;

			if(check >= UnityEngine.Random.Range(0, 101)){
				return true;
			}
			break;
		}
		return false;
	}

	/// <summary>
	/// 대성공 여부
	/// </summary>
	/// <returns><c>true</c>, if successing was ised, <c>false</c> otherwise.</returns>
	public bool isSuccessing(){
		return (c_successPoint > UnityEngine.Random.Range (0, 101)) ? true : false;
	}

	/// <summary>
	/// 파인콜라 여부
	/// </summary>
	/// <returns><c>true</c>, if pine cola was ised, <c>false</c> otherwise.</returns>
	public bool isPineCola(){
		return (pineCola > UnityEngine.Random.Range (0, c_pinecolaMax + 1)) ? true : false;
	}

	/// <summary>
	/// 직급 상승 비용
	/// </summary>
	/// <returns>The up cost.</returns>
	public int rankUpCost(){
		return ((int)m_rank) * 200;
	}

	/// <summary>
	/// 직급 상승
	/// </summary>
	/// <returns><c>true</c>, if rank up was employeed, <c>false</c> otherwise.</returns>
	public bool employeeRankUp(){
		//직급이 최대이면 상승 못함
		if ((int)m_rank >= Enum.GetValues (typeof(TYPE_RANK)).Length) {
			return false;
		}

		AccountClass.GetInstance.addAssets (-rankUpCost(), TYPE_FINANCE.EMPLOY);
		m_rank++;
		return true;
	}

	/// <summary>
	/// 직급 상승 가능 여부
	/// </summary>
	/// <returns><c>true</c>, if rank up was ised, <c>false</c> otherwise.</returns>
	public bool isRankUp(){
		//직급이 최대가 아니면 상승 가능
		if ((int)m_rank < Enum.GetValues (typeof(TYPE_RANK)).Length) {
			//일정량의 숙련도를 넘어서면 직급 상승 가능
			if (m_workmanship >= c_rankList [(int)m_rank])
				return true;
		}
		return false;
	}

}
