$(document).ready(function () {
    $.ajax({
        type: "POST",
        url: "/Home/UpdateProductIsReject1",
      
        success: function (da) {
           // alert("reject");
            //var tab = "<thead><tr><th class='text-center'>Sl No</th><th class='text-center'>Product Name</th><th class='text-center'>Product Type</th><th class='text-center'>Product Price</th><th class='text-center'>Inserted UserName</th><th class='text-center'>Inserted Date</th><th class='text-center'>Approved UserName</th><th class='text-center'>Approved Date</th><th class='text-center'>Action</th></tr></thead><tbody>";

            //tab += "<tr>";
            //tab += "<td class='text-center'>" + data.slno + "</td>";
            //tab += "<td class='text-center pna'>" + data.pname + "</td>";
            //tab += "<td class='text-center'>" + data.ptype + "</td>";
            //tab += "<td class='text-center'>" + data.pprice + "</td>";
            //tab += "<td class='text-center'>" + data.insertedby + "</td>";
            //tab += "<td class='text-center'>" + data.indate + "</td>";
            //tab += "<td class='text-center'>" + data.rejectby + "</td>";
            //tab += "<td class='text-center'>" + data.rejdate + "</td>";
            //tab += "<td class='text-center'><button class='btn btn-success'>Reject<button></td>";
            //tab += "</tr>";

            //tab += "</tbody>";
            //$("#aprproduct").html(tab);
            ////window.location.reload();

            if (Array.isArray(da) && da.length > 0) {

                var tab = "<thead><tr><th class='text-center'>Sl No</th><th class='text-center'>Product Name</th><th class='text-center'>Product Type</th><th class='text-center'>Product Price</th><th class='text-center'>Inserted UserName</th><th class='text-center'>Inserted Date</th><th class='text-center'>Reject by UserName</th><th class='text-center'>Approved Date</th></tr></thead><tbody>";
                for (var i = 0; i < da.length; i++) {
                    var data = da[i];
                    var rono = i + 1;
                    tab += "<tr>";
                    tab += "<td class='text-center'>" + rono + "</td>";
                    tab += "<td class='text-center pna'>" + data.pname + "</td>";
                    tab += "<td class='text-center'>" + data.ptype + "</td>";
                    tab += "<td class='text-center'>" + data.pprice + "</td>";
                    tab += "<td class='text-center'>" + data.insertedby + "</td>";
                    tab += "<td class='text-center'>" + data.indate + "</td>";
                    tab += "<td class='text-center'>" + data.rejectby + "</td>";
                    tab += "<td class='text-center'>" + data.rejdate + "</td>";
                    //tab += "<td class='text-center'><button class='btn btn-success'>Reject<button></td>";
                    tab += "</tr>";
                }
                tab += "</tbody>";
                $("#rejproduct").html(tab);
                $("#rejproduct").DataTable();

            } else {
                console.error('Received undefined data from the server.');
                alertify.alert("Failed to delete product.");
            }
        },
        error: function () {
            alertify.alert("something went wrong");
        }



    })
    $('#aprproduct').on('click', '.btreja', function () {
        var pname = $(this).closest('tr').find(".pna").text();
        //alert(pname);
        $.ajax({
            type: "POST",
            url: "/Home/UpdateProductIsReject",
            data: { pname: pname },
            success: function (data) {
                alertify.alert("Rejected");
                var tab = "<thead><tr><th class='text-center'>Sl No</th><th class='text-center'>Product Name</th><th class='text-center'>Product Type</th><th class='text-center'>Product Price</th><th class='text-center'>Inserted UserName</th><th class='text-center'>Inserted Date</th><th class='text-center'>Approved by UserName</th><th class='text-center'>Approved Date</th><th class='text-center'>Action</th></tr></thead><tbody>";

                tab += "<tr>";
                tab += "<td class='text-center'>" + data.slno + "</td>";
                tab += "<td class='text-center pna'>" + data.pname + "</td>";
                tab += "<td class='text-center'>" + data.ptype + "</td>";
                tab += "<td class='text-center'>" + data.pprice + "</td>";
                tab += "<td class='text-center'>" + data.insertedby + "</td>";
                tab += "<td class='text-center'>" + data.indate + "</td>";
                tab += "<td class='text-center'>" + data.rejectby + "</td>";
                tab += "<td class='text-center'>" + data.rejdate + "</td>";
                tab += "<td class='text-center'><button class='btn btn-success'>Reject</button></td>";
                tab += "</tr>";

                tab += "</tbody>";
                // $("#aprproduct").html(tab);
                $("#rejproduct").html(tab);
                $("#rejproduct").DataTable();
                window.location.reload();
            },
            error: function () {
                alertify.alert("something went wrong");
            }



        })
    })
})