var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Flight/GetAll"
        },
        "columns": [
            { "data": "airline.name", "width": "15%" }, 
            { "data": "flightNumber", "width": "10%" },
            {
                "data": "imageUrl",
                "render": function (data) {
                    if (data) {
                        return `<img src="${data}" alt="Image" style="width: 100px; height: auto;"/>`;
                    } else {
                        return 'NA';
                    }
                },
                "width": "10%"
            },
            { "data": "departureAirport", "width": "10%" },
            { "data": "destinationAirport", "width": "10%" },
            { "data": "departureTime", "width": "10%" },
            { "data": "arrivalTime", "width": "10%" }, 
            { "data": "availableSeats", "width": "10%" }, 
            {
                "data": "flightId",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a href="/Admin/Flight/SaveOrEdit/${data}" class="btn btn-info">
                                <i class="fas fa-edit"></i>
                            </a>
                            <a class="btn btn-danger" onclick="deleteFlight('/Admin/Flight/Delete/${data}')">
                                <i class="fas fa-trash"></i>
                            </a>
                        </div>`;
                }
            }
        ]
    });
}

function deleteFlight(url) {
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover this flight!",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                $.ajax({
                    url: url,
                    type: 'DELETE',
                    success: function (data) {
                        if (data.success) {
                            dataTable.ajax.reload();
                            toastr.success(data.message);
                        } else {
                            toastr.error(data.message);
                        }
                    }
                });
            }
        });
}
