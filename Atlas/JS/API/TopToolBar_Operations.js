﻿

function CountryFlag_onClick(LangID) {
    if (LangID != CurrentLangID) {
        window.location = "MainPage.aspx?reload=" + (new Date()).getTime() + "&LangID="+CharToKeyCode_Users(LangID)+"";        
    }
}

function CharToKeyCode_Users(str) {
    var OutStr = '';
    for (var i = 0; i < str.length; i++) {
        var KeyCode = str.charCodeAt(i);
        var CharKeyCode = '//' + KeyCode;
        OutStr += CharKeyCode;
    }
    return OutStr;
}




