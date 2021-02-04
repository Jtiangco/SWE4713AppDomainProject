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
            window.location.href = "/Home/Index/";
        },
        error: function (x, e) {
            if (x.status == 400) {
                alert("User already exists");
            }
            //console.log(jqXHR);
            //console.log(textStatus);
            //console.log(errorThrown);
            //alert(textStatus);
        }
    })

}

