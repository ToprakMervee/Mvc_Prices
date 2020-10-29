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
    //wprowadzaniedx();
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

const seal = [
    { exp:'interno nero, esterno nero'},
    { exp:'interno grigio, esterno nero'},
    { exp:'interno nero, esterno grigio'},
    { exp:'interno grigio, esterno grigio'}
    ];

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
function getCheckedBoxes(chkboxName) {
    var checkboxes = document.getElementsByName(chkboxName);
    var checkboxesChecked = [];
    // loop over them all
    for (var i = 0; i < checkboxes.length; i++) {
        // And stick the checked ones onto an array...
        if (checkboxes[i].checked) {
            checkboxesChecked.push(checkboxes[i].value);
        }
    }
    // Return the array if it is non-empty, or null
    return checkboxesChecked.length > 0 ? checkboxesChecked : [];
}
$('#btnAddtoCart').on("click", function() {
    var selectedOne = getSelectedOne();
    var checkForm = checkNextPage();
    if (!checkForm) {
        Swal.fire(
            'Errore',
            'Aggiunto.',
            'error'
        ).then((result) => {
            if (result.value) {

            }
        });
        return;
    }
    Hesap();
    var renkler = wobject.colors;
    var profiller = wobject.profiles;
    var system = document.configuratorForm.profil.value;
    var productid = selectedOne.id;
    var quantity = 1;
    ///////////////////////////////////////////////////////////////////
    var price = document.configuratorForm.finalPrice.value;
    ///////////////////////////////////////////////////////////////////
    var color = document.configuratorForm.a_renk.value;
    var colorInd = document.configuratorForm.a_185.value;
    
    var colorSide = color === "0" ? "" : wobject.decors[colorInd].name;
    var width = document.configuratorForm.a_width.value;
    var height = document.configuratorForm.a_height.value;
    var width2 = [];
    var downInps = $('input[name^="layoutDown"');
    for (let i = 0; i < downInps.length; i++) {
        width2.push(downInps[i].value);
    }
    var height2 = [];
    var rightInps = $('input[name^="layoutRight"');
    for (let i = 0; i < rightInps.length; i++) {
        height2.push(rightInps[i].value);
    }
    var camSayi = document.configuratorForm.a_186.value;
    var lamineTaraf = document.configuratorForm.a_191.value;
    var profilEk = document.configuratorForm.a_187.value =="1";
    var pervazTaraf = profilEk ? getCheckedBoxes("pervazTaraf") : [];
    var satinemi = document.configuratorForm.a_193.value == "1";
    var kol = document.configuratorForm.a_kol.value;
    var kolyeri = document.configuratorForm.armcm.value +"-CM";
    var kolyonu = document.configuratorForm.armoptions.value;
    if (kolyeri=="0") {
        kolyeri = "";
        kolyonu = "";
    }
    var bagprofilivarmi = document.configuratorForm.a_297.value == "1";
    var bagprofiltipi = document.configuratorForm.a_495.value;
    var bagprofilyon = getCheckedBoxes('cpSide');
    if (!bagprofilivarmi) {
        bagprofiltipi = "0";
        bagprofilyon = [];
    }
  
    var islem = "";
    if (profilEk) {
        islem = system == "1" ? "Tagliare Il Davanzale" : "Aletta In Casso";
    }
    var sogliabassa = false;


    var extra = {
        width2: width2, height2: height2, profIslem: islem, pITaraf: pervazTaraf, satinemi: satinemi,
        selected: selectedOne, profil: wobject.profiles[system].name, colorname: wobject.colors[color].name,
        colorSide: colorSide, handle: wobject.handles[kol].name, bagprofil: wobject.connection[bagprofiltipi].name,
        bagprofilvarmi: bagprofilivarmi, bagprofilyon: bagprofilyon, type: wobject.type, colorimg: wobject.colors[color].img, process: profilEk, kolyeri: kolyeri,
        kolyonu:kolyonu
    };
    var extraStr = JSON.stringify(extra);
    //document.form1.ilosc.value = 0
    var product = {
        ProductId: productid,
        Quantity: quantity,
        Price: price,
        ColorName: color,
        System: system,
        GlassLam: lamineTaraf,
        GlassQnt: camSayi,
        Width: width,
        Height: height,
        UpOpenning: selectedOne.name,
        DoorHandle: kol,
        LatoD: colorInd,
        Extra: extraStr
    };
    $.ajax({
        type: "POST",
        url: "/Product/Index",
        data: product,
        success: function (data) {
            nextPage(1);
            updateBasketIcon();
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
});

let updateBasketIcon = () => {
    let badge = $('span[id="badgecount"]');
    let number = ++badge[0].innerText;
    badge.text(number);
    badge.css('background-color', 'red');
    setTimeout(() => badge.css('background-color', 'lightblue'),3000 );
}

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
    for (var i = 1; i <= 50; i++) {
        var selected = "";
        if (i === value) {
            selected = "selected";
        }
        html += '<option value="' + i + '" ' + selected + ' >' + i + '</option>';
    }
    return new Handlebars.SafeString(html);
});

Handlebars.registerHelper('isActive', (value) => {
    var activeClass = value == "0" ? "active customer-click" : "";
    return activeClass;
});

Handlebars.registerHelper('isDisabled', (value) => {
    var activeClass = value === 2 ? "disabled" : "";
    return activeClass;
});

Handlebars.registerHelper('whichPart', (index, type = 0) => {
    var partname = Array('Superiore', 'Inferiore');
    var partDetail = Array('misurare dal bordo superiore al centro', 'misurare dal centro al bordo inferiore');
    return type === 0 ? partname[index] : partDetail[index];
});


Handlebars.registerHelper('isChecked', (value) => {
    var checked = value == "0" ? "checked" : "";
    return checked;
});

Handlebars.registerHelper('multiplicate', (quantity, price) => {
    var amount = (quantity * price);
    return amount.toFixed(0);
});
Handlebars.registerHelper('divide', (quantity, price) => {
    var amount = (quantity / price);
    return amount.toFixed(2);
});
Handlebars.registerHelper('average', (minval, maxval) => {
    var average = Math.ceil((minval + (maxval - minval) / 2) / 10) * 10;
    return average.toFixed(0);
});
Handlebars.registerHelper('rownumber', function (index) {

    return index + 1;
});
Handlebars.registerHelper('isBigger', function (value) {

    return value > 0;
});
Handlebars.registerHelper('times', function (n, block) {
    var accum = '';
    for (var i = 0; i < n; ++i)
        accum += block.fn(i);
    return accum;
});

Handlebars.registerHelper('convDate', function (date) {
    date = new Date(parseInt(date.substr(6)));
    return date.toLocaleDateString() + " - " + date.toLocaleTimeString();
});

