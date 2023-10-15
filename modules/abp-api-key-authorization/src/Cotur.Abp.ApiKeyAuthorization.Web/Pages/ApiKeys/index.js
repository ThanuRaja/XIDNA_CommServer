(function ($) {
    var l = abp.localization.getResource('ApiKeyAuthorization');

    var _apiKeyAppService = cotur.abp.apiKeyAuthorization.apiKeys.apiKeys;
    
    var _editModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'ApiKeys/UpdateModal',
        scriptUrl: "/Pages/ApiKeys/updateModal.js",
        modalClass: "apiKeyUpdateModal"
    });
    
    var _createModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'ApiKeys/CreateModal',
        scriptUrl: "/Pages/ApiKeys/createModal.js",
        modalClass: "apiKeyCreateModal"
    });
    
    var _permissionsModal = new abp.ModalManager(
        abp.appPath + 'AbpPermissionManagement/PermissionManagementModal'
    );

    // var _dataTable = null;


    $("#CreateApiKey").click(function (e) {
        e.preventDefault();
        _createModal.open();
    });


    abp.ui.extensions.entityActions.get('apiKeyAuthorization.apiKeys').addContributor(
        function(actionList) {
            return actionList.addManyTail(
                [
                    {
                        text: l('Edit'),
                        visible: abp.auth.isGranted(
                            'ApiKeyAuthorization.ApiKeys.Update'
                        ),
                        action: function (data) {
                            _editModal.open({
                                id: data.record.id,
                            });
                        },
                    },
                    {
                        text: l('Permissions'),
                        visible: abp.auth.isGranted(
                            'ApiKeyAuthorization.ApiKeys.ManagePermissions'
                        ),
                        action: function (data) {
                            _permissionsModal.open({
                                providerName: 'ApiKey',
                                providerKey: data.record.id,
                                providerKeyDisplayName: data.record.name
                            });
                        },
                    },
                    {
                        text: l('Delete'),
                        visible: abp.auth.isGranted(
                            'ApiKeyAuthorization.ApiKeys.Delete'
                        ),
                        confirmMessage: function (data) {
                            return l(
                                'ApiKeyDeletionConfirmationMessage',
                                data.record.name
                            );
                        },
                        action: function (data) {
                            _apiKeyAppService
                                .delete(data.record.id)
                                .then(function () {
                                    _dataTable.ajax.reload();
                                    abp.notify.success(l('SuccessfullyDeleted'));
                                });
                        },
                    }
                ]
            );
        }
    );

    abp.ui.extensions.tableColumns.get('apiKeyAuthorization.apiKeys').addContributor(
        function (columnList) {
            columnList.addManyTail(
                [
                    {
                        title: l("Actions"),
                        rowAction: {
                            items: abp.ui.extensions.entityActions.get('apiKeyAuthorization.apiKeys').actions.toArray()
                        }
                    },
                    {
                        title: l('Name'),
                        data: 'name'
                    },
                    {
                        title: l('Key'),
                        data: 'key',
                    },
                    {
                        title: l('Active'),
                        data: 'active',
                        render: function (data, type, row) {
                            row.active = $.fn.dataTable.render.text().display(row.active);
                            if (!row.active) {
                                return  'Disabled';
                            }else{
                                return  'Active';
                            }
                        }
                    },
                    {
                        title: l('ExpireAt'),
                        data: 'expireAt',
                        render: function (expireAt) {
                            if (!expireAt) {
                                return "";
                            }

                            var date = Date.parse(expireAt);
                            return (new Date(date)).toLocaleDateString(abp.localization.currentCulture.name);
                        }
                    },
                    {
                        title: l('XSenseCPP'),
                        data: 'xSense_Cpp',
                        render: function (data, type, row)
                        {
                            row.xsense_Cpp = $.fn.dataTable.render.text().display(row.xsense_Cpp);
                            if (!row.xSense_Cpp)
                            {
                                return 'Not Applicable';
                            }
                            else {
                                return '<button class="btn btn-info btn-sm downloadApiConfigFileClass" value="' + row.id + '">Download</button>';
                            }
                        }
                    }
                    

                ]
            );
        },
        0 //adds as the first contributor
    );

   
        var _$wrapper = $('#ApiKeysWrapper');
        var _$table = _$wrapper.find('table');
        var _dataTable = _$table.DataTable(
            abp.libs.datatables.normalizeConfiguration({
                order: [[1, 'asc']],
                processing: true,
                serverSide: true,
                scrollX: true,
                paging: true,
                ajax: abp.libs.datatables.createAjax(
                    _apiKeyAppService.getList, getFilter
                ),
                columnDefs: abp.ui.extensions.tableColumns.get('apiKeyAuthorization.apiKeys').columns.toArray()
            })
        );

        _createModal.onResult(function () {
            _dataTable.ajax.reload();
        });

        _editModal.onResult(function () {
            _dataTable.ajax.reload();
        });

        _$wrapper.find('button[name=CreateApiKey]').click(function (e) {
            e.preventDefault();
            _createModal.open();
        });
  



    //For dowloading the api config file... 
    $(document).on('click', '.downloadApiConfigFileClass', function (e) {
        e.preventDefault();
        abp.ajax({
            type: 'GET',
            url: '/api/api-keys/generate-apiconfig-file?id=' + e.target.value,
        }).done(function (resp) {
            console.log(resp);
            if (resp == "false") {
                abp.notify.error("Failed to download config file.");
            }
            else {
                //Convert Base64 string to Byte Array.
                var bytes = Base64ToBytes(resp.fileContent);
                console.log("Bytes");
                console.log(bytes);
                //Convert Byte Array to BLOB.
                var blob = new Blob([bytes], { type: resp.fileContentType });
                console.log("Blob");
                console.log(blob);
                //Check the Browser type and download the File.
                var isIE = false || !!document.documentMode;
                if (isIE) {
                    window.navigator.msSaveBlob(blob, resp.fileName);
                } else {
                    var url = window.URL || window.webkitURL;
                    link = url.createObjectURL(blob);
                    var a = $("<a />");
                    a.attr("download", resp.fileName);
                    a.attr("href", link);
                    $("body").append(a);
                    a[0].click();
                    $("body").remove(a);
                }
                _dataTable.ajax.reload(null, false);
                abp.notify.success(l('SuccessfullyDownloaded'));
                removeConfigFile();
            }
        });

    });
  
    function Base64ToBytes(base64) {
        var binary_string = window.atob(base64);
        var len = binary_string.length;
        var bytes = new Uint8Array(len);
        for (var i = 0; i < len; i++) {
            bytes[i] = binary_string.charCodeAt(i);
        }
        return bytes.buffer;
    }

    //For removing the config file after download...
    function removeConfigFile() {
        abp.ajax({
            type: 'GET',
            url: '/api/api-keys/removing-apiconfig-file',
        }).done(function (resp) {
            console.log(resp);
        });
    }

    $("#SearchForm").submit(function (e) {
        console.log("asdasdasd");
        e.preventDefault();
        _dataTable.ajax.reload();
    });

    var getFilter = function () {
        return {
            filterText: $("#FilterText").val(),
        };
    };


})(jQuery);
