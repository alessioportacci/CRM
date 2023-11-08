    let Servizi = "";
    function getSelected()
    {
        Servizi = ""
            $('input[type=checkbox]').each(function () {
                if (this.checked)
                    Servizi += `${this.value},`
            });
    $("#Servizi").val(Servizi);
        }
