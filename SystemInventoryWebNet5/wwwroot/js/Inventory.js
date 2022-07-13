var datatable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    datatable = $('#tblDatos').DataTable({
        "ajax": {
            "url": "/Inventory/Inventory/GetAll"
        },
        "columns": [
            { "data": "warehouse.name", "width": "20%" },
            { "data": "product.name", "width": "30%" },
            { "data": "product.cost", "width": "10%", "className": "text-right" },
            { "data": "amount", "width": "10%", "className": "text-right" }
        ]
    });
}