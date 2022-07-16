var datatable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    datatable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/User/GetAll"
        },
        "columns": [
            { "data": "name", "width": "15%" },
            { "data": "lastName", "width": "15%" },
            { "data": "email", "width": "20%" },
            { "data": "userName", "width": "20%" },
            { "data": "phoneNumber", "width": "15%" },
            {
                "data": { id: "id", lockoutEnd: "lockoutEnd" },
                "render": function (data)
                {
                    //return data.id;
                    var today = new Date().getTime();
                    var lock = new Date(data.lockoutEnd).getTime();
                    if (lock > today)
                    {
                        return `
                        <div class="text-center">
                            <a onclick="LockUnlock('${data.id}')" class="btn btn-danger text-white" style="cursor:pointer; width: 150px;">
                                <i class="fas fa-lock-open"></i> UnLock
                            </a>
                        </div>
                        `;
                    }
                    else
                    {
                        return `
                        <div class="text-center">
                            <a onclick="LockUnlock('${data.id}')" class="btn btn-success text-white" style="cursor:pointer; width: 150px;">
                                <i class="fas fa-lock"></i> Lock
                            </a>
                        </div>
                        `;
                    }
                }, "width": "15%"
            }
        ]
    });
}

function LockUnlock(id) {
    $.ajax({
        type: "POST",
        url: '/Admin/User/LockUnlock',
        contentType: 'application/json',
        data: JSON.stringify(id),
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
