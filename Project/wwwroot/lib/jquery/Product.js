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
                alert("Product is Succesfully stored");
                window.location.reload();
            },
            error: function () {
                alert("Something went worng");
            }
        })
    })
})