//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.36373
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
// </auto-generated>
//------------------------------------------------------------------------------
using System;

[Serializable]
public class CalenderAccountSerialClass{
	public int year;
	public int month;
	public int day;
	public int hour;
	public int weather;
	public CalenderAccountSerialClass(int year, int month, int day, int hour, int weather){
		this.year = year;
		this.month = month;
		this.day = day;
		this.hour = hour;
		this.weather = weather;
	}
}

public enum TYPE_WEATHER{SUNNY}

public class CalenderAccountClass
{
	readonly int[] days = {31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31};
	AccountClass m_parent;
	int[] m_timePack = new int[5];
//	int m_year = 2050;
//	int m_month = 1;
//	int m_day = 1;
//	int m_hour = 0;

	public int year{ get { return m_timePack [0]; } private set { m_timePack [0] = value; } }
	public int month{ get { return m_timePack[1]; } private set { m_timePack [1] = value; }}
	public int day{ get { return m_timePack[2]; } private set { m_timePack [2] = value; }}
	public int hour{ get { return m_timePack[3]; } private set { m_timePack [3] = value; }}
	public int weather{ get { return m_timePack[4]; } private set {m_timePack [4] = value; }}



	public CalenderAccountClass (AccountClass parent)
	{
		m_parent = parent;
		year = 0;
		month = 1;
		day = 1;
		hour = 0;
		weather = (int)TYPE_WEATHER.SUNNY;
	}

	/// <summary>
	/// 타임 패키지 가져오기
	/// </summary>
	/// <returns>The time pack.</returns>
	public int[] getTimePack(){
		return m_timePack;
	}

	/// <summary>
	/// 시간 흐름
	/// </summary>
	/// <param name="timer">Timer.</param>
	public void runTime(int timer){

		hour++;

		if(hour == timer){
			day++;
			hour = 0;

			//자동연구
			AccountClass.GetInstance.researchAccount.runAutomaticResearch();

			if(day > days[month-1]){
				
				month++;
				day = 1;
				
				
				if(month > 12){
					
					//최종정산
					
					year++;
					month = 1;
				}
				
				//월급 주기
				AccountClass.GetInstance.monthlySettleAccounts();
				//정산
				AccountClass.GetInstance.financeAccount.changePastFinance(month);

				//공과금 소비 1확장당 100 소비
				AccountClass.GetInstance.addAssets(-m_parent.extendCount * 100, TYPE_FINANCE.UTILITY);

			}
		}
	}




	/// <summary>
	/// 저장하기
	/// </summary>
	/// <returns>The data.</returns>
	public CalenderAccountSerialClass saveData(){
		CalenderAccountSerialClass calenderData = new CalenderAccountSerialClass (year, month, day, hour, weather);
		return calenderData;
	}

	/// <summary>
	/// 불러오기
	/// </summary>
	/// <returns><c>true</c>, if data was loaded, <c>false</c> otherwise.</returns>
	/// <param name="calenderData">Calender data.</param>
	public bool loadData(CalenderAccountSerialClass calenderData){
		if (calenderData != null) {
			year = calenderData.year;
			month = calenderData.month;
			day = calenderData.day;
			hour = calenderData.hour;
			weather = calenderData.weather;
			return true;
		}
		return false;
	}

}


