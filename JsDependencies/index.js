import "animate.css"
import "@fortawesome/fontawesome-free/css/all.css"
import panzoom from "panzoom";

window.getWindowWidth = function () {
    return window.innerWidth;
}

window.getWindowHeight = function () {
    return window.innerHeight;
}

window.enableCanvasZoom = function () {
    let canvas = document.querySelector("canvas");
    
    panzoom(canvas, {
        zoomSpeed: 0.065,
        maxZoom: 2,
        minZoom: 0.5,
    });
}