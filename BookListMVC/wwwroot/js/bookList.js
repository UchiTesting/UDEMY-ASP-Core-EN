var dataTable;

$(document).ready(function () {
    loadDatatable();
});

function loadDatatable() {
    dataTable = $("#DT_load").DataTable({
        "ajax": {
            "url": "/Books/GetAll/",
            "type": "GET",
            "dataType": "json"
        },
        "columns": [
            {
                "data": "title",
                "width": "40%"
            },
            {
                "data": "author",
                "width": "20%"
            },
            {
                "data": "isbn",
                "width": "20%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a class="btn btn-sm btn-danger text-white"
                                   style="cursor:pointer;"
                                   onclick="Delete('/Books/Delete?id=${data}')">
                                   Delete</a>

                                <a href="/Books/Upsert?id=${data}" 
                                   class="btn btn-sm btn-success text-white"
                                   style="cursor:pointer;">
                                   Edit</a>
                            </div>`
                },
                "width": "20%"
            }
        ],
        "language": {
            "emptyTable": "No data found"
        },
        "width": "100%"
    });
}

function Delete(apiUrl) {
    swal({
        title: "Are you sure?",
        text: "This operation cannot be reverted.",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: apiUrl,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}