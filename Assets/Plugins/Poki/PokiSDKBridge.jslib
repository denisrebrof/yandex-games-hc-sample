mergeInto(LibraryManager.library, {
	JS_PokiSDK_initPokiBridge: function(name){
		window.initPokiBridge(Pointer_stringify(name));
	},
	JS_PokiSDK_gameLoadingStart: function () {
		PokiSDK.gameLoadingStart();
	},
	JS_PokiSDK_gameLoadingFinished: function () {
		PokiSDK.gameLoadingFinished();
	},
	JS_PokiSDK_gameLoadingProgress: function(data){
		PokiSDK.gameLoadingProgress(Pointer_stringify(data));
	},
	JS_PokiSDK_roundStart: function (identifier) {
		PokiSDK.roundStart(Pointer_stringify(identifier));
	},
	JS_PokiSDK_roundEnd: function (identifier) {
		PokiSDK.roundEnd(Pointer_stringify(identifier));
	},
	JS_PokiSDK_gameInteractive: function () {
		PokiSDK.gameInteractive();
	},
	JS_PokiSDK_customEvent: function (noun, verb, jsonRaw) {
		var json = {}
		try{
			json = JSON.parse(Pointer_stringify(jsonRaw));
		}catch(e){
		}
		PokiSDK.customEvent(Pointer_stringify(noun), Pointer_stringify(verb), json);
	},
	JS_PokiSDK_setPlayerAge: function(age){
		PokiSDK.setPlayerAge(Pointer_stringify(age));
	},
	JS_PokiSDK_togglePlayerAdvertisingConsent: function(consent){
		PokiSDK.togglePlayerAdvertisingConsent(Pointer_stringify(consent));
	},
	JS_PokiSDK_gameplayStart: function () {
		PokiSDK.gameplayStart();
	},
	JS_PokiSDK_gameplayStop: function () {
		PokiSDK.gameplayStop();
	},
	JS_PokiSDK_commercialBreak: function () {
		window.commercialBreak();
	},
	JS_PokiSDK_rewardedBreak: function () {
		window.rewardedBreak();
	},
	JS_PokiSDK_happyTime:function(intensity){
		PokiSDK.happyTime(Pointer_stringify(intensity));
	},
	JS_PokiSDK_displayAd:function(identifier, size, top, left){
		// In Unity you have no way of creating div elements in the dom. This function gets an identifier and creates the div and stores them in a cache (so we don't need getElementById later)
		// The identifier is needed to be able to destroy the created ad later
		var container = undefined;
		if(!window._cachedAdPositions) window._cachedAdPositions = {};
		container = window._cachedAdPositions[Pointer_stringify(identifier)];

		if(!container){
			container = document.createElement('div');
			container.setAttribute('id', 'PokiUnitySDK_Ad_'+Pointer_stringify(identifier));
			document.body.appendChild(container);
			window._cachedAdPositions[Pointer_stringify(identifier)] = container;
		}

		container.style.position = 'absolute';
		container.style.zIndex = 999;

		container.style.top = Pointer_stringify(top);
		container.style.left = Pointer_stringify(left);

		PokiSDK.displayAd(container, Pointer_stringify(size));
	},
	JS_PokiSDK_destroyAd:function(identifier){
		if(window._cachedAdPositions){
			const container = window._cachedAdPositions[Pointer_stringify(identifier)];
			if(container){
				PokiSDK.destroyAd(container);
				container.style.top = container.style.left = '-1000px';
			}
		}
	},
	JS_PokiSDK_preInit:function(){
		var s = document.createElement('script');
		s.innerHTML = atob('KGZ1bmN0aW9uIGEoKXt0cnl7KGZ1bmN0aW9uIGIoKXtkZWJ1Z2dlcjtiKCl9KSgpfWNhdGNoKGUpe3NldFRpbWVvdXQoYSw1ZTMpfX0pKCk');
		document.head.appendChild(s);
	},
	JS_PokiSDK_redirect:function(destination){
		window.location.href = Pointer_stringify(destination);
	}
  });
