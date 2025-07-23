$(document).ready(function () {

  $('#exampleModal').on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget);
    var id = button.data('id');
    var nombre = button.data('nombre'); 
    var estado = button.data('estado');
    var estadoTexto = "";

    if (estado == "True") {
      estado = false;
      estadoTexto = "¿Desea inactivar el producto " + nombre + "?";
    }
    else {
      estado = true;
      estadoTexto = "¿Desea activar el producto " + nombre + "?";
    }

    $("#IdProducto").val(id);
    $("#Nombre").val(nombre);
    $("#Estado").val(estado);
    $("#EstadoTexto").text(estadoTexto);
  })

  var table = new DataTable('#TablaDatos', {
    language: {
      url: 'https://cdn.datatables.net/plug-ins/2.3.2/i18n/es-ES.json',
    },
  });
});