function extractPageName(hrefString) {
    var arr = hrefString.split('.');
    arr = arr[arr.length - 2].split('/');
    return arr[arr.length - 1].toLowerCase();
}
function setActiveMenu(arr, crtPage) {
    for (var i = 0; i < arr.length; i++)
        if (extractPageName(arr[i].href) == crtPage) {
            arr[i].className = "current";
            arr[i].parentNode.className = "current";
        }
}
function setPage() {
    if (document.location.href)
        hrefString = document.location.href;
    else
        hrefString = document.location;

    if (document.getElementById("navbar") != null)
        setActiveMenu(document.getElementById("navbar").getElementsByTagName("a"), extractPageName(hrefString));
}
