// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function DesassociarFilme() {

    $.ajax({
        url: this.href,
        type: "delete",
        success: function (data, status, xhr) {
            $("#minhas-tabelas").html(data);
        },
        error: function (jqXhr, textStatus, errorMessage) {
            $("#minhas-tabelas").html("<div class='alert alert-danger'>" + errorMessage + "</div>");
        },
        beforeSend: function () {
            $("#minhas-tabelas").html("<h5 class='text-danger'>Aguarde... atualizando dados...</h5>");
        }
    });

    return false;
}