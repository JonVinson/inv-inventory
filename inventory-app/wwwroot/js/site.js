function refreshData() {
    var lv = $("#listView").data("kendoListView");
    lv.dataSource.read();
}

function confirmDelete(event, itemType) {
    console.log("Intercepting event...");
    event.preventDefault();
    let message = `Do you want to delete this ${itemType}?`;
    kendo.confirm(message)
        .done(() => {
            var dataSource = $("#listView").data("kendoListView").dataSource;
            dataSource.remove(event.model);
            dataSource.sync();
        });
}
