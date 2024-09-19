BazaarPrintCallBack = {};
BazaarPrintCallBack.DotNetObject = null;

BazaarPrintCallBack.initJsCallBack = function (dotNetObject) {
    BazaarPrintCallBack.DotNetObject = dotNetObject;
};

BazaarPrintCallBack.startPrint = () => {
    if (BazaarPrintCallBack.DotNetObject) {
        BazaarPrintCallBack.DotNetObject.invokeMethodAsync('startPrint', "Start Print now...");
    }
};

BazaarPrintCallBack.finishedPrint = () => {
    if (BazaarPrintCallBack.DotNetObject) {
        BazaarPrintCallBack.DotNetObject.invokeMethodAsync('finishedPrint', "Closed Print now...");
    }
};

console.log('Print_Eng_Initialized!');
console.log('BazaarPrintCallBack' + BazaarPrintCallBack);