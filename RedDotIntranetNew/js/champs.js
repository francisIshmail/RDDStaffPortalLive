
function setChampYear(yr) {
    var urls = window.location.toString()

    if (urls.lastIndexOf("champions") < 0) {
        var newLoc = urls.substring(0, (urls.lastIndexOf("/") + 1)) + "champions.aspx";
        window.location = newLoc;
    }

    if (yr == "2010") {
        document.getElementById("tbl2010").style.display = "block";
        document.getElementById("tbl2009").style.display = "none";
        document.getElementById("tbl2008").style.display = "none";
        document.getElementById("tbl2007").style.display = "none";
        document.getElementById("tbl2006").style.display = "none";

    }
    else
        if (yr == "2009") {
            document.getElementById("tbl2010").style.display = "none";
            document.getElementById("tbl2009").style.display = "block";
            document.getElementById("tbl2008").style.display = "none";
            document.getElementById("tbl2007").style.display = "none";
            document.getElementById("tbl2006").style.display = "none";
        }
        else
            if (yr == "2008") {
                document.getElementById("tbl2010").style.display = "none";
                document.getElementById("tbl2009").style.display = "none";
                document.getElementById("tbl2008").style.display = "block";
                document.getElementById("tbl2007").style.display = "none";
                document.getElementById("tbl2006").style.display = "none";
            }
            else
                if (yr == "2007") {
                    document.getElementById("tbl2010").style.display = "none";
                    document.getElementById("tbl2009").style.display = "none";
                    document.getElementById("tbl2008").style.display = "none";
                    document.getElementById("tbl2007").style.display = "block";
                    document.getElementById("tbl2006").style.display = "none";
                }
                else
                    if (yr == "2006") {
                        document.getElementById("tbl2010").style.display = "none";
                        document.getElementById("tbl2009").style.display = "none";
                        document.getElementById("tbl2008").style.display = "none";
                        document.getElementById("tbl2007").style.display = "none";
                        document.getElementById("tbl2006").style.display = "block";
                    }
}