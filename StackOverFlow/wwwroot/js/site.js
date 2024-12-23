// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function myFunction(){
    var x = document.getElementById("myDIV");
    if (x.style.display === "block") {
        x.style.display = "none"
    }
    else {
        x.style.display="block"
    }
}
function cancelFuntion() {
    var x=document.getElementById("myDIV");
    x.style.display="none"
}