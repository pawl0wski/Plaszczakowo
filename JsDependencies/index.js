import "animate.css"
import "@fortawesome/fontawesome-free/css/all.css"
import panzoom from "panzoom";
import "@fontsource/caveat-brush";
import "@fontsource/dekko"
import "@fontsource/dseg14-modern"

window.enableCanvasZoom = function () {
    let canvas = document.querySelectorAll("canvas");

    canvas.forEach(c => {
        panzoom(c, {
            zoomSpeed: 0.065,
            maxZoom: 2,
            minZoom: 0.5,
        });
    })
}

window.scrollTextList = function (offset) {
    let element = document.querySelector("div.text-list");
    console.log(element);
    element.scrollLeft = (offset - Math.floor(window.innerWidth / 2 / 100)) * 100;
}