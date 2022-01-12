mergeInto(LibraryManager.library, {
  InitPurchases: function() {
    initPayments();
  },

  Purchase: function(id) {
    buy(id);
  },

  AuthenticateUser: function() {
    auth();
  },

  GetUserData: function() {
    getUserData();
  },

  ShowFullscreenAd: function () {
    showFullscreenAd();
  },

  ShowRewardedAd: function(placement) {
    showRewardedAd(placement);
    return placement;
  },

  OpenWindow: function(link){
    var url = UTF8ToString(link);
      document.onmouseup = function()
      {
        window.open(url);
        document.onmouseup = null;
      }
  },

  ShowInviteBox: function () {
    showInviteBox();
  },

  ShowWallPostBox: function(message, attachments) {
    var m = UTF8ToString(message);
    var a = UTF8ToString(attachments);
    showWallPostBox(m, a);
  },

  GetLang: function() {
    var returnStr = getLang();
    var bufferSize = lengthBytesUTF8(returnStr) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(returnStr, buffer, bufferSize);
    return buffer;
  },
  
  GetDeviceType: function() {
	var returnStr = getDeviceType();
    var bufferSize = lengthBytesUTF8(returnStr) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(returnStr, buffer, bufferSize);
    return buffer;
  }
});