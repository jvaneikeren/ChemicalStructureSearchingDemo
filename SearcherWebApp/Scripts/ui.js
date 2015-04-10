$(document).ready(function () {

    function updateStructureImage(div, molfileContents) {

        // Show a loading spinner.
        div.html("<img src='/Images/ajax_loader_thin_small.gif' style='margin-top: 50px'>");

        // Get the structure image.
        $.ajax({
            url: "/Home/GetStructureImageBase64",
            type: "POST",
            data: {
                molfileContents: molfileContents,
                width: div.width(),
                height: div.height(),
            },
            traditional: true,
            success: function (response) {
                div.html("<img src='data:image/png;base64," + response + "' />");
            },
            error: function (xhr) {
                div.text("ERROR");
            },
        });

    }

    function updateQuery(molFileContents) {

        $("#queryMolfileContents").val(molFileContents);
        $("#searchButton").removeClass("disabled");
        updateStructureImage($("#queryImage"), molFileContents);

    }

    function showSearchStart() {

        // Disable query panel, hide the results panel, and show the ajax spinner.
        $("#queryPanel").addClass("disabled");
        $("#resultsPanel").hide();
        $("#searchSpinner").show();

    }

    function showSearchComplete(resultsHTML) {

        // Clear out the existing results table.
        $("#resultsTable").remove();

        // Load the results HTML.
        var resultsPanel = $("#resultsPanel");
        resultsPanel.html(resultsHTML);

        // Setup the fixed header table.
        resultsPanel.find("#resultsTable").each(function () {

            var table = $(this);

            // Wrap the cell header cells in divs.
            table.find("th").each(function () {
                var headerCell = $(this);
                var html = headerCell.html();
                headerCell.html('<div class="th-inner">' + html + "</div>");
            });

            // Clone and append the first row.
            var headerRow = table.find("tr:first");
            var clonedHeaderRow = headerRow.clone().addClass("hidden-header");
            clonedHeaderRow.insertBefore(headerRow);

            // Remove border from first header.
            table.find("tr:nth-of-type(2)").find(".th-inner:first").css("border-left", "0px");

            // Wrap the table in the two divs.
            table.wrap('<div class="fixed-table-container-inner"></div>');
            table.parent().wrap('<div class="fixed-table-container"></div>');
        });

        // Enable query panel, show the results panel, and hide the ajax spinner.
        resultsPanel.show();
        $("#queryPanel").removeClass("disabled");
        $("#searchSpinner").hide();

        // Obtain the structure images for the search results.
        resultsPanel.find(".result").each(function () {
            updateStructureImage($(this).find(".resultImage"), $(this).find(".resultMolfile").val());
        });

        // Resize.
        resizeFixedHeaderTable();
    }

    $("#drawButton").click(function () {

        BootstrapDialog.show({
            title: "Draw Chemical Structure",
            message: $("<iframe id='ketcherFrame' src='/Ketcher/Ketcher.html' width='900' height='510' frameborder='0' />"),
            cssClass: "drawDialog",
            buttons: [
                {
                    label: "Import",
                    cssClass: "btn-primary",
                    action: function (dialog) {
                        updateQuery(document.getElementById("ketcherFrame").contentWindow.ketcher.getMolfile());
                        dialog.close();
                    },
                },
                {
                    label: "Close",
                    action: function (dialog) {
                        dialog.close();
                    },
                }
            ],
            onshow: function (dialog) {
                setTimeout(function () {
                    var molfileContents = $("#queryMolfileContents").val();
                    if (molfileContents) {
                        document.getElementById("ketcherFrame").contentWindow.ketcher.setMolecule(molfileContents);
                    }
                }, 1000);
            },
        });

    });

    $("#browseButton").click(function () {

        BootstrapDialog.show({
            title: "Browse for MolFile (*.mol)",
            message: $("<div><input type='file' id='fileInput' /></div>"),
            buttons: [
                {
                    label: "Import",
                    cssClass: "btn-primary",
                    action: function (dialog) {
                        var files = document.getElementById("fileInput").files;
                        if (files.length > 0) {
                            var reader = new FileReader();
                            reader.onloadend = function (evt) {
                                if (evt.target.readyState == FileReader.DONE) {
                                    updateQuery(evt.target.result);
                                    dialog.close();
                                }
                            };
                            var file = files[0];
                            var blob = file.slice(0, file.size);
                            reader.readAsBinaryString(blob);
                        }
                        else {
                            alert("Please select a file");
                        }
                    },
                },
                {
                    label: "Close",
                    action: function (dialog) {
                        dialog.close();
                    },
                }
            ],
        });

    });

    $("#pasteButton").click(function () {
        
        BootstrapDialog.show({
            title: "Paste MolFile Contents",
            message: $("<div><textarea id='molFileText' rows='4' cols='50'></textarea></div>"),
            buttons: [
                {
                    label: "Import",
                    cssClass: "btn-primary",
                    action: function (dialog) {
                        updateQuery($("#molFileText").val());
                        dialog.close();
                    },
                },
                {
                    label: "Close",
                    action: function (dialog) {
                        dialog.close();
                    },
                }
            ],
        });

    });

    $("#sampleButton").click(function () {

        BootstrapDialog.show({
            title: "Import Sample Molfile",
            message: $("<div><input type='radio' name='sampleName' id='sampleAcidChloride' value='acid_chloride' checked /><label for='sampleAcidChloride'>Acid Chloride</label></div>" + 
                       "<div><input type='radio' name='sampleName' id='sampleAmpicillin' value='ampicillin' /><label for='sampleAmpicillin'>Ampicillin</label></div>" +
                       "<div><input type='radio' name='sampleName' id='sampleProzac' value='prozac' /><label for='sampleProzac'>Prozac</label></div>"),
            buttons: [
                {
                    label: "Import",
                    cssClass: "btn-primary",
                    action: function (dialog) {
                        $.get("/Home/GetSampleMolfile/" + $("input:radio[name='sampleName']:checked").val(), function (data) {
                            updateQuery(data);
                            dialog.close();
                        });                       
                    },
                },
                {
                    label: "Close",
                    action: function (dialog) {
                        dialog.close();
                    },
                }
            ],
        });

        

    });

    $("#searchButton").click(function () {

        // Show search start.
        showSearchStart();

        // Send off the search.
        $.ajax({
            url: "/Home/Search",
            type: "POST",
            data: {
                molfileContents: $("#queryMolfileContents").val(),
                searchType: $("input:radio[name='searchType']:checked").val(),
            },
            traditional: true,
            success: function (response) {
                showSearchComplete(response);
            },
            error: function (xhr) {
                showSearchComplete(xhr.responseText);
            },
        });
        
    });

    // Drag/drop source: http://www.html5rocks.com/en/tutorials/file/dndfiles/

    function handleFileSelect(evt) {
        evt.stopPropagation();
        evt.preventDefault();

        var files = evt.dataTransfer.files;
        if (files.length > 0) {
            var reader = new FileReader();
            reader.onloadend = function (evt) {
                if (evt.target.readyState == FileReader.DONE) {
                    updateQuery(evt.target.result);
                }
            };
            var file = files[0];
            var blob = file.slice(0, file.size);
            reader.readAsBinaryString(blob);
        }
        else {
            var data = evt.dataTransfer.getData("text");
            var result = $("#" + data);
            updateQuery(result.find(".resultMolfile").val());
        }
    }

    function handleDragOver(evt) {
        evt.stopPropagation();
        evt.preventDefault();
        evt.dataTransfer.dropEffect = "copy";
    }

    var dropZone = document.getElementById('queryImage');
    dropZone.addEventListener('dragover', handleDragOver, false);
    dropZone.addEventListener('drop', handleFileSelect, false);


    function resizeFixedHeaderTable() {

        $(".fixed-table-container").each(function () {

            var container = $(this);

            var leftOffset = container.offset().left;
            var topOffset = container.offset().top;

            if (topOffset > 0) {

                var mywindow = $(window);
                var innerContainer = container.find(".fixed-table-container-inner");
                var table = container.find("#resultsTable")
                var windowHeight = mywindow.innerHeight();
                var windowWidth = mywindow.innerWidth();
                var height = windowHeight - topOffset - 30;

                // Determine maximum cell header height.
                var headerRowHeight = 0;
                table.find(".th-inner").each(function () {
                    var cellHeight = $(this).outerHeight();
                    if (cellHeight > headerRowHeight)
                        headerRowHeight = cellHeight;
                });

                // Set height conditionally.
                //alert(table.height() + ", " + container.height());
                if (table.height() > container.height()) {
                    container.css("height", height + "px");
                    innerContainer.css("width", (table.width() + 17) + "px");
                    container.css("width", (table.width() + 19) + "px");
                }
                else {
                    container.css("height", (table.height() + 32) + "px");
                    innerContainer.css("width", "auto");
                    container.css("width", (table.width() + 2) + "px");
                }
            }
        });
    }

    // Setup window resizing.
    $(window).resize(function () {
        resizeFixedHeaderTable();
    });

});