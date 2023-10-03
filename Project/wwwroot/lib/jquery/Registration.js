$(document).ready(function () {
    $("#btns").click(function () {
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
                alert("You have successfully Registered");
                window.location.reload();
            },
            error: function () {
                alert("Something Went Wrong");
            }
        })
    })

    function validation() {
        var pwd = $("#txtpwd").val();
       
    }
})