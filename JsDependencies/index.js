import "animate.css"
import "@fortawesome/fontawesome-free/css/all.css"
import panzoom from "panzoom";

window.enableCanvasZoom = function () {
    let canvas = document.querySelector("canvas");
    
    panzoom(canvas, {
        zoomSpeed: 0.065,
        maxZoom: 2,
        minZoom: 0.5,
    });
}