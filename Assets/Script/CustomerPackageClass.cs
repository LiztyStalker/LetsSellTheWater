//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.34209
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
// </auto-generated>
//------------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CustomerPackageClass : SingletonClass<CustomerPackageClass>
{
	const char c_lineChar = '\n';
	const char c_splitChar = '\t';

//	List<CustomerClass> m_customerList = new List<CustomerClass> ();
	Dictionary<int, List<CustomerClass>> m_customerDic = new Dictionary<int, List<CustomerClass>>();
	TextAsset m_textAsset;
//	List<CustomerClass> m_bufferCustomerList = new List<CustomerClass>();
//	int bufferIndex = 0;

	public CustomerPackageClass(){
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

	private void initParsing(){
		
		Sprite[] sprites = Resources.LoadAll<Sprite> ("Image/Customer");
		
//		TextAsset textAsset = (TextAsset)Resources.Load ("Data/Customer/Customer", typeof(TextAsset));
		string[] splitList = m_textAsset.text.Split (c_lineChar);
		
		//모든 데이터 구조체에 삽입
		for (int i = 0; i < splitList.Length; i++) {
			
			if(splitList[i] == "")
				continue;
			
			string[] subSplit = splitList[i].Split(c_splitChar);
			
			Sprite selectSprite = sprites.Where(sprite => sprite.name.Equals(subSplit[0])).Select(sprite => sprite).SingleOrDefault();
			
			if(selectSprite != null){
				
				TYPE_CUSTOMER typeCustomer = (TYPE_CUSTOMER)int.Parse(subSplit[0].Substring(2, 2));
				int volume = int.Parse(subSplit[2]);
				int count = int.Parse(subSplit[3]);

				if(!m_customerDic.ContainsKey((int)typeCustomer)){
					m_customerDic.Add((int)typeCustomer, new List<CustomerClass>());
				}

				m_customerDic[(int)typeCustomer].Add(new CustomerClass(selectSprite, subSplit[0], subSplit[1], typeCustomer, volume, count));
				Debug.Log("소비자 : " + selectSprite);
			}
			else{
				Debug.Log("데이터 없음 : " + subSplit[0]);
			}
		}

	}

	/// <summary>
	/// 소비자 랜덤 가져오기
	/// </summary>
	/// <returns>The customer.</returns>
//	public CustomerClass randomCustomer(){
//		return new CustomerClass(m_customerList [Random.Range (0, m_customerList.Count)]);
//	}

	/// <summary>
	/// 소비자에 따른 랜덤 가져오기
	/// </summary>
	/// <returns>The random customer.</returns>
	/// <param name="index">Index.</param>
	public CustomerClass getRandomCustomer(int index){
		return new CustomerClass (getCustomer(index));
	}

//	public CustomerClass getCustomer(string key){
//
//	}

	/// <summary>
	/// 해당하는 고객에 대한 랜덤 고객 가져오기
	/// </summary>
	/// <returns>The customer.</returns>
//	/// <param name="index">Index.</param>
	private CustomerClass getCustomer(int index){
		if (m_customerDic[index].Count > 0) {
			return m_customerDic[index][Random.Range(0, m_customerDic[index].Count)];
		}
		return null;
	}


	/// <summary>
	/// 소비자 비율에 따른 랜덤 가져오기
	/// </summary>
	/// <returns>The customer.</returns>
//	public CustomerClass randomCustomer(){
//		return m_customerList [Random.Range (0, m_customerList.Count)];
//	}




}

