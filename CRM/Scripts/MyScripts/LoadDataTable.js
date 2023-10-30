
function LoadDataTable(iconsColumn)
/*Funzione che carica la datadable, prende in input un numero che andrà ad indicare la colonna
   dove sono presenti le icone su cui non bisogna filtrare*/
{
    let table = new DataTable('#myTable',
        {
            //Tabella responsive per smartphones
            responsive: true,

            //Traduce il testo a schermo indicato
            "language":
            {
                "lengthMenu": "_MENU_  per pagina",
                "zeroRecords": "Nessun elemento trovato",
                "info": "Pagina _PAGE_ di _PAGES_",
                "infoEmpty": "Nessun elemento disponibile",
                "infoFiltered": "(su _MAX_ totali)",
                "sSearch": "Cerca:",
                "oPaginate":
                {
                    "sFirst": "Primo",
                    "sLast": "Ultimo",
                    "sNext": "Succ.",
                    "sPrevious": "Prec."
                },
            },

            //Applica delle proprietà specifiche ad alcune colonne
            columnDefs:
            [{
                'targets': iconsColumn, //Lista delle colonne prese in causa
                'orderable': false,       //Disabilito l'ordinamento
            }],
    });
}
