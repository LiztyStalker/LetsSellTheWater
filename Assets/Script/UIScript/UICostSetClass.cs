using UnityEngine;
using System.Collections;

public class UICostSetClass : MonoBehaviour {

	const float c_time = 1.0f;

	//[SerializeField] GUITexture m_icon;
	//[SerializeField] GUIText m_text;

	[SerializeField] Sprite m_pinecolaIcons;
	[SerializeField] Sprite[] m_icons;

	Vector2 originPos;

	void Awake(){
		gameObject.SetActive (false);
//		m_text.gameObject.SetActive (false);
	}

	public void costSetPos(UIProductionAreaClass area){
		gameObject.transform.position = area.transform.position + Camera.main.WorldToViewportPoint (gameObject.transform.position - area.transform.position);

		//로컬 포지션 필요
		originPos = new Vector2 (gameObject.transform.localPosition.x, gameObject.transform.localPosition.y);
//		gameObject.transform.position = Camera.main.WorldToViewportPoint(m_icon.transform.position);

	}

	/// <summary>
	/// 생산품 획득 뷰
	/// </summary>
	/// <param name="facilityType">Facility type.</param>
	/// <param name="count">Count.</param>
	public void costSet(TYPE_FACILITY facilityType, int count){

		gameObject.SetActive (true);
		//Debug.Log ("costSet");

		//m_icon.texture = m_icons [(int)facilityType].texture;
		//m_text.text = string.Format("+{0}", count);

		//switch (facilityType) {
		//case TYPE_FACILITY.PRODUCT:
		//	m_text.color = Color.cyan;
		//	break;
		//case TYPE_FACILITY.MANUFACTURE:
		//	m_text.color = Color.green;
		//	break;
		//case TYPE_FACILITY.BUSINESS:
		//	m_text.color = Color.yellow;
		//	break;
		//case TYPE_FACILITY.RESEARCH:
		//	m_text.color = Color.magenta;
		//	break;
		//}

		StartCoroutine (viewCoroutine ());

	}

	/// <summary>
	/// 파인콜라 획득 뷰
	/// </summary>
	/// <param name="count">Count.</param>
	public void pinecolaSet(int count){
		gameObject.SetActive (true);
		Debug.Log ("pinecolaSet");

		//m_icon.texture = m_pinecolaIcons.texture;
		//m_text.text = string.Format ("+{0}", count);
		//m_text.color = Color.red;

		AccountClass.GetInstance.addPineCola (count);
		StartCoroutine (viewCoroutine ());


	}


	IEnumerator viewCoroutine(){
		float timer = c_time;
		//m_text.gameObject.SetActive (true);

		while (timer > 0f) {
			timer -= 0.05f;
			transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + 0.002f);
			yield return new WaitForSeconds(0.05f);
		}
		transform.localPosition = originPos;

		//m_icon.gameObject.SetActive (false);
		gameObject.SetActive (false);
	}

}
