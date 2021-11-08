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



public class PrepClass{


	static readonly string[] literUnit = {"mL", "L", "kL", "ML", "GL", "TL", "PL", "EL", "ZL", "YL"};

	/// <summary>
	/// 비율 계산기
	/// </summary>
	/// <returns> 0f~1f의 비율 출력 </returns>
	/// <param name="now">현재값.</param>
	/// <param name="max">최대값.</param>
	public static float ratioCalculate(float now, float max = 1f){
		float ratio = now / max;
		if (ratio > 1f) 
			ratio = 1f;
		else if (ratio < 0f)
			ratio = 0f;
		return ratio;
	}


	/// <summary>
	/// 리터 단위 계산기
	/// </summary>
	/// <returns>The calculate.</returns>
	/// <param name="milliliter">Milliliter.</param>
	public static string literCalculate(int milliliter){
		int unit = 0;
		double data = (double)milliliter;
		while (data >= 1000) {
			data *= 0.001;
			unit++;
		}

		if(unit >= literUnit.Length){
			unit = literUnit.Length - 1;

			if(data >= 1000)
				data = 999;

		}

		if (unit != 0) {
			if (data / 100 >= 1) {
				return string.Format ("{0,0:##0}{1}", data, literUnit [unit]);
			} else if (data / 10 >= 1) {
				return string.Format ("{0,0:#0.0}{1}", data, literUnit [unit]);
			} else {
				return string.Format ("{0,0:0.00}{1}", data, literUnit [unit]);
			}
		} else {
			return string.Format ("{0,0:##0}{1}", data, literUnit [unit]);
		}

	}

}

