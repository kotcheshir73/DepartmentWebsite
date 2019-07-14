// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(function () {
    $.ajax({
        type: 'POST',
        url: '/Home/MainMenu',
        success: function (result) {
            if (result) {
                $('#menu').html(result);
            }
        }
    });

    $('body').on('click', '.sub-menu-item', function () {
        var controller = $(this).data('controller');
        var action = $(this).data('action');

        if (controller && action) {
            window.location.href = '/' + controller + '/' + action + '?Id=' + $(this).data('id');
        }
    });

    $('body').on('click', '.lecturer-element', function () {
        window.location.href = '/Lecturer/Lecturer?Id=' + $(this).data('id');
    });

    $('body').on('click', '.education-title', function () {
        window.location.href = '/EducationDirection/EducationDirection?Id=' + $(this).data('id');
    });

    $('.education-panel-collapse').on('show.bs.collapse', function () {
        $(this).siblings('.education-panel-heading').addClass('active');
    });

    $('.education-panel-collapse').on('hide.bs.collapse', function () {
        $(this).siblings('.education-panel-heading').removeClass('active');
    });
});