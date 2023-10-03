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
                        alert("Login Success");
                        if (Status.usertype == 'ADMIN') {
                            window.location.href = '/Home/UserDetails';
                        }
                        else {
                            window.location.href = '/Home/InsertProduct?email=' + Status.email+'';
                        }
                        
                    }
                    else if (Status.status == 10) {
                        alert("Invalid User!");
                        return false;
                    }
                    else if (Status.status == 20) {
                        alert("Wrong Password!");
                        return false;
                    }
                    else if (Status.status == 21) {
                        alert("Contact to Admin");
                        return false;
                    }
                    else {
                        alert("something went wrong!");
                        return false;
                    }
                },
                error: function () {
                    alert("sometihng went wrong");
                }


            })
        }
    })
    function Validation() {
        var email = $("#email").val();
        if (email == "") {
            alert("please Enter the UserName");
            return false;
        }
        var password = $("#password").val();
        if (password == "") {
            alert("please Enter the Password");
            return false;
        }
        return true;
    }
})