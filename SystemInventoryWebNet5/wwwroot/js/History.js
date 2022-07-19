var datatable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    datatable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Inventory/Inventory/GetHistory"
        },
        "columns": [
            {
                "data": "startDate", "width": "15%",
                "render": function (data) {
                    var d = new Date(data);
                    return d.toLocaleDateString();
                }
            },
            {
                "data": "endDate", "width": "15%",
                "render": function (data) {
                    var d = new Date(data);
                    return d.toLocaleDateString();
                }
            },
            { "data": "warehouse.name", "width": "15%" },
            {
                "data": function UserName(data) {
                    return data.userApp.name + " " + data.userApp.lastName;
                }, "width": "20%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a href="/Inventory/Inventory/DetailHistory/${data}" class="btn btn-primary text-white" style="cursor:pointer">
                             Detail
                            </a>                           
                        </div>
                        `;
                }, "width": "10%"
            }
        ]
    });
}