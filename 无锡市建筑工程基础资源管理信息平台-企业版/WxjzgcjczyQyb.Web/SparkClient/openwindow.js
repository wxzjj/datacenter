function EditQYzz(qyID, id, loginID) {
    var url = "Modify.aspx?qyID=" + qyID + "&ID=" + id + "&LoginID=" + loginID;
    var arguments = window;
    var features = "dialogWidth=900px;dialogHeight=450px;center=yes;status=no";

    var temp = new Date();
    temp = temp.toLocaleString() + Math.random();

    url = url + '&temp=' + temp;
    window.showModalDialog(url, arguments, features);
}   