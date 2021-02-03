$(document).ready(function () {
});

function btnRegister_Click() {
    $.ajax({
        type: "POST",
        url: '/Account/Register',     //your action
        data: $('#action_page').serialize(),   //your form name.it takes all the values of model               
        dataType: 'json',
        success: function (result) {
            alert("Registration complete");
        },
        error: function (error) {
            alert("Registration error: Please contact Administrator");
        }
    })

}