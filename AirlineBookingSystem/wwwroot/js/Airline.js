var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Airline/GetAll"
        },
        "columns": [
            { "data": "name", "width": "15%" },
            { "data": "code", "width": "20%" },
          
            {
                "data": "airlineId",
                "render": function (data) {
                    return `
                    <div class="text-center">
                        <button class="btn btn-info btn-edit">Edit</button>
                        <a class="btn btn-danger" onclick="Delete('/Admin/Airline/Delete/${data}')">
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
$('#tblData').on('click', '.btn-edit', function () {
    var row = $(this).closest('tr');
    row.find('td').prop('contenteditable', true);
    $(this).removeClass('btn-info').addClass('btn-success').text('Save').addClass('btn-save');
});

$('#tblData').on('click', '.btn-save', function () {
    var row = $(this).closest('tr');
    var editedData = {
        airlineId: dataTable.row(row).data().airlineId,
        name: row.find('td:eq(0)').text(),
        code: row.find('td:eq(1)').text()
    };
    var formData = new FormData();
    formData.append('AirlineId', editedData.airlineId);
    formData.append('Name', editedData.name);
    formData.append('Code', editedData.code);

    $.ajax({
        url: '/Admin/Airline/SaveOrEdit',
        type: 'POST',
        data: formData,
        contentType: false,
        processData: false,
        success: function (response) {
            if (response.success) {
                row.find('.btn-save').removeClass('btn-success').addClass('btn-info').text('Edit').removeClass('btn-save');
                row.find('td').prop('contenteditable', false);
                swal({
                    title: "Success",
                    text: "Data updated successfully!",
                    icon: "success",
                    button: "OK",
                });
            } else {
                toastr.error(response.message);
            }
        },
        error: function () {
            toastr.error("Failed to update data!");
        }
    });
});

