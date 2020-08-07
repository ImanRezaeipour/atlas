

$(document).ready
        (
            function () {                               
                var OpenWithLogin = document.getElementById('hfOpenWithLoginPage_PasswordChange').value;
                if (OpenWithLogin == '')
                    parent.DialogLoading.Close();
                OpenPasswordChangeWithLoginPage_PasswordChange(OpenWithLogin);
                document.body.dir = document.PasswordChangeForm.dir;
                SetWrapper_Alert_Box(document.PasswordChangeForm.id);
                GetCurrentUser_PasswordChange();
            }
        );
