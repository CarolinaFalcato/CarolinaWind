document.querySelectorAll('.form-outline').forEach((formOutline) => {
    new mdb.Input(formOutline).init();
});

document.querySelectorAll('.datepicker').forEach((datepicker) => {
    new mdb.Datepicker(datepicker, {
        min: new Date(1900, 0, 1), // Month is 0-based
        max: new Date(),
        startDay: 1,
        format: 'dd/mm/yyyy'
    });
});

function onBeginSubmit() {
    destroyTooltips();
    document.getElementById('projectsTable').style.opacity = 0;
}

function onCompleteSubmit() {
    initializeTooltips();
    initializePopConfirm();
    document.getElementById('projectsTable').style.opacity = 1;
}

function initializeTooltips() {
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-mdb-toggle="tooltip"]'));
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new mdb.Tooltip(tooltipTriggerEl);
    });
}

function destroyTooltips() {
    $('[data-mdb-toggle="tooltip"]').tooltip('dispose');
}

function initializePopConfirm() {
    popconfirmRemoveProjects = document.querySelectorAll('.popconfirm-toggle');
    popconfirmRemoveProjects.forEach((currentElement) => {
        new mdb.Popconfirm(currentElement);
    });

    popconfirmRemoveProjects.forEach(function (popconfirmRemove) {
        popconfirmRemove.addEventListener('confirm.mdb.popconfirm', function () {
            removeProject(popconfirmRemove.id);
        })
    });
}

function removeFilter() {
    $('#projectNameFilter').val('');
    $('#projectNumberFilter').val('');
    $('#dateFromFilter').val('');
    $('#dateToFilter').val('');
    getTableResults(null, null);
}