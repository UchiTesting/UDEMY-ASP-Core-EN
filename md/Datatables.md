Extra JS libraries
==========

## DataTables

DataTables needs to do AJAX calls to retrieve its data hence implementing an API.
Also it is trageting some specific elements in the page to hook.

The important bit with Datatables library is it is simpler to work with a table that has an id attribute.
In jQuery, we will select that element and chain the `Datatables()` method which take an object that holds the configuration.

This condiguration has 2 main parts:
- AJAX config in the `"ajax"` property to provide: 
  - the URL of the API endpoint
  - the HTTP method to be used (liely GET)
  - the data type (likely JSON)
- The column definition in the `"columns"` property.
  - `"data"` property allows to specify which property in the data is relevant and to be displayed in the defined column.
  - `"render"` may be optionlly defined to customize the rendering of a cell in the defined column. Likely to receive an item `id` for administrative tasks.
  - `"width"` is self explanatory

> After deletion is complete, we use `dataTable.ajax.reload()` to update the content of the table. Notice the Razor generated version is not updated until the view is refreshed which is perfectly normal.

## SweetAlert

It provides a more elegant alert popup. It is called with `swal()` that takes an object for setup.
It uses the `then()` method to implement the action to be taken if the <kbd>OK</kbd> button has been pressed.

## Toastr

Toastr is simple to use. It defines a `toastr` object which has  methods `success()` or `error()` that takes a message as parameter.

```javascript
toastr.success(data.message);
```
>  
```javascript
toastr.error(data.message);
```