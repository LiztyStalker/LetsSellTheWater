using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class UIAdbClass : MonoBehaviour {


	BannerView bannerView = null; //배너
	InterstitialAd interstitial = null; //전면광고


	//닫음 버튼 선택
	public void adCloseEvent(object sender, System.EventArgs args){
		Debug.Log ("ad Close");
		AdRequest.Builder builder = new AdRequest.Builder ();
		AdRequest request = builder.AddTestDevice (AdRequest.TestDeviceSimulator).AddTestDevice ("").Build ();
		interstitial.LoadAd (request); //전면 광고 새로 요청
	}


	void OnGUI(){
		//버튼 선택시 전면 광고 출력
		//interstitial.Show ();
	}

	// Use this for initialization
	void Start () {
		interstitial = new InterstitialAd ("ca-app-pub-1380020138279880/7658466857");
		interstitial.AdClosed += adCloseEvent;

		bannerView = new BannerView ("ca-app-pub-1380020138279880/3228267257", AdSize.SmartBanner, AdPosition.Bottom);

		//서버광고 요청
		AdRequest.Builder builder = new AdRequest.Builder ();

		//테스트 디바이스 등록 - 테스트 디바이스는 결제가 안됨
		AdRequest request = builder.AddTestDevice (AdRequest.TestDeviceSimulator).AddTestDevice ("").Build ();

		interstitial.LoadAd (request);

		bannerView.LoadAd (request);
		bannerView.Show ();

	}

	void OnDisable(){
//		Destroy (bannerView);
//		Destroy (interstitial);
	}



}
