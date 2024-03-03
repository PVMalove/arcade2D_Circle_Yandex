var cloudSaves = 'noData';

    async function SaveCloud(jsonTechnicalData, jsonPlayerData, flush) {
        if (player == null) {
            console.error('[INDEX] -> CRASH Save Cloud: ', 'Didnt have time to load');
            return;
        }
        try {
            await player.setData({
                technicalData: [jsonTechnicalData],
                playerData: [jsonPlayerData],
            }, flush);
            console.log('[INDEX] -> Save cloud data is set');
        } catch (e) {
            console.error('[INDEX] -> CRASH Save Cloud: ', e.message);
        }
    }

    async function SaveCloudTechnicalData(jsonTechnicalData, flush) {
        let jsonPlayerData;
        if (player == null) {
            console.error('[INDEX] -> CRASH Save Cloud: ', 'Didnt have time to load');
            return;
        }
        try {
            const player = await ysdk.getPlayer({ scopes: false });
            const data = await player.getData(["playerData"]);
            jsonPlayerData = data.playerData[0];
            console.log('[INDEX] -> Load player current state data server: ', jsonPlayerData);
        } catch (e) {
            console.error('[INDEX] -> CRASH Load saves Cloud: ', e.message);
        }
        SaveCloud(jsonTechnicalData, jsonPlayerData, flush);
    }

    async function LoadCloudPlayerData(sendback) {
        if (ysdk === null) {
            if (sendback) {
                myGameInstance.SendMessage('YandexGame', 'SetLoadSaves', 'noData');
            }
            return 'noData';
        }
        try {
            const player = await ysdk.getPlayer({ scopes: false });
            const data = await player.getData(["playerData"]);
            if (data.playerData[0].trim() !== "") {
                if (sendback) {
                    myGameInstance.SendMessage('YandexGame', 'SetLoadSavesPlayerDataAsync', JSON.stringify(data.playerData));
                }
                return JSON.stringify(data.playerData);                
            } else {
                if (sendback) {
                    myGameInstance.SendMessage('YandexGame', 'SetLoadSavesPlayerDataAsync', 'noData');
                }
                return 'noData';
            }
        } catch (e) {
            console.error('[INDEX] -> CRASH Load saves Cloud: ', e.message);
            if (sendback) {
                myGameInstance.SendMessage('YandexGame', 'SetLoadSavesPlayerDataAsync', 'noData');
            }
            return 'noData';
        }
    }

    async function LoadCloud(sendback) {
        if (ysdk === null) {
            if (sendback) {
                myGameInstance.SendMessage('YandexGame', 'SetLoadSaves', 'noData');
            }
            return 'noData';
        }
        try {
            const player = await ysdk.getPlayer({ scopes: false });
            const data = await player.getData(["technicalData"]);
            if (data.technicalData[0].trim() !== "") {
                console.log('[INDEX] -> Technical data server: ', data.technicalData);
                if (sendback) {
                    myGameInstance.SendMessage('YandexGame', 'SetLoadSaves', JSON.stringify(data.technicalData));
                }
                return JSON.stringify(data.technicalData);
            } else {
                if (sendback) {
                    console.log('[INDEX] -> Technical data server: noData');
                    myGameInstance.SendMessage('YandexGame', 'SetLoadSaves', 'noData');
                }
                return 'noData';
            }
        } catch (e) {
            console.error('[INDEX] -> CRASH Load saves Cloud: ', e.message);
            if (sendback) {
                myGameInstance.SendMessage('YandexGame', 'SetLoadSaves', 'noData');
            }
            return 'noData';
        }
    }    
