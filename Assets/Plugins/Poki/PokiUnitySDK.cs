// =======================================
// Poki SDK Wrapper for Unity WebGL
// Copyright Poki 2020
// =======================================

using UnityEngine;
using System;
using System.Text;
using System.Runtime.InteropServices;

#if POKI_SDK

public class PokiException : System.Exception {
	public PokiException(string message) : base(message){}
}

#endif

public class PokiUnitySDK : MonoBehaviour {
	
	#if POKI_SDK
	
	[DllImport("__Internal")]
	private static extern void JS_PokiSDK_initPokiBridge(string instanceName);
	[DllImport("__Internal")]
	private static extern void JS_PokiSDK_gameLoadingStart();
	[DllImport("__Internal")]
	private static extern void JS_PokiSDK_gameLoadingFinished();
	[DllImport("__Internal")]
	private static extern void JS_PokiSDK_gameLoadingProgress(string data);
	[DllImport("__Internal")]
	private static extern void JS_PokiSDK_roundStart(string indentifier);
	[DllImport("__Internal")]
	private static extern void JS_PokiSDK_roundEnd(string indentifier);
	[DllImport("__Internal")]
	private static extern void JS_PokiSDK_gameInteractive();
	[DllImport("__Internal")]
	private static extern void JS_PokiSDK_customEvent(string noun, string verb, string data);
	[DllImport("__Internal")]
	private static extern void JS_PokiSDK_setPlayerAge(string age);
	[DllImport("__Internal")]
	private static extern void JS_PokiSDK_togglePlayerAdvertisingConsent(string consent);
	[DllImport("__Internal")]
	private static extern void JS_PokiSDK_gameplayStart();
	[DllImport("__Internal")]
	private static extern void JS_PokiSDK_gameplayStop();
	[DllImport("__Internal")]
	private static extern void JS_PokiSDK_commercialBreak();
	[DllImport("__Internal")]
	private static extern void JS_PokiSDK_rewardedBreak();
	[DllImport("__Internal")]
	private static extern void JS_PokiSDK_happyTime(string intensity);
	[DllImport("__Internal")]
	private static extern void JS_PokiSDK_displayAd(string indentifier, string size, string top, string left);
	[DllImport("__Internal")]
	private static extern void JS_PokiSDK_destroyAd(string indentifier);
	[DllImport("__Internal")]
	private static extern void JS_PokiSDK_preInit();
	[DllImport("__Internal")]
	private static extern void JS_PokiSDK_redirect(string destination);

	private static PokiUnitySDK _instance;
	public static PokiUnitySDK Instance {
		get {
			if (_instance == null) {
				_instance = (PokiUnitySDK) FindObjectOfType(typeof(PokiUnitySDK));

				if (FindObjectsOfType(typeof(PokiUnitySDK)).Length > 1) {
					Debug.LogError("[Singleton] Something went really wrong " +
						" - there should never be more than 1 singleton!" +
						" Reopening the scene might fix it.");
					return _instance;
				}

				if (_instance == null) {
					GameObject singleton = new GameObject();
					_instance = singleton.AddComponent<PokiUnitySDK>();
					singleton.name = "(singleton) "+ typeof(PokiUnitySDK).ToString();

					DontDestroyOnLoad(singleton);

					Debug.Log("[Singleton] An instance of " + typeof(PokiUnitySDK) +
						" is needed in the scene, so '" + singleton +
						"' was created with DontDestroyOnLoad.");
				} else {
					Debug.Log("[Singleton] Using instance already created: " +
						_instance.gameObject.name);
				}
			}

			return _instance;
		}
	}

	public PokiUnitySDK () {}

	private bool initialized = false;
	public bool adblocked = false;
	public bool isShowingAd { get; set;}

	public delegate void CommercialBreakDelegate();
 	public CommercialBreakDelegate commercialBreakCallBack;
	public delegate void RewardedBreakDelegate(bool withReward);
 	public RewardedBreakDelegate rewardedBreakCallBack;

	public void init(){
		#if UNITY_EDITOR
		Debug.Log("PokiUnitySDK: Initializing");
		#else
		if (initialized) {
			throw new PokiException ("PokiUnitySDK is already initialized");
		}
		checkInit();
		JS_PokiSDK_initPokiBridge(PokiUnitySDK.Instance.name);
		#endif
	}

	public bool isInitialized(){
		return initialized;
	}

	public bool adsBlocked(){
		return adblocked;
	}

	public void gameLoadingStart(){
		#if UNITY_EDITOR
		Debug.Log("PokiUnitySDK: gameLoadingStart");
		#else
		if (!initialized) {
			throw new PokiException ("PokiUnitySDK is not yet initialized, make sure you call PokiUnitySDK.Instance.Init() first");
		}
		JS_PokiSDK_gameLoadingStart();
		#endif
	}

	public void gameLoadingFinished (){
		#if UNITY_EDITOR
		Debug.Log("PokiUnitySDK: gameLoadingFinished");
		#else
		if (!initialized) {
			throw new PokiException ("PokiUnitySDK is not yet initialized, make sure you call PokiUnitySDK.Instance.Init() first");
		}
		JS_PokiSDK_gameLoadingFinished();
		#endif
	}

	public void roundStart(string indentifier){
		#if UNITY_EDITOR
		Debug.Log("PokiUnitySDK: roundStart");
		#else
		if (!initialized) {
			throw new PokiException ("PokiUnitySDK is not yet initialized, make sure you call PokiUnitySDK.Instance.Init() first");
		}
		JS_PokiSDK_roundStart(indentifier);
		#endif
	}

	public void roundEnd (string indentifier){
		#if UNITY_EDITOR
		Debug.Log("PokiUnitySDK: roundEnd");
		#else
		if (!initialized) {
			throw new PokiException ("PokiUnitySDK is not yet initialized, make sure you call PokiUnitySDK.Instance.Init() first");
		}
		JS_PokiSDK_roundEnd(indentifier);
		#endif
	}

	public void gameInteractive() {
		#if UNITY_EDITOR
		Debug.Log("PokiUnitySDK: gameInteractive");
		#else
		if (!initialized) {
			throw new PokiException ("PokiUnitySDK is not yet initialized, make sure you call PokiUnitySDK.Instance.Init() first");	
		}
		JS_PokiSDK_gameInteractive();
		#endif
	}

	public void customEvent (string eventNoun, string eventVerb, ScriptableObject eventData){
		#if UNITY_EDITOR
		Debug.Log("PokiUnitySDK: roundEnd");
		#else
		if (!initialized) {
			throw new PokiException ("PokiUnitySDK is not yet initialized, make sure you call PokiUnitySDK.Instance.Init() first");
		}
		JS_PokiSDK_customEvent(eventNoun, eventVerb, JsonUtility.ToJson(eventData ,true));
		#endif
	}

	public void setPlayerAge(int age){
		#if UNITY_EDITOR
		Debug.Log("PokiUnitySDK: set player age"+age.ToString());
		#else
		if (!initialized) {
			throw new PokiException ("PokiUnitySDK is not yet initialized, make sure you call PokiUnitySDK.Instance.Init() first");
		}
		JS_PokiSDK_setPlayerAge(age.ToString());
		#endif
	}

	public void togglePlayerAdvertisingConsent(bool consent){
		#if UNITY_EDITOR
		Debug.Log("PokiUnitySDK: set advertising consent"+consent.ToString());
		#else
		if (!initialized) {
			throw new PokiException ("PokiUnitySDK is not yet initialized, make sure you call PokiUnitySDK.Instance.Init() first");
		}
		JS_PokiSDK_togglePlayerAdvertisingConsent(consent.ToString().ToLower());
		#endif
	}

	public class LoadingProgressData : ScriptableObject {
		public float percentageDone;
		public int kbLoaded;
		public int kbTotal;
		public String fileNameLoaded;
		public int filesLoaded;
		public int filesTotal;
	}
	public void gameLoadingProgress(LoadingProgressData data){
		#if UNITY_EDITOR
		Debug.Log("PokiUnitySDK: gameLoadingProgress");
		#else
		if (!initialized) {
			throw new PokiException ("PokiUnitySDK is not yet initialized, make sure you call PokiUnitySDK.Instance.Init() first");
		}
		JS_PokiSDK_gameLoadingProgress(JsonUtility.ToJson(data ,true));
		#endif
	}

	public void gameplayStart() {
		#if UNITY_EDITOR
		Debug.Log("PokiUnitySDK: gameplayStart");
		#else
		if (!initialized) {
			throw new PokiException ("PokiUnitySDK is not yet initialized, make sure you call PokiUnitySDK.Instance.Init() first");
		}
		JS_PokiSDK_gameplayStart();
		#endif
	}

	public void gameplayStop() {
		#if UNITY_EDITOR
		Debug.Log("PokiUnitySDK: gameplayStop");
		#else
		if (!initialized) {
			throw new PokiException ("PokiUnitySDK is not yet initialized, make sure you call PokiUnitySDK.Instance.Init() first");
		}
		JS_PokiSDK_gameplayStop();
		#endif
	}

	public void commercialBreak() {
		isShowingAd = true;
		#if UNITY_EDITOR
		Debug.Log("PokiUnitySDK: commercialBreak");
		commercialBreakCompleted();
		#else
		if(adblocked){
			commercialBreakCompleted();
			return;
		}
		if (!initialized) {
			throw new PokiException ("PokiUnitySDK is not yet initialized, make sure you call PokiUnitySDK.Instance.Init() first");
		}
		JS_PokiSDK_commercialBreak();
		#endif
	}

	public void rewardedBreak(){
		isShowingAd = true;
		#if UNITY_EDITOR
		Debug.Log("PokiUnitySDK: rewardedBreak");
		rewardedBreakCompleted("true");
		#else
		if(adblocked){
			rewardedBreakCompleted("false");
			return;
		}
		if (!initialized) {
			throw new PokiException ("PokiUnitySDK is not yet initialized, make sure you call PokiUnitySDK.Instance.Init() first");
		}
		JS_PokiSDK_rewardedBreak();
		#endif
	}

	public void happyTime(float intensity){
		#if UNITY_EDITOR
		Debug.Log("PokiUnitySDK: happyTime with intensity"+intensity.ToString());
		#else
		if (!initialized) {
			throw new PokiException ("PokiUnitySDK is not yet initialized, make sure you call PokiUnitySDK.Instance.Init() first");
		}
		JS_PokiSDK_happyTime(intensity.ToString());
		#endif
	}

	public void displayAd(string identifier, string size, string top, string left){
		#if UNITY_EDITOR
		Debug.Log("PokiUnitySDK: displayAd with identifier:"+identifier+", size:"+size+" top:"+top+" left:"+left);
		#else

		if (!initialized) {
			throw new PokiException ("PokiUnitySDK is not yet initialized, make sure you call PokiUnitySDK.Instance.Init() first");
		}

		string[] availableSizes = {"970x250", "300x250", "728x90", "160x600", "320x50"};
		bool allowSize = Array.Exists(availableSizes, element => element == size);

		 if(identifier.Length<3){
			throw new PokiException ("PokiUnitySDK identifier must be at least 3 characters");
		 }
		 if(!allowSize){
			throw new PokiException ("PokiUnitySDK displayAd size:"+size+" is currently not supported");
		 }
		if((top.Contains("%") || top.Contains("px")) == false || (left.Contains("%") || left.Contains("px")) == false){
			throw new PokiException ("PokiUnitySDK displayAd unsupported top or left syntax, please use format '10px' or '10%'");
		}

		JS_PokiSDK_displayAd(identifier, size, top, left);
		#endif
	}

	public void destroyAd(string identifier){
		#if UNITY_EDITOR
		Debug.Log("PokiUnitySDK: destroyAd with identifier:"+identifier);
		#else
		if (!initialized) {
			throw new PokiException ("PokiUnitySDK is not yet initialized, make sure you call PokiUnitySDK.Instance.Init() first");
		}
		JS_PokiSDK_destroyAd(identifier);
		#endif
	}

	public void checkInit(){
		if(Application.isEditor) return;
		string[] hosts = {"bG9jYWxob3N0", "LnBva2kuY29t", "LnBva2ktZ2RuLmNvbQ=="};
		//// localhost, .poki.com, .poki-gdn.com 
		// we will only run on subdomains of Poki or localhost

		try{

			Uri myUri = new Uri(Application.absoluteURL);
			string liveHost = myUri.Host;

			bool allowed = false;
			for(int i = 0; i<hosts.Length; i++){
				byte[] decodedBytes = Convert.FromBase64String (hosts[i]);
				string host = Encoding.UTF8.GetString (decodedBytes);

				if(i == 0 && !liveHost.StartsWith(host)) JS_PokiSDK_preInit(); // 0 must be localhost

				if(liveHost.EndsWith(host)){
					allowed = true;
					break;
				}
			}
			if(!allowed){
				string targetURL = "aHR0cHM6Ly9wb2tpLmNvbS9zaXRlbG9jaw==";
				byte[] decodedBytes = Convert.FromBase64String (targetURL);
				string url = Encoding.UTF8.GetString (decodedBytes);
				JS_PokiSDK_redirect(url);
			}
		}
		catch{
			//Application is running in the editor
		}
	}

	public void ready(){
		initialized = true;
	}

	public void adblock(){
		initialized = true;
		adblocked = true;
	}

	public void commercialBreakCompleted(){
		isShowingAd = false;
		#if UNITY_EDITOR
		Debug.Log("PokiUnitySDK: commercialBreak completed");
		#else
		commercialBreakCallBack();
		#endif
	}

	public void rewardedBreakCompleted(string withReward){
		isShowingAd = false;
		#if UNITY_EDITOR
		Debug.Log("PokiUnitySDK: rewardedBreak completed, received reward:"+withReward);
		#else
		rewardedBreakCallBack((withReward == "true"));
		#endif
	}
	
	#endif
}
