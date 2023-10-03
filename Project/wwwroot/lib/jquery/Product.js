$(document).ready(function () {
    $("#btnps").click(function () {
        $.ajax({
            type: "POST",
            url: "/Home/InsProd",
        })
    })
})