window.collapseCard = async (collapseTarget) => {
    document.getElementById(collapseTarget).click();

    var modal = new bootstrap.Modal(document.getElementById("modal"), {});

    modal.show();
}