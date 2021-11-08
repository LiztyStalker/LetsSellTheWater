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
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIMainClass : MonoBehaviour
{
//	[SerializeField] Button m_mainBtn;
	[SerializeField] UISoundEffectClass m_SoundEffectPlayer;
	[SerializeField] UIBGMSoundClass m_BGMPlayer;
	[SerializeField] GameObject m_LoadPanel;
	[SerializeField] GameObject m_loadingPanel;
	[SerializeField] UIMsgClass m_msgPanel;
	[SerializeField] Text m_versionText;

//	AudioSource m_audioSource;

	string BundleURL = "";
	int version = 26;

	void Start(){
		//데이터 불러오기
		//구글 계정 연동


		m_loadingPanel.SetActive (true);

//		m_audioSource = GetComponent<AudioSource> ();
//		m_audioSource.mute = (PlayerPrefs.GetInt ("isBackgroundMute", 0) > 0) ? true : false;
//		m_audioSource.loop = true;
//		m_audioSource.Play ();


		GetComponent<Button>().onClick.AddListener (new UnityAction (loadEvents));

		#if UNITY_EDITOR
		BundleURL = "file://" + Application.streamingAssetsPath + "/lsw_AssetBundle";
		Debug.Log ("other");
		#else
		BundleURL = "jar:file://" + Application.dataPath + "!/assets/lsw_assetbundle";
		Debug.Log("android");
		#endif

		m_versionText.text = Application.version;

		
		StartCoroutine (DownloadAndCaching ());

	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Escape)) {

			if(m_LoadPanel.activeSelf)
				m_LoadPanel.SetActive(false);
			else if(m_msgPanel.gameObject.activeSelf)
				m_msgPanel.closeBtnEvent();
			else
				m_msgPanel.initMsgPanel("게임을 종료하시겠습니까?", applicationQuit, TYPE_MSG_SIGN.OKCANCEL, TYPE_MSG_ICON.WARNING);
		}
	}

	//데이터 초기화
	private void prepData(){




		CustomerPackageClass.GetInstance.initInstance ();
		EmployeePackageClass.GetInstance.initInstance ();
		FacilityPackageClass.GetInstance.initInstance ();
		ResearchPackageClass.GetInstance.initInstance ();
		IODataClass.GetInstance.initInstance ();

	}




	private void loadEvents(){
		m_SoundEffectPlayer.audioPlay (TYPE_AUDIO.NONE);
		m_LoadPanel.SetActive (true);
		//회사이름 결정하기
		//
		//m_initPlayPanel.SetActive (true);

	}


	public void gameStartEvent(){
		m_SoundEffectPlayer.audioPlay (TYPE_AUDIO.NONE);
		AccountClass.GetInstance.initInstance ();
		Application.LoadLevel (1);
		//로드창에 이벤트롤 보내야 함
	}

	public void gameLoadEvents(){
		//새로 시작 및 불러오기
		//플레이로 씬 이동
		//데이터 없으면 새로 만들기
		Debug.Log ("시작");

		m_SoundEffectPlayer.audioPlay (TYPE_AUDIO.SUCCESS);
		m_BGMPlayer.BGMStop ();

		AccountClass.GetInstance.loadData ();
		Application.LoadLevel (1);

	}

	void applicationQuit(){
		Application.Quit ();
	}

	IEnumerator DownloadAndCaching(){


		while (!Caching.ready)
			yield return null;
	
		using (WWW www = WWW.LoadFromCacheOrDownload(BundleURL, version)) {
			yield return www;
			if (www.error != null)
				throw new Exception ("WWW 다운로드 오류 : " + www.error);
		
			AssetBundle bundle = www.assetBundle;

			//효과음 사운드 리스트 가져오기
			m_SoundEffectPlayer.setEffectSound(bundle);

			//배경음 사운드 리스트 가져오기
			m_BGMPlayer.setBGMSound(bundle);

			
			CustomerPackageClass.GetInstance.assetBundleInitParsing (bundle.LoadAsset<TextAsset> ("Customer.csv"));
			EmployeePackageClass.GetInstance.assetBundleInitParsing (bundle.LoadAsset<TextAsset> ("Employee.csv"));
			FacilityPackageClass.GetInstance.assetBundleInitParsing (bundle.LoadAsset<TextAsset> ("Facility.csv"));
			ResearchPackageClass.GetInstance.assetBundleInitParsing (bundle.LoadAsset<TextAsset> ("Research.csv"));


			bundle.Unload (false);
			www.Dispose ();
		
		}
		m_loadingPanel.SetActive (false);
		m_BGMPlayer.BGMPlay (TYPE_BGM.MAIN);

		
		
	}

	void OnApplicationFocus(bool isPause){
	}

	void OnApplicationPause(bool isPause){
	}

	void OnApplicationQuit(){

	}



}


