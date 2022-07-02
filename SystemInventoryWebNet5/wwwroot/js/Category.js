var datatable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    datatable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Category/GetAll"
        },
        "columns": [
            { "data": "name", "width": "60%" },
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
                            <a href="/Admin/Category/Upsert/${data}" class="text-success" style="cursor:pointer; text-decoration: none;">
                                <i class="fas fa-edit"></i>
                            </a>
                            <a onclick="Delete('/Admin/Category/Delete/${data}')" class="text-danger" style="cursor:pointer; text-decoration: none">
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
