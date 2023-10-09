$(document).ready(function () {
    $("#btns").click(function () {
        if (Validation()) {
            var data = {
                username: $("#txtname").val(),
                mob: $("#txtmob").val(),
                email: $("#txtemail").val(),
                password: $("#txtpwd").val(),
                usertype: $("#usertype").val()
            }
            $.ajax({
                type: "POST",
                url: "/Home/Registration1",
                data: data,
                success: function () {
                    alertify.alert("You have successfully Registered");
                    window.location.reload();
                },
                //error: function () {
                //    alertify.alert("Something Went Wrong");
                //}
                error: function (xhr) {
                    // Display a user-friendly error message based on the response status
                    if (xhr.status === 500) {
                        alertify.alert("An error occurred while Register: " + xhr.responseText);
                    } else {
                        alertify.alert("Something went wrong. Please try again later.");
                    }
                }
            })
        }
    })

    function Validation() {
        var usertype = $("#usertype").val();
        if (usertype == $("#def").val()) {
            alertify.error("please Select An User Role");
            return false;
        }
        var name = $("#txtname").val();
        if (name == "") {
            alertify.error("please Enter the Name");
            return false;
        }
        var mob = $("#txtmob").val();
        if (mob == "") {
            alertify.error("please Enter the Mobile No");
            return false;
        }
        var email = $("#txtemail").val();
        if (email == "") {
            alertify.error("please Enter the UserName/Email");
            return false;
        }
        var password = $("#txtpwd").val();
        if (password == "" || password.length<8) {
            alertify.error("please Enter a Valid Password");
            return false;
        }
       
       
        return true;
    }
})