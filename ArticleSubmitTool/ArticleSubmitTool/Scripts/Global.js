function globalInit() {


    $("label.input-field").css('cssText', 'font-size: 12px');

    $("input.input-field").focus(function () {
        var id = $(this).attr('id');
        $("label.input-field[for='" + id + "']").css('cssText', 'color: rgba(0,151,167, 1) !important; font-size: 14px');
    });


    $("input.input-field").blur(function () {
        var id = $(this).attr('id');
        $("label.input-field[for='" + id + "']").css('cssText', 'color: rgba(69,90,100, 0.8) !important; font-size: 12px');
    });

    $("textarea.input-field").focus(function () {
        var id = $(this).attr('id');
        $("label.input-field[for='" + id + "']").css('cssText', 'color: rgba(0,151,167, 1) !important; font-size: 14px');
    });


    $("textarea.input-field").blur(function () {
        var id = $(this).attr('id');
        $("label.input-field[for='" + id + "']").css('cssText', 'color: rgba(69,90,100, 0.8) !important; font-size: 12px');
    });

    $("select.input-field").focus(function () {
        console.log('clicked');
        var id = $(this).attr('id');
        $("label.input-field[for='" + id + "']").css('cssText', 'color: rgba(0,151,167, 1) !important; font-size: 14px');
    });


    $("input.select-dropdown").blur(function () {
        var id = $(this).attr('id');
        $("label.input-field[for='" + id + "']").css('cssText', 'color: rgba(69,90,100, 0.8) !important; font-size: 12px');
    });

    $('select').material_select();

    $('.dropdown-button').dropdown({
        inDuration: 200,
        outDuration: 200,
        constrain_width: false, // Does not change width of dropdown to that of the activator
        hover: true, // Activate on hover
        gutter: 0, // Spacing from edge
        belowOrigin: true, // Displays dropdown below the button
        alignment: 'left' // Displays dropdown with edge aligned to the left of button
    });

    $('.materialboxed').materialbox();


}
