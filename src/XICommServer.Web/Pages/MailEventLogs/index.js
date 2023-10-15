$(function () {
    var l = abp.localization.getResource("XICommServer");
	
	var mailEventLogService = window.xICommServer.mailEventLogs.mailEventLogs;
	
	
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + "MailEventLogs/CreateModal",
        scriptUrl: "/Pages/MailEventLogs/createModal.js",
        modalClass: "mailEventLogCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "MailEventLogs/EditModal",
        scriptUrl: "/Pages/MailEventLogs/editModal.js",
        modalClass: "mailEventLogEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            timestampMin: $("#TimestampFilterMin").data().datepicker.getFormattedDate('yyyy-mm-dd'),
			timestampMax: $("#TimestampFilterMax").data().datepicker.getFormattedDate('yyyy-mm-dd'),
			smtpId: $("#SmtpIdFilter").val(),
			eventType: $("#EventTypeFilter").val(),
			category: $("#CategoryFilter").val(),
			sendGridEventId: $("#SendGridEventIdFilter").val(),
			sendGridMessageId: $("#SendGridMessageIdFilter").val(),
			tLS: $("#TLSFilter").val(),
			marketingCampainId: $("#MarketingCampainIdFilter").val(),
			marketingCampainName: $("#MarketingCampainNameFilter").val(),
            isLogSynced: (function () {
                var value = $("#IsLogSyncedFilter").val();
                if (value === undefined || value === null || value === '') {
                    return '';
                }
                return value === 'true';
            })()
        };
    };

    var dataTable = $("#MailEventLogsTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: true,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(mailEventLogService.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('XICommServer.MailEventLogs.Edit'),
                                action: function (data) {
                                    editModal.open({
                                     id: data.record.id
                                     });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('XICommServer.MailEventLogs.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    mailEventLogService.delete(data.record.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
			{
                data: "timestamp",
                render: function (timestamp) {
                    if (!timestamp) {
                        return "";
                    }
                    
					var date = Date.parse(timestamp);
                    return (new Date(date)).toLocaleDateString(abp.localization.currentCulture.name);
                }
            },
			{ data: "smtpId" },
            {
                data: "eventType",
                render: function (eventType) {
                    if (eventType === undefined ||
                        eventType === null) {
                        return "";
                    }

                    var localizationKey = "Enum:EventType." + eventType;
                    var localized = l(localizationKey);

                    if (localized === localizationKey) {
                        abp.log.warn("No localization found for " + localizationKey);
                        return "";
                    }

                    return localized;
                }
            },
			{ data: "category" },
			{ data: "sendGridEventId" },
			{ data: "sendGridMessageId" },
			{ data: "tls" },
			{ data: "marketingCampainId" },
			{ data: "marketingCampainName" },
            {
                data: "isLogSynced",
                render: function (isLogSynced) {
                    return isLogSynced ? '<i class="fa fa-check"></i>' : '<i class="fa fa-times"></i>';
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

    $("#NewMailEventLogButton").click(function (e) {
        e.preventDefault();
        createModal.open();
    });

	$("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    $("#ExportToExcelButton").click(function (e) {
        e.preventDefault();

        mailEventLogService.getDownloadToken().then(
            function(result){
                    var input = getFilter();
                    var url =  abp.appPath + 'api/app/mail-event-logs/as-excel-file' + 
                        abp.utils.buildQueryString([
                            { name: 'downloadToken', value: result.token },
                            { name: 'filterText', value: input.filterText },
                            { name: 'timestampMin', value: input.timestampMin },
                            { name: 'timestampMax', value: input.timestampMax }, 
                            { name: 'smtpId', value: input.smtpId }, 
                            { name: 'eventType', value: input.eventType }, 
                            { name: 'category', value: input.category }, 
                            { name: 'sendGridEventId', value: input.sendGridEventId }, 
                            { name: 'sendGridMessageId', value: input.sendGridMessageId }, 
                            { name: 'tLS', value: input.tLS }, 
                            { name: 'marketingCampainId', value: input.marketingCampainId }, 
                            { name: 'marketingCampainName', value: input.marketingCampainName }, 
                            { name: 'isLogSynced', value: input.isLogSynced }
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
