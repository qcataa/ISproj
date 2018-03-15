// Write your JavaScript code.
function showLogin(string) {
    HideAllBoxes();
    var elems = $("div[id$=" + string + "]");
    for (var it = 0; it < elems.length; it++) {
        elems[it].classList.add("visible");
    }
}

function HideAllBoxes() {
    var elems = document.getElementsByClassName("visible");
    for (var it = 0; it < elems.length; it++) {
        if (!elems[it].id.includes("Label"))
            elems[it].classList.remove("visible");;
    }
}

