$(function () {
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

    $('.discipline-panel-collapse').on('show.bs.collapse', function () {
        $(this).siblings('.discipline-panel-heading').addClass('active');
    });

    $('.discipline-panel-collapse').on('hide.bs.collapse', function () {
        $(this).siblings('.discipline-panel-heading').removeClass('active');
    });

    $('body').on('click', '.course-item', function () {
        var seleted = $('.selected');
        seleted.removeClass('selected');
        $(this).addClass('selected');

        $.ajax({
            type: 'GET',
            url: '/EducationDirection/EducationDirectionCourse?Id=' + $(this).data('id'),
            success: function (result) {
                if (result) {
                    $('#course').html(result);
                }
            },
            error: function (result) {
                ShowError(result);
            }
        });
    });

    $('.datepicker').datepicker({
        changeMonth: true,
        changeYear: true,
        minDate: "-99Y",
        dateFormat: "dd.MMMM.yyyy"
    });
});

ShowError = function (result) {
    $('<div></div>').html(result.responseText).dialog({
        title: 'Ошибка',
        resizable: false,
        width: '700px',
        modal: true
    });
};