$(function () {
    var l = abp.localization.getResource("XICommServer");
	
	var sendGridKeyService = window.xICommServer.sendGridKeys.sendGridKeys;
	
	
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + "SendGridKeys/CreateModal",
        scriptUrl: "/Pages/SendGridKeys/createModal.js",
        modalClass: "sendGridKeyCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "SendGridKeys/EditModal",
        scriptUrl: "/Pages/SendGridKeys/editModal.js",
        modalClass: "sendGridKeyEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            name: $("#NameFilter").val(),
			aPIKey: $("#APIKeyFilter").val(),
			displayName: $("#DisplayNameFilter").val(),
			emailAddress: $("#EmailAddressFilter").val(),
            isDefault: (function () {
                var value = $("#IsDefaultFilter").val();
                if (value === undefined || value === null || value === '') {
                    return '';
                }
                return value === 'true';
            })()
        };
    };

    var dataTable = $("#SendGridKeysTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: true,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(sendGridKeyService.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('XICommServer.SendGridKeys.Edit'),
                                action: function (data) {
                                    editModal.open({
                                     id: data.record.id
                                     });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('XICommServer.SendGridKeys.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    sendGridKeyService.delete(data.record.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
			{ data: "name" },
            {
                data: "aPIKey",
                render: function (aPIKey,row,qx) {
                    return qx.apiKey;
                }
            },
			{ data: "displayName" },
			{ data: "emailAddress" },
            {
                data: "isDefault",
                render: function (isDefault) {
                    return isDefault ? '<i class="fa fa-check"></i>' : '<i class="fa fa-times"></i>';
                }
            }
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $("#NewSendGridKeyButton").click(function (e) {
        e.preventDefault();
        createModal.open();
    });

	$("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    $("#ExportToExcelButton").click(function (e) {
        e.preventDefault();

        sendGridKeyService.getDownloadToken().then(
            function(result){
                    var input = getFilter();
                    var url =  abp.appPath + 'api/app/send-grid-keys/as-excel-file' + 
                        abp.utils.buildQueryString([
                            { name: 'downloadToken', value: result.token },
                            { name: 'filterText', value: input.filterText }, 
                            { name: 'name', value: input.name }, 
                            { name: 'aPIKey', value: input.aPIKey }, 
                            { name: 'displayName', value: input.displayName }, 
                            { name: 'emailAddress', value: input.emailAddress }, 
                            { name: 'isDefault', value: input.isDefault }
                            ]);
                            
                    var downloadWindow = window.open(url, '_blank');
                    downloadWindow.focus();
            }
        )
    });

    $('#AdvancedFilterSectionToggler').on('click', function (e) {
        $('#AdvancedFilterSection').toggle();
    });

    $('#AdvancedFilterSection').on('keypress', function (e) {
        if (e.which === 13) {
            dataTable.ajax.reload();
        }
    });

    $('#AdvancedFilterSection select').change(function() {
        dataTable.ajax.reload();
    });
    
    
});
