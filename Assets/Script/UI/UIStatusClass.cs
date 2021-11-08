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
using UnityEngine;
using UnityEngine.UI;

public class UIStatusClass : UIObjectClass
{
	
	[SerializeField] Image m_companyIcon; //회사 아이콘

	[SerializeField] Text m_nameText; //회사명
	[SerializeField] Text m_gradeText; //등급
	[SerializeField] Text m_dateText; //진행 날짜
	[SerializeField] Text m_pineColaText; //파인콜라
	[SerializeField] Text m_assetText; //자금
	[SerializeField] Text m_contaminationText; //오염도

	[SerializeField] Text m_employeeText; //직원 수
	[SerializeField] Text m_facilityText; //시설 수
	[SerializeField] Text m_runLineText; //가동 라인 수 - 정상적으로 생산중인 라인
	[SerializeField] Text m_extendText; //확장 횟수 - 라인 확장 횟수

	[SerializeField] Text m_totalCustomerText; //총 소비자 수
	[SerializeField] Text m_reputationText; //총 인지도
	[SerializeField] Text m_vipText; //VIP가 왔다 간 횟수
	[SerializeField] Text m_topCustomerText; //최대 사용 소비자

	[SerializeField] Text m_totalExpenditureText; //총 지출
	[SerializeField] Text m_totalProfitText; //총 이익
	[SerializeField] Text m_totalCalculateText; //최종 결산
	[SerializeField] Text m_companyValueText; //회사 가치

	[SerializeField] Button m_extendBtn; //확장 버튼
	[SerializeField] Button m_adbBtn; // 광고 버튼
	

	protected override void OnEnable ()
	{
		base.OnEnable ();
		StartCoroutine (statusViewRun ());
	}

	IEnumerator statusViewRun(){
		while (gameObject.activeSelf) {
			UIPanelData();
			yield return null;
		}
	}


	private void UIPanelData(){


		m_companyIcon.sprite = AccountClass.GetInstance.companyIcon;
		m_nameText.text = AccountClass.GetInstance.companyName;
		m_gradeText.text = TranslatorClass.GetInstance.getCompanyGrade(AccountClass.GetInstance.grade);
		m_dateText.text =  string.Format("{0}", AccountClass.GetInstance.runDate);
		m_pineColaText.text = string.Format("{0}", AccountClass.GetInstance.pinecola);
		m_assetText.text =  string.Format("{0}", AccountClass.GetInstance.assets);
		m_contaminationText.text = string.Format("{0:f3}", AccountClass.GetInstance.contamination);

		m_employeeText.text = string.Format("{0}명", AccountClass.GetInstance.employeeCount);
        m_facilityText.text = string.Format("{0}대", AccountClass.GetInstance.facilityCount);
		m_runLineText.text = string.Format("{0}대", UIMadiatorClass.getRunningLineCount ());
		m_extendText.text = string.Format("{0}회", AccountClass.GetInstance.extendCount);


		m_totalCustomerText.text = string.Format("{0}명", AccountClass.GetInstance.totalCustomer);
		m_reputationText.text = string.Format("{0:f0}%", AccountClass.GetInstance.reputationAccount.averageReputation () * 100f);
		m_vipText.text = string.Format("{0}명", AccountClass.GetInstance.totalVIP);
		m_topCustomerText.text = string.Format ("{0}", AccountClass.GetInstance.getBestCustomer ());

		m_totalExpenditureText.text = string.Format ("{0:#,##0}", AccountClass.GetInstance.totalExpenditure); //총 지출
		m_totalProfitText.text = string.Format ("{0:#,##0}", AccountClass.GetInstance.totalProfit); //총 이익
		m_totalCalculateText.text = string.Format ("{0:#,##0}", AccountClass.GetInstance.totalCalculate); //최종 결산
		m_companyValueText.text = string.Format ("{0:#,##0}", AccountClass.GetInstance.companyValue); //회사 가치


		if (AccountClass.GetInstance.extendValue < 0) {
			m_extendBtn.GetComponentInChildren<Text> ().text = string.Format ("-");
			m_extendBtn.enabled = false;
		} else {
			m_extendBtn.GetComponentInChildren<Text> ().text = string.Format ("회사확장\n{0}", AccountClass.GetInstance.extendValue);
			m_extendBtn.enabled = true;
		}
	}

	protected override void OnDisable(){
		base.OnDisable ();
	}

//	public void viewPanelClose(){
//		panelClose ();
//	}

	public void extendAreaEvents(){
		UIMadiatorClass.UIEffectSoundPlayer(TYPE_AUDIO.NONE);
		UIMadiatorClass.UIMsgPanelView.initMsgPanel(string.Format("부지를 확장하시겠습니까?\n사용비용 : {0}", AccountClass.GetInstance.extendValue), extendAresClicked, TYPE_MSG_SIGN.OKCANCEL , TYPE_MSG_ICON.INFOR);
	}

	private void extendAresClicked(){
		if (AccountClass.GetInstance.extendValue < AccountClass.GetInstance.assets) {
			AccountClass.GetInstance.addAssets (-AccountClass.GetInstance.extendValue, TYPE_FINANCE.UTILITY);
			if (UIMadiatorClass.extendAreaEvents ()) {
				effectSoundPlayer(TYPE_AUDIO.EXTEND);
				UIMadiatorClass.UIMsgPanelView.initMsgPanel ("확장 완료!", TYPE_MSG_ICON.INFOR);
				UIMadiatorClass.setMidBtns ();
			} else {
				AccountClass.GetInstance.addAssets (AccountClass.GetInstance.extendValue, TYPE_FINANCE.UTILITY);
				UIMadiatorClass.UIMsgPanelView.initMsgPanel ("확장 최대치를 넘었습니다.", TYPE_MSG_ICON.ERROR);
				effectSoundPlayer(TYPE_AUDIO.WARNING);
			}
		} else {
			UIMadiatorClass.UIMsgPanelView.initMsgPanel ("자금이 부족합니다.", TYPE_MSG_ICON.ERROR);		
			effectSoundPlayer(TYPE_AUDIO.WARNING);
		}
	}
}

