mergeInto(LibraryManager.library,
{
	InitCloudStorage_js: function()
	{
		var returnStr = cloudSaves;
		var bufferSize = lengthBytesUTF8(returnStr) + 1;
		var buffer = _malloc(bufferSize);
		stringToUTF8(returnStr, buffer, bufferSize);
		return buffer;
	},	

    SaveYG: function (jsonTechnicalData, jsonPlayerData, flush) {
        SaveCloud(UTF8ToString(jsonTechnicalData), UTF8ToString(jsonPlayerData), flush);
    },
    
    SaveYGTechnicalData: function (jsonTechnicalData, flush) {
        SaveCloudTechnicalData(UTF8ToString(jsonTechnicalData), flush);
    },
    
    LoadYGPlayerData: function (sendback) {
        LoadCloudPlayerData(sendback);
    },
    
    LoadYG: function (sendback)	{
        LoadCloud(sendback);
    },   
});