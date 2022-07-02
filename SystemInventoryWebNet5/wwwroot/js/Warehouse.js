var datatable;

$(document).ready(function () {
    loadDataTable();
});

//David Cingolani15: 37
//“Write a program that prints the numbers from 1 to 100. 
//But for multiples of three print “Fizz”
//instead of the number and for the multiples of five print “Buzz”. 
//For numbers which are multiples of both three and five print “FizzBuzz”.”
function ShowResult() {
    for (var i = 1; i < 100; i++) {

        if (i % 3 === 0 && i % 5 === 0) {
            console.log("FizzBuzz");
        }
        else if (i % 3 === 0) {
            console.log("Fizz");
        }
        else if (i % 5 === 0) {
            console.log("Buzz");
        }
        else {
            console.log(i);
        }
    }
}

function loadDataTable() {
    datatable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Warehouse/GetAll"
        },
        "columns": [
            { "data": "name", "width": "20%" },
            { "data": "description", "width": "40%" },
            {
                "data": "status",
                "render": function (data) {
                    if (data) return "Active";
                    return "Inactive";
                }, "width": "20%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a href="/Admin/Warehouse/Upsert/${data}" class="text-success" style="cursor:pointer; text-decoration: none;">
                                <i class="fas fa-edit"></i>
                            </a>
                            <a onclick="Delete('/Admin/Warehouse/Delete/${data}')" class="text-danger" style="cursor:pointer; text-decoration: none">
                                <i class="fas fa-trash"></i>
                            </a>
                        </div>
                        `;
                }, "width": "20%"
            }
        ]
    });
}

    function Delete(uri) {
        swal({
            title: "Are you sure?",
            text: "You are deleting a record permanently",
            icon: "warning",
            buttons: true,
            dangerMode: true
        }).then((yes) => {
            if (yes) {
                $.ajax({
                    type: "DELETE",
                    url: uri,
                    success: function (data) {
                        if (data.success) {
                            toastr.success(data.msg);
                            datatable.ajax.reload();
                        }
                        else {
                            toastr.error(data.msg);
                        }
                    }
                });
            }
        });
    }
