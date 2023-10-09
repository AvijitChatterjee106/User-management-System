$(document).ready(function () {

    $('#allproduct').on('click', '.pdel', function () {
        var pname = $(this).closest('tr').find(".pna").text();
        //alert(pname);
        $.ajax({
            type: "POST",
            url: "/Home/UpdateProductIsDelete",
            data: { pname: pname } , 
            success: function (data) {
                //alertify.alert("Deleted");
                var st = null;
                var ac = null;
                if (data.iswithdraw == true) {
                    alertify.alert("Delete/Withdraw");
                    st = "Delete/Withdraw";
                    ac = "Active";
                }
                else if (data.isreject == 1) {
                    st = "Reject"
                    ac = "By Admin";
                }
                if (data.iswithdraw == true) {
                    //alert("successfull");
                   // $('#allproduct').row($(this).closest('tr')).remove().draw();

                    var tab = "<thead><tr><th class='text-center'>Sl No</th><th class='text-center'>Product Name</th><th class='text-center'>Product Type</th><th class='text-center'>Product Price</th><th class='text-center'>Status</th><th class='text-center'>Action</th></tr></thead><tbody>";

                    tab += "<tr>";
                    tab += "<td class='text-center'>" + data.slno + "</td>";
                    tab += "<td class='text-center pna'>" + data.pname + "</td>";
                    tab += "<td class='text-center'>" + data.ptype + "</td>";
                    tab += "<td class='text-center'>" + data.pprice + "</td>";
                    tab += "<td class='text-center'>" + st + "</td>";
                    tab += "<td class='text-center'><button class='btn btn-success'>"+ac+"</button></td>";
                    tab += "</tr>";

                    tab += "</tbody>";
                    $("#wdrproduct").html(tab);
                    $("#wdrproduct").DataTable();
                    //window.location.href = "/Home/ProductList1";
                    window.location.reload();

                } else {
                    alertify.alert("Failed to delete product.");
                }
            },
            //error: function () {
            //    alertify.alert("Something went wrong while deleting the product.");
            //}
            error: function (xhr) {
                // Display a user-friendly error message based on the response status
                if (xhr.status === 500) {
                    alertify.alert("An error occurred while deleting/withdrawing the product: " + xhr.responseText);
                } else {
                    alertify.alert("Something went wrong. Please try again later.");
                }
            }
        });
    })


       $.ajax({
            type: "POST",
            url: "/Home/ProductList1",
            success: function (data) {
                var j = 1;
                var tab = "<thead><tr><th class='text-center'>Sl No</th><th class='text-center'>Product Name</th><th class='text-center'>Product Type</th><th class='text-center'>Product Price</th><th class='text-center'>Action</th></tr></thead><tbody>";
                for (var i = 0; i < data.length; i++) {
                    tab = tab + "<tr><td class='text-center'>" + j + "</td><td class='text-center pna'>" + data[i].pname + "</td><td class='text-center'>" + data[i].ptype + "</td><td class='text-center'>" + data[i].pprice + "</td><td class='text-center'><button class='btn btn-success pdel'>Delete</button>&nbsp;&nbsp;&nbsp<button class='btn btn-warning pdel'>Withdraw</button></td></tr>";
                    j++;
                }
                tab = tab + "</tbody>";
                $("#allproduct").html(tab);
                $("#allproduct").DataTable();
                wdr();
            },
            error: function () {
                alertify.alert("Something went Wrong");
            }
        });


    $('#wdrproduct').on('click', '.btnact', function () {
        var pname = $(this).closest('tr').find(".pna").text();
        //alert(pname);
        $.ajax({
            type: "POST",
            url: "/Home/UpdateProductdoActive",
            data: { pname: pname },
            success: function () {
                alertify.alert("Success");
                window.location.reload();
            },
            error: function (xhr) {
                // Display a user-friendly error message based on the response status
                if (xhr.status === 500) {
                    alertify.alert("An error occurred while deleting/withdrawing the product: " + xhr.responseText);
                } else {
                    alertify.alert("Something went wrong. Please try again later.");
                }
            }
        })

    })

    function wdr() {
        $.ajax({
            type: "POST",
            url: "/Home/delProductList1",
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
                    var tab = "<thead><tr><th class='text-center'>Sl No</th><th class='text-center'>Product Name</th><th class='text-center'>Product Type</th><th class='text-center'>Product Price</th><th class='text-center'>Status</th><th class='text-center'>Action</th></tr></thead><tbody>";
                    for (var i = 0; i < da.length; i++) {
                        var data = da[i];
                        var st = null;
                        var ac = null;

                        if (data.iswithdraw) {
                            console.log("Withdraw");
                            st = "Delete/Withdraw";
                            ac = "Active";
                        } else if (data.isreject) {
                            console.log("Reject");
                            st = "Reject";
                            ac = "By Admin";
                        } else {
                            console.log("Other condition");
                        }
                    
                           // alert("successfull");
                            // $('#allproduct').row($(this).closest('tr')).remove().draw();
                            var ron = i + 1;
                            tab += "<tr>";
                            tab += "<td class='text-center'>" + ron + "</td>";
                            tab += "<td class='text-center pna'>" + data.pname + "</td>";
                            tab += "<td class='text-center'>" + data.ptype + "</td>";
                            tab += "<td class='text-center'>" + data.pprice + "</td>";
                        tab += "<td class='text-center'>" + st + "</td>";
                        if (ac == "Active") {
                            tab += "<td class='text-center'><button class='btn btn-success btnact'>" + ac + "</button></td>";
                        }
                        else {
                            tab += "<td class='text-center'><b class='text-center'>" + ac + "</b></td>";

                        }
                            tab += "</tr>";
                        
                    }
                        tab += "</tbody>";
                    $("#wdrproduct").html(tab);
                    $("#wdrproduct").DataTable();

                } else {
                        console.error('Received undefined data from the server.');
                    alertify.alert("Failed to delete product.");
                    }
                
            },
            error: function () {
                alertify.alert("Something went wrong while deleting the product.");
            }
        });
    }
    
   
  
})