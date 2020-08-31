var productdetail = ["(non apribile)", "", "soglia bassa in alluminio", "con nodo centrale mobile", "", "", "soglia bassa in alluminio", "soglia bassa in alluminio", "soglia bassa in alluminio"];

var n = 0;
$(".products")
    .mouseenter(function () {
        n += 1;
        var alt = $(this).attr('alt');
        var id = $(this).attr('id');

        $('#productdetail').text(alt);
        $('#productsubdetail').text(productdetail[id - 1]);
    })
    .mouseleave(function () {
        $('#productdetail').html('<div class=\"alert alert-danger topmargin text-center\"><strong>Errore!</strong> Nessun prodotto selezionato !</div>');
        $('#productsubdetail').text("");
    });

$(document).ready(function () {
    var aa = $('.productdet');
    aa.height = window.innerHeight;
    wprowadzaniedx();
    $('#direction').on('change', strona);

    $(function () {
        $('[data-toggle="tooltip"]').tooltip();
    });
});



function zmiana(id) {
    var quantity = $("#quantity_" + id).val();
    var price = $("#price_" + id).text();
    var amount = parseFloat(price) * parseFloat(quantity);
    $("#amount_" + id).text(amount);
    var sumamount = 0;
    $('.amount').each(function () {
        sumamount += parseFloat($(this).text());
    });
    $("#topamount").val(sumamount);

    var sumquant = 0;
    $('.quantity').each(function () {
        sumquant += parseFloat($(this).val());
    });
    $("#topquantity").val(sumquant);

}
function strona() {
    var direction = $('#direction').is(':checked');

    if (direction) {
        $("input[name='strona']").val(2);
        imptotale();
    } else {
        $("input[name='strona']").val(1);
        imptotale();

    }
}

$('#addtobasket').on("click", function () {

    if (document.form1.ilosc.value == 0) {
        Swal.fire(
            'Nessuna quantità?',
            'si prega di selezionare la quantità',
            'question'
        );
    } else {
        var doorhandle;
        var url = window.location.pathname;
        var productid = url.substring(url.lastIndexOf('/') + 1);
        var quantity = document.form1.ilosc.value;
        ///////////////////////////////////////////////////////////////////
        var price = document.form1.dodatki.value;
        ///////////////////////////////////////////////////////////////////
        var color = $("select[name='kolorprofili'] option:selected").text();
        var system = $("select[name='system'] option:selected").text();
        var glasslam = document.form1.konfiguracja.value;
        var direction = document.form1.strona.value == 2 ? "destra" : "sinistra";
        var glassqnt = document.form1.kolorszkla.value;
        var width = document.form1.szer.value;
        var height = document.form1.wys.value;
        var isarmdfixed = $("#armfixed").is(":checked");
        var armdirection="";
        var armtype = "";
        var armcm = "";
        var armlatch = "";
        
        if (isarmdfixed) {
            armtype = "Standard";
            doorhandle = "Standard";
        } else {
            armdirection = $('input[name=armoptions]:checked').val();
            armcm = $("#armcm").val() + " CM";
            armtype = "speciale - " + armdirection + " " + armcm;
            doorhandle = $("select[name='door'] option:selected").text();
        }
        if (productLimits.ModelId === 11) {
            var islatch = $("#productVasistas").is(":checked");
            if (islatch) {
                armlatch = "crichetto";
            } else {
                armlatch = "Maniglia";
            }
        } else {
            armlatch = "Maniglia";
        }
       
        //document.form1.ilosc.value = 0
        var product = {
            ProductId: productid,
            Quantity: quantity,
            Price: price,
            ColorName: color,
            System: system,
            GlassLam: glasslam,
            Direction: direction,
            GlassQnt: glassqnt,
            Width: width,
            Height: height,
            ArmType: armtype,
            DoorHandle: doorhandle,
            LatchArm: armlatch,
            UpOpenning: radioLeftRight
        };
        $.ajax({
            type: "POST",
            url: "/Product/Index",
            data: product,
            success: function (data) {
                var x = parseInt($("#badgecount").text()) + 1;
                document.getElementById('badgecount').innerText=x;
                document.form1.ilosc.value = 0;
                document.form1.dodatki.value = 0;
                document.getElementById('cenaokna').innerHTML = "0";
                Swal.fire(
                    'Success',
                    'Aggiunto.',
                    'success'
                ).then((result) => {
                    if (result.value) {

                    }
                });


            }
        });

    }

});
function deneme() {
    $.ajax({
        type: "GET",
        url: "/Product/GetProducts",
        data: {},
        success: function (data) {
            tempData = data;
        }
    });
}
var modalcolors = [
    {
        colorname: "bianco",
        colorurl: "probkabianco.jpg"
    },
    {
        colorname: "laminate bianco",
        colorurl: "probkabianco.jpg"
    },
    {
        colorname: "golden oak",
        colorurl: "probkadab.jpg"
    },
    {
        colorname: "noce",
        colorurl: "probkanoce1.jpg"
    },
    {
        colorname: "mogano",
        colorurl: "probkamogano.jpg"
    },
    {
        colorname: "winchester",
        colorurl: "probkairish.jpg"
    },
    {
        colorname: "verde muschio",
        colorurl: "verde1.jpg"
    }];
function modalcolorchange() {
    var imgurl = colorsearch($("#modalprofilecolor").val());
    $("#modalcolorimg").attr("src", "/Content/img/" + imgurl);
}
function colorsearch(nameKey) {
    for (var i = 0; i < modalcolors.length; i++) {
        if (modalcolors[i].colorname === nameKey) {
            return modalcolors[i].colorurl;
        }
    }
}
Handlebars.registerHelper('selected', function (value) {
    var html = "";
    for (var i = 1; i <= 10; i++) {
        var selected = "";
        if (i == value) {
            selected = "selected";
        }
        html += '<option value="' + i + '" ' + selected + ' >' + i + '</option>';
    }
    return html;
});

Handlebars.registerHelper('multiplicate', function (quantity,price) {
    var amount = (quantity * price);
    return amount.toFixed(2);
});

Handlebars.registerHelper('rownumber', function (index) {

    return index + 1;
});


Handlebars.registerHelper('convDate', function (date) {
    date = new Date(parseInt(date.substr(6)));
    return date.toLocaleDateString() + " - " + date.toLocaleTimeString();
});