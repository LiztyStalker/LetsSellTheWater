using UnityEngine;
using System.Collections;

public class UIProductionAreaClass : MonoBehaviour {

	[SerializeField] SpriteRenderer m_background;
	//[SerializeField] GUIText m_text;

	const float m_sizeX = 720f;
	const float m_sizeY = 1280f;


	public void setBackground(int index){
//		Sprite sprite = (Sprite)Resources.Load (string.Format("Image/Background/Background{0}", index), typeof(Sprite));


		Sprite sprite = (Sprite)Resources.Load (string.Format("Image/Background/Background0"), typeof(Sprite));



		if(sprite != null)
			m_background.sprite = sprite;

		//사이즈
		//1280 700을 기준으로 확장 및 축소



//		Vector2 size = new Vector2 (Screen.width / m_sizeX + (Screen.width / m_sizeX * 0.01f), Screen.height / m_sizeY + (Screen.height / m_sizeY * 0.01f));
//		m_background.transform.localScale = size;
//		m_text.text = string.Format ("{0} {1}", size.x, size.y);

	}




}
