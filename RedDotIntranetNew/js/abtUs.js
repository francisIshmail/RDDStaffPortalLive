
function setAbout(value) {

    var urls = window.location.toString()


    if (urls.lastIndexOf("about-us") < 0) {
        var newLoc = urls.substring(0, (urls.lastIndexOf("/") + 1)) + "about-us.aspx";
        window.location = newLoc;
    }
   
}