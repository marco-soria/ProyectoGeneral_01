﻿var dataTable;

$(document).ready(function () {
    cargarDataTable();
});


function cargarDataTable() {
    dataTable = $("#tblArticulos").DataTable({
        "ajax": {
            "url": "/Admin/Articulos/GetAll",
            "type": "GET",
            "datatype": "json"

        },
        "columns": [
            { "data": "id", "width": "5%" },
            { "data": "nombre", "width": "20%" },
            { "data": "categoria.nombre", "width": "15%" },
            {
                "data": "urlImagen",
                "render": function (image) {
                    return `<image src="../${image}" width="60%" height="60%">`
                }
            },
            { "data": "fechaCreacion", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Admin/Articulos/Edit/${data}" class="btn btn-success text-white" style="cursor:pointer; width:100px;">
                                <i class="far fa-edit"></i>Editar Articulo
                                </a>
                                &nbsp;
                                <a onclick=Delete("/Admin/Articulos/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer; width:100px;">
                                <i class="far fa-trash-alt"></i>Eliminar Articulo
                                </a>
                            </div>
                            `;
                }, "width": "30%"
            }
        ],
        "language": {
            "decimal": "",
            "emptyTable": "No ahi registros guardados",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
            "infoEmpty": "Mostrando 0 to 0 of 0 Entradas",
            "infoFiltered": "(Filtrado de _MAX_ total entradas)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Mostrar _MENU_ Entradas",
            "loadingRecords": "Cargando...",
            "processing": "Procesando...",
            "search": "Buscar:",
            "zeroRecords": "Sin resultados encontrados",
            "paginate": {
                "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        },
        "width": "100%"
    });
}

function Delete(url) {
    swal({
        title: "Estas seguro de borrar este Articulo?",
        text: "Este contenido no podra ser recuperado!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Si, Borar!",
        closeOnconfirm: true
    }, function () {
        $.ajax({
            type: 'DELETE',
            url: url,
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
    });
}