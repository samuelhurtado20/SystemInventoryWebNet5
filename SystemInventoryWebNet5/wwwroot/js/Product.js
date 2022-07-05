var datatable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    datatable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Product/GetAll"
        },
        "columns": [
            { "data": "name", "width": "15%" },
            { "data": "serialNumber", "width": "15%" },
            { "data": "category.name", "width": "15%" },
            { "data": "brand.name", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a href="/Admin/Product/Upsert/${data}" class="btn btn-success" style="cursor:pointer; text-decoration: none;">
                                <i class="fas fa-edit"></i>
                            </a>
                            <a onclick="Delete('/Admin/Product/Delete/${data}')" class="btn btn-danger" style="cursor:pointer; text-decoration: none">
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
