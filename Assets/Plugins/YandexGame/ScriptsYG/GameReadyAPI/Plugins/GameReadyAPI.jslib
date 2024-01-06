mergeInto(LibraryManager.library,
{
	GameReadyAPI_js: function() {
		if (ysdk !== null && ysdk.features.LoadingAPI !== undefined && ysdk.features.LoadingAPI !== null) {
			ysdk.features.LoadingAPI.ready();
			console.log('[GameReadyAPI] -> Game Ready');
		}
		else{
			console.error('Failed - Game Ready');
		}
	}
});