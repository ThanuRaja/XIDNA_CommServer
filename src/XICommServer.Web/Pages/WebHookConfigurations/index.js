$(function () {
    var l = abp.localization.getResource("XICommServer");
	
	var webHookConfigurationService = window.xICommServer.webHookConfigurations.webHookConfigurations;
	
	
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + "WebHookConfigurations/CreateModal",
        scriptUrl: "/Pages/WebHookConfigurations/createModal.js",
        modalClass: "webHookConfigurationCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "WebHookConfigurations/EditModal",
        scriptUrl: "/Pages/WebHookConfigurations/editModal.js",
        modalClass: "webHookConfigurationEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            apiSignatureVerificationKey: $("#ApiSignatureVerificationKeyFilter").val(),
			clientWebhookUrl: $("#ClientWebhookUrlFilter").val(),
            listenProcessed: (function () {
                var value = $("#ListenProcessedFilter").val();
                if (value === undefined || value === null || value === '') {
                    return '';
                }
                return value === 'true';
            })(),
            listenDeferred: (function () {
                var value = $("#ListenDeferredFilter").val();
                if (value === undefined || value === null || value === '') {
                    return '';
                }
                return value === 'true';
            })(),
            listenDelivered: (function () {
                var value = $("#ListenDeliveredFilter").val();
                if (value === undefined || value === null || value === '') {
                    return '';
                }
                return value === 'true';
            })(),
            listenOpen: (function () {
                var value = $("#ListenOpenFilter").val();
                if (value === undefined || value === null || value === '') {
                    return '';
                }
                return value === 'true';
            })(),
            listenClick: (function () {
                var value = $("#ListenClickFilter").val();
                if (value === undefined || value === null || value === '') {
                    return '';
                }
                return value === 'true';
            })(),
            listenBounce: (function () {
                var value = $("#ListenBounceFilter").val();
                if (value === undefined || value === null || value === '') {
                    return '';
                }
                return value === 'true';
            })(),
            listenDropped: (function () {
                var value = $("#ListenDroppedFilter").val();
                if (value === undefined || value === null || value === '') {
                    return '';
                }
                return value === 'true';
            })(),
            listenSpamReport: (function () {
                var value = $("#ListenSpamReportFilter").val();
                if (value === undefined || value === null || value === '') {
                    return '';
                }
                return value === 'true';
            })(),
            listenUnsubscribe: (function () {
                var value = $("#ListenUnsubscribeFilter").val();
                if (value === undefined || value === null || value === '') {
                    return '';
                }
                return value === 'true';
            })(),
            listenGroupUnsubscribe: (function () {
                var value = $("#ListenGroupUnsubscribeFilter").val();
                if (value === undefined || value === null || value === '') {
                    return '';
                }
                return value === 'true';
            })(),
            listenGroupResubscribe: (function () {
                var value = $("#ListenGroupResubscribeFilter").val();
                if (value === undefined || value === null || value === '') {
                    return '';
                }
                return value === 'true';
            })(),
            isDefault: (function () {
                var value = $("#IsDefaultFilter").val();
                if (value === undefined || value === null || value === '') {
                    return '';
                }
                return value === 'true';
            })()
        };
    };

    var dataTable = $("#WebHookConfigurationsTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: true,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(webHookConfigurationService.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('XICommServer.WebHookConfigurations.Edit'),
                                action: function (data) {
                                    editModal.open({
                                     id: data.record.id
                                     });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('XICommServer.WebHookConfigurations.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    webHookConfigurationService.delete(data.record.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
			{ data: "apiSignatureVerificationKey" },
			{ data: "clientWebhookUrl" },
            {
                data: "listenProcessed",
                render: function (listenProcessed) {
                    return listenProcessed ? '<i class="fa fa-check"></i>' : '<i class="fa fa-times"></i>';
                }
            },
            {
                data: "listenDeferred",
                render: function (listenDeferred) {
                    return listenDeferred ? '<i class="fa fa-check"></i>' : '<i class="fa fa-times"></i>';
                }
            },
            {
                data: "listenDelivered",
                render: function (listenDelivered) {
                    return listenDelivered ? '<i class="fa fa-check"></i>' : '<i class="fa fa-times"></i>';
                }
            },
            {
                data: "listenOpen",
                render: function (listenOpen) {
                    return listenOpen ? '<i class="fa fa-check"></i>' : '<i class="fa fa-times"></i>';
                }
            },
            {
                data: "listenClick",
                render: function (listenClick) {
                    return listenClick ? '<i class="fa fa-check"></i>' : '<i class="fa fa-times"></i>';
                }
            },
            {
                data: "listenBounce",
                render: function (listenBounce) {
                    return listenBounce ? '<i class="fa fa-check"></i>' : '<i class="fa fa-times"></i>';
                }
            },
            {
                data: "listenDropped",
                render: function (listenDropped) {
                    return listenDropped ? '<i class="fa fa-check"></i>' : '<i class="fa fa-times"></i>';
                }
            },
            {
                data: "listenSpamReport",
                render: function (listenSpamReport) {
                    return listenSpamReport ? '<i class="fa fa-check"></i>' : '<i class="fa fa-times"></i>';
                }
            },
            {
                data: "listenUnsubscribe",
                render: function (listenUnsubscribe) {
                    return listenUnsubscribe ? '<i class="fa fa-check"></i>' : '<i class="fa fa-times"></i>';
                }
            },
            {
                data: "listenGroupUnsubscribe",
                render: function (listenGroupUnsubscribe) {
                    return listenGroupUnsubscribe ? '<i class="fa fa-check"></i>' : '<i class="fa fa-times"></i>';
                }
            },
            {
                data: "listenGroupResubscribe",
                render: function (listenGroupResubscribe) {
                    return listenGroupResubscribe ? '<i class="fa fa-check"></i>' : '<i class="fa fa-times"></i>';
                }
            },
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

    $("#NewWebHookConfigurationButton").click(function (e) {
        e.preventDefault();
        createModal.open();
    });

	$("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    $("#ExportToExcelButton").click(function (e) {
        e.preventDefault();

        webHookConfigurationService.getDownloadToken().then(
            function(result){
                    var input = getFilter();
                    var url =  abp.appPath + 'api/app/web-hook-configurations/as-excel-file' + 
                        abp.utils.buildQueryString([
                            { name: 'downloadToken', value: result.token },
                            { name: 'filterText', value: input.filterText }, 
                            { name: 'apiSignatureVerificationKey', value: input.apiSignatureVerificationKey }, 
                            { name: 'clientWebhookUrl', value: input.clientWebhookUrl }, 
                            { name: 'listenProcessed', value: input.listenProcessed }, 
                            { name: 'listenDeferred', value: input.listenDeferred }, 
                            { name: 'listenDelivered', value: input.listenDelivered }, 
                            { name: 'listenOpen', value: input.listenOpen }, 
                            { name: 'listenClick', value: input.listenClick }, 
                            { name: 'listenBounce', value: input.listenBounce }, 
                            { name: 'listenDropped', value: input.listenDropped }, 
                            { name: 'listenSpamReport', value: input.listenSpamReport }, 
                            { name: 'listenUnsubscribe', value: input.listenUnsubscribe }, 
                            { name: 'listenGroupUnsubscribe', value: input.listenGroupUnsubscribe }, 
                            { name: 'listenGroupResubscribe', value: input.listenGroupResubscribe }, 
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
