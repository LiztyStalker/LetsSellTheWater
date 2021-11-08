using UnityEngine;
using System.Collections;
using System;

public enum TYPE_FACILITY{NONE = -1, PRODUCT, MANUFACTURE, BUSINESS, RESEARCH}

public static class VolumeClass{
	static readonly int[] m_volumeList = {10, 50, 100, 250, 355, 500, 1000, 1500, 2000, 5000, 18900, 20000, 22000, 30000, 50000, 60000, 100000, 120000, 200000, 300000, 400000, 500000, 1000000};

	public static int getVolumeCount(){return m_volumeList.Length;}
	public static int getVolume(int index){
		return m_volumeList [index];
	}
}

[Serializable]
public class FacilitySerialClass{
	public string key;
	public int usePos;
	public FacilitySerialClass(FacilityClass facility){
		key = facility.key;
		usePos = facility.usePos;
	}
}

public class FacilityClass {

	Sprite m_sprite; //이미지
	string m_key; // 키
	string m_name; //이름
	TYPE_FACILITY m_facilityType; //시설 종류
	float m_time; //시간
	int m_product; //획득상품
	int m_upkeep; //유지비
	int m_cost; //구입비
	float m_contamination; //오염도
	string m_contents; //설명

	//사용 위치
	//시설 종류에 따른 인덱스
	//0이면 사용 위치 없음
	//1이상 배열에 따른 사용 위치
	int m_usePos;

	public Sprite sprite{ get { return m_sprite; } }
	public string key{ get { return m_key; } }
	public string name{ get { return m_name; } }
	public TYPE_FACILITY facilityType{ get { return m_facilityType; } }
	public float time{ get { return m_time; } }
	public string contents{ get { return m_contents; } }


	/// <summary>
	/// 생산품 가져오기
	/// </summary>
	/// <value>The product.</value>
	public int product{ 
		get { 
			switch(m_facilityType){
			case TYPE_FACILITY.RESEARCH:
				return m_product + AccountClass.GetInstance.researchAccount.getResearchProductCount(m_product);
			default:
				return m_product; 
			}
		} 
	}
	public int upkeep{ get { return m_upkeep; } }
	public int cost{ get { return m_cost; } }
	public float contamination{ get { return m_contamination; } }
	public int sell{ get { return m_cost / 2; } }

	/// <summary>
	/// 사용 위치
	/// </summary>
	/// <value>0이면 사용하지 않음. 1이상이면 위치 있음</value>
	public int usePos{ get { return m_usePos; } set { m_usePos = value; } }

	public FacilityClass(){
		m_key = "RS02----";
		m_name = "구덩이";
		m_facilityType = TYPE_FACILITY.PRODUCT;
		m_time = 0f;
		m_product = 0;
		m_upkeep = 0;
		m_cost = 0;
		m_contamination = 0f;

		Debug.Log ("시설 데이터 생성");
	}

	/// <summary>
	/// 테스트용
	/// </summary>
	/// <param name="usePos">Use position.</param>
//	public FacilityClass(int usePos){
//		m_key = "FA0001";
//		m_name = "우물";
//		m_facilityType = TYPE_FACILITY.PRODUCT;
//		m_time = 5f;
//		m_product = 10;
//		m_upkeep = 1;
//		m_cost = 10;
//		m_usePos = usePos;
//		m_contamination = 3f;
//		Debug.Log ("시설 데이터 생성");
//	}

	public FacilityClass(Sprite sprite, string key, string name, TYPE_FACILITY type, float time, int product, int upkeep, int cost, float contamination, string contents){

		m_key = key;
		m_sprite = sprite;
		m_name = name;
		m_facilityType = type;
		m_time = time;
		m_product = product;
		m_upkeep = upkeep;
		m_cost = cost;
		m_contamination = contamination;
		m_contents = contents;
		m_usePos = 0;
	}
	
	public FacilityClass(FacilityClass facility){
		m_sprite = facility.sprite;
		m_key = facility.key;
		m_name = facility.name;
		m_facilityType = facility.facilityType;
		m_time = facility.time;
		m_product = facility.product;
		m_upkeep = facility.upkeep;
		m_cost = facility.cost;
		m_contamination = facility.contamination;
		m_contents = facility.contents;
//		Debug.Log ("시설 데이터 복사");
	}

	public void setFacilitySerial(FacilitySerialClass facilitySerial){
		m_usePos = facilitySerial.usePos;
	}

	/// <summary>
	/// 생산품 가져오기
	/// </summary>
	/// <returns>The product.</returns>
//	public int getProduct(){
//		return m_product;
//	}

	
}
