//LOADINNG
function IniciarLoading() {
    Swal.fire({
        icon: "info",
        allowOutsideClick: false,
        title: 'Procesando Información',
        text: 'Espere, por favor!...',
        timer: 50000
    });
    Swal.showLoading();
}

function DetenerLoading() {
    Swal.close();
}

function MensajeSesionFinalizada() {
    Swal.fire({
        icon: 'error',
        title: 'Alerta',
        text: 'La sesión ha caducado',
        type: "error",
        timer: 3000,
        confirmButtonText: 'OK',
        allowOutsideClick: false,
    });
}

function MensajeSesionFinalizadaConfirmacionRedireccion(url) {
    Swal.fire({
        title: 'Alerta',
        text: 'La sesión ha caducado',
        icon: 'error',
        confirmButtonText: 'OK',
        allowOutsideClick: false,
    }).then(function () {
        window.location.href = url;
    });
}

function MensajeExitosoSwalConfirmacionRedireccion(Mensaje, modulo, url) {
    Swal.fire({
        title: modulo,
        html: Mensaje,
        icon: 'success',
        confirmButtonText: 'OK',
        allowOutsideClick: false,
    }).then(function () {
        window.location.href = url;
    });
}

function MensajeGrowl(Mensaje) {
    DetenerLoading();
    $.toast({
        heading: '¡Alerta!',
        text: Mensaje,
        position: 'top-right',
        stack: 6,
        hideAfter: 5000,
        showHideTransition: 'fade',
        icon: 'error'
    });
}

function MensajeGrowlSwal(Mensaje) {
    DetenerLoading();
    Swal.fire({
        icon: 'error',
        title: 'EDOCPYME',
        html: Mensaje,
        type: "error",
        confirmButtonText: 'OK',
        allowOutsideClick: false,
    });
}

function MensajeErrorSwal(mensaje, modulo) {
    DetenerLoading();
    Swal.fire({
        icon: "error",
        title: modulo,
        html: mensaje
    });
}

function MensajeExitoso(Mensaje) {
    DetenerLoading();
    $.toast({
        heading: 'Exitoso',
        text: Mensaje,
        position: 'top-right',
        stack: 5,
        showHideTransition: 'fade',
        icon: 'success'
    });
}

function MsgAjaxError(result) {
    DetenerLoading();
    var msg = "Existió un error, favor intentelo de nuevo.";
    var UrlBaseSite = $("#UrlBaseSite").val();
    console.log(UrlBaseSite);
    try {
        if (result.status === 401) {
            MensajeSesionFinalizadaConfirmacionRedireccion(UrlBaseSite);
        } else if (result.status === 403) {
            MensajeGrowl('El usuario no tiene permiso para acceder a este contenido');
        } else if (result.status === 404) {
            MensajeGrowl('Error al procesar la solicitud.');
        } else {
            MensajeGrowl(msg);
        }
    } catch (e) {
        MensajeGrowl(e);
    }
}

function ConfirmacionEliminar(urlAction, urlRedirect, data) {
    Swal.fire({
        title: 'Eliminar registro',
        text: "¿Está seguro de eliminar este registro? ¡No podrás revertir esto!",
        icon: 'question',
        showCancelButton: true,
        cancelButtonText: 'Cancelar',
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Eliminar'
    }).then((result) => {
        if (result.value) {
            IniciarLoading();
            $.post(urlAction, data, function (response) {
                DetenerLoading();
                if (response.estado) {
                    MensajeExitosoSwalConfirmacionRedireccion(response.mensaje, "", urlRedirect);
                } else {
                    MensajeGrowl(response.mensaje);
                }
            }).fail(function (error) {
                MsgAjaxError(error);
            });
        }
    });
}


function ConfirmacionEditar(urlAction, urlRedirect, data) {
    Swal.fire({
        title: 'Editar registro',
        text: "¿Está seguro de editar este registro? ¡No podrás revertir esto!",
        icon: 'question',
        showCancelButton: true,
        cancelButtonText: 'Cancelar',
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'EditarAgente'
    }).then((result) => {
        if (result.value) {
            IniciarLoading();
            $.post(urlAction, data, function (response) {
                DetenerLoading();
                if (response.estado) {
                    MensajeExitosoSwalConfirmacionRedireccion(response.mensaje, "", urlRedirect);
                } else {
                    MensajeGrowl(response.mensaje);
                }
            }).fail(function (error) {
                MsgAjaxError(error);
            });
        }
    });
}

function isEmpty(val) {
    return (val === undefined || val === null || val.length <= 0 || val.length === "") ? true : false;
}