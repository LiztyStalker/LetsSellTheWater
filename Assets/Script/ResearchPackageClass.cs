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
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ResearchPackageClass : SingletonClass<ResearchPackageClass>
{

	const char c_lineChar = '\n';
	const char c_splitChar = '\t';
	
	//Sprite m_emptyResearch;
	List<ResearchClass> m_researchList = new List<ResearchClass> ();

	TextAsset m_textAsset;

	public List<ResearchClass> researchList{ get { return m_researchList; } }

	public ResearchPackageClass ()
	{
		//initParsing ();
	}


	public bool assetBundleInitParsing (TextAsset textAsset){
		
		m_textAsset = textAsset;
		if (m_textAsset != null) {
			initParsing ();
			return true;
		} else {
			throw new MissingReferenceException("TextAsset 없음 : " + textAsset);
			return false;
		}
		
	}

	/// <summary>
	/// 연구 가져오기
	/// </summary>
	/// <returns>The research.</returns>
	/// <param name="key">Key.</param>
	public ResearchClass getResearch(string key){
		return m_researchList.Where (research => research.key == key).Select (research => research).SingleOrDefault ();
	}

	private void initParsing(){
		
		Sprite[] sprites = Resources.LoadAll<Sprite> ("Image/Research");

		Debug.Log ("spriteCnt : " + sprites.Length);

//		TextAsset textAsset = (TextAsset)Resources.Load ("Data/Research/Research", typeof(TextAsset));
		string[] splitList = m_textAsset.text.Split (c_lineChar);
		
		//일반 스프라이트
		//m_emptyResearch = sprites.Where(sprite => sprite.name.Equals(s_emptyFacilityKey)).Select(sprite => sprite).SingleOrDefault();
		
		//모든 데이터 구조체에 삽입
		for (int i = 0; i < splitList.Length; i++) {
			
			if(splitList[i] == "")
				continue;
			
			string[] subSplit = splitList[i].Split(c_splitChar);

			Sprite selectSprite = sprites.Where(sprite => sprite.name.Equals(subSplit[0])).Select(sprite => sprite).SingleOrDefault();
			Debug.Log("sprite : " + selectSprite);

			if(selectSprite != null){

				string key = subSplit[0];
				string name = subSplit[1];
				int product = int.Parse(subSplit[2]);
				int cost = int.Parse(subSplit[3]);
				List<string> children = new List<string>();
				for(int j = 0; j < 5; j++){
					if(subSplit[j + 4].Equals("-")){ 
						//Debug.Log("continue");
						continue;
					}
					children.Add(subSplit[j + 4]);
				}
				string contents = subSplit[9];
				m_researchList.Add(new ResearchClass(selectSprite, key, name, contents, product, cost, children.ToArray()));
			}
			else{
				Debug.Log("이미지 없음 : " + subSplit[0]);
			}

		}
	}

}

