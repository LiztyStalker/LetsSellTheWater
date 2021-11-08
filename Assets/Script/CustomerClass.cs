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
using UnityEngine;

public enum TYPE_CUSTOMER {CHILD, TEENAGER, ADULT, MID, OLD}

[Serializable]
public class CustomerSerialClass{
	public string key;
	public int volume;
	public int count;
	
	public float nowWaitTime;
	public float maxWaitTime;

	public CustomerSerialClass(CustomerClass customer){
		this.key = customer.key;
		this.volume = customer.volume;
		this.count = customer.count;
		this.nowWaitTime = customer.nowWaitTime;
		this.maxWaitTime = customer.maxWaitTime;
	}
}

public class CustomerClass
{

	const int c_visitCustomer = 1000;	//손님 n당 +1 추가
	const int c_yearCustomer = 5; //n년마다 손님 +1 추가
	const int c_extendCustomer = 2; //1번 확장마다 +n 추가

	Sprite m_sprite;
	string m_key;
	string m_name;
	TYPE_CUSTOMER m_typeCustomer;
	int m_volume;
	int m_count;

	float m_nowWaitTime = 10f;
	float m_maxWaitTime = 10f;

	public Sprite sprite{ get { return m_sprite; } }
	public string name{ get { return m_name; } }
	public string key{ get { return m_key; } }
	public TYPE_CUSTOMER typeCustomer{ get { return m_typeCustomer; } }
	public int volume{ get { return m_volume; } }
	public int count{ get { return m_count; } }
	public float nowWaitTime{ get { return m_nowWaitTime; } }
	public float maxWaitTime{ get { return m_maxWaitTime; } }

	public CustomerClass(){

	}

	public CustomerClass(Sprite sprite, string key, string name, TYPE_CUSTOMER typeCustomer, int volume, int count){
		m_key = key;
		m_sprite = sprite;
		m_name = name;
		m_typeCustomer = typeCustomer;
		m_volume = volume;
		m_count = count;
	}

	public CustomerClass(CustomerClass customer){
		m_key = customer.key;
		m_sprite = customer.sprite;
		m_name = customer.name;
		m_typeCustomer = customer.typeCustomer;
		m_volume = customer.volume;
		m_count = countCalculator (customer.count);
	}

	private int countCalculator(int defaultCount){
		//기본

		int count = defaultCount;

		//손님 n당 +1 추가
		count += AccountClass.GetInstance.totalCustomer / c_visitCustomer;

		//인지도 50% 이상시 +1 이후로 10%당 +1추가
		if (AccountClass.GetInstance.reputationAccount.averageReputation () >= 0.5f)
			count += (int)((AccountClass.GetInstance.reputationAccount.averageReputation () - 0.4f) / 0.1f);

		//n년마다 +1 추가
		count += AccountClass.GetInstance.calenderAccount.year / c_yearCustomer;

		//확장마다 +2
		count += AccountClass.GetInstance.extendCount * c_extendCustomer;


		//vip 획득시 +1 추가 - 보류

		return UnityEngine.Random.Range (1, count + 1);

	}

	/// <summary>
	/// 대기 시간 false이면 대기시간 넘음
	/// </summary>
	/// <returns><c>true</c>, if wait time was used, <c>false</c> otherwise.</returns>
	/// <param name="time">Time.</param>
	public bool useWaitTime(float time = 1f){
		if (m_nowWaitTime - time < 0f) {
			return false;
		}
		m_nowWaitTime -= time;
		return true;
	}

	/// <summary>
	/// 대기시간 비율
	/// </summary>
	/// <returns>The time ratio.</returns>
	public float waitTimeRatio(){
		return PrepClass.ratioCalculate ((float)m_nowWaitTime, (float)m_maxWaitTime);
	}
}


