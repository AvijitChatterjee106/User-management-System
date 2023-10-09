$(document).ready(function () {
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
            success: function () {
                alertify.alert("Product is Succesfully stored");
                window.location.reload();
            },
            //error: function () {
            //    alert("Something went worng");
            //}
            error: function (xhr) {
                // Display a user-friendly error message based on the response status
                if (xhr.status === 500) {
                    alertify.alert("An error occurred while storing the product: " + xhr.responseText);
                } else {
                    alertify.alert("Something went wrong. Please try again later.");
                }
            }
        })
    })
    
})