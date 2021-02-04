function objectifyForm(formArray) {//serialize data function
    var returnArray = {};
    for (var i = 0; i < formArray.length; i++) {
        returnArray[formArray[i]['name']] = formArray[i]['value'];
    }
    return returnArray;
}

function btnRegister_Click() {
    var fdata = objectifyForm($('#frmRegister').serializeArray());
    $.ajax({
        type: 'POST',
        url: '/Account/Register',     //your action
        data: { model: fdata },   //your form name.it takes all the values of model               
        //dataType: 'json',
        //cache: false,
        success: function (result) {
            alert("Registration complete");
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log(jqXHR);
            console.log(textStatus);
            console.log(errorThrown);
            //alert("Registration error: Please contact Administrator" + " " + error);
        }
    })

}

