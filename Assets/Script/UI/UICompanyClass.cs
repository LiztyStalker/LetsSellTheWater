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
public class UICompanyClass : MonoBehaviour
{
	[SerializeField] Text m_companyNameText;
	[SerializeField] Image m_companyIconImage;

	[SerializeField] Text m_pinecolaText;
	[SerializeField] Text m_assetsText;

	void Start(){
		StartCoroutine (dataUpdate());
	}

	IEnumerator dataUpdate(){
		while (gameObject.activeSelf) {
			m_assetsText.text = string.Format("{0:#,##0}", AccountClass.GetInstance.assets);
			m_pinecolaText.text = string.Format("{0}", AccountClass.GetInstance.pinecola);
			yield return null;
		}
	}


}

