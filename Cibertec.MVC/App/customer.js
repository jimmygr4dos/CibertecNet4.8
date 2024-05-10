(function (customer) {
    //Paginación
    customer.pages = 1;
    customer.rowSize = 25;

    //SignalR
    customer.IDs = [];
    customer.recordInUse = false;
    customer.hub = {};

    //Mensajes
    customer.success = successReload;

    //SignalR
    customer.addCustomer = addCustomerID;
    customer.removeCustomer = removeCustomerID;
    customer.validate = validate;

    $(function () {
        connectToHub();
        init();
    })

    //init();

    return customer;

    //Función para los mensajes
    function successReload(option) {
        cibertec.closeModal(option);
    }

    //Funciones para el SignalR
    //El objetivo es bloquear el registro de Customer que está siendo usado
    function connectToHub() {
        customer.hub = $.connection.customerHub;
        customer.hub.client.customerStatus = customerStatus;
    }

    function customerStatus(customerIDs) {
        console.log(customerIDs);
        customer.IDs = customerIDs;
    }

    function addCustomerID(customerID) {
        console.log(customerID);
        //Llamamos al método AddCustomerID que está en el Servidor
        customer.hub.server.addCustomerID(customerID);
    }

    function removeCustomerID(customerID) {
        //Llamamos al método RemoveCustomerID que está en el Servidor
        customer.hub.server.removeCustomerID(customerID);
    }

    function validate(customerID) {
        customer.recordInUse = (customer.IDs.indexOf(customerID) > -1);
        if (customer.recordInUse) {
            $("#inUse").removeClass('hidden');
        }
    }
    //Fin de Funciones para el SignalR

    //Función para la paginación
    function init() {
        $.get('/Customer/Count/' + customer.rowSize,
            function (data) {
                customer.pages = data;
                $('.pagination').bootpag({
                    total: customer.pages,
                    page: 1,
                    maxVisible: 5,
                    leaps: true,
                    firstLastUse: true,
                    first: '<--',
                    last: '-->',
                    wrapClass: 'pagination',
                    activeClass: 'active',
                    disableClass: 'disabled',
                    nextClass: 'next',
                    prevClass: 'prev',
                    lastClass: 'last',
                    firstClass: 'first'
                }).on('page', function (event, num) {
                    getCustomers(num);
                });
                getCustomers(1);
            });
    }

    function getCustomers(num) {
        var url = '/Customer/List/' + num + '/' + customer.rowSize;
        $.get(url, function (data) {
            $('.content').html(data);
        });
    }

})(window.customer = window.customer || {});