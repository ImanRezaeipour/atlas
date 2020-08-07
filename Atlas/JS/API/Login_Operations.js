
function initControls_Login() {
    try {
        if (document.getElementById('theLogincontrol_UserName') != null) {
            document.getElementById('theLogincontrol_UserName').focus();
            document.getElementById('theLogincontrol_UserName').onclick = function () {
                this.select();
            };
        }
        if (document.getElementById('theLogincontrol_UserName') != null) {
            document.getElementById('theLogincontrol_UserName').onfocus = function () {
                this.select();
            };
        }
        if (document.getElementById('theLogincontrol_Password') != null) {
            document.getElementById('theLogincontrol_Password').onclick = function () {
                this.select();
            };
        }
        if (document.getElementById('theLogincontrol_Password') != null) {
            document.getElementById('theLogincontrol_Password').onfocus = function () {
                this.select();
            };
        }
    } catch (e) {

    }
}

function ShowKeyboard() {
    VKI_show(document.getElementById('theLogincontrol_Password'));
}

function CheckReferer() {
    if ((this.location.search != '' && this.location.search.indexOf('.aspx') >= 0 && this.location.search.indexOf('MainPage.aspx') < 0) || (this.location != '' && this.location.pathname.indexOf('.aspx') >= 0 && this.location.pathname.indexOf('MainPage.aspx')  < 0)) {
        if (parent.window == this.window)
            return;
        var parentWindow = parent.window;
        while (true) {
            if (parentWindow.document.getElementById('MainForm') != null) {
                parentWindow.location = 'Login.aspx';
                break;
            }
            else
                parentWindow = parentWindow.parent;
        };
    }    
}

function RefreshSecurityImage() {
    document.getElementById('imgSecurityImageViewer').src = 'SecurityImageViewer.aspx?reload=' + (new Date()).getTime() + '&Refresh=true';
}

function EncryptData_Login(){
    var keyHelper = '$$$###$$$!@#$%^&*()';
    if (!(document.getElementById("Password").value.toString().substring(0, keyHelper.length)==keyHelper)) {
        var iv = CryptoJS.enc.Utf8.parse('1234567891234567');
        var key = CryptoJS.enc.Utf8.parse("www.ghadirco.net");
        var password = document.getElementById("Password").value;
        var encryptedPassword = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(password), key, {
            keySize: 128 / 8,
            iv: iv,
            mode: CryptoJS.mode.CBC,
            padding: CryptoJS.pad.Pkcs7
        });
        document.getElementById("Password").value = keyHelper + encryptedPassword;
    }    
}

function Login_OnKeyPress(event) {
   var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
   if (keyCode == 13) {
        EncryptData_Login();                
        WebForm_DoPostBackWithOptions(new WebForm_PostBackOptions("theLogincontrol$Login", "", true, "", "", false, true));       
    }
} 

