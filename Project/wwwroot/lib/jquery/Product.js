﻿$(document).ready(function () {
    $("#btnps").click(function () {
        var data = {
            ptype: $("#ptype").val(),
            pname: $("#pname").val(),
            pprice: $("#pprice").val()
        };
        $.ajax({
            type: "POST",
            url: "/Home/InsProd",
            data: data,
            success: function (pro) {
                if (pro.code == 401) {
                    alertify.alert(pro.msg);
                    setTimeout(function () {

                        window.location.href = '/Home/LogIn';

                    }, 1500)
                }
                 if(pro.code == 200) {
                    alertify.success(pro.msg);
                    setTimeout(function () {
                        window.location.reload();
                    }, 1500)
                }
            },
            //error: function () {
            //    alert("Something went worng");
            //}
            error: function (xhr) {
                 //Display a user-friendly error message based on the response status
               if (xhr.status === 500) {
                    alertify.alert("An error occurred while storing the product: " + xhr.msg);
                } else {
                    alertify.alert("Something went wrong. Please try again later.");
                }
            }
        })
    })
    
})