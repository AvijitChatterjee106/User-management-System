$(document).ready(function () {
    $.ajax({
        type: "POST",
        url: "/Home/UserDetails1",
        success: function (data) {
            var j = 1;
            var tab = "<thead><tr><th class='text-center'>Sl No</th><th class='text-center'>Name</th><th class='text-center'>Contact No</th><th class='text-center'>Email</th><th class='text-center'>Action</th></tr></thead><tbody>";
            for (var i = 0; i < data.length; i++) {
                if (data[i].isActive == 1 && data[i].isApprove == 0) {
                    tab = tab + "<tr><td class='text-center'>" + j + "</td><td class='text-center'>" + data[i].username + "</td><td class='text-center'>" + data[i].mob + "</td><td class='text-center uem'>" + data[i].email + "</td><td class='text-center'><button class='btn btn-success btadapr'>Approve</button>&nbsp;&nbsp;&nbsp<button class='btn btn-warning btadin'>InActive</button></td></tr>";//<button class='btn btn-success btadapr'>Approve</button>
                }
                else if (data[i].isApprove == 1 && data[i].isActive == 0) {
                    tab = tab + "<tr><td class='text-center'>" + j + "</td><td class='text-center'>" + data[i].username + "</td><td class='text-center'>" + data[i].mob + "</td><td class='text-center uem'>" + data[i].email + "</td><td class='text-center'><button class='btn btn-primary btadact'>Active</button></td></tr>";//<button class='btn btn-warning btadin'>InActive</button>
                }
                else if (data[i].isActive == 1 && data[i].isApprove == 1) {
                    tab = tab + "<tr><td class='text-center'>" + j + "</td><td class='text-center'>" + data[i].username + "</td><td class='text-center'>" + data[i].mob + "</td><td class='text-center uem'>" + data[i].email + "</td><td class='text-center'><button class='btn btn-warning btadin'>InActive</button></td></tr>";//<button class='btn btn-warning btadin'>InActive</button>
                }
                else if (data[i].isActive == 0 && data[i].isApprove == 0) {
                    tab = tab + "<tr><td class='text-center'>" + j + "</td><td class='text-center'>" + data[i].username + "</td><td class='text-center'>" + data[i].mob + "</td><td class='text-center uem'>" + data[i].email + "</td><td class='text-center'><button class='btn btn-success btadapr'>Approve</button>&nbsp;&nbsp;&nbsp<button class='btn btn-primary btadact'>Active</button></td></tr>";//<button class='btn btn-success btadapr'>Approve</button>
                }
                j++;
            }
            tab = tab + "</tbody>";
            $("#alluserdata").html(tab);
            $("#alluserdata").DataTable();
        },
        error: function () {
            alertify.alert("Something went Wrong");
        }
    });

    $('#alluserdata').on('click', '.btadact', function () {
        var email = $(this).closest('tr').find(".uem").text();

        //alert(email);
        $.ajax({
            type: "POST",
            url: "/Home/UpdateuserdoActive",
            data: { email: email },
            success: function () {
                alertify.alert("activated");
                window.location.reload();
            },
            error: function () {
                alertify.alert("something went worng")
            }
        })
    })
    $('#alluserdata').on('click', '.btadapr', function () {
        var email = $(this).closest('tr').find(".uem").text();

        //alert(email);
        $.ajax({
            type: "POST",
            url: "/Home/UpdateuserdoApprove",
            data: { email: email },
            success: function () {
                alertify.alert("approved");
                window.location.reload();
            },
            error: function () {
                alertify.alert("something went worng")
            }
        })
    })
    $('#alluserdata').on('click', '.btadin', function () {
        var email = $(this).closest('tr').find(".uem").text();

        //alert(email);
        $.ajax({
            type: "POST",
            url: "/Home/UpdateuserdoInActive",
            data: { email: email },
            success: function () {
                alertify.alert("InActivated");
                window.location.reload();
            },
            error: function () {
                alertify.alert("something went worng")
            }
        })
    })

    $('#allproduct').on('click', '.btap', function () {
        var pname = $(this).closest('tr').find(".pna").text();
        //alert(pname);
        $.ajax({
            type: "POST",
            url: "/Home/UpdateProductIsApprove",
            data: { pname: pname },
            success: function (data) {
                var tab = "<thead><tr><th class='text-center'>Sl No</th><th class='text-center'>Product Name</th><th class='text-center'>Product Type</th><th class='text-center'>Product Price</th><th class='text-center'>Inserted UserName</th><th class='text-center'>Inserted Date</th><th class='text-center'>Approved by UserName</th><th class='text-center'>Approved Date</th><th class='text-center'>Action</th></tr></thead><tbody>";

                tab += "<tr>";
                tab += "<td class='text-center'>" + data.slno + "</td>";
                tab += "<td class='text-center pna'>" + data.pname + "</td>";
                tab += "<td class='text-center'>" + data.ptype + "</td>";
                tab += "<td class='text-center'>" + data.pprice + "</td>";
                tab += "<td class='text-center'>" + data.insertedby + "</td>";
                tab += "<td class='text-center'>" + data.indate + "</td>";
                tab += "<td class='text-center'>" + data.approveby + "</td>";
                tab += "<td class='text-center'>" + data.apdate + "</td>";
                tab += "<td class='text-center'><button class='btn btn-success .btreja'>Reject</button></td>";
                tab += "</tr>";

                tab += "</tbody>";
                $("#aprproduct").html(tab);
                $("#aprproduct").DataTable();
                window.location.reload();
            },
            error: function () {
                alertify.alert("something went wrong");
            }



        })
    })
    $('#allproduct').on('click', '.btrej', function () {
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
   

    $.ajax({
        type: "POST",
        url: "/Home/Product",
        success: function (data) {
            var j = 1;
           
            var tab = "<thead><tr><th class='text-center'>Sl No</th><th class='text-center'>Product Name</th><th class='text-center'>Product Type</th><th class='text-center'>Product Price</th><th class='text-center'>Inserted By</th><th class='text-center'>Inserted On</th><th class='text-center'>Action</th></tr></thead><tbody>";
          //  for (var i = 0; i < data.length; i++) {
            //    tab = tab + "<tr><td class='text-center'>" + j + "</td><td class='text-center pna'>" + data[i].pname + "</td><td class='text-center'>" + data[i].ptype + "</td><td class='text-center'>" + data[i].pprice + "</td><td class='text-center'> " + data[i].username + "</td ><td class='text-center'>" + data[i].date+"</td><td class='text-center'><button class='btn btn-success btap'>Approve</button><button class='btn btn-warning btrej'>Reject</button></td></tr>";
              //  j++;
            //}
            for (var i = 0; i < data.length; i++) {
                console.log(data[i].username);
                tab = tab + "<tr><td class='text-center'>" + j + "</td><td class='text-center pna'>" + data[i].pname + "</td><td class='text-center'>" + data[i].ptype + "</td><td class='text-center'>" + data[i].pprice + "</td><td class='text-center'> " + data[i].username + "</td ><td class='text-center'>" + data[i].date + "</td><td class='text-center'><button class='btn btn-success btap'>Approve</button>&nbsp;&nbsp;&nbsp<button class='btn btn-warning btrej'>Reject</button></td></tr>";
                j++;
            }
            tab = tab + "</tbody>";
            $("#allproduct").html(tab);
            $("#allproduct").DataTable();
          
        },
        error: function () {
            alertify.alert("Something went Wrong");
        }
    });

 
});
