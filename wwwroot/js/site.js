// collect the selected depot and pass to the back end. Load a partial view to the current page and append to the div
function showVehicleByDepot() {
    var depotList = document.getElementById("depotList");
    depotList = depotList.options[depotList.selectedIndex].value;

    $.get("/Home/EditVehicles", { 'Id': depotList }, function (html) {
        var $div = $('<div class="text-center"></div>');
        $div.html(html);
        $("#vehicleList").remove();
        $("#loadVehiclesByDepot").append($div);
    });
}

// collect the vehicle registration from the back end and load partial view to the tax update modal view
function loadTaxUpdaterModal(vehicleReg) {
    var selectedVehicle = vehicleReg;
    $.get("/Home/UpdateVehicleTax", { 'data': selectedVehicle }, function (html) {
        var $div = $('<div class="text-center"></div>');
        $div.html(html);
        $("#taxUpdater").append($div);
    })
}

// collect the vehicle registration from the back end and load partial view to the MOT update modal view
function loadMOTUpdaterModal(vehicleReg) {
    var selectedVehicle = vehicleReg;
    $.get("/Home/UpdateVehicleMOT", { 'data': selectedVehicle }, function (html) {
        var $div = $('<div class="text-center"></div>');
        $div.html(html);
        $("#motUpdater").append($div);
    })
}

// collect the vehicle registration from the back end and load partial view to the service update modal view
function loadServiceUpdaterModal(vehicleReg) {
    var selectedVehicle = vehicleReg;
    $.get("/Home/UpdateVehicleService", { 'data': selectedVehicle }, function (html) {
        var $div = $('<div class="text-center"></div>');
        $div.html(html);
        $("#serviceUpdater").append($div);
    })
}

// collect the vehicle registration from the back end and load partial view to the service update modal view
function showVehiclesByDepotToDiscontinue() {
    var selectedDepot = document.getElementById("depotListDiscontinue");
    selectedDepot = selectedDepot.options[selectedDepot.selectedIndex].value;

    $.post("/Home/VehiclesToDiscontinue", { 'Id': selectedDepot }, function (html) {
        var $div = $('<div class="text-center"></div>');
        $div.html(html);
        $("#depotListView").remove();
        $("#loadVehiclesToDiscontinue").append($div);
    });
}

// collect the vehicle registration from the back end and load partial view to the service update modal view
function loadDeductionConfirmation(vehicleReg) {
    var selectedVehicle = vehicleReg;
    $.get("/Home/LoadDiscontinueVehicleModal", { 'data': selectedVehicle }, function (html) {
        var $div = $('<div class="text-center"></div>');
        $div.html(html);

        $("#discontinue").append($div);
    })
}

// collect the vehicle registration from the back end and load partial view to the reinstate vehicle modal view
function reinstateUpdaterModal(vehicleReg) {
    var selectedVehicle = vehicleReg;
    $.get("/Home/UpdateReinstateVehicle", { 'data': selectedVehicle }, function (html) {
        var $div = $('<div class="text-center"></div>');
        $div.html(html);
        $("#reinstateUpdater").append($div);
    })
}

// collect form input data and send to the back end
function postDiscontinueVehicle() {
        var vehicleReg = $("#vReg").html();
        var selectedReason = $("#reasonList").val();
        var comments = $("#discontinueComments").val();
        var discontinueDate = $("#discontinueDate").html();
        var userName = $("#discontinueUser").html();
        var data = { Reg: vehicleReg, Comments: comments, Date: discontinueDate, Reason: selectedReason, User: userName };
        var dataToSend = JSON.stringify(data);
        
    if (selectedReason == null || selectedReason == "" || selectedReason == 0) {
        alert("You must select a reason");
        return false
    }
    else {
        $.post("/Home/SaveDiscontinue", { 'data': dataToSend });
        window.location = "/Home/Vehicles";
    }
}

// collect form input data and send to the back end
function postReinstateVehicle() {
    var vehicleReg = $("#reinstateReg").html();
    var selectedReason = $("#reinstateReason").val();
    var comments = $("#reinstateComments").val();
    var discontinueDate = $("#reinstateDate").html();
    var userName = $("#reinstateUser").html();
    var data = { Reg: vehicleReg, Comments: comments, Date: discontinueDate, Reason: selectedReason, User: userName  };
    var dataToSend = JSON.stringify(data);

    alert(dataToSend);

    if (selectedReason == null || selectedReason == "" || selectedReason == 0) {
        alert("You must select a reason");
        return false
    }
    else {
        $.post("/Home/SaveReinstate", { 'data': dataToSend });
        window.location = "/Home/Vehicles";
    }
}

// collect the vehicle registration from the back end and load partial view to the update warranty modal view
function loadWarrantyUpdaterModal(vehicleReg) {
    var selectedVehicle = vehicleReg;
    $.get("/Home/UpdateVehicleWarranty", { 'data': selectedVehicle }, function (html) {
        var $div = $('<div class="text-center"></div>');
        $div.html(html);
        $("#warrantyUpdater").append($div);
    })
}

/*
    HIRE VEHICLES JAVASCRIPT
 */
function load(id) {
    var buttonsHeader = document.getElementById("buttonsHeader");
    buttonsHeader.style.visibility = "hidden";
    $.get("/Home/LoadHireForm", { 'id': id }, function (html) {
        var $div = $('<div class="text-center"></div>');
        $div.html(html);
        $("#loadedForm").append($div);
    });

}

function showVehiclesByDepotToUpload() {
    var selectedDepot = document.getElementById("depotListUpload");
    selectedDepot = selectedDepot.options[selectedDepot.selectedIndex].value;

    $.post("/Home/UploadDocs", { 'Id': selectedDepot }, function (html) {
        var $div = $('<div class="text-center"></div>');
        $div.html(html);
        $("#uploadDepotList").remove();
        $("#loadVehiclesToUpload").append($div);
    });
}

function loadUploadConfirmationModal(vehicleReg) {
    var selectedVehicle = vehicleReg;
    $.get("/Home/UpdateDocuments", { 'data': selectedVehicle }, function (html) {
        var $div = $('<div class="text-center"></div>');
        $div.html(html);
        $("#upload").append($div);
    })
}

function loadHireDiscontinueModal(vehicleReg) {
    var selectedVehicle = vehicleReg;
    $.get("/Home/LoadDiscontinueHireVehicleModal", { 'data': selectedVehicle }, function (html) {
        var $div = $('<div class="text-center"></div>');
        $div.html(html);
        $("#discontinueHireModal").append($div);
    })
}

function postDiscontinueHireVehicle() {
    var vehicleReg = $("#hireReg").html();
    var selectedReason = $("#reasonHireList").val();
    var comments = $("#discontinueHireComments").val();
    var discontinueDate = $("#discontinueHireDate").html();
    var userName = $("#discontinueHireUser").html();
    var data = { Reg: vehicleReg, Comments: comments, Date: discontinueDate, Reason: selectedReason, User: userName };
    var dataToSend = JSON.stringify(data);

    if (selectedReason == null || selectedReason == "" || selectedReason == 0) {
        alert("You must select a reason");
        return false
    }
    else {
        $.post("/Home/SaveHireDiscontinue", { 'data': dataToSend });
        window.location = "/Home/Vehicles";
    }
}

/* GLOBAL */
function exportTableToExcel(tableID, filename = '') {
    var downloadLink;
    var dataType = 'application/vnd.ms-excel';
    var tableSelect = document.getElementById(tableID);
    var tableHTML = tableSelect.outerHTML.replace(/ /g, '%20');

    // Specify file name
    filename = filename ? filename + '.xls' : 'excel_data.xls';

    // Create download link element
    downloadLink = document.createElement("a");

    document.body.appendChild(downloadLink);

    if (navigator.msSaveOrOpenBlob) {
        var blob = new Blob(['\ufeff', tableHTML], {
            type: dataType
        });
        navigator.msSaveOrOpenBlob(blob, filename);
    } else {
        // Create a link to the file
        downloadLink.href = 'data:' + dataType + ', ' + tableHTML;

        // Setting the file name
        downloadLink.download = filename;

        //triggering the function
        downloadLink.click();
    }
}