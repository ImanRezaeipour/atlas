﻿
var CurrentPageIndex_cmbPersonnel_Users = 0;
var CurrentPageCombosCallBcakStateObj = new Object();
var LoadState_cmbPersonnel_Users = 'Normal';
var box_UserSearch_Users_IsShown = false;
var box_SearchByPersonnel_Users_IsShown = false;
var CurrentPageState_Users = 'View';
var LoadState_Users = 'Normal';
var CurrentPageIndex_GridUsers_Users = 0;
var SelectedActiveDirectoryUser_Users = null;
var SelectedPersonnel_Users = null;
var SelectedRole_Users = null;
var ConfirmState_Users = null;
var ObjUser_Users = null;
var AdvancedSearchTerm_cmbPersonnel_Users = '';
var ObjexpandingDataAccessLevelNode_Users = null;

function GetBoxesHeaders_Users() {
    document.getElementById('clmnBarCode_cmbPersonnel_Users').innerHTML = document.getElementById('hfclmnBarCode_cmbPersonnel_Users').value;
    document.getElementById('clmnName_cmbPersonnel_Users').innerHTML = document.getElementById('hfclmnName_cmbPersonnel_Users').value;
    document.getElementById('clmnCardNum_cmbPersonnel_Users').innerHTML = document.getElementById('hfclmnCardNum_cmbPersonnel_Users').value;
    document.getElementById('header_UsersBox_Users').innerHTML = document.getElementById('hfheader_UsersBox_Users').value;
    document.getElementById('header_UserDetailBox_Users').innerHTML = document.getElementById('hfheader_UserDetailBox_Users').value;
    document.getElementById('header_SearchBox_Users').innerHTML = document.getElementById('hfheader_SearchBox_Users').value;
    document.getElementById('header_UserSearchBox_Users').innerHTML = document.getElementById('hfheader_UserSearchBox_Users').value;
    document.getElementById('header_SearchByPersonnelBox_Users').innerHTML = document.getElementById('hfheader_SearchByPersonnelBox_Users').value;
    document.getElementById('footer_GridUsers_Users').innerHTML = document.getElementById('hffooter_GridUsers_Users').value;
    //parent.document.getElementById('Title_DialogActiveDirectory').InnerHTML = document.getElementById('hfTitle_DialogActiveDirectory').value;
}

function ChangeEnabled_DropDownDives_Users(state) {
    ChangeEnabled_DropDownDive_Users('imgbox_UserSearch_Users', state);
    ChangeEnabled_DropDownDive_Users('imgbox_SearchByPersonnel_Users', state);
}

function ChangeEnabled_DropDownDive_Users(dropdownDive, state) {
    switch (state) {
        case 'enabled':
            switch (dropdownDive) {
                case 'imgbox_UserSearch_Users':
                    document.getElementById(dropdownDive).onclick = function () {
                        ShowHide_UserSearchBox_Users();
                    };
                    break;
                case 'imgbox_SearchByPersonnel_Users':
                    document.getElementById(dropdownDive).onclick = function () {
                        ShowHide_SearchByPersonnelBox_Users();
                    };
                    break;
            }
            document.getElementById(dropdownDive).src = 'Images/Ghadir/arrowDown.jpg';
            break;
        case 'disabled':
            document.getElementById(dropdownDive).onclick = '';
            document.getElementById(dropdownDive).src = 'Images/Ghadir/arrowDown_silver.jpg';
            break;
    }
}

function SetHorizontalScrollingDirection_GridUsers_Users_Opera() {
    if (navigator.userAgent.indexOf('Opera') != -1 && parent.CurrentLangID == "fa-IR")
        document.getElementById('GridUsers_Users').style.direction = "ltr";
}

function SetPosition_dropdowndive_Users() {
    if (navigator.userAgent.indexOf('Opera') != -1) {
        var Lag = 50;
        switch (parent.CurrentLangID) {
            case 'en-US':
                document.getElementById('box_UserSearch_Users').style.right = Lag;
                document.getElementById('box_SearchByPersonnel_Users').style.right = Lag;
                break;
            case 'fa-IR':
                document.getElementById('box_UserSearch_Users').style.left = Lag;
                document.getElementById('box_SearchByPersonnel_Users').style.left = Lag;
                break;
        }
    }
}

function CharToKeyCode_Users(str) {
    var OutStr = '';
    if (str != null && str != undefined) {
        for (var i = 0; i < str.length; i++) {
            var KeyCode = str.charCodeAt(i);
            var CharKeyCode = '//' + KeyCode;
            OutStr += CharKeyCode;
        }
    }
    return OutStr;
}

function ShowDialogPersonnelSearch(state) {
    var ObjDialogPersonnelSearch = new Object();
    ObjDialogPersonnelSearch.Caller = state;
    parent.DialogPersonnelSearch.set_value(ObjDialogPersonnelSearch);
    parent.DialogPersonnelSearch.Show();
    CollapseControls_Users();
}

function ShowHide_SearchByPersonnelBox_Users() {
    imgbox_SearchByPersonnel_Users_onClick();
}

function Users_onCancel() {
    ChangePageState_Users('View');
    ClearUserList_Users();
}

function Users_onSave() {
    if (CurrentPageState_Users != 'Delete')
        UpdateUsers_Users();
    else
        ShowDialogConfirm('Delete');
}

function NavigateUsers_Users(selectedUserItem) {
    if (selectedUserItem != undefined) {
        document.getElementById('chbActiveUser_Users').checked = selectedUserItem.getMember('Active').get_value();
        SelectedPersonnel_Users = new Object();
        SelectedPersonnel_Users.ID = selectedUserItem.getMember('PersonID').get_text();
        SelectedPersonnel_Users.Name = selectedUserItem.getMember('PersonName').get_text();
        SelectedPersonnel_Users.BarCode = selectedUserItem.getMember('PersonCode').get_text();
        SelectedRole_Users = new Object();
        SelectedRole_Users.ID = selectedUserItem.getMember('RoleID').get_text();
        SelectedRole_Users.Name = selectedUserItem.getMember('RoleName').get_text();
        document.getElementById('cmbUserRole_Users_Input').value = SelectedRole_Users.Name = selectedUserItem.getMember('RoleName').get_text();
        var IsActiveDirectoryAuthenticated = selectedUserItem.getMember('ActiveDirectoryAuthenticate').get_value();
        if (IsActiveDirectoryAuthenticated != null && IsActiveDirectoryAuthenticated != undefined && IsActiveDirectoryAuthenticated != '' && IsActiveDirectoryAuthenticated != false) {
            SelectedActiveDirectoryUser_Users = new Object();
            SelectedActiveDirectoryUser_Users.DomainID = selectedUserItem.getMember('TheDoaminId').get_text();
            document.getElementById('cmbDomainName_Users_Input').value = SelectedActiveDirectoryUser_Users.DomainName = selectedUserItem.getMember('TheDoaminName').get_text();
            document.getElementById('cmbDomainUserName_Users_Input').value = SelectedActiveDirectoryUser_Users.UserName = selectedUserItem.getMember('UserName').get_text();
            document.getElementById('rdbActiveDirValidation_Users').checked = true;
            document.getElementById('txtUserName_Users').value = '';
            document.getElementById('txtPassword_Users').value = document.getElementById('txtPasswordRepeat_Users').value = '';
            if (CurrentPageState_Users == 'Edit')
                ChangeItemsEnabled_onCheckChange('ActiveDirValidation');
        }
        else {
            document.getElementById('txtUserName_Users').value = selectedUserItem.getMember('UserName').get_text();
            document.getElementById('txtPassword_Users').value = document.getElementById('txtPasswordRepeat_Users').value = selectedUserItem.getMember('Password').get_text();
            document.getElementById('rdbUserNameIntroduction_Users').checked = true;
            document.getElementById('cmbDomainName_Users_Input').value = '';
            document.getElementById('cmbDomainUserName_Users_Input').value = '';
            SelectedActiveDirectoryUser_Users = null;
            if (CurrentPageState_Users == 'Edit')
                ChangeItemsEnabled_onCheckChange('UserNameIntroduction');
        }
    }
}

function UpdateUsers_Users() {
    ObjUser_Users = new Object();
    ObjUser_Users.ID = '0';
    ObjUser_Users.IsActive = false;
    ObjUser_Users.PersonnelID = '0';
    ObjUser_Users.PersonnelName = null;
    ObjUser_Users.PersonnelBarCode = null;
    ObjUser_Users.RoleID = '0';
    ObjUser_Users.RoleName = null;
    ObjUser_Users.UserName = null;
    ObjUser_Users.Password = null;
    ObjUser_Users.ConfirmPassword = null;
    ObjUser_Users.IsActiveDirectoryAuthenticate = false;
    ObjUser_Users.DomainID = '0';
    ObjUser_Users.IsPasswordChange = false;

    var SelectedItems_GridUsers_Users = GridUsers_Users.getSelectedItems();
    if (SelectedItems_GridUsers_Users.length > 0)
        ObjUser_Users.ID = SelectedItems_GridUsers_Users[0].getMember("ID").get_text();

    if (CurrentPageState_Users != 'Delete') {
        ObjUser_Users.IsActive = document.getElementById('chbActiveUser_Users').checked;
        if (cmbPersonnel_Users.getSelectedItem() != undefined) {
            ObjUser_Users.PersonnelID = cmbPersonnel_Users.getSelectedItem().get_value();
            ObjUser_Users.PersonnelName = cmbPersonnel_Users.getSelectedItem().get_text();
            ObjUser_Users.PersonnelBarCode = cmbPersonnel_Users.getSelectedItem().BarCode;
        }
        else {
            if (SelectedPersonnel_Users != null) {
                ObjUser_Users.PersonnelID = SelectedPersonnel_Users.ID;
                ObjUser_Users.PersonnelName = SelectedPersonnel_Users.Name;
                ObjUser_Users.PersonnelBarCode = SelectedPersonnel_Users.BarCode;
            }
        }
        if (trvUserRole_cmbUserRole_Users.get_selectedNode() != undefined) {
            if (trvUserRole_cmbUserRole_Users.get_selectedNode().get_parentNode() != undefined)
                ObjUser_Users.RoleID = trvUserRole_cmbUserRole_Users.get_selectedNode().get_id();
            ObjUser_Users.RoleName = trvUserRole_cmbUserRole_Users.get_selectedNode().get_text();
        }
        else {
            if (SelectedRole_Users != undefined) {
                ObjUser_Users.RoleID = SelectedRole_Users.ID;
                ObjUser_Users.RoleName = SelectedRole_Users.Name;
            }
        }
        ObjUser_Users.IsActiveDirectoryAuthenticate = document.getElementById('rdbActiveDirValidation_Users').checked;
        if (ObjUser_Users.IsActiveDirectoryAuthenticate) {
            if (cmbDomainName_Users.getSelectedItem() != undefined)
                ObjUser_Users.DomainID = cmbDomainName_Users.getSelectedItem().get_value();
            else {
                if (SelectedActiveDirectoryUser_Users.DomainID != undefined)
                    ObjUser_Users.DomainID = SelectedActiveDirectoryUser_Users.DomainID;
            }
            if (cmbDomainUserName_Users.getSelectedItem() != undefined)
                ObjUser_Users.UserName = cmbDomainUserName_Users.getSelectedItem().get_text();
            else {
                if (SelectedActiveDirectoryUser_Users.UserName != undefined)
                    ObjUser_Users.UserName = SelectedActiveDirectoryUser_Users.UserName;
            }
        }
        else {
            ObjUser_Users.UserName = document.getElementById('txtUserName_Users').value;
            ObjUser_Users.Password = document.getElementById('txtPassword_Users').value;
            ObjUser_Users.ConfirmPassword = document.getElementById('txtPasswordRepeat_Users').value;
        }
        switch (CurrentPageState_Users) {
            case 'Add':
                ObjUser_Users.IsPasswordChange = true;
                break;
            case 'Edit':
                if (GridUsers_Users.getSelectedItems()[0] != undefined) {
                    if (ObjUser_Users.Password != SelectedItems_GridUsers_Users[0].getMember('Password').get_text())
                        ObjUser_Users.IsPasswordChange = true;
                }
                break;
        }
        if (!ObjUser_Users.IsActiveDirectoryAuthenticate) {
            ObjUser_Users.Password = EncryptData_Users(document.getElementById('txtPassword_Users').value).toString();
            ObjUser_Users.ConfirmPassword = EncryptData_Users(document.getElementById('txtPasswordRepeat_Users').value).toString();
        }

    }
    else {
        if (SelectedPersonnel_Users != null) {
            ObjUser_Users.PersonnelID = SelectedPersonnel_Users.ID;
            ObjUser_Users.PersonnelName = SelectedPersonnel_Users.Name;
            ObjUser_Users.PersonnelBarCode = SelectedPersonnel_Users.BarCode;
        }
    }
    UpdateUser_UsersPage(CharToKeyCode_Users(CurrentPageState_Users), CharToKeyCode_Users(ObjUser_Users.ID), CharToKeyCode_Users(ObjUser_Users.IsActive.toString()), CharToKeyCode_Users(ObjUser_Users.PersonnelID), CharToKeyCode_Users(ObjUser_Users.RoleID), CharToKeyCode_Users(ObjUser_Users.UserName), CharToKeyCode_Users(ObjUser_Users.Password), CharToKeyCode_Users(ObjUser_Users.ConfirmPassword), CharToKeyCode_Users(ObjUser_Users.IsActiveDirectoryAuthenticate.toString()), CharToKeyCode_Users(ObjUser_Users.DomainID), CharToKeyCode_Users(ObjUser_Users.IsPasswordChange.toString()));
    DialogWaiting.Show();
}

function UpdateUser_UsersPage_onCallBack(Response) {
    var RetMessage = Response;
    if (RetMessage != null && RetMessage.length > 0) {
        DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_Users').value;
            Response[1] = document.getElementById('hfConnectionError_Users').value;
        }
        showDialog(RetMessage[0], Response[1], RetMessage[2]);
        if (RetMessage[2] == 'success') {
            ClearUserList_Users();
            User_OnAfterUpdate(Response);
            ChangePageState_Users('View');
        }
        else {
            if (CurrentPageState_Users == 'Delete') {
                ChangePageState_Users('View');
            }
        }
    }
}

function User_OnAfterUpdate(Response) {
    if (ObjUser_Users != null) {
        var IsActive = ObjUser_Users.IsActive;
        var PersonnelID = ObjUser_Users.PersonnelID;
        var PersonnelName = ObjUser_Users.PersonnelName;
        var PersonnelBarCode = ObjUser_Users.PersonnelBarCode;
        var RoleID = ObjUser_Users.RoleID;
        var RoleName = ObjUser_Users.RoleName;
        var UserName = ObjUser_Users.UserName;
        var Password = ObjUser_Users.Password;
        var ConfirmPassword = ObjUser_Users.ConfirmPassword;
        var IsActiveDirectoryAuthenticate = ObjUser_Users.IsActiveDirectoryAuthenticate;
        var IsPasswordChange = ObjUser_Users.IsPasswordChange;

        var UserItem = null;

        GridUsers_Users.beginUpdate();
        switch (CurrentPageState_Users) {
            case 'Add':
                UserItem = GridUsers_Users.get_table().addEmptyRow(GridUsers_Users.get_recordCount());
                UserItem.setValue(0, Response[3], false);
                GridUsers_Users.selectByKey(Response[3], 0, false);
                UpdateFeatures_GridUsers_Users();
                break;
            case 'Edit':
                GridUsers_Users.selectByKey(Response[3], 0, false);
                UserItem = GridUsers_Users.getItemFromKey(0, Response[3]);
                break;
            case 'Delete':
                GridUsers_Users.selectByKey(ObjUser_Users.ID, 0, false);
                GridUsers_Users.deleteSelected();
                UpdateFeatures_GridUsers_Users();
                break;
        }
        if (CurrentPageState_Users != 'Delete') {
            UserItem.setValue(1, PersonnelID, false);
            UserItem.setValue(2, IsActive, false);
            UserItem.setValue(3, PersonnelBarCode, false);
            UserItem.setValue(4, PersonnelName, false);
            UserItem.setValue(5, UserName, false);
            UserItem.setValue(6, RoleID, false);
            UserItem.setValue(7, RoleName, false);
            UserItem.setValue(8, Password, false);
            UserItem.setValue(9, IsActiveDirectoryAuthenticate, false);
        }
        GridUsers_Users.endUpdate();
    }
}

function UpdateFeatures_GridUsers_Users() {
    var UsersCount = parseInt(document.getElementById('hfUsersCount_Users').value);
    var UsersPageCount = parseInt(document.getElementById('hfUsersPageCount_Users').value);
    var UsersPageSize = parseInt(document.getElementById('hfUsersPageSize_Users').value);
    var Lag = 0;
    switch (CurrentPageState_Users) {
        case 'Add':
            Lag = Lag + 1;
            break;
        case 'Delete':
            Lag = Lag - 1;
            break;
    }
    if ((UsersCount > 0 && CurrentPageState_Users == 'Delete') || CurrentPageState_Users == 'Add') {
        UsersCount = UsersCount + Lag;
        var divRem = mod(UsersCount, UsersPageSize);
        switch (CurrentPageState_Users) {
            case 'Add':
                if (GridUsers_Users.get_table().getRowCount() > UsersPageSize) {
                    UsersPageCount = UsersPageCount + Lag;
                    CurrentPageIndex_GridUsers_Users = CurrentPageIndex_GridUsers_Users + Lag;
                }
                break;
            case 'Delete':
                if (divRem == 0) {
                    UsersPageCount = UsersPageCount + Lag;
                    if (CurrentPageIndex_GridUsers_Users == UsersPageCount) {
                        CurrentPageIndex_GridUsers_Users = CurrentPageIndex_GridUsers_Users + Lag >= 0 ? CurrentPageIndex_GridUsers_Users + Lag : 0;
                    }
                }
                break;
        }
        SetPageIndex_GridUsers_Users(CurrentPageIndex_GridUsers_Users);
        document.getElementById('hfUsersCount_Users').value = UsersCount.toString();
        document.getElementById('hfUsersPageCount_Users').value = UsersPageCount.toString();
        Changefooter_GridUsers_Users();
    }
}

function mod(a, b) {
    return a - (b * Math.floor(a / b));
}

function ClearUserList_Users() {
    cmbPersonnel_Users.unSelect();
    document.getElementById('cmbPersonnel_Users_Input').value = '';
    cmbUserRole_Users.unSelect();
    document.getElementById('cmbUserRole_Users_Input').value = '';
    cmbDomainName_Users.unSelect();
    $('#trvUserRole_cmbUserRole_Users div').removeClass('SelectedTreeNode');
    trvUserRole_cmbUserRole_Users.beginUpdate();
    trvUserRole_cmbUserRole_Users.set_selectedNode(null);
    trvUserRole_cmbUserRole_Users.endUpdate();
    document.getElementById('cmbDomainName_Users_Input').value = '';
    cmbDomainUserName_Users.unSelect();
    document.getElementById('cmbDomainUserName_Users_Input').value = '';
    document.getElementById('txtUserName_Users').value = '';
    document.getElementById('txtPassword_Users').value = '';
    document.getElementById('txtPasswordRepeat_Users').value = '';
    document.getElementById('rdbUserNameIntroduction_Users').checked = false;
    document.getElementById('rdbActiveDirValidation_Users').checked = false;
    document.getElementById('chbActiveUser_Users').checked = false;
}

function SetActionMode_Users(state) {
    document.getElementById('ActionMode_Users').innerHTML = document.getElementById("hf" + state + "_Users").value;
}

function ChangePageState_Users(State) {
    CurrentPageState_Users = State;
    SetActionMode_Users(State);
    if (State == 'Add' || State == 'Edit' || State == 'Delete') {
        if (TlbUsers.get_items().getItemById('tlbItemNew_TlbUsers') != null) {
            TlbUsers.get_items().getItemById('tlbItemNew_TlbUsers').set_enabled(false);
            TlbUsers.get_items().getItemById('tlbItemNew_TlbUsers').set_imageUrl('add_silver.png');
        }
        if (TlbUsers.get_items().getItemById('tlbItemEdit_TlbUsers') != null) {
            TlbUsers.get_items().getItemById('tlbItemEdit_TlbUsers').set_enabled(false);
            TlbUsers.get_items().getItemById('tlbItemEdit_TlbUsers').set_imageUrl('edit_silver.png');
        }
        if (TlbUsers.get_items().getItemById('tlbItemDelete_TlbUsers') != null) {
            TlbUsers.get_items().getItemById('tlbItemDelete_TlbUsers').set_enabled(false);
            TlbUsers.get_items().getItemById('tlbItemDelete_TlbUsers').set_imageUrl('remove_silver.png');
        }
        TlbUsers.get_items().getItemById('tlbItemSave_TlbUsers').set_enabled(true);
        TlbUsers.get_items().getItemById('tlbItemSave_TlbUsers').set_imageUrl('save.png');
        TlbUsers.get_items().getItemById('tlbItemCancel_TlbTlbUsers').set_enabled(true);
        TlbUsers.get_items().getItemById('tlbItemCancel_TlbTlbUsers').set_imageUrl('cancel.png');
        TlbPaging_PersonnelSearch_Users.get_items().getItemById('tlbItemRefresh_TlbPaging_PersonnelSearch_Users').set_enabled(true);
        TlbPaging_PersonnelSearch_Users.get_items().getItemById('tlbItemRefresh_TlbPaging_PersonnelSearch_Users').set_imageUrl('refresh.png');
        TlbPaging_PersonnelSearch_Users.get_items().getItemById('tlbItemLast_TlbPaging_PersonnelSearch_Users').set_enabled(true);
        TlbPaging_PersonnelSearch_Users.get_items().getItemById('tlbItemLast_TlbPaging_PersonnelSearch_Users').set_imageUrl("last.png");
        TlbPaging_PersonnelSearch_Users.get_items().getItemById('tlbItemNext_TlbPaging_PersonnelSearch_Users').set_enabled(true);
        TlbPaging_PersonnelSearch_Users.get_items().getItemById('tlbItemNext_TlbPaging_PersonnelSearch_Users').set_imageUrl("Next.png");
        TlbPaging_PersonnelSearch_Users.get_items().getItemById('tlbItemBefore_TlbPaging_PersonnelSearch_Users').set_enabled(true);
        TlbPaging_PersonnelSearch_Users.get_items().getItemById('tlbItemBefore_TlbPaging_PersonnelSearch_Users').set_imageUrl("Before.png");
        TlbPaging_PersonnelSearch_Users.get_items().getItemById('tlbItemFirst_TlbPaging_PersonnelSearch_Users').set_enabled(true);
        TlbPaging_PersonnelSearch_Users.get_items().getItemById('tlbItemFirst_TlbPaging_PersonnelSearch_Users').set_imageUrl("first.png");
        if (TlbSearchPersonnel_Users.get_items().getItemById('tlbItemSearch_TlbSearchPersonnel_Users') != null) {
            TlbSearchPersonnel_Users.get_items().getItemById('tlbItemSearch_TlbSearchPersonnel_Users').set_enabled(true);
            TlbSearchPersonnel_Users.get_items().getItemById('tlbItemSearch_TlbSearchPersonnel_Users').set_imageUrl('search.png');
        }
        if (TlbAdvancedSearch_Users.get_items().getItemById('tlbItemAdvancedSearch_TlbAdvancedSearch_Users') != null) {
            TlbAdvancedSearch_Users.get_items().getItemById('tlbItemAdvancedSearch_TlbAdvancedSearch_Users').set_enabled(true);
            TlbAdvancedSearch_Users.get_items().getItemById('tlbItemAdvancedSearch_TlbAdvancedSearch_Users').set_imageUrl('eyeglasses.png');
        }
        document.getElementById('rdbUserNameIntroduction_Users').disabled = '';
        document.getElementById('rdbActiveDirValidation_Users').disabled = '';
        document.getElementById('chbActiveUser_Users').disabled = '';
        if (State != 'Edit') {
            ChangeEnabled_DropDownDive_Users('imgbox_SearchByPersonnel_Users', 'enabled');
        }        
        if (State == 'Add') {
            ShowHide_SearchByPersonnelBox_Users();
            document.getElementById('rdbUserNameIntroduction_Users').checked = true;
            ChangeItemsEnabled_onCheckChange('UserNameIntroduction');
        }
        if (State == 'Edit')
            NavigateUsers_Users(GridUsers_Users.getSelectedItems()[0]);
        if (State == 'Delete')
            Users_onSave();
        document.getElementById('chbActiveUser_Users').disabled = false;
        cmbPersonnel_Users.enable();
        cmbUserRole_Users.enable();
    }
    if (State == 'View') {
        if (TlbUsers.get_items().getItemById('tlbItemNew_TlbUsers') != null) {
            TlbUsers.get_items().getItemById('tlbItemNew_TlbUsers').set_enabled(true);
            TlbUsers.get_items().getItemById('tlbItemNew_TlbUsers').set_imageUrl('add.png');
        }
        if (TlbUsers.get_items().getItemById('tlbItemEdit_TlbUsers') != null) {
            TlbUsers.get_items().getItemById('tlbItemEdit_TlbUsers').set_enabled(true);
            TlbUsers.get_items().getItemById('tlbItemEdit_TlbUsers').set_imageUrl('edit.png');
        }
        if (TlbUsers.get_items().getItemById('tlbItemDelete_TlbUsers') != null) {
            TlbUsers.get_items().getItemById('tlbItemDelete_TlbUsers').set_enabled(true);
            TlbUsers.get_items().getItemById('tlbItemDelete_TlbUsers').set_imageUrl('remove.png');
        }
        TlbUsers.get_items().getItemById('tlbItemSave_TlbUsers').set_enabled(false);
        TlbUsers.get_items().getItemById('tlbItemSave_TlbUsers').set_imageUrl('save_silver.png');
        TlbUsers.get_items().getItemById('tlbItemCancel_TlbTlbUsers').set_enabled(false);
        TlbUsers.get_items().getItemById('tlbItemCancel_TlbTlbUsers').set_imageUrl('cancel_silver.png');
        TlbPaging_PersonnelSearch_Users.get_items().getItemById('tlbItemRefresh_TlbPaging_PersonnelSearch_Users').set_enabled(false);
        TlbPaging_PersonnelSearch_Users.get_items().getItemById('tlbItemRefresh_TlbPaging_PersonnelSearch_Users').set_imageUrl('refresh_silver.png');
        TlbPaging_PersonnelSearch_Users.get_items().getItemById('tlbItemLast_TlbPaging_PersonnelSearch_Users').set_enabled(false);
        TlbPaging_PersonnelSearch_Users.get_items().getItemById('tlbItemLast_TlbPaging_PersonnelSearch_Users').set_imageUrl("last_silver.png");
        TlbPaging_PersonnelSearch_Users.get_items().getItemById('tlbItemNext_TlbPaging_PersonnelSearch_Users').set_enabled(false);
        TlbPaging_PersonnelSearch_Users.get_items().getItemById('tlbItemNext_TlbPaging_PersonnelSearch_Users').set_imageUrl("Next_silver.png");
        TlbPaging_PersonnelSearch_Users.get_items().getItemById('tlbItemBefore_TlbPaging_PersonnelSearch_Users').set_enabled(false);
        TlbPaging_PersonnelSearch_Users.get_items().getItemById('tlbItemBefore_TlbPaging_PersonnelSearch_Users').set_imageUrl("Before_silver.png");
        TlbPaging_PersonnelSearch_Users.get_items().getItemById('tlbItemFirst_TlbPaging_PersonnelSearch_Users').set_enabled(false);
        TlbPaging_PersonnelSearch_Users.get_items().getItemById('tlbItemFirst_TlbPaging_PersonnelSearch_Users').set_imageUrl("first_silver.png");
        if (TlbSearchPersonnel_Users.get_items().getItemById('tlbItemSearch_TlbSearchPersonnel_Users') != null) {
            TlbSearchPersonnel_Users.get_items().getItemById('tlbItemSearch_TlbSearchPersonnel_Users').set_enabled(false);
            TlbSearchPersonnel_Users.get_items().getItemById('tlbItemSearch_TlbSearchPersonnel_Users').set_imageUrl('search_silver.png');
        }
        if (TlbAdvancedSearch_Users.get_items().getItemById('tlbItemAdvancedSearch_TlbAdvancedSearch_Users') != null) {
            TlbAdvancedSearch_Users.get_items().getItemById('tlbItemAdvancedSearch_TlbAdvancedSearch_Users').set_enabled(false);
            TlbAdvancedSearch_Users.get_items().getItemById('tlbItemAdvancedSearch_TlbAdvancedSearch_Users').set_imageUrl('eyeglasses_silver.png');
        }
        ChangeEnabled_DropDownDive_Users('imgbox_SearchByPersonnel_Users', 'disabled');
        if (box_UserSearch_Users_IsShown)
            ShowHide_UserSearchBox_Users();
        if (box_SearchByPersonnel_Users_IsShown)
            ShowHide_SearchByPersonnelBox_Users();
        document.getElementById('rdbUserNameIntroduction_Users').disabled = 'disabled';
        document.getElementById('rdbActiveDirValidation_Users').disabled = 'disabled';
        document.getElementById('chbActiveUser_Users').disabled = 'disabled';
        document.getElementById('chbActiveUser_Users').disabled = true;
        document.getElementById('txtUserName_Users').disabled = true;
        document.getElementById('txtPassword_Users').disabled = true;
        document.getElementById('txtPasswordRepeat_Users').disabled = true;
        cmbPersonnel_Users.disable();
        cmbUserRole_Users.disable();
        cmbDomainName_Users.disable();
        cmbDomainUserName_Users.disable();
    }
}

function tlbItemNew_TlbUsers_onClick() {
    ClearUserList_Users();
    ChangePageState_Users('Add');
}

function tlbItemEdit_TlbUsers_onClick() {
    ChangePageState_Users('Edit');
}

function tlbItemDelete_TlbUsers_onClick() {
    ChangePageState_Users('Delete');
}

function tlbItemSave_TlbUsers_onClick() {
    CollapseControls_Users();
    Users_onSave();
}

function tlbItemCancel_TlbTlbUsers_onClick() {
    Users_onCancel();
}

function tlbItemAccessLevelsInformation_TlbUsers_onClick() {
    ShowDialogUserInterfaceAccessLevels();
}

function ShowDialogUserInterfaceAccessLevels() {
    parent.DialogUserInterfaceAccessLevels.Show();
}

function tlbItemExcelExport_TlbUsers_onClick() {
    ExcelExport_GridUsers_Users();
}

function ExcelExport_GridUsers_Users() {
    var StrColumns = "";
    var Separator = "#";
    var Columns = GridUsers_Users.get_table().get_columns();
    for (var i = 0; i < Columns.length; i++) {
        if (i == Columns.length - 1)
            Separator = "";
        if (Columns[i].get_visible())
            StrColumns = StrColumns + Columns[i].get_dataField() + ":" + CharToKeyCode_Users(Columns[i].get_headingText()) + Separator;
    }
    var PageSize = GridUsers_Users.get_pageSize().toString();
    ExcelExport_GridUsers_Users_UsersPage(StrColumns, SearchKey, SearchText, GridUsers_Users_PageIndex.toString(), PageSize.toString());
}

function ExcelExport_GridUsers_Users_UsersPage_onCallBack(Response) {
    if (Response != "")
        document.getElementById('hidden_IFrame_Users').src = "XLS/" + Response + "";
}

function tlbItemExit_TlbUsers_onClick() {
    ShowDialogConfirm('Exit');
}

function GridUsers_Users_onLoad(sender, e) {
    document.getElementById('loadingPanel_GridUsers_Users').innerHTML = '';
}

function CallBack_GridUsers_Users_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Users').value;
    if (error != "") {
        var errorParts = eval('(' + error + ')');
        if (errorParts[3] == 'Reload')
            SetPageIndex_GridUsers_Users(0);
        else
            showDialog(errorParts[0], errorParts[1], errorParts[2]);
    }
    else
        Changefooter_GridUsers_Users();
}

function Changefooter_GridUsers_Users() {
    var retfooterVal = '';
    var footerVal = document.getElementById('footer_GridUsers_Users').innerHTML;
    var footerValCol = footerVal.split(' ');
    for (var i = 0; i < footerValCol.length; i++) {
        if (i == 1)
            footerValCol[i] = parseInt(document.getElementById('hfUsersPageCount_Users').value) > 0 ? CurrentPageIndex_GridUsers_Users + 1 : 0;
        if (i == 3)
            footerValCol[i] = document.getElementById('hfUsersPageCount_Users').value;
        retfooterVal += footerValCol[i] + ' ';
    }
    document.getElementById('footer_GridUsers_Users').innerHTML = retfooterVal;
}

function GridUsers_Users_onItemSelect(sender, e) {
    if (CurrentPageState_Users != 'Add')
        NavigateUsers_Users(e.get_item());
}

function tlbItemRefresh_TlbPaging_GridUsers_Users_onClick() {
    ChangeLoadState_GridUsers_Users('Normal');
}

function ChangeLoadState_GridUsers_Users(state) {
    LoadState_Users = state;
    SetPageIndex_GridUsers_Users(0);
}

function tlbItemFirst_TlbPaging_GridUsers_Users_onClick() {
    SetPageIndex_GridUsers_Users(0);
}

function tlbItemBefore_TlbPaging_GridUsers_Users_onClick() {
    if (CurrentPageIndex_GridUsers_Users != 0) {
        CurrentPageIndex_GridUsers_Users = CurrentPageIndex_GridUsers_Users - 1;
        SetPageIndex_GridUsers_Users(CurrentPageIndex_GridUsers_Users);
    }
}

function tlbItemNext_TlbPaging_GridUsers_Users_onClick() {
    if (CurrentPageIndex_GridUsers_Users < parseInt(document.getElementById('hfUsersPageCount_Users').value) - 1) {
        CurrentPageIndex_GridUsers_Users = CurrentPageIndex_GridUsers_Users + 1;
        SetPageIndex_GridUsers_Users(CurrentPageIndex_GridUsers_Users);
    }
}

function tlbItemLast_TlbPaging_GridUsers_Users_onClick() {
    SetPageIndex_GridUsers_Users(parseInt(document.getElementById('hfUsersPageCount_Users').value) - 1);
}

function SetPageIndex_GridUsers_Users(pageIndex) {
    CurrentPageIndex_GridUsers_Users = pageIndex;
    Fill_GridUsers_Users(pageIndex);
}

function Fill_GridUsers_Users(pageIndex) {
    document.getElementById('loadingPanel_GridUsers_Users').innerHTML = GetLoadingMessage(document.getElementById('hfloadingPanel_GridUsers_Users').value);
    var pageSize = parseInt(document.getElementById('hfUsersPageSize_Users').value);
    var searchKey = 'NotSpecified';
    var searchTerm = '';
    if (box_UserSearch_Users_IsShown) {
        if (cmbSearchField_Users.getSelectedItem() != undefined)
            searchKey = cmbSearchField_Users.getSelectedItem().get_value();
        searchTerm = document.getElementById('txtUserSearch_Users').value;
    }
    CallBack_GridUsers_Users.callback(CharToKeyCode_Users(LoadState_Users), CharToKeyCode_Users(pageSize.toString()), CharToKeyCode_Users(pageIndex.toString()), CharToKeyCode_Users(searchKey), CharToKeyCode_Users(searchTerm));
}

function imgbox_UserSearch_Users_onClick() {
    CollapseControls_Users();
    setSlideDownSpeed(200);

    if (box_SearchByPersonnel_Users_IsShown)
        ShowHide_SearchByPersonnelBox_Users();
    slidedown_showHide('box_UserSearch_Users');

    if (box_UserSearch_Users_IsShown) {
        box_UserSearch_Users_IsShown = false;
        document.getElementById('imgbox_UserSearch_Users').src = 'Images/Ghadir/arrowDown.jpg';
    }
    else {
        box_UserSearch_Users_IsShown = true;
        document.getElementById('imgbox_UserSearch_Users').src = 'Images/Ghadir/arrowUp.jpg';
    }
}

function imgbox_SearchByPersonnel_Users_onClick() {
    CollapseControls_Users();
    setSlideDownSpeed(200);

    if (box_UserSearch_Users_IsShown)
        ShowHide_UserSearchBox_Users();
    slidedown_showHide('box_SearchByPersonnel_Users');

    if (box_SearchByPersonnel_Users_IsShown) {
        box_SearchByPersonnel_Users_IsShown = false;
        document.getElementById('imgbox_SearchByPersonnel_Users').src = 'Images/Ghadir/arrowDown.jpg';
        cmbPersonnel_Users.collapse();
    }
    else {
        box_SearchByPersonnel_Users_IsShown = true;
        document.getElementById('imgbox_SearchByPersonnel_Users').src = 'Images/Ghadir/arrowUp.jpg';
    }
}

function ShowHide_UserSearchBox_Users() {
    imgbox_UserSearch_Users_onClick();
}

function cmbSearchField_Users_onExpand(sender, e) {
    if (cmbSearchField_Users.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbSearchField_Users == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbSearchField_Users = true;
        Fill_cmbSearchField_Users();
    }
}
function Fill_cmbSearchField_Users() {
    ComboBox_onBeforeLoadData('cmbSearchField_Users');
    CallBack_cmbSearchField_Users.callback();
}

function cmbSearchField_Users_onBeforeCallback(sender, e) {
    cmbSearchField_Users.dispose();
}

function cmbSearchField_Users_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_SearchFields').value;
    if (error == "") {
        document.getElementById('cmbSearchField_Users_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbSearchField_Users_DropImage').mousedown();
        cmbSearchField_Users.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbSearchField_Users_DropDown').style.display = 'none';
    }
}

function tlbItemUserSearch_TlbUserSearch_Users_onClick() {
    LoadState_Users = 'Search';
    SetPageIndex_GridUsers_Users(0);
}

function tlbItemRefresh_TlbPaging_PersonnelSearch_Users_onClick() {
    Refresh_cmbPersonnel_Users();
}

function Refresh_cmbPersonnel_Users() {
    LoadState_cmbPersonnel_Users = 'Normal';
    SetPageIndex_cmbPersonnel_Users(0);
}

function tlbItemFirst_TlbPaging_PersonnelSearch_Users_onClick() {
    SetPageIndex_cmbPersonnel_Users(0);
}

function tlbItemBefore_TlbPaging_PersonnelSearch_Users_onClick() {
    if (CurrentPageIndex_cmbPersonnel_Users != 0) {
        CurrentPageIndex_cmbPersonnel_Users = CurrentPageIndex_cmbPersonnel_Users - 1;
        SetPageIndex_cmbPersonnel_Users(CurrentPageIndex_cmbPersonnel_Users);
    }
}

function tlbItemNext_TlbPaging_PersonnelSearch_Users_onClick() {
    if (CurrentPageIndex_cmbPersonnel_Users < parseInt(document.getElementById('hfPersonnelPageCount_Users').value) - 1) {
        CurrentPageIndex_cmbPersonnel_Users = CurrentPageIndex_cmbPersonnel_Users + 1;
        SetPageIndex_cmbPersonnel_Users(CurrentPageIndex_cmbPersonnel_Users);
    }
}

function tlbItemLast_TlbPaging_PersonnelSearch_Users_onClick() {
    SetPageIndex_cmbPersonnel_Users(parseInt(document.getElementById('hfPersonnelPageCount_Users').value) - 1);
}

function SetPageIndex_cmbPersonnel_Users(pageIndex) {
    CurrentPageIndex_cmbPersonnel_Users = pageIndex;
    Fill_cmbPersonnel_Users(pageIndex);
}

function Fill_cmbPersonnel_Users(pageIndex) {
    var SearchTerm = '';
    var pageSize = parseInt(document.getElementById('hfPersonnelPageSize_Users').value);
    switch (LoadState_cmbPersonnel_Users) {
        case 'Normal':
            break;
        case 'Search':
            SearchTerm = document.getElementById('txtPersonnelSearch_Users').value;
            break;
        case 'AdvancedSearch':
            SearchTerm = AdvancedSearchTerm_cmbPersonnel_Users;
            break;
    }
    ComboBox_onBeforeLoadData('cmbPersonnel_Users');
    CallBack_cmbPersonnel_Users.callback(CharToKeyCode_Users(LoadState_cmbPersonnel_Users), CharToKeyCode_Users(pageSize.toString()), CharToKeyCode_Users(pageIndex.toString()), CharToKeyCode_Users(SearchTerm));
}

function cmbPersonnel_Users_onExpand(sender, e) {
    if (cmbPersonnel_Users.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbPersonnel_Users == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbPersonnel_Users = true;
        SetPageIndex_cmbPersonnel_Users(0);
    }
}

function CallBack_cmbPersonnel_Users_onBeforeCallback(sender, e) {
    cmbPersonnel_Users.dispose();
}

function CallBack_cmbPersonnel_Users_onCallBackComplete(sender, e) {
    document.getElementById('clmnName_cmbPersonnel_Users').innerHTML = document.getElementById('hfclmnName_cmbPersonnel_Users').value;
    document.getElementById('clmnBarCode_cmbPersonnel_Users').innerHTML = document.getElementById('hfclmnBarCode_cmbPersonnel_Users').value;
    document.getElementById('clmnCardNum_cmbPersonnel_Users').innerHTML = document.getElementById('hfclmnCardNum_cmbPersonnel_Users').value;

    var error = document.getElementById('ErrorHiddenField_Personnel_Users').value;
    if (error == "") {
        document.getElementById('cmbPersonnel_Users_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbPersonnel_Users_DropImage').mousedown();
        else
            cmbPersonnel_Users.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbPersonnel_Users_DropDown').style.display = 'none';
    }
}

function CheckNavigator_onCmbCallBackCompleted() {
    if (navigator.userAgent.indexOf('Safari') != -1 || navigator.userAgent.indexOf('Chrome') != -1)
        return true;
    return false;
}

function tlbItemSearch_TlbSearchPersonnel_Users_onClick() {
    LoadState_cmbPersonnel_Users = 'Search';
    SetPageIndex_cmbPersonnel_Users(0);
}

function tlbItemAdvancedSearch_TlbAdvancedSearch_Users_onClick() {
    LoadState_cmbPersonnel_Users = 'AdvancedSearch';
    ShowDialogPersonnelSearch('Users');
}

function rdbUserNameIntroduction_Users_onClick() {
    ChangeItemsEnabled_onCheckChange('UserNameIntroduction');
}

function rdbActiveDirValidation_Users_onClick() {
    ChangeItemsEnabled_onCheckChange('ActiveDirValidation');
}

function ChangeItemsEnabled_onCheckChange(state) {
    var disabled = null;
    switch (state) {
        case 'UserNameIntroduction':
            disabled = false;
            cmbDomainName_Users.disable();
            cmbDomainUserName_Users.disable();
            break;
        case 'ActiveDirValidation':
            disabled = true;
            cmbDomainName_Users.enable();
            cmbDomainUserName_Users.enable();
            break;
    }
    document.getElementById('txtUserName_Users').disabled = disabled;
    document.getElementById('txtPassword_Users').disabled = disabled;
    document.getElementById('txtPasswordRepeat_Users').disabled = disabled;
}

function cmbUserRole_Users_onExpand(sender, e) {
    if (cmbUserRole_Users.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbUserRole_Users == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbUserRole_Users = true;
        Fill_cmbUserRole_Users();
    }
}
function Fill_cmbUserRole_Users() {
    ComboBox_onBeforeLoadData('cmbUserRole_Users');
    CallBack_cmbUserRole_Users.callback();
}

function cmbUserRole_Users_onBeforeCallback(sender, e) {
    cmbUserRole_Users.dispose();
}

function cmbUserRole_Users_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_UsersRoles').value;
    if (error == "") {
        document.getElementById('cmbUserRole_Users_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbUserRole_Users_DropImage').mousedown();
        cmbUserRole_Users.expand();
        ChangeDirection_trvUserRole_cmbUserRole_Users();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbUserRole_Users_DropDown').style.display = 'none';
    }
}

function trvUserRole_cmbUserRole_Users_onNodeSelect(sender, e) {
    cmbUserRole_Users.set_text(e.get_node().get_text());
    UserRolID_Users = e.get_node().get_id();
    cmbUserRole_Users.collapse();
}

function cmbDomainName_Users_onChange(sender, e) {
    ClearItems_cmbDomainUserName_Users();
}

function ClearItems_cmbDomainUserName_Users() {
    cmbDomainUserName_Users.beginUpdate();
    cmbDomainUserName_Users.removeAll();
    CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbDomainUserName_Users = undefined;
    cmbDomainUserName_Users.endUpdate();
}

function cmbDomainName_Users_onExpand(sender, e) {
    if (cmbDomainName_Users.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbDomainName_Users == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbDomainName_Users = true;
        Fill_cmbDomainName_Users();
    }
}
function Fill_cmbDomainName_Users() {
    ComboBox_onBeforeLoadData('cmbDomainName_Users');
    CallBack_cmbDomainName_Users.callback();
}

function cmbDomainName_Users_onCollapse(sender, e) {
    if (cmbDomainName_Users.getSelectedItem() == undefined) {
        if (SelectedActiveDirectoryUser_Users != null && SelectedActiveDirectoryUser_Users.DomainID != null && SelectedActiveDirectoryUser_Users.DomainID != undefined)
            document.getElementById('cmbDomainName_Users_Input').value = SelectedActiveDirectoryUser_Users.DomainName;
    }
}

function CallBack_cmbDomainName_Users_onBeforeCallback(sender, e) {
    cmbDomainName_Users.dispose();
}

function CallBack_cmbDomainName_Users_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_Domains').value;
    if (error == "") {
        document.getElementById('cmbDomainName_Users_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbDomainName_Users_DropImage').mousedown();
        cmbDomainName_Users.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbDomainName_Users_DropDown').style.display = 'none';
    }
}

function cmbDomainUserName_Users_onExpand(sender, e) {
    if (cmbDomainUserName_Users.get_itemCount() == 0 && CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbDomainUserName_Users == undefined) {
        CurrentPageCombosCallBcakStateObj.IsExpandOccured_cmbDomainUserName_Users = true;
            Fill_cmbDomainUserName_Users();
    }
}
function Fill_cmbDomainUserName_Users() {
    ComboBox_onBeforeLoadData('cmbDomainUserName_Users');
    var domainID = '-1';
    if (cmbDomainName_Users.getSelectedItem() != undefined)
        domainID = cmbDomainName_Users.getSelectedItem().get_value();
    if (SelectedActiveDirectoryUser_Users != undefined && SelectedActiveDirectoryUser_Users != null && SelectedActiveDirectoryUser_Users.DomainID != undefined)
        domainID = SelectedActiveDirectoryUser_Users.DomainID;
    CallBack_cmbDomainUserName_Users.callback(CharToKeyCode_Users(domainID));
}

function cmbDomainUserName_Users_onCollapse(sender, e) {
    if (cmbDomainUserName_Users.getSelectedItem() == undefined) {
        if (SelectedActiveDirectoryUser_Users != null && SelectedActiveDirectoryUser_Users.UserName != null && SelectedActiveDirectoryUser_Users.UserName != undefined)
            document.getElementById('cmbDomainUserName_Users_Input').value = SelectedActiveDirectoryUser_Users.UserName;
    }
}

function CallBack_cmbDomainUserName_Users_onBeforeCallback(sender, e) {
    cmbDomainUserName_Users.dispose();
}

function CallBack_cmbDomainUserName_Users_onCallbackComplete(sender, e) {
    var error = document.getElementById('ErrorHiddenField_DomainUsers').value;
    if (error == "") {
        document.getElementById('cmbDomainUserName_Users_DropDown').style.display = 'none';
        if (CheckNavigator_onCmbCallBackCompleted())
            $('#cmbDomainUserName_Users_DropImage').mousedown();
        cmbDomainUserName_Users.expand();
    }
    else {
        var errorParts = eval('(' + error + ')');
        showDialog(errorParts[0], errorParts[1], errorParts[2]);
        document.getElementById('cmbDomainUserName_Users_DropDown').style.display = 'none';
    }
}

function tlbItemOk_TlbOkConfirm_onClick() {
    var role = DialogConfirm.get_value();
    switch (ConfirmState_Users) {
        case 'Delete':
            DialogConfirm.Close();
            UpdateUsers_Users();
            break;
        case 'Exit':
            ClearUserList_Users();
            parent.CloseCurrentTabOnTabStripMenus();
            break;
    }
}

function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
    ChangePageState_Users('View');
}

function ShowDialogConfirm(confirmState) {
    ConfirmState_Users = confirmState;
    if (CurrentPageState_Users == 'Delete')
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfDeleteMessage_Users').value;
    else
        document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_Users').value;
    DialogConfirm.Show();
    CollapseControls_Users();
}

function CallBack_GridUsers_Users_onCallbackError(sender, e) {
    document.getElementById('loadingPanel_GridUsers_Users').innerHTML = '';
    ShowConnectionError_Users();
}

function ShowConnectionError_Users() {
    var error = document.getElementById('hfErrorType_Users').value;
    var errorBody = document.getElementById('hfConnectionError_Users').value;
    showDialog(error, errorBody, 'error');
}

function CallBack_GridUsers_Users_OnCallbackError(sender, e) {
    ShowConnectionError_Users();
}

function cmbSearchField_Users_onCallbackError(sender, e) {
    ShowConnectionError_Users();
}

function CallBack_cmbPersonnel_Users_onCallbackError(sender, e) {
    ShowConnectionError_Users();
}

function cmbUserRole_Users_onCallbackError(sender, e) {
    ShowConnectionError_Users();
}

function CallBack_cmbDomainName_Users_onCallbackError(sender, e) {
    ShowConnectionError_Users();
}

function CallBack_cmbDomainUserName_Users_onCallbackError(sender, e) {
    ShowConnectionError_Users();
}

function CollapseControls_Users() {
    cmbSearchField_Users.collapse();
    cmbPersonnel_Users.collapse();
    cmbUserRole_Users.collapse();
    cmbDomainName_Users.collapse();
    cmbDomainUserName_Users.collapse();
}

function Users_onAfterPersonnelAdvancedSearch(SearchTerm) {
    AdvancedSearchTerm_cmbPersonnel_Users = SearchTerm;
    SetPageIndex_cmbPersonnel_Users(0);
}

function tlbItemFormReconstruction_TlbUsers_onClick() {
    parent.DialogLoading.Show();
    parent.document.getElementById('pgvUsersIntroduction_iFrame').src =parent.ModulePath +  'Users.aspx';
}

function tlbItemDataAccessLevels_TlbUsers_onClick() {
    ShowDialogDataAccessLevels();
}

function ShowDialogDataAccessLevels() {
    var SelectedItems_GridUsers_Users = GridUsers_Users.getSelectedItems();
    var ObjDialogMasterDataAccessLevels = new Object();
    ObjDialogMasterDataAccessLevels.UserID = '0';
    ObjDialogMasterDataAccessLevels.SearchKey = 'Null';
    ObjDialogMasterDataAccessLevels.SearchTerm = '';
    ObjDialogMasterDataAccessLevels.DataAccessLevelOperationType = null;
    ObjDialogMasterDataAccessLevels.UserCount = '0';
    if (SelectedItems_GridUsers_Users.length > 0) {
        ObjDialogMasterDataAccessLevels.DataAccessLevelOperationType = 'Single';
        ObjDialogMasterDataAccessLevels.UserID = SelectedItems_GridUsers_Users[0].getMember('ID').get_text();
        ObjDialogMasterDataAccessLevels.UserCount = '1';
    }
    else {
        ObjDialogMasterDataAccessLevels.DataAccessLevelOperationType = 'Group';
        if (LoadState_Users == 'Search') {
            if (cmbSearchField_Users.getSelectedItem() != undefined)
                ObjDialogMasterDataAccessLevels.SearchKey = cmbSearchField_Users.getSelectedItem().get_value();
            else
                ObjDialogMasterDataAccessLevels.SearchKey = 'NotSpecified';
            ObjDialogMasterDataAccessLevels.SearchTerm = document.getElementById('txtUserSearch_Users').value;
        }
        ObjDialogMasterDataAccessLevels.UserCount = document.getElementById('hfUsersCount_Users').value;
    }
    parent.DialogMasterDataAccessLevels.set_value(ObjDialogMasterDataAccessLevels);
    parent.DialogMasterDataAccessLevels.Show();
}

function tlbItemHelp_TlbUsers_onClick() {
    LoadHelpPage('tlbItemHelp_TlbUsers');
}

function trvUserRole_cmbUserRole_Users_onNodeExpand(sender, e) {
    ChangeDirection_trvUserRole_cmbUserRole_Users();
}

function ChangeDirection_trvUserRole_cmbUserRole_Users() {
    if (parent.CurrentLangID == 'fa-IR' && (navigator.userAgent.indexOf('Opera') != -1 || (navigator.appVersion == 'MacOS' && navigator.userAgent.indexOf('Safari') != -1))) {
        document.getElementById('trvUserRole_cmbUserRole_Users').style.direction = 'ltr';
    }
}

function txtUserSearch_Users_onKeyPess(event) {

    var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
    if (keyCode == 13) {
        tlbItemUserSearch_TlbUserSearch_Users_onClick();
    }
}
function txtPersonnelSearch_Users_onKeyPess(event) {

    var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
    if (keyCode == 13) {
        tlbItemSearch_TlbSearchPersonnel_Users_onClick();
    }
}
function ComboBox_onBeforeLoadData(cmbID) {
    document.getElementById(cmbID).onmouseover = " ";
    document.getElementById(cmbID).onmouseout = " ";
    document.getElementById(cmbID + '_Input').onfocus = " ";
    document.getElementById(cmbID + '_Input').onblur = " ";
    document.getElementById(cmbID + '_Input').onkeydown = " ";
    document.getElementById(cmbID + '_Input').onmouseup = " ";
    document.getElementById(cmbID + '_Input').onmousedown = " ";
    document.getElementById(cmbID + '_DropImage').src = 'Images/ComboBox/ComboBoxLoading.gif';
    eval(cmbID).disable();
}

function GetLoadingMessage(message) {
    return '<span style="vertical-align:top">' + message + '</span>' + '<span><img src="Images/Grid/gridLoading.gif"/></span>';
}
function EncryptData_Users(text) {
    var iv = CryptoJS.enc.Utf8.parse('1234567891234567');
    var key = CryptoJS.enc.Utf8.parse("www.ghadirco.net");    
    var encryptedText = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(text), key, {
        keySize: 128 / 8,
        iv: iv,
        mode: CryptoJS.mode.CBC,
        padding: CryptoJS.pad.Pkcs7
    });
    return encryptedText;
}













































