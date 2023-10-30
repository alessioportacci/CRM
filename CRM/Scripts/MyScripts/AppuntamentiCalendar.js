let calendar

function AddToCalendar(Appuntamento)
//Funzione che dato un appuntamento, restituisce l'oggetto nel formato leggibile dal calendario
{
    return {
        id: Appuntamento.id,
        title: Appuntamento.Tipologia,
        allDay: false,
        start: Appuntamento.Inizio,
        end: Appuntamento.Fine,
        //startTime: '10:00:00',
        //endTime: '3:00:00',
        //url: '../Appuntamenti/Details/' + Appuntamento.id,
        interactive: 'false',
        classNames: ['evento'],
        display: 'block',
        backgroundColor: Appuntamento.Colore,
        textColor: 'white',
        extendedProps:
        {
            department: 'BioChemistry'
        },
    }
}

function AddEvent()
//Funzione che cliccando un appuntamento, lo aggiunge al db/calendario
{
    //Mi creo il modello da passare al metodo
    let AppuntamentoModel =
    {
        FkCliente: $("#FkCliente").val(),
        FkTipologia: $("#FkTipologia").val(),
        Date: $("#Date").val(),
        OraInizio: $("#OraInizio").val(),
        OraFine: $("#OraFine").val(),
        Descrizione: $("#Descrizione").val(),
        Note: $("#Note").val(),
        VisibilitaGlobale: $("#VisibilitaGlobale").prop('checked')
    }

    $.ajax
        ({
            method: 'POST',
            url: "Create",
            data: AppuntamentoModel,
            success: function (Appuntamento)
            {
                calendar.addEvent(AddToCalendar(Appuntamento))
                $('#Modal').modal('toggle');
            },
            error: function (e)
            {
                console.log(e)
            },
        })
}

function UpdateEvent(Appuntamento)
//Funzione che dato un appuntamento spostato tramite drag n drop, aggiorna la sua data
{
    let data = Appuntamento.event.start.toISOString()

    $.ajax
        ({
            method: 'GET',
            url: "UpdateDataAppuntamento",
            data:
            {
                Id: Appuntamento.event.id,
                Date: data
            },
            success: function (appuntamento) { },
            error: function (e)
            {
                Appuntamento.revert();
                console.log(e)
            },
        })
}

function LoadDetails(IdAppuntamento)
//Funzione che dato un ID Apre la modale con le info sull'appuntamento
{
    $.ajax
        ({
            method: 'GET',
            url: "LoadAppuntamentoDetails",
            data: { Id: IdAppuntamento },
            success: function (appuntamento)
            {
                console.log(appuntamento)

                $('#Modal-vis-titolo').text(appuntamento.Tipologia + " con " + appuntamento.NomeCliente)
                $('#Modal-vis-link').html("<i class='fa-solid fa-link m-3 fw-bold'></i>")
                    
                $('#Modal-vis-data').text("Dalle " + appuntamento.Inizio.substring(11) + " alle " + appuntamento.Fine.substring(11))
                $('#Modal-vis-descrizione').text(appuntamento.Descrizione)
                $('#Modal-vis-note').text(appuntamento.Note)
                $('#Modal-vis-modifica').attr("onclick", "EditNote(" + appuntamento.id + ")")
                $('#Modal-vis-modifica-appuntamento').attr("href", "Edit/" + appuntamento.id + "")

                //Un po' di css per fare le modali a tema a seconda del tipo di appuntamento
                $('#Modal-vis-content').css("border", "4px solid" + appuntamento.Colore)
                $('#Modal-vis-modifica').css("background-color", appuntamento.Colore)
                $('#Modal-vis-header').css("background-color", appuntamento.Colore3)
                $('#Modal-vis-header').css("color", appuntamento.Colore)





                //Faccio sparire il modale
                $('#Modal-vis').modal('toggle');
            },
            error: function (e) {
                console.log(e)
            },
        })
}

function EditNote(IdAppuntamento)
{
    $.ajax
        ({
            method: 'GET',
            url: "EditNoteAppuntamento",
            data:
            {
                Id: IdAppuntamento,
                Note: $("#Modal-vis-note").val()
            },
            success: function (appuntamento)
            {
                $('#Modal-vis').modal('toggle');
                console.log(appuntamento)
            },
            error: function (e) {
                Appuntamento.revert();
                console.log(e)
            },
        })
}

document.addEventListener('DOMContentLoaded', function ()
{
    $.ajax
        ({
            method: 'GET',
            url: "LoadCalendar",
            success: function (Appuntamenti)
            {
                /* Inserisco tutti gli appuntamenti nella variabile Events che utlizzierò in seguito
                 * per caricare il calendario */
                console.log(Appuntamenti)
                let Events = []
                $.each(Appuntamenti, function (i, item)
                {
                    console.log(item)
                    Events.push(AddToCalendar(item))
                })

                //Mi prendo il div del calendario e lo utilizzo per renderizzare il calendario
                let calendarEl = document.getElementById('calendar');
                calendar = new FullCalendar.Calendar(calendarEl,
                    {
                        initialView: 'dayGridMonth',
                        locale: 'it',
                        selectable: true,
                        contentHeight: 800,
                        timeZone: 'UTC',
                        droppable: true,
                        editable: true,
                        events: Events,     //Variable con i miei appuntamenti

                        //Toolbar top e bot
                        headerToolbar:
                        {
                            start: 'title',
                            center: '',
                            end: 'prev,next'
                        },
                        footerToolbar:
                        {
                            start: 'dayGridMonth,timeGridWeek,listWeek',
                            end: 'today'
                        },

                        //Eventi giornalieri massimi visualizzabili
                        dayMaxEventRows: 4,

                        //Giorni non visualizzati (Domenica)
                        hiddenDays: [0],

                        //Giorni cliccabili per visualizzazione giornaliera
                        navLinks: true,

                        //Indicatore dell'ora corrente
                        //nowIndicator: true,

                        //Cambio il testo dei pulsanti
                        buttonText:
                        {
                            today: 'Oggi',
                            month: 'Mese',
                            week: 'Setimana',
                            day: 'Giorno',
                            list: 'Lista',
                        },

                        //Funzione al click di una data
                        dateClick: function (info)
                        {
                            //La data su cui ho cliccato
                            //alert('Hai cliccato su: ' + info.dateStr);

                            //Assegno la data del giorno cliccato al form (campo hidden)
                            $("#Date").val(info.dateStr.substring(0, 10))
                            $("#AggiungiTitle").text("Nuovo appuntamento per il " + info.dateStr.substring(8, 10) + "/" + info.dateStr.substring(5, 7))

                            //Se clicco sulla visualizzazione settimanale, imposto anche l'ora
                            if (info.view.type === "timeGridWeek")
                                $("#OraInizio").val(info.dateStr.substring(11, 16))

                            //Faccio apparire il modale
                            $('#Modal').modal('toggle');


                            // Rosso temporaneo per vedere che ho cliccato un giorno
                            // info.dayEl.style.backgroundColor = 'red';
                        },

                        //Funzione al trascinare di un evento
                        eventDrop: function (info)
                        {
                            UpdateEvent(info)
                            //alert(info.event.id + " è stato spostato su " + info.event.start.toISOString());
                        },

                        //Funzione al click di un evento
                        eventClick: function (info)
                        {
                            LoadDetails(info.event.id)
                        }

                    });
                //Renderizzo il calendario
                $("#calendario-spinner").addClass("d-none")
                calendar.render();

            },
            error: function (e) {
                console.log(e)
            }
        })
});


