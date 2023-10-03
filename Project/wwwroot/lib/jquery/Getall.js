$(document).ready(function () {
    $.ajax({
        type: "POST",
        url: "/Home/UserDetails1",
        success: function (data) {
            var j = 1;
            var tab = "<thead><tr><th class='text-center'>Sl No</th><th class='text-center'>Name</th><th class='text-center'>Contact No</th><th class='text-center'>Email</th><th class='text-center'>Action</th></tr></thead><tbody>";
            for (var i = 0; i < data.length; i++) {
                tab = tab + "<tr><td class='text-center'>" + j + "</td><td class='text-center'>" + data[i].username + "</td><td class='text-center'>" + data[i].mob + "</td><td class='text-center'>" + data[i].email + "</td><td class='text-center'><button class='btn btn-success'>Approve</button><button class='btn btn-warning'>InActive</button></td></tr>";
                j++;
            }
            tab = tab + "</tbody>";
            $("#alluserdata").html(tab);
            $('#alluserdata').DataTable();
        },
        error: function () {
            alert("Something went Wrong");
        }
    });
    $.ajax({
        type: "POST",
        url: "/Home/Product",
        success: function (data) {
            var j = 1;
            var tab = "<thead><tr><th class='text-center'>Sl No</th><th class='text-center'>Product Name</th><th class='text-center'>Product Type</th><th class='text-center'>Product Price</th><th class='text-center'>Inserted By</th><th class='text-center'>Inserted On</th><th class='text-center'>Action</th></tr></thead><tbody>";
            for (var i = 0; i < data.length; i++) {
                tab = tab + "<tr><td class='text-center'>" + j + "</td><td class='text-center'>" + data[i].pname + "</td><td class='text-center'>" + data[i].ptype + "</td><td class='text-center'>" + data[i].pprice + "</td><td class='text-center'> " + data[i].username + "</td ><td class='text-center'>" + data[i].date+"</td><td class='text-center'><button class='btn btn-success'>Approve</button><button class='btn btn-warning'>InActive</button></td></tr>";
                j++;
            }
            tab = tab + "</tbody>";
            $("#allproduct").html(tab);
            $('#allproduct').DataTable();
        },
        error: function () {
            alert("Something went Wrong");
        }
    });
});
