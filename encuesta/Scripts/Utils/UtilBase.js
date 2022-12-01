var util = {};
(function (global) {
    var requestServer = function (accion, type, success, d, id) {
        var path = window.top.document.getElementById("path").value,
            objAjax = {};
        objAjax.cache = false;
        objAjax.type = type;
        objAjax.url = path + accion;
        objAjax.data = { "d": d };
        objAjax.success = function (data, textStatus, request) {
            if (request.getResponseHeader("X-Responded-JSON") != null && JSON.parse(request.getResponseHeader("X-Responded-JSON")).status == "401") { window.removeEventListener("beforeunload", noCerrarVentana); window.location.assign("/AccesoCotizador"); };
            if (typeof (id) == "undefined") {
                success(data);
            } else {
                success(data + "^" + id);
            }
            ocultarCargando();
        }
        objAjax.error = function (error) {
            ocultarCargando();
        };
        mostrarCargando();
        $.ajax(objAjax);
    }

    var requestServerSinLoader = function (accion, type, success, d, id) {
        var path = window.top.document.getElementById("path").value,
            objAjax = {};
        objAjax.cache = false;
        objAjax.type = type;
        objAjax.url = path + accion;
        objAjax.data = { "d": d };
        objAjax.success = function (data, textStatus, request) {
            if (request.getResponseHeader("X-Responded-JSON") != null && JSON.parse(request.getResponseHeader("X-Responded-JSON")).status == "401") { window.removeEventListener("beforeunload", noCerrarVentana); window.location.assign("/AccesoCotizador"); };
            if (typeof (id) == "undefined") {
                success(data);
            } else {
                success(data + "^" + id);
            }
        }
        objAjax.error = function (error) {
        };
        $.ajax(objAjax);
    }

    var mostrarCargando = function () {
        if ($('#loaderAjax1').css('display') == 'none') {
            $("#loaderAjax1").modal({ backdrop: "static" });
        }
    }

    var ocultarCargando = function () {
        $("#loaderAjax1").modal("hide");
        $('.modal-backdrop').remove();
    }

    var mostrarCargando = function (jq) {
        if (jq('#loaderAjax1').css('display') == 'none') {
            jq("#loaderAjax1").modal({ backdrop: "static" });
        }
    }

    var ocultarCargando = function (jq) {
        jq("#loaderAjax1").modal("hide");
    }

    var mostrarDialogInfo = function (mensaje) {
        $("#mensajeDialog .modal-body").html('<h5>' + mensaje + '</h5>');
        $("#mensajeDialog").modal();
    }
    var mostrarConfirmarDialog = function (elemento, alineacion, textoContenido, dataid, confirmarAccion) {
        var top = 0,
            left = 0,
            handlerOk,
            handlerCancel,
            idConfirm = "#confirmationDialog_" + elemento.id;

        handlerOk = function () {
            confirmarAccion(dataid);
        }

        handlerCancel = function () {
            ocultarConfirmarDialog(elemento);
        }

        if (alineacion == "left") {
            $(idConfirm).attr("class", "popover confirmation fade left in");
            $(idConfirm + " .arrow").css("left", "");
            $(idConfirm + " .arrow").css("top", "50%");
            $(idConfirm + " .confirmation-content").html(textoContenido);
            top = cumulativeOffset(elemento).top - $(idConfirm).height() / 2 + elemento.clientHeight / 2;
            left = cumulativeOffset(elemento).left - $(idConfirm).width();

            if (cumulativeOffset(elemento).left < $(idConfirm).width()) {
                mostrarConfirmarDialog(elemento, "top", textoContenido, dataid, confirmarAccion);
            } else {
                $(idConfirm).css("display", "block");
                $(idConfirm).css("top", top);
                $(idConfirm).css("left", left);
                $(idConfirm + " .confirmation-buttons .btn-success").unbind("click");
                $(idConfirm + " .confirmation-buttons .btn-success").bind("click", handlerOk);
                $(idConfirm + " .confirmation-buttons .btn-danger").unbind("click");
                $(idConfirm + " .confirmation-buttons .btn-danger").bind("click", handlerCancel);
            }

        } else if (alineacion == "top") {
            $(idConfirm).attr("class", "popover confirmation fade top in");
            $(idConfirm + " .arrow").css("top", "");
            $(idConfirm + " .arrow").css("left", "50%");
            $(idConfirm + " .confirmation-content").html(textoContenido);
            top = cumulativeOffset(elemento).top - $(idConfirm).height();
            left = cumulativeOffset(elemento).left - $(idConfirm).width() / 2 + elemento.clientWidth / 2;
            $(idConfirm).css("display", "block");
            $(idConfirm).css("top", top);
            $(idConfirm).css("left", left);

            $(idConfirm + " .confirmation-buttons .btn-success").unbind("click");
            $(idConfirm + " .confirmation-buttons .btn-success").bind("click", handlerOk);
            $(idConfirm + " .confirmation-buttons .btn-danger").unbind("click");
            $(idConfirm + " .confirmation-buttons .btn-danger").bind("click", handlerCancel);
        }
    }

    var ocultarConfirmarDialog = function (elemento) {
        var idConfirm = "#confirmationDialog_" + elemento.id;
        if ($(idConfirm).css("display") != "none") {
            $(idConfirm).css("display", "none");
            $(idConfirm + " .confirmation-content").html("");
        }
    }

    var cumulativeOffset = function (element) {
        var top = 0, left = 0;
        //do {
        top += element.offsetTop || 0;
        left += element.offsetLeft || 0;
        element = element.offsetParent;
        //} while (element);

        return {
            top: top,
            left: left
        };
    }

    var crearCombo = function (lista, nombreCombo, separador, nombreItem, valorItem, registroDuplicado, opcionTodos) {
        var contenido = "";
        if (nombreItem != null && valorItem != null) {
            contenido += "<option value='";
            contenido += valorItem;
            contenido += "'>";
            contenido += nombreItem;
            contenido += "</option>";
        }
        var nRegistros = lista.length;
        if (nRegistros > 0) {

            if (opcionTodos) {
                contenido += "<option value='T'>";
                contenido += "TODOS";
                contenido += "</option>";
            }

            var campos;
            if (registroDuplicado) {
                for (var i = 0; i < nRegistros; i++) {
                    campos = lista[i];
                    contenido += "<option value='";
                    contenido += campos;
                    contenido += "'>";
                    contenido += campos;
                    contenido += "</option>";
                }
            }
            else {
                for (var i = 0; i < nRegistros; i++) {
                    campos = lista[i].split(separador);
                    contenido += "<option value='";
                    contenido += campos[0];
                    contenido += "'>";
                    contenido += campos[1];
                    contenido += "</option>";
                }
            }
        }
        var cbo = document.getElementById(nombreCombo);
        if (cbo != null) cbo.innerHTML = contenido;
    }

    var autocomplete = function (inp, arr) {
        /*the autocomplete function takes two arguments,
        the text field element and an array of possible autocompleted values:*/
        var currentFocus;
        inp = document.getElementById(inp);
        /*execute a function when someone writes in the text field:*/
        inp.addEventListener("input", function (e) {
            var a, b, i, val = this.value;
            /*close any already open lists of autocompleted values*/
            closeAllLists();
            if (!val) { return false; }
            currentFocus = -1;
            /*create a DIV element that will contain the items (values):*/
            a = document.createElement("DIV");
            a.setAttribute("id", this.id + "autocomplete-list");
            a.setAttribute("class", "autocomplete-items");
            /*append the DIV element as a child of the autocomplete container:*/
            this.parentNode.appendChild(a);
            /*for each item in the array...*/
            for (i = 0; i < arr.length; i++) {
                /*check if the item contain with the some letters as the text field value:*/
                var value = arr[i][1];
                if (arr[i].length > 2) value += " - " + arr[i][2];

                if (value/*.normalize()*/.toUpperCase().indexOf(val/*.normalize()*/.toUpperCase()) > -1) {
                    /*create a DIV element for each matching element:*/
                    b = document.createElement("DIV");
                    /*make the matching letters bold:*/
                    b.innerHTML = value.resaltarNegritaA(val);
                    /*insert a input field that will hold the current array item's value:*/
                    b.innerHTML += "<input type='hidden' value='" + value + "' data-id='" + arr[i][0] + "^" + arr[i][1] + "'>";
                    /*execute a function when someone clicks on the item value (DIV element):*/
                    b.addEventListener("click", function (e) {
                        /*insert the value for the autocomplete text field:*/
                        inp.value = this.getElementsByTagName("input")[0].value;
                        inp.setAttribute("data-id", this.getElementsByTagName("input")[0].getAttribute("data-id"));
                        /*close the list of autocompleted values,
                        (or any other open lists of autocompleted values:*/
                        closeAllLists();
                    });
                    a.appendChild(b);
                }
            }
        });
        /*execute a function presses a key on the keyboard:*/
        inp.addEventListener("keydown", function (e) {
            var x = document.getElementById(this.id + "autocomplete-list");
            if (x) x = x.getElementsByTagName("div");
            if (e.keyCode == 40) {
                /*If the arrow DOWN key is pressed,
                increase the currentFocus variable:*/
                currentFocus++;
                /*and and make the current item more visible:*/
                addActive(x);
            } else if (e.keyCode == 38) { //up
                /*If the arrow UP key is pressed,
                decrease the currentFocus variable:*/
                currentFocus--;
                /*and and make the current item more visible:*/
                addActive(x);
            } else if (e.keyCode == 13) {
                /*If the ENTER key is pressed, prevent the form from being submitted,*/
                e.preventDefault();
                if (currentFocus > -1) {
                    /*and simulate a click on the "active" item:*/
                    if (x) x[currentFocus].click();
                }
            }
        });
        function addActive(x) {
            /*a function to classify an item as "active":*/
            if (!x) return false;
            /*start by removing the "active" class on all items:*/
            removeActive(x);
            if (currentFocus >= x.length) currentFocus = 0;
            if (currentFocus < 0) currentFocus = (x.length - 1);
            /*add class "autocomplete-active":*/
            x[currentFocus].classList.add("autocomplete-active");
        }
        function removeActive(x) {
            /*a function to remove the "active" class from all autocomplete items:*/
            for (var i = 0; i < x.length; i++) {
                x[i].classList.remove("autocomplete-active");
            }
        }
        function closeAllLists(elmnt) {
            /*close all autocomplete lists in the document,
            except the one passed as an argument:*/
            var x = document.getElementsByClassName("autocomplete-items");
            for (var i = 0; i < x.length; i++) {
                if (elmnt != x[i] && elmnt != inp) {
                    x[i].parentNode.removeChild(x[i]);
                }
            }
        }
        /*execute a function when someone clicks in the document:*/
        document.addEventListener("click", function (e) {
            closeAllLists(e.target);
        });
    }

    function roundNumber(num, scale) {
        if (!("" + num).includes("e")) {
            return +(Math.round(num + "e+" + scale) + "e-" + scale);
        } else {
            var arr = ("" + num).split("e");
            var sig = ""
            if (+arr[1] + scale > 0) {
                sig = "+";
            }
            return +(Math.round(+arr[0] + "e" + sig + (+arr[1] + scale)) + "e-" + scale);
        }
    }
    // Exponer nuestro módulo al módulo global
    global.requestServer = requestServer;
    global.requestServerSinLoader = requestServerSinLoader;
    global.crearCombo = crearCombo;
    global.autocomplete = autocomplete;
    global.mostrarCargando = mostrarCargando;
    global.ocultarCargando = ocultarCargando;
    global.mostrarDialogInfo = mostrarDialogInfo;
    global.mostrarConfirmarDialog = mostrarConfirmarDialog;
    global.ocultarConfirmarDialog = ocultarConfirmarDialog;
    global.roundNumber = roundNumber;

})(util);