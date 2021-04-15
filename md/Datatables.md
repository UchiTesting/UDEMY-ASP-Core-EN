Datatables
==========

DataTables needs to do AJAX calls to retrieve its data hence implementing an API.
Also it is trageting some specific elements in the page to hook.

The important bit with Datatables library is it is simpler to work with a table that has an id attribute.
In jQuery, we will select that element and chain the `Datatables()` method which take an object that holds the configuration.