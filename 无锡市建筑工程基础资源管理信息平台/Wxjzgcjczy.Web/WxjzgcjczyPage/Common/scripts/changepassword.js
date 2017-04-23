LG.changepassword = function(userid) {
    $(document).bind('keydown.changepassword', function(e) {
        if (e.keyCode == 13) {
            doChangePassword(userid);
        }
    });

    if (!window.changePasswordWin) {
        var changePasswordPanle = $("<form></form>");
        changePasswordPanle.ligerForm({
            fields: [
                { display: '新密码', name: 'NewPassword', type: 'password', validate: { maxlength: 50, required: true, messages: { required: '请输入密码'}} },
                { display: '确认密码', name: 'NewPassword2', type: 'password', validate: { maxlength: 50, required: true, equalTo: '#NewPassword', messages: { required: '请输入密码', equalTo: '两次密码输入不一致'}} }
            ]
        });

        //验证
        jQuery.metadata.setType("attr", "validate");
        LG.validate(changePasswordPanle);

        window.changePasswordWin = $.ligerDialog.open({
            width: 400,
            height: 140, top: 200,
            isResize: true,
            title: '用户修改密码',
            target: changePasswordPanle,
            buttons: [
            { text: '确定', onclick: function() {
                doChangePassword(userid);
            }
            },
            { text: '取消', onclick: function() {
                window.changePasswordWin.hide();
                $(document).unbind('keydown.changepassword');
            }
            }
            ]
        });
    }
    else {
        window.changePasswordWin.show();
    }

    function doChangePassword(userid) {
        var LoginPassword = $("#NewPassword").val();
        if (changePasswordPanle.valid()) {
            $.ajax({
                type: 'post', cache: false, dataType: 'json',
                url: 'Handler/Login.ashx',
                data: [
                    { name: 'Action', value: 'Changepwd' },
                    { name: 'userid', value: userid },
                    { name: 'password', value: LoginPassword }
                    ],
                success: function(result) {
                    if (!result) {
                        LG.showError('登陆失败,账号或密码有误!');
                    } else {
                        LG.showSuccess('密码修改成功!');
                    }
                },
                error: function() {
                    LG.showError('发送系统错误,请与系统管理员联系!');
                },
                beforeSend: function() {
                    LG.showLoading('密码修改中...');
                },
                complete: function() {
                    LG.hideLoading();
                    window.changePasswordWin.hide();
                    $(document).unbind('keydown.changepassword');
                    location.href = "Login.htm?Action=Exist"; //修改成功之后，需要重新登录系统
                }
            });
        }
    }

};