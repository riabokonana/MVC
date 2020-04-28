// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// получаем кнопки и форму в html
let btn = document.getElementById("removeBtn");
let sel = document.getElementById("serverfiles");
let dwn = document.getElementById("downloadBtn");

// кнопка удалить
btn.addEventListener('click', () => {
    // при нажатии устанавливаем форме action RemoveFile
    sel.action = "/Home/RemoveFile";
    return true;
});

// кнопка скачать
dwn.addEventListener('click', () => {
    // при нажатии устанавливаем форме action GetVirtualFile
    sel.action = "/Home/GetVirtualFile";
    return true;
});