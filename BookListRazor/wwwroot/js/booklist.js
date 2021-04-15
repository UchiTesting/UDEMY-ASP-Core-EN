var datatable;

$(document).ready(function () {
    loadDatatable();
});

function loadDatatable() {
    datatable = $("#DT_load").DataTable({
        "ajax": {
            "url": "/apimvc/Book",
            "type": "GET",
            "daraSrc":"",
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
                                   style="cursor:pointer;">
                                   Delete</a>

                                <a href="/BookList/EditBook?id=${data}" 
                                   class="btn btn-sm btn-success text-white"
                                   style="cursor:pointer;">
                                   Edit</a>
                            </div>`
                },
                "width":"20%"
            }
        ],
        "language": {
            "emptyTable":"No data found"
        },
        "width": "100%"
    });
}