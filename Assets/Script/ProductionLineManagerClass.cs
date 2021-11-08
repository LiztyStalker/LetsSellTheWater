using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProductionLineManagerClass {

	UIProductionLineManagerClass m_parent;
	List<UIProductionLineClass> m_productionLine; //사용중인 라인


	public ProductionLineManagerClass(UIProductionLineManagerClass parent){
		m_parent = parent;
	}

	/// <summary>
	/// 생산라인 추가
	/// </summary>
	/// <param name="productionLine">Production line.</param>
	public void addProductionLine(UIProductionLineClass productionLine){
		m_productionLine.Add (productionLine);
	}

	/// <summary>
	/// 생산라인 반환
	/// </summary>
	public void returnProductionLine(UIProductionLineClass productionLine){
		m_parent.returnProductionLine (productionLine);
	}

//	public void initStack(){
//
//	}
//
//	public void businessWakeUp(){
//		m_parent.businessWakeUp ();
//	}



}
