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
using UnityEngine;


public class UIEmployeeSpriteClass : MonoBehaviour
{

	enum ANIM_CHARACTER{ICON, HEAD, BACKHEAD, FACE, BODY, ARM, LEG, WORK = 10, HAND = 11}
	enum ANIM_EMP_FACE{NONE = 3, SKILL = 7, EXHAUST, SUCCESS}
	 
	Sprite[] m_sprites;

	[SerializeField] SpriteRenderer m_head;
	[SerializeField] SpriteRenderer m_backhead;
	[SerializeField] SpriteRenderer m_face;	
	[SerializeField] SpriteRenderer m_successface;
	[SerializeField] SpriteRenderer m_body;
	[SerializeField] SpriteRenderer[] m_arms;
	[SerializeField] SpriteRenderer[] m_hands;
	[SerializeField] SpriteRenderer[] m_legs;

	public void setSprite(Sprite[] employeeSprite){

		if (employeeSprite != null) {
			m_sprites = employeeSprite;
			initSprite();
		} else {
			m_sprites = null;
			m_head.sprite = null;
			m_backhead.sprite = null;
			m_face.sprite = null;
			m_body.sprite = null;
			m_arms [0].sprite = null;
			m_arms [1].sprite = null;
			m_hands [0].sprite = null;
			m_hands [1].sprite = null;
			m_legs [0].sprite = null;
			m_legs [1].sprite = null;
		}
		m_successface.gameObject.SetActive(false);
	}

	/// <summary>
	/// 기본형으로 초기화
	/// </summary>
	public void initSprite(){
		//Debug.Log ("초기화");
		m_head.sprite = m_sprites [(int)ANIM_CHARACTER.HEAD];
		m_backhead.sprite = m_sprites [(int)ANIM_CHARACTER.BACKHEAD];
		m_face.sprite = m_sprites [(int)ANIM_CHARACTER.FACE];
		m_successface.sprite = m_sprites [(int)ANIM_EMP_FACE.SUCCESS];
		m_body.sprite = m_sprites [(int)ANIM_CHARACTER.BODY];
		m_arms [0].sprite = m_sprites [(int)ANIM_CHARACTER.ARM];
		m_arms [1].sprite = m_sprites [(int)ANIM_CHARACTER.ARM];
		m_hands [0].sprite = m_sprites [(int)ANIM_CHARACTER.HAND];
		m_hands [1].sprite = m_sprites [(int)ANIM_CHARACTER.HAND];
		m_legs [0].sprite = m_sprites [(int)ANIM_CHARACTER.LEG];
		m_legs [1].sprite = m_sprites [(int)ANIM_CHARACTER.LEG];
	}



	/// <summary>
	/// 대성공
	/// </summary>
//	public void changeSuccess(){
//		Debug.Log ("대성공");
//		//m_arms [0].sprite = m_sprites [(int)ANIM_CHARACTER.ARM];
//		//m_arms [1].sprite = m_sprites [(int)ANIM_CHARACTER.ARM];
//		//m_face.sprite = m_sprites [(int)ANIM_EMP_FACE.SUCCESS];
//	}

	/// <summary>
	/// 일하는 중
	/// </summary>
	public void changeWork(){
		//Debug.Log ("일하는 중");
		m_arms[0].sprite = m_sprites [(int)ANIM_CHARACTER.WORK];
		m_arms[1].sprite = m_sprites [(int)ANIM_CHARACTER.WORK];
	}

	/// <summary>
	/// 휴식중
	/// </summary>
	public void changeExhaust(){
		m_face.sprite = m_sprites [(int)ANIM_EMP_FACE.EXHAUST];
	}

	/// <summary>
	/// 스킬 발동
	/// </summary>
	public void changeSkill(){
		m_face.sprite = m_sprites [(int)ANIM_EMP_FACE.SKILL];
	}


}

