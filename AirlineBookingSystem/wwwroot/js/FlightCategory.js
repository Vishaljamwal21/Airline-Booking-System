var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/FlightCategory/GetAll"
        },
        "columns": [
            { "data": "name", "width": "15%" },
            { "data": "price", "width": "20%" },

            {
                "data": "flightCategoryId",
                "render": function (data) {
                    return `
                    <div class="text-center">
                        <a href="/Admin/FlightCategory/SaveOrEdit/${data}" class="btn btn-info">
                            <i class="fas fa-edit"></i>
                        </a>
                        <a class="btn btn-danger" onclick="Delete('/Admin/FlightCategory/Delete/${data}')">
                            <i class="fas fa-trash"></i>
                        </a>
                    </div>`;
                }
            }
        ]
    });
}

function Delete(url) {
    swal({
        title: "Want to delete data?",
        text: "Delete information!!!",
        icon: "warning",
        buttons: true,
        dangerModel: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}
