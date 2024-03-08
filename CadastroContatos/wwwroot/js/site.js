// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// Write your JavaScript code.

$(document).ready(function () {
    //getDatatable('#table-contatos');
    //getDatatable2('#table-usuarios');

    

    function checkScreenSize() {
        var screenWidth = $(window).width();

        try
        {
            if (screenWidth < 768) {
                console.log("tela pequena")

                if ($.fn.DataTable.isDataTable('#table-contatos')) {
                    $('#table-contatos').DataTable().destroy();
                }

                getDatatable2('#table-contatos');

                if ($.fn.DataTable.isDataTable('#table-usuarios')) {
                    $('#table-usuarios').DataTable().destroy();
                }
                getDatatable2('#table-usuarios');
            } else {
                console.log("tela grande")

                if ($.fn.DataTable.isDataTable('#table-contatos')) {
                    $('#table-contatos').DataTable().destroy();
                }
                getDatatable('#table-contatos');

                if ($.fn.DataTable.isDataTable('#table-usuarios')) {
                    $('#table-usuarios').DataTable().destroy();
                }
                getDatatable('#table-usuarios');

                // Aqui você pode chamar uma função para criar um DataTable mais avançado, se necessário.
            }
        }
        catch
        {

        }
    }    // Chama a função ao carregar a página e redimensionar a tela
    $(window).on('load resize', function () {
        checkScreenSize();
    });

    $('.btn-total-contatos').click(function () {
        var usuarioId = $(this).attr('usuario-id');


        $.ajax({
                type:'GET',
            url: '/Usuario/ListarContatosPorUsuarioId/' + usuarioId,
            success: function (result) {
                $('#modalContatosUser').modal('show');
                $('#listaContatosUsuario').html(result);
                //getDatatable('#table-contatos-usuario');

            }
        });
    })


    $('.close-modal').click(function () {
        $('#modalContatosUser').modal('hide');
    });
})


function getDatatable2(id) {
    $(id).DataTable({
        "responsive": true,
        columnDefs: [
            { responsivePriority: 1, targets: 0 }, // ID column
            { responsivePriority: 1, targets: 1 }, // Name column
            { responsivePriority: 1, targets: 2 }, // Login column
            { responsivePriority: 1, targets: 3 } // Email column
        //    { responsivePriority: 1, targets: 4 }, // perfil column
        //    { responsivePriority: 1, targets: 5 }, // total column
        //    { responsivePriority: 5, targets: 6 }, // data column
        //    { responsivePriority: 10, targets: 7 } // btn column
        ],
        responsive: {
            details: false
        },
        "ordering": true,
        "paging": false,
        "searching": false
      
    });

}
function getDatatable(id) {
    $(id).DataTable({
        "ordering": true,
        "paging": true,
        "searching": true,
        "oLanguage": {
            "sEmptyTable": "Nenhum registro encontrado na tabela",
            "sInfo": "Mostrar _START_ at&eacute; _END_ de _TOTAL_ registros",
            "sInfoEmpty": "Mostrar 0 at&eacute; 0 de 0 Registros",
            "sInfoFiltered": "(Filtrar de _MAX_ total registros)",
            "sInfoPostFix": "",
            "sInfoThousands": ".",
            "sLengthMenu": "Mostrar _MENU_ registros por pagina",
            "sLoadingRecords": "Carregando...",
            "sProcessing": "Processando...",
            "sZeroRecords": "Nenhum registro encontrado",
            "sSearch": "Pesquisar",
            "oPaginate": {
                "sNext": "Proximo",
                "sPrevious": "Anterior",
                "sFirst": "Primeiro",
                "sLast": "Ultimo"
            },
            "oAria": {
                "sSortAscending": ": Ordenar colunas de forma ascendente",
                "sSortDescending": ": Ordenar colunas de forma descendente"
            }
        }
    });
}
$('.close-alert').click(function () {
    $('.alert').hide('hide')
});

