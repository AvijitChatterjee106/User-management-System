
$(document).ready(function () {
   
    $.ajax({
        type: "POST",
        url: "/Home/delProductList",
        success: function (da) {
            //alert(data.iswithdraw);
            //var st = null;
            //var ac = null;
            //if (data.iswithdraw == true) {
            //    alert("with");
            //    st = "Delete/Withdraw";
            //    ac = "Active";
            //}
            //else if (data.isreject == true) {
            //    st = "Reject"
            //    ac = "By Admin";
            //}
            if (Array.isArray(da) && da.length > 0) {
                var tab = "<thead><tr><th class='text-center'>Sl No</th><th class='text-center'>Product Name</th><th class='text-center'>Product Type</th><th class='text-center'>Product Price</th><th class='text-center'>Deleted By</th><th class='text-center'>Inserted Date</th></tr></thead><tbody>"; //<th class='text-center'>Action</th>
                for (var i = 0; i < da.length; i++) {
                    var data = da[i];
                    //var st = null;
                    //var ac = null;

                    //if (data.iswithdraw) {
                    //    console.log("Withdraw");
                    //    st = "Delete/Withdraw";
                    //    ac = "Active";
                    //} else if (data.isreject) {
                    //    console.log("Reject");
                    //    st = "Reject";
                    //    ac = "By Admin";
                    //} else {
                    //    console.log("Other condition");
                    //}
                    //if (data.iswithdraw == true) {
                        //alert("successfull");
                        // $('#allproduct').row($(this).closest('tr')).remove().draw();
             

                    
                        var rowno = i + 1;
                        tab += "<tr>";
                        tab += "<td class='text-center'>" + rowno+ "</td>";
                        tab += "<td class='text-center pna'>" + data.pname + "</td>";
                        tab += "<td class='text-center'>" + data.ptype + "</td>";
                        tab += "<td class='text-center'>" + data.pprice + "</td>";
                        tab += "<td class='text-center'>" + data.userid + "</td>";
                        tab += "<td class='text-center'>" + data.udate + "</td>";
                        //tab += "<td class='text-center'><button class='btn btn-success'>" + ac + "</button></td>";
                        tab += "</tr>";
                   // }
                }
                tab += "</tbody>";
                $("#wdrproduct").html(tab);
                $("#wdrproduct").DataTable();
                appr();

            } else {
                console.error('Received undefined data from the server.');
                alertify.alert("Failed to delete product.");
            }

        },
        error: function () {
            alertify.alert("Something went wrong while deleting the product.");
        }
    });

    function appr() {
        $.ajax({
            type: "POST",
            url: "/Home/UpdateProductIsApprove1",

            success: function (da) {
                if (Array.isArray(da) && da.length > 0) {

                    var tab = "<thead><tr><th class='text-center'>Sl No</th><th class='text-center'>Product Name</th><th class='text-center'>Product Type</th><th class='text-center'>Product Price</th><th class='text-center'>Inserted UserName</th><th class='text-center'>Inserted Date</th><th class='text-center'>Approved by UserName</th><th class='text-center'>Approved Date</th><th class='text-center'>Action</th></tr></thead><tbody>";
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
                        tab += "<td class='text-center'>" + data.approveby + "</td>";
                        tab += "<td class='text-center'>" + data.apdate + "</td>";
                        tab += "<td class='text-center'><button class='btn btn-success btreja'>Reject</button></td>";
                        tab += "</tr>";
                    }
                    tab += "</tbody>";
                    $("#aprproduct").html(tab);	
                    $("#aprproduct").DataTable();

                } else {
                    console.error('Received undefined data from the server.');
                    alertify.alert("Failed to delete product.");
                }
            },
            error: function () {
                alertify.alert("something went wrong");
            }



        })
    }

  
   
})
