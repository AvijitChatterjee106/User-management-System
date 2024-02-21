$(document).ready(function () {

    $("#btnlog").click(function () {
        if (Validation()) {
            var data = {
                email: $("#email").val(),
                Password: $("#password").val(),
            };
            $.ajax({
                type: "POST",
                url: '/Home/Login1',
                data: data,
                dataType: 'json',
                success: function (Status) {
                    if (Status.status == 200) {
                        alertify.alert("Login Success");
                        setTimeout(function () {
                        if (Status.usertype == 'ADMIN') {
                            window.location.href = '/Home/UserDetails';
                        }
                        else {
                            window.location.href = '/Home/InsertProduct?email=' + Status.email+'';
                        }
                        },1500)
                    }
                    else if (Status.status == 10) {
                        alertify.error("Invalid User!");
                        return false;
                    }
                    else if (Status.status == 20) {
                        alertify.error("Wrong Password!");
                        return false;
                    }
                    else if (Status.status == 21) {
                        alertify.alert("Contact to Admin");
                        return false;
                    }
                    else {
                        alertify.error("something went wrong!");
                        return false;
                    }
                },
                //error: function () {
                //    alertify.alert("sometihng went wrong");
                //}
                error: function (xhr) {
                    // Display a user-friendly error message based on the response status
                    if (xhr.status === 500) {
                        alertify.alert("An error occurred while LogIn: " + xhr.responseText);
                    } else {
                        alertify.alert("Something went wrong. Please try again later.");
                    }
                }

            })
        }
    })
    function Validation() {
        var email = $("#email").val();
        if (email == "") {
            alertify.error("please Enter the UserName");
            return false;
        }
        var password = $("#password").val();
        if (password == "") {
            alertify.error("please Enter the Password");
            return false;
        }
        return true;
    }
})